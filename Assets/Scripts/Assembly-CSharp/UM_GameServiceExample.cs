using UnityEngine;

public class UM_GameServiceExample : BaseIOSFeaturePreview
{
	public int hiScore = 100;

	private string leaderBoardId = "LeaderBoardSample_1";

	private string leaderBoardId2 = "LeaderBoardSample_2";

	private string TEST_ACHIEVEMENT_1_ID = "AchievementSample_1";

	private string TEST_ACHIEVEMENT_2_ID = "AchievementSample_2";

	private void Awake()
	{
		UM_ExampleStatusBar.text = "Connecting To Game Service";
		UM_GameServiceManager.OnPlayerConnected += OnPlayerConnected;
		UM_GameServiceManager.OnPlayerDisconnected += OnPlayerDisconnected;
		if (SA_Singleton<UM_GameServiceManager>.instance.ConnectionSate == UM_ConnectionState.CONNECTED)
		{
			OnPlayerConnected();
		}
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		if (SA_Singleton<UM_GameServiceManager>.instance.ConnectionSate == UM_ConnectionState.CONNECTED && GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Disconnect"))
		{
			SA_Singleton<UM_GameServiceManager>.Instance.Disconnect();
		}
		if ((SA_Singleton<UM_GameServiceManager>.instance.ConnectionSate == UM_ConnectionState.DISCONNECTED || SA_Singleton<UM_GameServiceManager>.instance.ConnectionSate == UM_ConnectionState.UNDEFINED) && GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Connect"))
		{
			SA_Singleton<UM_GameServiceManager>.Instance.Connect();
		}
		if (SA_Singleton<UM_GameServiceManager>.instance.ConnectionSate == UM_ConnectionState.CONNECTED)
		{
			GUI.enabled = true;
		}
		else
		{
			GUI.enabled = false;
		}
		if (SA_Singleton<UM_GameServiceManager>.Instance.Player != null)
		{
			GUI.Label(new Rect(100f, 10f, Screen.width, 40f), "ID: " + SA_Singleton<UM_GameServiceManager>.Instance.Player.PlayerId);
			GUI.Label(new Rect(100f, 20f, Screen.width, 40f), "Name: " + SA_Singleton<UM_GameServiceManager>.Instance.Player.Name);
			if (SA_Singleton<UM_GameServiceManager>.Instance.Player.SmallPhoto != null)
			{
				GUI.DrawTexture(new Rect(10f, 10f, 75f, 75f), SA_Singleton<UM_GameServiceManager>.Instance.Player.SmallPhoto);
			}
		}
		StartY += YLableStep;
		StartY += YLableStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "GameCneter Leaderboards", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leaderboards"))
		{
			SA_Singleton<UM_GameServiceManager>.instance.ShowLeaderBoardsUI();
			UM_ExampleStatusBar.text = "Showing Leader Boards UI";
		}
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leader Board1"))
		{
			SA_Singleton<UM_GameServiceManager>.instance.ShowLeaderBoardUI(leaderBoardId);
			UM_ExampleStatusBar.text = "Showing " + leaderBoardId + " UI";
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Score LB 1"))
		{
			hiScore++;
			UM_GameServiceManager.ActionScoreSubmitted += HandleActionScoreSubmitted;
			SA_Singleton<UM_GameServiceManager>.instance.SubmitScore(leaderBoardId, hiScore);
			UM_ExampleStatusBar.text = "Score " + hiScore + " Submited to " + leaderBoardId;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Score LB 1"))
		{
			long longScore = SA_Singleton<UM_GameServiceManager>.instance.GetCurrentPlayerScore(leaderBoardId).LongScore;
			UM_ExampleStatusBar.text = "GetCurrentPlayerScore from  " + leaderBoardId + " is: " + longScore;
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leader Board2"))
		{
			SA_Singleton<UM_GameServiceManager>.instance.ShowLeaderBoardUI(leaderBoardId2);
			UM_ExampleStatusBar.text = "Showing " + leaderBoardId2 + " UI";
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Score LB2"))
		{
			hiScore++;
			SA_Singleton<UM_GameServiceManager>.instance.SubmitScore(leaderBoardId2, hiScore);
			UM_ExampleStatusBar.text = "Score " + hiScore + " Submited to " + leaderBoardId2;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Score LB 2"))
		{
			long longScore2 = SA_Singleton<UM_GameServiceManager>.instance.GetCurrentPlayerScore(leaderBoardId2).LongScore;
			UM_ExampleStatusBar.text = "GetCurrentPlayerScore from  " + leaderBoardId2 + " is: " + longScore2;
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Achievements", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Achievements"))
		{
			SA_Singleton<UM_GameServiceManager>.instance.ShowAchievementsUI();
			UM_ExampleStatusBar.text = "Showing Achievements UI";
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Reset Achievements"))
		{
			SA_Singleton<UM_GameServiceManager>.instance.ResetAchievements();
			UM_ExampleStatusBar.text = "Al acievmnets reseted";
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Achievements1"))
		{
			SA_Singleton<UM_GameServiceManager>.instance.UnlockAchievement(TEST_ACHIEVEMENT_1_ID);
			UM_ExampleStatusBar.text = "Achievement " + TEST_ACHIEVEMENT_1_ID + " Reported";
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Increment Achievements2"))
		{
			SA_Singleton<UM_GameServiceManager>.instance.IncrementAchievement(TEST_ACHIEVEMENT_2_ID, 20f);
			UM_ExampleStatusBar.text = "Achievement " + TEST_ACHIEVEMENT_2_ID + " Oncremented by 20%";
		}
	}

	private void HandleActionScoreSubmitted(UM_LeaderboardResult res)
	{
		if (res.IsSucceeded)
		{
			UM_Score currentPlayerScore = res.Leaderboard.GetCurrentPlayerScore(UM_TimeSpan.ALL_TIME, UM_CollectionType.GLOBAL);
			Debug.Log("Score submitted, new player high score: " + currentPlayerScore.LongScore);
			return;
		}
		Debug.Log("Score submission failed: " + res.Error.Code + " / " + res.Error.Description);
	}

	private void OnPlayerConnected()
	{
		UM_ExampleStatusBar.text = "Player Connected";
	}

	private void OnPlayerDisconnected()
	{
		UM_ExampleStatusBar.text = "Player Disconnected";
	}
}
