using System;
using UnityEngine;

public class JSHelper : MonoBehaviour
{
	private string leaderboardId = "your.leaderboard1.id.here";

	private string TEST_ACHIEVEMENT_1_ID = "your.achievement.id1.here";

	private string TEST_ACHIEVEMENT_2_ID = "your.achievement.id2.here";

	private void InitGameCneter()
	{
		GameCenterManager.RegisterAchievement(TEST_ACHIEVEMENT_1_ID);
		GameCenterManager.RegisterAchievement(TEST_ACHIEVEMENT_2_ID);
		GameCenterManager.OnAchievementsLoaded += HandleOnAchievementsLoaded;
		GameCenterManager.OnAchievementsProgress += HandleOnAchievementsProgress;
		GameCenterManager.OnAchievementsReset += HandleOnAchievementsReset;
		GameCenterManager.OnScoreSubmitted += OnScoreSubmitted;
		GameCenterManager.OnAuthFinished += HandleOnAuthFinished;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		GameCenterManager.Init();
		Debug.Log("InitGameCenter");
	}

	private void SubmitScore(int val)
	{
		Debug.Log("SubmitScore");
		GameCenterManager.ReportScore(val, leaderboardId);
	}

	private void SubmitAchievement(string data)
	{
		string[] array = data.Split("|"[0]);
		float percent = Convert.ToSingle(array[0]);
		string text = array[1];
		Debug.Log("SubmitAchievement: " + text + "  " + percent);
		GameCenterManager.SubmitAchievement(percent, text);
	}

	private void HandleOnAchievementsLoaded(ISN_Result res)
	{
		Debug.Log("Achievements loaded from iOS Game Center");
		foreach (GK_AchievementTemplate achievement in GameCenterManager.Achievements)
		{
			Debug.Log(achievement.Id + ":  " + achievement.Progress);
		}
	}

	private void HandleOnAchievementsProgress(GK_AchievementProgressResult progress)
	{
		Debug.Log("OnAchievementProgress");
		GK_AchievementTemplate achievement = progress.Achievement;
		Debug.Log(achievement.Id + ":  " + achievement.Progress);
	}

	private void HandleOnAchievementsReset(ISN_Result res)
	{
		Debug.Log("All Achievements were reset");
	}

	private void OnScoreSubmitted(GK_LeaderboardResult result)
	{
		if (result.IsSucceeded)
		{
			GK_Score currentPlayerScore = result.Leaderboard.GetCurrentPlayerScore(GK_TimeSpan.ALL_TIME, GK_CollectionType.GLOBAL);
			IOSNativePopUpManager.showMessage("Leaderboard " + currentPlayerScore.LeaderboardId, "Score: " + currentPlayerScore.LongScore + "\nRank:" + currentPlayerScore.Rank);
		}
	}

	private void HandleOnAuthFinished(ISN_Result r)
	{
		if (r.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Player Authenticated", "ID: " + GameCenterManager.Player.Id + "\nName: " + GameCenterManager.Player.DisplayName);
		}
	}
}
