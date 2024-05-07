using System;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayManager : SA_Singleton<GooglePlayManager>
{
	private GooglePlayerTemplate _player;

	private Dictionary<string, GooglePlayerTemplate> _players = new Dictionary<string, GooglePlayerTemplate>();

	private List<string> _friendsList = new List<string>();

	private List<string> _deviceGoogleAccountList = new List<string>();

	private List<GPGameRequest> _gameRequests = new List<GPGameRequest>();

	private string _loadedAuthToken = string.Empty;

	private string _currentAccount = string.Empty;

	private static bool _IsLeaderboardsDataLoaded = false;

	public string currentAccount
	{
		get
		{
			return _currentAccount;
		}
	}

	public GooglePlayerTemplate player
	{
		get
		{
			return _player;
		}
	}

	public Dictionary<string, GooglePlayerTemplate> players
	{
		get
		{
			return _players;
		}
	}

	[Obsolete("leaderBoards is deprectaed, please use LeaderBoards instead")]
	public List<GPLeaderBoard> leaderBoards
	{
		get
		{
			return LeaderBoards;
		}
	}

	public List<GPLeaderBoard> LeaderBoards
	{
		get
		{
			return AndroidNativeSettings.Instance.Leaderboards;
		}
	}

	[Obsolete("achievements is deprectaed, please use Achievements instead")]
	public List<GPAchievement> achievements
	{
		get
		{
			return Achievements;
		}
	}

	public List<GPAchievement> Achievements
	{
		get
		{
			return AndroidNativeSettings.Instance.Achievements;
		}
	}

	public List<string> friendsList
	{
		get
		{
			return _friendsList;
		}
	}

	public List<GPGameRequest> gameRequests
	{
		get
		{
			return _gameRequests;
		}
	}

	public List<string> deviceGoogleAccountList
	{
		get
		{
			return _deviceGoogleAccountList;
		}
	}

	public string loadedAuthToken
	{
		get
		{
			return _loadedAuthToken;
		}
	}

	public static bool IsLeaderboardsDataLoaded
	{
		get
		{
			return _IsLeaderboardsDataLoaded;
		}
	}

	public static event Action<GP_LeaderboardResult> ActionScoreSubmited;

	public static event Action<GP_LeaderboardResult> ActionScoresListLoaded;

	public static event Action<GooglePlayResult> ActionLeaderboardsLoaded;

	public static event Action<GP_AchievementResult> ActionAchievementUpdated;

	public static event Action<GooglePlayResult> ActionFriendsListLoaded;

	public static event Action<GooglePlayResult> ActionAchievementsLoaded;

	public static event Action<GooglePlayGiftRequestResult> ActionSendGiftResultReceived;

	public static event Action ActionRequestsInboxDialogDismissed;

	public static event Action<List<GPGameRequest>> ActionPendingGameRequestsDetected;

	public static event Action<List<GPGameRequest>> ActionGameRequestsAccepted;

	public static event Action<List<string>> ActionAvailableDeviceAccountsLoaded;

	public static event Action<string> ActionOAuthTokenLoaded;

	static GooglePlayManager()
	{
		GooglePlayManager.ActionScoreSubmited = delegate
		{
		};
		GooglePlayManager.ActionScoresListLoaded = delegate
		{
		};
		GooglePlayManager.ActionLeaderboardsLoaded = delegate
		{
		};
		GooglePlayManager.ActionAchievementUpdated = delegate
		{
		};
		GooglePlayManager.ActionFriendsListLoaded = delegate
		{
		};
		GooglePlayManager.ActionAchievementsLoaded = delegate
		{
		};
		GooglePlayManager.ActionSendGiftResultReceived = delegate
		{
		};
		GooglePlayManager.ActionRequestsInboxDialogDismissed = delegate
		{
		};
		GooglePlayManager.ActionPendingGameRequestsDetected = delegate
		{
		};
		GooglePlayManager.ActionGameRequestsAccepted = delegate
		{
		};
		GooglePlayManager.ActionAvailableDeviceAccountsLoaded = delegate
		{
		};
		GooglePlayManager.ActionOAuthTokenLoaded = delegate
		{
		};
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Create()
	{
		Debug.Log("GooglePlayManager was created");
		SA_Singleton<GooglePlayQuests>.instance.Init();
	}

	public GP_PlayServicesStatus GetPlayServicesStatus()
	{
		return AN_GMSGeneralProxy.GetPlayServicesStatus();
	}

	public void RetrieveDeviceGoogleAccounts()
	{
		AN_GMSGeneralProxy.loadGoogleAccountNames();
	}

	public void LoadToken(string accountName, string scopes)
	{
		AN_GMSGeneralProxy.loadToken(accountName, scopes);
	}

	public void LoadToken()
	{
		LoadToken(currentAccount, "oauth2:https://www.googleapis.com/auth/games");
	}

	public void GetGamesServerAuthCode()
	{
	}

	public void InvalidateToken(string token)
	{
		AN_GMSGeneralProxy.invalidateToken(token);
	}

	[Obsolete("showAchievementsUI is deprecated, please use ShowAchievementsUI instead.")]
	public void showAchievementsUI()
	{
		ShowAchievementsUI();
	}

	public void ShowAchievementsUI()
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.showAchievementsUI();
		}
	}

	[Obsolete("showLeaderBoardsUI is deprecated, please use showLeaderBoardsUI instead.")]
	public void showLeaderBoardsUI()
	{
		ShowLeaderBoardsUI();
	}

	public void ShowLeaderBoardsUI()
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.showLeaderBoardsUI();
		}
	}

	[Obsolete("showLeaderBoard is deprecated, please use ShowLeaderBoard instead.")]
	public void showLeaderBoard(string leaderboardName)
	{
		ShowLeaderBoard(leaderboardName);
	}

	public void ShowLeaderBoard(string leaderboardName)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.showLeaderBoard(leaderboardName);
		}
	}

	[Obsolete("showLeaderBoardById is deprecated, please use ShowLeaderBoardById instead.")]
	public void showLeaderBoardById(string leaderboardId)
	{
		ShowLeaderBoardById(leaderboardId);
	}

	public void ShowLeaderBoardById(string leaderboardId)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.showLeaderBoardById(leaderboardId);
		}
	}

	[Obsolete("submitScore is deprecated, please use SubmitScore instead.")]
	public void submitScore(string leaderboardName, long score)
	{
		SubmitScore(leaderboardName, score);
	}

	public void SubmitScore(string leaderboardName, long score)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.submitScore(leaderboardName, score);
		}
	}

	[Obsolete("submitScoreById is deprecated, please use SubmitScoreById instead.")]
	public void submitScoreById(string leaderboardId, long score)
	{
		SubmitScoreById(leaderboardId, score);
	}

	public void SubmitScoreById(string leaderboardId, long score)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.submitScoreById(leaderboardId, score);
		}
	}

	[Obsolete("loadLeaderBoards is deprecated, please use LoadLeaderBoards instead.")]
	public void loadLeaderBoards()
	{
		LoadLeaderBoards();
	}

	public void LoadLeaderBoards()
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.loadLeaderBoards();
		}
	}

	public void UpdatePlayerScoreLocal(GPLeaderBoard leaderboard)
	{
		if (GooglePlayConnection.CheckState())
		{
			int nextId = SA_IdFactory.NextId;
			leaderboard.CreateScoreListener(nextId);
			AN_GMSGeneralProxy.loadLeaderboardInfoLocal(leaderboard.Id, nextId);
		}
	}

	[Obsolete("loadPlayerCenteredScores is deprecated, please use LoadPlayerCenteredScores instead.")]
	public void loadPlayerCenteredScores(string leaderboardId, GPBoardTimeSpan span, GPCollectionType collection, int maxResults)
	{
		LoadPlayerCenteredScores(leaderboardId, span, collection, maxResults);
	}

	public void LoadPlayerCenteredScores(string leaderboardId, GPBoardTimeSpan span, GPCollectionType collection, int maxResults)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.loadPlayerCenteredScores(leaderboardId, (int)span, (int)collection, maxResults);
		}
	}

	[Obsolete("loadTopScores is deprecated, please use LoadTopScores instead.")]
	public void loadTopScores(string leaderboardId, GPBoardTimeSpan span, GPCollectionType collection, int maxResults)
	{
		LoadTopScores(leaderboardId, span, collection, maxResults);
	}

	public void LoadTopScores(string leaderboardId, GPBoardTimeSpan span, GPCollectionType collection, int maxResults)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.loadTopScores(leaderboardId, (int)span, (int)collection, maxResults);
		}
	}

	public void UnlockAchievement(string achievementName)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.reportAchievement(achievementName);
		}
	}

	public void UnlockAchievementById(string achievementId)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.reportAchievementById(achievementId);
		}
	}

	[Obsolete("reportAchievement is deprecated, please use unlockAchievement instead.")]
	public void reportAchievement(string achievementName)
	{
		UnlockAchievement(achievementName);
	}

	[Obsolete("reportAchievementById is deprecated, please use unlockAchievementById instead.")]
	public void reportAchievementById(string achievementId)
	{
		UnlockAchievementById(achievementId);
	}

	[Obsolete("revealAchievement is deprecated, please use RevealAchievement instead.")]
	public void revealAchievement(string achievementName)
	{
		RevealAchievement(achievementName);
	}

	public void RevealAchievement(string achievementName)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.revealAchievement(achievementName);
		}
	}

	[Obsolete("revealAchievementById is deprecated, please use RevealAchievementById instead.")]
	public void revealAchievementById(string achievementId)
	{
		RevealAchievementById(achievementId);
	}

	public void RevealAchievementById(string achievementId)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.revealAchievementById(achievementId);
		}
	}

	[Obsolete("incrementAchievement is deprecated, please use IncrementAchievement instead.")]
	public void incrementAchievement(string achievementName, int numsteps)
	{
		IncrementAchievement(achievementName, numsteps);
	}

	public void IncrementAchievement(string achievementName, int numsteps)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.incrementAchievement(achievementName, numsteps.ToString());
		}
	}

	[Obsolete("incrementAchievementById is deprecated, please use IncrementAchievementById instead.")]
	public void incrementAchievementById(string achievementId, int numsteps)
	{
		IncrementAchievementById(achievementId, numsteps);
	}

	public void IncrementAchievementById(string achievementId, int numsteps)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.incrementAchievementById(achievementId, numsteps.ToString());
		}
	}

	public void SetStepsImmediate(string achievementId, int numsteps)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.setStepsImmediate(achievementId, numsteps.ToString());
		}
	}

	[Obsolete("loadAchievements is deprecated, please use LoadAchievements instead.")]
	public void loadAchievements()
	{
		LoadAchievements();
	}

	public void LoadAchievements()
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.loadAchievements();
		}
	}

	[Obsolete("resetAchievement is deprecated, please use ResetAchievement instead.")]
	public void resetAchievement(string achievementId)
	{
		ResetAchievement(achievementId);
	}

	public void ResetAchievement(string achievementId)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.resetAchievement(achievementId);
		}
	}

	public void ResetAllAchievements()
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.ResetAllAchievements();
		}
	}

	[Obsolete("resetLeaderBoard is deprecated, please use ResetLeaderBoard instead.")]
	public void resetLeaderBoard(string leaderboardId)
	{
		ResetLeaderBoard(leaderboardId);
	}

	public void ResetLeaderBoard(string leaderboardId)
	{
		if (!GooglePlayConnection.CheckState())
		{
			return;
		}
		AN_GMSGeneralProxy.resetLeaderBoard(leaderboardId);
		foreach (GPLeaderBoard leaderBoard in LeaderBoards)
		{
			if (leaderBoard.Id.Equals(leaderboardId))
			{
				LeaderBoards.Remove(leaderBoard);
				break;
			}
		}
	}

	[Obsolete("loadConnectedPlayers is deprecated, please use LoadFriends instead.")]
	public void loadConnectedPlayers()
	{
		LoadFriends();
	}

	[Obsolete("LoadConnectedPlayers is deprecated, please use LoadFriends instead.")]
	public void LoadConnectedPlayers()
	{
		LoadFriends();
	}

	public void LoadFriends()
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.loadConnectedPlayers();
		}
	}

	public void SendGiftRequest(GPGameRequestType type, int requestLifetimeDays, Texture2D icon, string description, string playload = "")
	{
		if (GooglePlayConnection.CheckState())
		{
			byte[] inArray = icon.EncodeToPNG();
			string icon2 = Convert.ToBase64String(inArray);
			AN_GMSGiftsProxy.sendGiftRequest((int)type, playload, requestLifetimeDays, icon2, description);
		}
	}

	public void ShowRequestsAccepDialog()
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGiftsProxy.showRequestAccepDialog();
		}
	}

	public void AcceptRequests(params string[] ids)
	{
		if (GooglePlayConnection.CheckState() && ids.Length != 0)
		{
			AN_GMSGiftsProxy.acceptRequests(string.Join("|", ids));
		}
	}

	public void DismissRequest(params string[] ids)
	{
		if (GooglePlayConnection.CheckState() && ids.Length != 0)
		{
			AN_GMSGiftsProxy.dismissRequest(string.Join("|", ids));
		}
	}

	public void DispatchLeaderboardUpdateEvent(GP_LeaderboardResult result)
	{
		GooglePlayManager.ActionScoreSubmited(result);
	}

	public GPLeaderBoard GetLeaderBoard(string leaderboardId)
	{
		foreach (GPLeaderBoard leaderBoard in LeaderBoards)
		{
			if (leaderBoard.Id.Equals(leaderboardId))
			{
				return leaderBoard;
			}
		}
		GPLeaderBoard gPLeaderBoard = new GPLeaderBoard(leaderboardId, string.Empty);
		LeaderBoards.Add(gPLeaderBoard);
		return gPLeaderBoard;
	}

	public GPAchievement GetAchievement(string achievementId)
	{
		foreach (GPAchievement achievement in Achievements)
		{
			if (achievement.Id.Equals(achievementId))
			{
				return achievement;
			}
		}
		return null;
	}

	public GooglePlayerTemplate GetPlayerById(string playerId)
	{
		if (players.ContainsKey(playerId))
		{
			return players[playerId];
		}
		return null;
	}

	public GPGameRequest GetGameRequestById(string id)
	{
		foreach (GPGameRequest gameRequest in _gameRequests)
		{
			if (gameRequest.id.Equals(id))
			{
				return gameRequest;
			}
		}
		return null;
	}

	private void OnGiftSendResult(string data)
	{
		Debug.Log("OnGiftSendResult");
		string[] array = data.Split("|"[0]);
		GooglePlayGiftRequestResult obj = new GooglePlayGiftRequestResult(array[0]);
		GooglePlayManager.ActionSendGiftResultReceived(obj);
	}

	private void OnRequestsInboxDialogDismissed(string data)
	{
		GooglePlayManager.ActionRequestsInboxDialogDismissed();
	}

	private void OnAchievementsLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		GooglePlayResult googlePlayResult = new GooglePlayResult(array[0]);
		if (googlePlayResult.IsSucceeded)
		{
			Achievements.Clear();
			for (int i = 1; i < array.Length && !(array[i] == "endofline"); i += 7)
			{
				GPAchievement gPAchievement = new GPAchievement(array[i], array[i + 1], array[i + 2], array[i + 3], array[i + 4], array[i + 5], array[i + 6]);
				Debug.Log(gPAchievement.Name);
				Debug.Log(gPAchievement.Type);
				Achievements.Add(gPAchievement);
			}
			Debug.Log("Loaded: " + Achievements.Count + " Achievements");
		}
		GooglePlayManager.ActionAchievementsLoaded(googlePlayResult);
	}

	private void OnAchievementUpdated(string data)
	{
		string[] array = data.Split("|"[0]);
		GP_AchievementResult gP_AchievementResult = new GP_AchievementResult(array[0]);
		gP_AchievementResult.achievementId = array[1];
		GooglePlayManager.ActionAchievementUpdated(gP_AchievementResult);
	}

	private void OnScoreDataRecevied(string data)
	{
		Debug.Log("OnScoreDataRecevide");
		string[] array = data.Split("|"[0]);
		GP_LeaderboardResult gP_LeaderboardResult = new GP_LeaderboardResult(null, array[0]);
		if (gP_LeaderboardResult.IsSucceeded)
		{
			GPBoardTimeSpan vTimeSpan = (GPBoardTimeSpan)Convert.ToInt32(array[1]);
			GPCollectionType sCollection = (GPCollectionType)Convert.ToInt32(array[2]);
			string leaderboardId = array[3];
			string lName = array[4];
			GPLeaderBoard leaderBoard = GetLeaderBoard(leaderboardId);
			leaderBoard.UpdateName(lName);
			gP_LeaderboardResult = new GP_LeaderboardResult(leaderBoard, array[0]);
			for (int i = 5; i < array.Length && !(array[i] == "endofline"); i += 8)
			{
				long vScore = Convert.ToInt64(array[i]);
				int vRank = Convert.ToInt32(array[i + 1]);
				string text = array[i + 2];
				if (!players.ContainsKey(text))
				{
					GooglePlayerTemplate p = new GooglePlayerTemplate(text, array[i + 3], array[i + 4], array[i + 5], array[i + 6], array[i + 7]);
					AddPlayer(p);
				}
				GPScore score = new GPScore(vScore, vRank, vTimeSpan, sCollection, leaderBoard.Id, text);
				leaderBoard.UpdateScore(score);
				if (text.Equals(player.playerId))
				{
					leaderBoard.UpdateCurrentPlayerScore(score);
				}
			}
		}
		GooglePlayManager.ActionScoresListLoaded(gP_LeaderboardResult);
	}

	private void OnLeaderboardDataLoaded(string data)
	{
		Debug.Log("OnLeaderboardDataLoaded");
		string[] array = data.Split("|"[0]);
		GooglePlayResult googlePlayResult = new GooglePlayResult(array[0]);
		if (googlePlayResult.IsSucceeded)
		{
			for (int i = 1; i < array.Length && !(array[i] == "endofline"); i += 26)
			{
				string leaderboardId = array[i];
				string lName = array[i + 1];
				GPLeaderBoard leaderBoard = GetLeaderBoard(leaderboardId);
				leaderBoard.UpdateName(lName);
				int num = i + 2;
				for (int j = 0; j < 6; j++)
				{
					long vScore = Convert.ToInt64(array[num]);
					int vRank = Convert.ToInt32(array[num + 1]);
					GPBoardTimeSpan vTimeSpan = (GPBoardTimeSpan)Convert.ToInt32(array[num + 2]);
					GPCollectionType sCollection = (GPCollectionType)Convert.ToInt32(array[num + 3]);
					GPScore score = new GPScore(vScore, vRank, vTimeSpan, sCollection, leaderBoard.Id, player.playerId);
					num += 4;
					leaderBoard.UpdateScore(score);
					leaderBoard.UpdateCurrentPlayerScore(score);
				}
			}
			Debug.Log("Loaded: " + LeaderBoards.Count + " Leaderboards");
		}
		_IsLeaderboardsDataLoaded = true;
		GooglePlayManager.ActionLeaderboardsLoaded(googlePlayResult);
	}

	private void OnPlayerScoreUpdated(string data)
	{
		if (data.Equals(string.Empty))
		{
			Debug.Log("GooglePlayManager OnPlayerScoreUpdated, no data avaiable");
			return;
		}
		Debug.Log("OnPlayerScoreUpdated " + data);
		string[] array = data.Split("|"[0]);
		GP_ScoreResult gP_ScoreResult = new GP_ScoreResult(array[0]);
		string leaderboardId = array[1];
		int requestId = Convert.ToInt32(array[2]);
		GPLeaderBoard leaderBoard = GetLeaderBoard(leaderboardId);
		if (gP_ScoreResult.IsSucceeded)
		{
			GPBoardTimeSpan vTimeSpan = (GPBoardTimeSpan)Convert.ToInt32(array[3]);
			GPCollectionType sCollection = (GPCollectionType)Convert.ToInt32(array[4]);
			long vScore = Convert.ToInt64(array[5]);
			int vRank = Convert.ToInt32(array[6]);
			leaderBoard.ReportLocalPlayerScoreUpdate(gP_ScoreResult.score = new GPScore(vScore, vRank, vTimeSpan, sCollection, leaderBoard.Id, player.playerId), requestId);
		}
		else
		{
			leaderBoard.ReportLocalPlayerScoreUpdateFail(array[0], requestId);
		}
	}

	private void OnScoreSubmitted(string data)
	{
		Debug.Log("OnScoreSubmitted " + data);
		if (data.Equals(string.Empty))
		{
			Debug.Log("GooglePlayManager OnScoreSubmitted, no data avaiable");
			return;
		}
		string[] array = data.Split("|"[0]);
		GPLeaderBoard leaderBoard = GetLeaderBoard(array[1]);
		GP_LeaderboardResult gP_LeaderboardResult = new GP_LeaderboardResult(leaderBoard, array[0]);
		if (gP_LeaderboardResult.IsSucceeded)
		{
			Debug.Log("Score was submitted to leaderboard -> " + leaderBoard);
			UpdatePlayerScoreLocal(leaderBoard);
		}
		else
		{
			GooglePlayManager.ActionScoreSubmited(gP_LeaderboardResult);
		}
	}

	private void OnPlayerDataLoaded(string data)
	{
		Debug.Log("OnPlayerDataLoaded");
		if (data.Equals(string.Empty))
		{
			Debug.Log("GooglePlayManager OnPlayerLoaded, no data avaiable");
			return;
		}
		string[] array = data.Split("|"[0]);
		_player = new GooglePlayerTemplate(array[0], array[1], array[2], array[3], array[4], array[5]);
		AddPlayer(_player);
		_currentAccount = array[6];
	}

	private void OnPlayersLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		GooglePlayResult googlePlayResult = new GooglePlayResult(array[0]);
		if (googlePlayResult.IsSucceeded)
		{
			for (int i = 1; i < array.Length && !(array[i] == "endofline"); i += 6)
			{
				GooglePlayerTemplate googlePlayerTemplate = new GooglePlayerTemplate(array[i], array[i + 1], array[i + 2], array[i + 3], array[i + 4], array[i + 5]);
				AddPlayer(googlePlayerTemplate);
				if (!_friendsList.Contains(googlePlayerTemplate.playerId))
				{
					_friendsList.Add(googlePlayerTemplate.playerId);
				}
			}
		}
		Debug.Log("OnPlayersLoaded, total:" + players.Count);
		GooglePlayManager.ActionFriendsListLoaded(googlePlayResult);
	}

	private void OnGameRequestsLoaded(string data)
	{
		_gameRequests = new List<GPGameRequest>();
		if (data.Length != 0)
		{
			string[] array = data.Split("|"[0]);
			for (int i = 0; i < array.Length && !(array[i] == "endofline"); i += 6)
			{
				GPGameRequest gPGameRequest = new GPGameRequest();
				gPGameRequest.id = array[i];
				gPGameRequest.playload = array[i + 1];
				gPGameRequest.expirationTimestamp = Convert.ToInt64(array[i + 2]);
				gPGameRequest.creationTimestamp = Convert.ToInt64(array[i + 3]);
				gPGameRequest.sender = array[i + 4];
				gPGameRequest.type = (GPGameRequestType)Convert.ToInt32(array[i + 5]);
				_gameRequests.Add(gPGameRequest);
			}
			GooglePlayManager.ActionPendingGameRequestsDetected(_gameRequests);
		}
	}

	private void OnGameRequestsAccepted(string data)
	{
		List<GPGameRequest> list = new List<GPGameRequest>();
		string[] array = data.Split("|"[0]);
		for (int i = 0; i < array.Length && !(array[i] == "endofline"); i += 6)
		{
			GPGameRequest gPGameRequest = new GPGameRequest();
			gPGameRequest.id = array[i];
			gPGameRequest.playload = array[i + 1];
			gPGameRequest.expirationTimestamp = Convert.ToInt64(array[i + 2]);
			gPGameRequest.creationTimestamp = Convert.ToInt64(array[i + 3]);
			gPGameRequest.sender = array[i + 4];
			gPGameRequest.type = (GPGameRequestType)Convert.ToInt32(array[i + 5]);
			list.Add(gPGameRequest);
		}
		GooglePlayManager.ActionGameRequestsAccepted(list);
	}

	private void OnAccountsLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		_deviceGoogleAccountList.Clear();
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text != "endofline")
			{
				_deviceGoogleAccountList.Add(text);
			}
		}
		GooglePlayManager.ActionAvailableDeviceAccountsLoaded(_deviceGoogleAccountList);
	}

	private void OnTokenLoaded(string token)
	{
		_loadedAuthToken = token;
		GooglePlayManager.ActionOAuthTokenLoaded(_loadedAuthToken);
	}

	public static GP_Participant ParseParticipanData(string[] data, int index)
	{
		GP_Participant gP_Participant = new GP_Participant(data[index], data[index + 1], data[index + 2], data[index + 3], data[index + 4], data[index + 5]);
		if (Convert.ToBoolean(data[index + 6]))
		{
			GP_ParticipantResult result = new GP_ParticipantResult(data, index + 7);
			gP_Participant.SetResult(result);
		}
		return gP_Participant;
	}

	public static List<GP_Participant> ParseParticipantsData(string[] data, int index)
	{
		List<GP_Participant> list = new List<GP_Participant>();
		for (int i = index; i < data.Length && !(data[i] == "endofline"); i += 11)
		{
			GP_Participant item = ParseParticipanData(data, i);
			list.Add(item);
		}
		return list;
	}

	private void AddPlayer(GooglePlayerTemplate p)
	{
		if (!_players.ContainsKey(p.playerId))
		{
			_players.Add(p.playerId, p);
		}
	}
}
