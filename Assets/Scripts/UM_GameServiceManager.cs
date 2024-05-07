using System;
using System.Collections.Generic;
using UnityEngine;

public class UM_GameServiceManager : SA_Singleton<UM_GameServiceManager>
{
	private bool _IsInitedCalled;

	private bool _IsDataLoaded;

	private int _DataEventsCount;

	private int _CurrentEventsCount;

	private UM_Player _Player;

	private UM_ConnectionState _ConnectionSate;

	private static List<string> _FriendsList = new List<string>();

	private bool _WaitingForLeaderboardsData;

	private int _LeaderboardsDataEventsCount;

	private int _CurrentLeaderboardsEventsCount;

	public List<string> FriendsList
	{
		get
		{
			return _FriendsList;
		}
	}

	public UM_ConnectionState ConnectionSate
	{
		get
		{
			return _ConnectionSate;
		}
	}

	public bool IsConnected
	{
		get
		{
			return ConnectionSate == UM_ConnectionState.CONNECTED;
		}
	}

	[Obsolete("player is deprectaed, plase use Player instead")]
	public UM_Player player
	{
		get
		{
			return _Player;
		}
	}

	public UM_Player Player
	{
		get
		{
			return _Player;
		}
	}

	public static event Action OnPlayerConnected;

	public static event Action OnPlayerDisconnected;

	public static event Action<UM_ConnectionState> OnConnectionStateChnaged;

	public static event Action<UM_LeaderboardResult> ActionScoreSubmitted;

	public static event Action<UM_LeaderboardResult> ActionScoresListLoaded;

	public static event Action<UM_Result> ActionFriendsListLoaded;

	public static event Action<UM_Result> ActionAchievementsInfoLoaded;

	public static event Action<UM_Result> ActionLeaderboardsInfoLoaded;

