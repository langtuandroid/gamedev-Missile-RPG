using UnityEngine;

public class GameCenterExample : BaseIOSFeaturePreview
{
	private int hiScore = 200;

	private string TEST_LEADERBOARD_1 = "your.ios.leaderbord1.id";

	private string TEST_LEADERBOARD_2 = "combined.board.1";

	private string TEST_ACHIEVEMENT_1_ID = "your.achievement.id1.here";

	private string TEST_ACHIEVEMENT_2_ID = "your.achievement.id2.here";

	private static bool IsInitialized;

	private static long LB2BestScores;

	private void Awake()
	{
		if (!IsInitialized)
		{
			GameCenterManager.RegisterAchievement(TEST_ACHIEVEMENT_1_ID);
			GameCenterManager.RegisterAchievement(TEST_ACHIEVEMENT_2_ID);
			GameCenterManager.OnAchievementsProgress += HandleOnAchievementsProgress;
			GameCenterManager.OnAchievementsReset += HandleOnAchievementsReset;
			GameCenterManager.OnAchievementsLoaded += OnAchievementsLoaded;
			GameCenterManager.OnScoreSubmitted += OnScoreSubmitted;
			GameCenterManager.OnLeadrboardInfoLoaded += OnLeadrboardInfoLoaded;
			GameCenterManager.OnAuthFinished += OnAuthFinished;
			GameCenterManager.Init();
			IsInitialized = true;
		}
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		if (GameCenterManager.Player != null)
		{
			GUI.Label(new Rect(100f, 10f, Screen.width, 40f), "ID: " + GameCenterManager.Player.Id);
			GUI.Label(new Rect(100f, 20f, Screen.width, 40f), "Name: " + GameCenterManager.Player.DisplayName);
			GUI.Label(new Rect(100f, 30f, Screen.width, 40f), "Alias: " + GameCenterManager.Player.Alias);
			if (GameCenterManager.Player.SmallPhoto != null)
			{
				GUI.DrawTexture(new Rect(10f, 10f, 75f, 75f), GameCenterManager.Player.SmallPhoto);
			}
		}
		StartY += YLableStep;
		StartY += YLableStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Game Center Leaderboards", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leaderboards"))
		{
			GameCenterManager.ShowLeaderboards();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Load Sets Info"))
		{
			GameCenterManager.OnLeaderboardSetsInfoLoaded += OnLeaderboardSetsInfoLoaded;
			GameCenterManager.LoadLeaderboardSetInfo();
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leaderboard 1"))
		{
			GameCenterManager.ShowLeaderboard(TEST_LEADERBOARD_1);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Score LB 1"))
		{
			hiScore++;
			GameCenterManager.ReportScore(hiScore, TEST_LEADERBOARD_1);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Score LB 1"))
		{
			GameCenterManager.LoadLeaderboardInfo(TEST_LEADERBOARD_1);
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Leaderboard #2, user best score: " + LB2BestScores, style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leader Board2"))
		{
			GameCenterManager.ShowLeaderboard(TEST_LEADERBOARD_2);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leaderboard 2 Today"))
		{
			GameCenterManager.ShowLeaderboard(TEST_LEADERBOARD_2, GK_TimeSpan.TODAY);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Score LB2"))
		{
			hiScore++;
			GameCenterManager.OnScoreSubmitted += OnScoreSubmitted;
			GameCenterManager.ReportScore(hiScore, TEST_LEADERBOARD_2);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Score LB 2"))
		{
			GameCenterManager.LoadLeaderboardInfo(TEST_LEADERBOARD_2);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Send Challenge"))
		{
			GameCenterManager.IssueLeaderboardChallenge(TEST_LEADERBOARD_2, "Here's a tiny challenge for you");
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Game Center Achievements", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Achievements"))
		{
			GameCenterManager.ShowAchievements();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Reset Achievements"))
		{
			GameCenterManager.ResetAchievements();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Submit Achievements1"))
		{
			GameCenterManager.SubmitAchievement(GameCenterManager.GetAchievementProgress(TEST_ACHIEVEMENT_1_ID) + 2.432f, TEST_ACHIEVEMENT_1_ID);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Submit Achievements2"))
		{
			GameCenterManager.SubmitAchievement(88.66f, TEST_ACHIEVEMENT_2_ID, false);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Send Challenge"))
		{
			GameCenterManager.IssueAchievementChallenge(TEST_ACHIEVEMENT_1_ID, "Here's a tiny challenge for you");
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "More", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Retrieve Signature"))
		{
			GameCenterManager.RetrievePlayerSignature();
			GameCenterManager.OnPlayerSignatureRetrieveResult += OnPlayerSignatureRetrieveResult;
		}
	}

	private void OnAchievementsLoaded(ISN_Result result)
	{
		Debug.Log("OnAchievementsLoaded");
		Debug.Log(result.IsSucceeded);
		if (!result.IsSucceeded)
		{
			return;
		}
		Debug.Log("Achievements were loaded from iOS Game Center");
		foreach (GK_AchievementTemplate achievement in GameCenterManager.Achievements)
		{
			Debug.Log(achievement.Id + ":  " + achievement.Progress);
		}
	}

	private void OnLeaderboardSetsInfoLoaded(ISN_Result res)
	{
		Debug.Log("OnLeaderboardSetsInfoLoaded");
		GameCenterManager.OnLeaderboardSetsInfoLoaded -= OnLeaderboardSetsInfoLoaded;
		if (res.IsSucceeded)
		{
			foreach (GK_LeaderboardSet leaderboardSet in GameCenterManager.LeaderboardSets)
			{
				Debug.Log(leaderboardSet.Title);
				Debug.Log(leaderboardSet.Identifier);
				Debug.Log(leaderboardSet.GroupIdentifier);
			}
		}
		if (GameCenterManager.LeaderboardSets.Count != 0)
		{
			GK_LeaderboardSet gK_LeaderboardSet = GameCenterManager.LeaderboardSets[0];
			gK_LeaderboardSet.OnLoaderboardsInfoLoaded += OnLoaderboardsInfoLoaded;
			gK_LeaderboardSet.LoadLeaderBoardsInfo();
		}
	}

	private void OnLoaderboardsInfoLoaded(ISN_LoadSetLeaderboardsInfoResult res)
	{
		res.LeaderBoardsSet.OnLoaderboardsInfoLoaded -= OnLoaderboardsInfoLoaded;
		if (!res.IsSucceeded)
		{
			return;
		}
		foreach (GK_LeaderBoardInfo item in res.LeaderBoardsSet.BoardsInfo)
		{
			Debug.Log(item.Title);
			Debug.Log(item.Description);
			Debug.Log(item.Identifier);
		}
	}

	private void HandleOnAchievementsReset(ISN_Result obj)
	{
		Debug.Log("All Achievements were reset");
	}

	private void HandleOnAchievementsProgress(GK_AchievementProgressResult result)
	{
		if (result.IsSucceeded)
		{
			GK_AchievementTemplate achievement = result.Achievement;
			Debug.Log(achievement.Id + ":  " + achievement.Progress);
		}
	}

	private void OnScoreSubmitted(GK_LeaderboardResult result)
	{
		if (result.IsSucceeded)
		{
			GK_Score currentPlayerScore = result.Leaderboard.GetCurrentPlayerScore(GK_TimeSpan.ALL_TIME, GK_CollectionType.GLOBAL);
			IOSNativePopUpManager.showMessage("Leaderboard " + currentPlayerScore.LongScore, "Score: " + currentPlayerScore.LongScore + "\nRank:" + currentPlayerScore.Rank);
		}
	}

	private void OnLeadrboardInfoLoaded(GK_LeaderboardResult result)
	{
		if (result.IsSucceeded)
		{
			GK_Score currentPlayerScore = result.Leaderboard.GetCurrentPlayerScore(GK_TimeSpan.ALL_TIME, GK_CollectionType.GLOBAL);
			IOSNativePopUpManager.showMessage("Leaderboard " + currentPlayerScore.LeaderboardId, "Score: " + currentPlayerScore.LongScore + "\nRank:" + currentPlayerScore.Rank);
			Debug.Log("double score representation: " + currentPlayerScore.DecimalFloat_2);
			Debug.Log("long score representation: " + currentPlayerScore.LongScore);
			if (currentPlayerScore.LeaderboardId.Equals(TEST_LEADERBOARD_2))
			{
				Debug.Log("Updating leaderboard 2 score");
				LB2BestScores = currentPlayerScore.LongScore;
			}
		}
	}

	private void OnScoreSubmitted(ISN_Result result)
	{
		GameCenterManager.OnScoreSubmitted -= OnScoreSubmitted;
		if (result.IsSucceeded)
		{
			Debug.Log("Score Submitted");
		}
		else
		{
			Debug.Log("Score Submit Failed");
		}
	}

	private void OnAuthFinished(ISN_Result res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Player Authed ", "ID: " + GameCenterManager.Player.Id + "\nAlias: " + GameCenterManager.Player.Alias);
			GameCenterManager.LoadLeaderboardInfo(TEST_LEADERBOARD_1);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Game Center ", "Player authentication failed");
		}
	}

	private void OnPlayerSignatureRetrieveResult(GK_PlayerSignatureResult result)
	{
		Debug.Log("OnPlayerSignatureRetrieveResult");
		if (result.IsSucceeded)
		{
			Debug.Log("PublicKeyUrl: " + result.PublicKeyUrl);
			Debug.Log("Signature: " + result.Signature);
			Debug.Log("Salt: " + result.Salt);
			Debug.Log("Timestamp: " + result.Timestamp);
		}
		else
		{
			Debug.Log("Error code: " + result.Error.Code);
			Debug.Log("Error description: " + result.Error.Description);
		}
		GameCenterManager.OnPlayerSignatureRetrieveResult -= OnPlayerSignatureRetrieveResult;
	}
}
