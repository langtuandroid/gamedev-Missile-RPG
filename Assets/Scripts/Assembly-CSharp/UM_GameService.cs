using UnityEngine;

public class UM_GameService : BaseIOSFeaturePreview
{
	public static UM_GameService me;

	public int hiScore = 100;

	private string leaderBoardId = "rank";

	private string TEST_ACHIEVEMENT_1_ID = "ftd_infi_rank";

	private void Awake()
	{
		if (me == null)
		{
			me = this;
			SA_Singleton<UM_GameServiceManager>.Instance.Connect();
		}
	}

	public void Show_Board()
	{
		SA_Singleton<UM_GameServiceManager>.Instance.Connect();
		SA_Singleton<UM_GameServiceManager>.instance.ShowLeaderBoardUI(leaderBoardId);
	}

	public void Show_Arch()
	{
		SA_Singleton<UM_GameServiceManager>.Instance.Connect();
		SA_Singleton<UM_GameServiceManager>.instance.ShowAchievementsUI();
	}

	public void Reset_Arch()
	{
		SA_Singleton<UM_GameServiceManager>.Instance.Connect();
		SA_Singleton<UM_GameServiceManager>.instance.ResetAchievements();
	}

	public void ReportScore(int score)
	{
		if (SA_Singleton<UM_GameServiceManager>.instance.ConnectionSate == UM_ConnectionState.CONNECTED)
		{
			SA_Singleton<UM_GameServiceManager>.instance.SubmitScore(leaderBoardId, score);
		}
	}

	public void ReportArchievement(string ArchName)
	{
		if (SA_Singleton<UM_GameServiceManager>.instance.ConnectionSate == UM_ConnectionState.CONNECTED)
		{
			SA_Singleton<UM_GameServiceManager>.instance.ReportAchievement(ArchName);
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
}