	static UM_GameServiceManager()
	{
		UM_GameServiceManager.OnPlayerConnected = delegate
		{
		};
		UM_GameServiceManager.OnPlayerDisconnected = delegate
		{
		};
		UM_GameServiceManager.OnConnectionStateChnaged = delegate
		{
		};
		UM_GameServiceManager.ActionScoreSubmitted = delegate
		{
		};
		UM_GameServiceManager.ActionScoresListLoaded = delegate
		{
		};
		UM_GameServiceManager.ActionFriendsListLoaded = delegate
		{
		};
		UM_GameServiceManager.ActionAchievementsInfoLoaded = delegate
		{
		};
		UM_GameServiceManager.ActionLeaderboardsInfoLoaded = delegate
		{
		};
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Init()
	{
		_IsInitedCalled = true;
		_DataEventsCount = 0;
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.AutoLoadAchievementsInfo)
			{
				_DataEventsCount++;
				GameCenterManager.OnAchievementsLoaded += OnGameCenterServiceDataLoaded;
			}
			if (UltimateMobileSettings.Instance.AutoLoadLeaderboardsInfo)
			{
				_DataEventsCount += UltimateMobileSettings.Instance.Leaderboards.Count;
			}
			GameCenterManager.OnLeadrboardInfoLoaded += OnGameCenterServiceLeaderDataLoaded;
			foreach (UM_Achievement achievement in UltimateMobileSettings.Instance.Achievements)
			{
				GameCenterManager.RegisterAchievement(achievement.IOSId);
			}
			GameCenterManager.OnAuthFinished += OnAuthFinished;
			GameCenterManager.OnScoreSubmitted += IOS_HandleOnScoreSubmitted;
			GameCenterManager.OnScoresListLoaded += IOS_HandleOnScoresListLoaded;
			GameCenterManager.OnFriendsListLoaded += IOS_OnFriendsListLoaded;
			GameCenterManager.OnAchievementsLoaded += IOS_AchievementsDataLoaded;
			GameCenterManager.OnLeadrboardInfoLoaded += IOS_LeaderboardsDataLoaded;
			break;
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.AutoLoadAchievementsInfo)
			{
				_DataEventsCount++;
				GooglePlayManager.ActionAchievementsLoaded += OnGooglePlayServiceDataLoaded;
			}
			if (UltimateMobileSettings.Instance.AutoLoadLeaderboardsInfo)
			{
				_DataEventsCount++;
			}
			GooglePlayManager.ActionLeaderboardsLoaded += OnGooglePlayLeaderDataLoaded;
			GooglePlayConnection.ActionPlayerConnected += OnAndroidPlayerConnected;
			GooglePlayConnection.ActionPlayerDisconnected += OnAndroidPlayerDisconnected;
			GooglePlayManager.ActionScoreSubmited += Android_HandleActionScoreSubmited;
			GooglePlayManager.ActionScoresListLoaded += Android_HandleActionScoresListLoaded;
			GooglePlayManager.ActionFriendsListLoaded += Android_ActionFriendsListLoaded;
			GooglePlayManager.ActionAchievementsLoaded += Android_AchievementsDataLoaded;
			GooglePlayManager.ActionLeaderboardsLoaded += Android_LeaderboardsDataLoaded;
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void Connect()
	{
		if (!_IsInitedCalled)
		{
			Init();
		}
		if (_ConnectionSate != UM_ConnectionState.CONNECTED && _ConnectionSate != UM_ConnectionState.CONNECTING)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.IPhonePlayer:
				GameCenterManager.Init();
				break;
			case RuntimePlatform.Android:
				SA_Singleton<GooglePlayConnection>.Instance.Connect();
				break;
			}
			SetConnectionState(UM_ConnectionState.CONNECTING);
		}
	}

	public void Disconnect()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			break;
		case RuntimePlatform.Android:
			SA_Singleton<GooglePlayConnection>.Instance.Disconnect();
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void LoadFriends()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.RetrieveFriends();
			break;
		case RuntimePlatform.Android:
			SA_Singleton<GooglePlayManager>.Instance.LoadFriends();
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public bool IsParticipantFriend(UM_TBM_Participant playerParticipant)
	{
		return FriendsList.Contains(playerParticipant.Playerid);
	}

	public UM_Player GetPlayer(string playerId)
	{
		UM_Player result = null;
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		{
			GK_Player playerById2 = GameCenterManager.GetPlayerById(playerId);
			if (playerById2 != null)
			{
				result = new UM_Player(playerById2, null);
			}
			break;
		}
		case RuntimePlatform.Android:
		{
			GooglePlayerTemplate playerById = SA_Singleton<GooglePlayManager>.Instance.GetPlayerById(playerId);
			if (playerById != null)
			{
				result = new UM_Player(null, playerById);
			}
			break;
		}
		}
		return result;
	}

	public void LoadAchievementsInfo()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.LoadAchievements();
			break;
		case RuntimePlatform.Android:
			SA_Singleton<GooglePlayManager>.Instance.LoadAchievements();
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void ShowAchievementsUI()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ShowAchievements();
			break;
		case RuntimePlatform.Android:
			SA_Singleton<GooglePlayManager>.Instance.ShowAchievementsUI();
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void RevealAchievement(string id)
	{
		RevealAchievement(UltimateMobileSettings.Instance.GetAchievementById(id));
	}

	public void RevealAchievement(UM_Achievement achievement)
	{
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.Android)
		{
			SA_Singleton<GooglePlayManager>.Instance.RevealAchievementById(achievement.AndroidId);
		}
	}

	[Obsolete("ReportAchievement is deprecated, please use UnlockAchievement instead.")]
	public void ReportAchievement(string id)
	{
		UnlockAchievement(id);
	}

	[Obsolete("ReportAchievement is deprecated, please use UnlockAchievement instead.")]
	public void ReportAchievement(UM_Achievement achievement)
	{
		ReportAchievement(achievement);
	}

	public void UnlockAchievement(string id)
	{
		UM_Achievement achievementById = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievementById == null)
		{
			Debug.LogError("Achievment not found with id: " + id);
		}
		else
		{
			UnlockAchievement(achievementById);
		}
	}

	private void UnlockAchievement(UM_Achievement achievement)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.SubmitAchievement(100f, achievement.IOSId);
			break;
		case RuntimePlatform.Android:
			SA_Singleton<GooglePlayManager>.Instance.UnlockAchievementById(achievement.AndroidId);
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void IncrementAchievement(string id, float percentages)
	{
		UM_Achievement achievementById = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievementById == null)
		{
			Debug.LogError("Achievment not found with id: " + id);
		}
		else
		{
			IncrementAchievement(achievementById, percentages);
		}
	}

	public void IncrementAchievement(UM_Achievement achievement, float percentages)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.SubmitAchievement(percentages, achievement.IOSId);
			break;
		case RuntimePlatform.Android:
		{
			GPAchievement achievement2 = SA_Singleton<GooglePlayManager>.Instance.GetAchievement(achievement.AndroidId);
			if (achievement2 != null)
			{
				if (achievement2.Type == GPAchievementType.TYPE_INCREMENTAL)
				{
					int numsteps = Mathf.CeilToInt((float)achievement2.TotalSteps / 100f * percentages);
					SA_Singleton<GooglePlayManager>.Instance.IncrementAchievementById(achievement.AndroidId, numsteps);
				}
				else
				{
					SA_Singleton<GooglePlayManager>.Instance.UnlockAchievementById(achievement.AndroidId);
				}
			}
			break;
		}
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void IncrementAchievementByCurrentSteps(string id, int steps)
	{
		UM_Achievement achievementById = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievementById == null)
		{
			Debug.LogError("Achievement not found with id: " + id);
		}
		else
		{
			IncrementAchievementByCurrentSteps(achievementById, steps);
		}
	}

	public void IncrementAchievementByCurrentSteps(UM_Achievement achievement, int steps)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		{
			float percent = (float)steps / (float)achievement.Steps * 100f;
			GameCenterManager.SubmitAchievement(percent, achievement.IOSId);
			break;
		}
		case RuntimePlatform.Android:
		{
			GPAchievement achievement2 = SA_Singleton<GooglePlayManager>.Instance.GetAchievement(achievement.AndroidId);
			if (achievement2 != null)
			{
				if (achievement2.Type == GPAchievementType.TYPE_INCREMENTAL)
				{
					SA_Singleton<GooglePlayManager>.Instance.IncrementAchievementById(achievement2.Id, steps - achievement2.CurrentSteps);
				}
				else
				{
					SA_Singleton<GooglePlayManager>.Instance.UnlockAchievementById(achievement2.Id);
				}
			}
			break;
		}
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void ResetAchievements()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ResetAchievements();
			break;
		case RuntimePlatform.Android:
			SA_Singleton<GooglePlayManager>.Instance.ResetAllAchievements();
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public float GetAchievementProgress(string id)
	{
		UM_Achievement achievementById = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievementById == null)
		{
			Debug.LogError("Achievment not found with id: " + id);
			return 0f;
		}
		return GetAchievementProgress(achievementById);
	}

	public float GetAchievementProgress(UM_Achievement achievement)
	{
		if (achievement == null)
		{
			return 0f;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			return GameCenterManager.GetAchievementProgress(achievement.IOSId);
		case RuntimePlatform.Android:
		{
			GPAchievement achievement2 = SA_Singleton<GooglePlayManager>.Instance.GetAchievement(achievement.AndroidId);
			if (achievement2 != null)
			{
				if (achievement2.Type == GPAchievementType.TYPE_INCREMENTAL)
				{
					return (float)achievement2.CurrentSteps / (float)achievement2.TotalSteps * 100f;
				}
				if (achievement2.State == GPAchievementState.STATE_UNLOCKED)
				{
					return 100f;
				}
				return 0f;
			}
			break;
		}
		}
		return 0f;
	}

	public int GetAchievementProgressInSteps(string id)
	{
		UM_Achievement achievementById = UltimateMobileSettings.Instance.GetAchievementById(id);
		if (achievementById == null)
		{
			Debug.LogError("Achievement not found with id: " + id);
			return 0;
		}
		return GetAchievementProgressInSteps(achievementById);
	}

	public int GetAchievementProgressInSteps(UM_Achievement achievement)
	{
		if (achievement == null)
		{
			Debug.LogError("Achievement is null. No progress can be retrieved.");
			return 0;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		{
			float achievementProgress = GameCenterManager.GetAchievementProgress(achievement.IOSId);
			return Mathf.CeilToInt((float)achievement.Steps / 100f * achievementProgress);
		}
		case RuntimePlatform.Android:
		{
			GPAchievement achievement2 = SA_Singleton<GooglePlayManager>.Instance.GetAchievement(achievement.AndroidId);
			if (achievement2 != null)
			{
				if (achievement2.Type == GPAchievementType.TYPE_INCREMENTAL)
				{
					return achievement2.CurrentSteps;
				}
				return (achievement2.State == GPAchievementState.STATE_UNLOCKED) ? 1 : 0;
			}
			break;
		}
		}
		return 0;
	}

	public void LoadLeaderboardsInfo()
	{
		if (_WaitingForLeaderboardsData)
		{
			return;
		}
		_WaitingForLeaderboardsData = true;
		_LeaderboardsDataEventsCount = 0;
		_CurrentLeaderboardsEventsCount = 0;
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			_LeaderboardsDataEventsCount = UltimateMobileSettings.Instance.Leaderboards.Count;
			{
				foreach (UM_Leaderboard leaderboard in UltimateMobileSettings.Instance.Leaderboards)
				{
					GameCenterManager.LoadLeaderboardInfo(leaderboard.IOSId);
				}
				break;
			}
		case RuntimePlatform.Android:
			SA_Singleton<GooglePlayManager>.Instance.LoadLeaderBoards();
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public UM_Leaderboard GetLeaderboard(string leaderboardId)
	{
		return UltimateMobileSettings.Instance.GetLeaderboardById(leaderboardId);
	}

	public void ShowLeaderBoardsUI()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ShowLeaderboards();
			break;
		case RuntimePlatform.Android:
			SA_Singleton<GooglePlayManager>.Instance.ShowLeaderBoardsUI();
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void ShowLeaderBoardUI(string id)
	{
		ShowLeaderBoardUI(UltimateMobileSettings.Instance.GetLeaderboardById(id));
	}

	public void ShowLeaderBoardUI(UM_Leaderboard leaderboard)
	{
		if (leaderboard != null)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.IPhonePlayer:
				GameCenterManager.ShowLeaderboard(leaderboard.IOSId);
				break;
			case RuntimePlatform.Android:
				SA_Singleton<GooglePlayManager>.Instance.ShowLeaderBoardById(leaderboard.AndroidId);
				break;
			case RuntimePlatform.PS3:
			case RuntimePlatform.XBOX360:
				break;
			}
		}
	}

	public void SubmitScore(string LeaderboardId, long score)
	{
		SubmitScore(UltimateMobileSettings.Instance.GetLeaderboardById(LeaderboardId), score);
	}

	public void SubmitScore(UM_Leaderboard leaderboard, long score)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			GameCenterManager.ReportScore(score, leaderboard.IOSId);
			break;
		case RuntimePlatform.Android:
			SA_Singleton<GooglePlayManager>.Instance.SubmitScoreById(leaderboard.AndroidId, score);
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public UM_Score GetCurrentPlayerScore(string leaderBoardId, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		return GetCurrentPlayerScore(UltimateMobileSettings.Instance.GetLeaderboardById(leaderBoardId), timeSpan, collection);
	}

	public UM_Score GetCurrentPlayerScore(UM_Leaderboard leaderboard, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		if (leaderboard != null)
		{
			return leaderboard.GetCurrentPlayerScore(timeSpan, collection);
		}
		return null;
	}

	public void LoadPlayerCenteredScores(string leaderboardId, int maxResults, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		UM_Leaderboard leaderboardById = UltimateMobileSettings.Instance.GetLeaderboardById(leaderboardId);
		LoadPlayerCenteredScores(leaderboardById, maxResults, timeSpan, collection);
	}

	public void LoadPlayerCenteredScores(UM_Leaderboard leaderboard, int maxResults, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		if (leaderboard == null)
		{
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		{
			UM_Score currentPlayerScore = GetCurrentPlayerScore(leaderboard, timeSpan, collection);
			int num = 0;
			if (currentPlayerScore != null)
			{
				num = currentPlayerScore.Rank;
			}
			int startIndex = Math.Max(0, num - maxResults / 2);
			GameCenterManager.LoadScore(leaderboard.IOSId, startIndex, maxResults, timeSpan.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
			break;
		}
		case RuntimePlatform.Android:
			SA_Singleton<GooglePlayManager>.Instance.LoadPlayerCenteredScores(leaderboard.AndroidId, timeSpan.Get_GP_TimeSpan(), collection.Get_GP_CollectionType(), maxResults);
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void LoadTopScores(string leaderboardId, int maxResults, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		UM_Leaderboard leaderboardById = UltimateMobileSettings.Instance.GetLeaderboardById(leaderboardId);
		LoadTopScores(leaderboardById, maxResults, timeSpan, collection);
	}

	public void LoadTopScores(UM_Leaderboard leaderboard, int maxResults, UM_TimeSpan timeSpan = UM_TimeSpan.ALL_TIME, UM_CollectionType collection = UM_CollectionType.GLOBAL)
	{
		if (leaderboard != null)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.IPhonePlayer:
				GameCenterManager.LoadScore(leaderboard.IOSId, 1, maxResults, timeSpan.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
				break;
			case RuntimePlatform.Android:
				SA_Singleton<GooglePlayManager>.Instance.LoadTopScores(leaderboard.AndroidId, timeSpan.Get_GP_TimeSpan(), collection.Get_GP_CollectionType(), maxResults);
				break;
			case RuntimePlatform.PS3:
			case RuntimePlatform.XBOX360:
				break;
			}
		}
	}

	private void OnServiceConnected()
	{
		if (_IsDataLoaded || _DataEventsCount <= 0)
		{
			_IsDataLoaded = true;
			OnAllLoaded();
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.AutoLoadAchievementsInfo)
			{
				GameCenterManager.LoadAchievements();
			}
			if (!UltimateMobileSettings.Instance.AutoLoadLeaderboardsInfo)
			{
				break;
			}
			{
				foreach (UM_Leaderboard leaderboard in UltimateMobileSettings.Instance.Leaderboards)
				{
					GameCenterManager.LoadLeaderboardInfo(leaderboard.IOSId);
				}
				break;
			}
		case RuntimePlatform.Android:
			if (UltimateMobileSettings.Instance.AutoLoadAchievementsInfo)
			{
				SA_Singleton<GooglePlayManager>.Instance.LoadAchievements();
			}
			if (UltimateMobileSettings.Instance.AutoLoadLeaderboardsInfo)
			{
				SA_Singleton<GooglePlayManager>.Instance.LoadLeaderBoards();
			}
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	private void OnGooglePlayServiceDataLoaded(GooglePlayResult result)
	{
		_CurrentEventsCount++;
		if (_CurrentEventsCount == _DataEventsCount)
		{
			OnAllLoaded();
		}
	}

	private void OnGooglePlayLeaderDataLoaded(GooglePlayResult res)
	{
		foreach (GPLeaderBoard leaderBoard in SA_Singleton<GooglePlayManager>.Instance.LeaderBoards)
		{
			UM_Leaderboard leaderboardByAndroidId = UltimateMobileSettings.Instance.GetLeaderboardByAndroidId(leaderBoard.Id);
			if (leaderboardByAndroidId != null)
			{
				leaderboardByAndroidId.Setup(leaderBoard);
			}
		}
		OnGooglePlayServiceDataLoaded(res);
	}

	private void OnGameCenterServiceDataLoaded(ISN_Result e)
	{
		_CurrentEventsCount++;
		if (_CurrentEventsCount == _DataEventsCount)
		{
			OnAllLoaded();
		}
	}

	private void OnGameCenterServiceLeaderDataLoaded(GK_LeaderboardResult res)
	{
		if (res.IsSucceeded && res.Leaderboard != null)
		{
			UM_Leaderboard leaderboardByIOSId = UltimateMobileSettings.Instance.GetLeaderboardByIOSId(res.Leaderboard.Id);
			if (leaderboardByIOSId != null)
			{
				leaderboardByIOSId.Setup(res.Leaderboard);
			}
		}
		OnGameCenterServiceDataLoaded(res);
	}

	private void OnAllLoaded()
	{
		_IsDataLoaded = true;
		_Player = new UM_Player(GameCenterManager.Player, SA_Singleton<GooglePlayManager>.Instance.player);
		SetConnectionState(UM_ConnectionState.CONNECTED);
		UM_GameServiceManager.OnPlayerConnected();
	}

	private void OnAchievementsDataLoaded(UM_Result result)
	{
		UM_GameServiceManager.ActionAchievementsInfoLoaded(result);
	}

	private void OnLeaderboardsDataLoaded(UM_Result result)
	{
		_WaitingForLeaderboardsData = false;
		UM_GameServiceManager.ActionLeaderboardsInfoLoaded(result);
	}

	private void OnAuthFinished(ISN_Result res)
	{
		if (res.IsSucceeded)
		{
			OnServiceConnected();
			return;
		}
		SetConnectionState(UM_ConnectionState.DISCONNECTED);
		UM_GameServiceManager.OnPlayerDisconnected();
	}

	private void IOS_HandleOnScoreSubmitted(GK_LeaderboardResult res)
	{
		UM_Leaderboard leaderboardByIOSId = UltimateMobileSettings.Instance.GetLeaderboardByIOSId(res.Leaderboard.Id);
		if (leaderboardByIOSId != null)
		{
			leaderboardByIOSId.Setup(res.Leaderboard);
			UM_LeaderboardResult obj = new UM_LeaderboardResult(leaderboardByIOSId, res);
			UM_GameServiceManager.ActionScoreSubmitted(obj);
		}
	}

	private void IOS_HandleOnScoresListLoaded(GK_LeaderboardResult res)
	{
		UM_Leaderboard leaderboardByIOSId = UltimateMobileSettings.Instance.GetLeaderboardByIOSId(res.Leaderboard.Id);
		if (leaderboardByIOSId != null)
		{
			leaderboardByIOSId.Setup(res.Leaderboard);
			UM_LeaderboardResult obj = new UM_LeaderboardResult(leaderboardByIOSId, res);
			UM_GameServiceManager.ActionScoresListLoaded(obj);
		}
	}

	private void IOS_OnFriendsListLoaded(ISN_Result res)
	{
		SetFriendList(GameCenterManager.FriendsList);
		UM_GameServiceManager.ActionFriendsListLoaded(new UM_Result(res));
	}

	private void IOS_AchievementsDataLoaded(ISN_Result res)
	{
		UM_Result result = new UM_Result(res);
		OnAchievementsDataLoaded(result);
	}

	private void IOS_LeaderboardsDataLoaded(GK_LeaderboardResult res)
	{
		if (_WaitingForLeaderboardsData)
		{
			_CurrentLeaderboardsEventsCount++;
			if (_CurrentLeaderboardsEventsCount >= _LeaderboardsDataEventsCount)
			{
				UM_Result result = new UM_Result();
				OnLeaderboardsDataLoaded(result);
			}
		}
	}

	private void OnAndroidPlayerConnected()
	{
		OnServiceConnected();
	}

	private void OnAndroidPlayerDisconnected()
	{
		SetConnectionState(UM_ConnectionState.DISCONNECTED);
		UM_GameServiceManager.OnPlayerDisconnected();
	}

	private void Android_HandleActionScoresListLoaded(GP_LeaderboardResult res)
	{
		UM_Leaderboard leaderboardByAndroidId = UltimateMobileSettings.Instance.GetLeaderboardByAndroidId(res.Leaderboard.Id);
		if (leaderboardByAndroidId != null)
		{
			leaderboardByAndroidId.Setup(res.Leaderboard);
			UM_LeaderboardResult obj = new UM_LeaderboardResult(leaderboardByAndroidId, res);
			UM_GameServiceManager.ActionScoresListLoaded(obj);
		}
	}

	private void Android_HandleActionScoreSubmited(GP_LeaderboardResult res)
	{
		UM_Leaderboard leaderboardByAndroidId = UltimateMobileSettings.Instance.GetLeaderboardByAndroidId(res.Leaderboard.Id);
		if (leaderboardByAndroidId != null)
		{
			leaderboardByAndroidId.Setup(res.Leaderboard);
			UM_LeaderboardResult obj = new UM_LeaderboardResult(leaderboardByAndroidId, res);
			UM_GameServiceManager.ActionScoreSubmitted(obj);
		}
	}

	private void Android_ActionFriendsListLoaded(GooglePlayResult res)
	{
		SetFriendList(SA_Singleton<GooglePlayManager>.Instance.friendsList);
		UM_GameServiceManager.ActionFriendsListLoaded(new UM_Result(res));
	}

	private void Android_AchievementsDataLoaded(GooglePlayResult res)
	{
		UM_Result result = new UM_Result(res);
		OnAchievementsDataLoaded(result);
	}

	private void Android_LeaderboardsDataLoaded(GooglePlayResult res)
	{
		UM_Result result = new UM_Result(res);
		OnLeaderboardsDataLoaded(result);
	}

	private void SetConnectionState(UM_ConnectionState NewState)
	{
		if (_ConnectionSate != NewState)
		{
			_ConnectionSate = NewState;
			UM_GameServiceManager.OnConnectionStateChnaged(_ConnectionSate);
		}
	}

	private void SetFriendList(List<string> friendsIds)
	{
		_FriendsList.Clear();
		foreach (string friendsId in friendsIds)
		{
			if (!friendsId.Equals(Player.PlayerId))
			{
				_FriendsList.Add(friendsId);
			}
		}
	}
}
