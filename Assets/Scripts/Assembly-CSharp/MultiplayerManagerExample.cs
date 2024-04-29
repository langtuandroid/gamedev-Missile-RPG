using UnityEngine;

public class MultiplayerManagerExample : MonoBehaviour
{
	private void Awake()
	{
		GameCenterManager.Init();
		GameCenter_RTM.ActionMatchStarted += HandleActionMatchStarted;
		GameCenter_RTM.ActionPlayerStateChanged += HandleActionPlayerStateChanged;
		GameCenter_RTM.ActionDataReceived += HandleActionDataReceived;
		GameCenterInvitations.ActionPlayerRequestedMatchWithRecipients += HandleActionPlayerRequestedMatchWithRecipients;
		GameCenterInvitations.ActionPlayerAcceptedInvitation += HandleActionPlayerAcceptedInvitation;
	}

	private void HandleActionPlayerAcceptedInvitation(GK_MatchType math, GK_Invite invite)
	{
		ISN_Singleton<GameCenter_RTM>.Instance.StartMatchWithInvite(invite, true);
	}

	private void HandleActionPlayerRequestedMatchWithRecipients(GK_MatchType matchType, string[] recepientIds, GK_Player[] recepients)
	{
		Debug.Log("inictation received");
		if (matchType == GK_MatchType.RealTime)
		{
			string msg = "Come play with me, bro.";
			ISN_Singleton<GameCenter_RTM>.Instance.FindMatchWithNativeUI(recepientIds.Length, recepientIds.Length, msg, recepientIds);
		}
	}

	private void OnGUI()
	{
	}

	private void HandleActionPlayerStateChanged(GK_Player player, GK_PlayerConnectionState state, GK_RTM_Match match)
	{
		Debug.Log("Player State Changed " + player.Alias + " state: " + state.ToString() + "\n  ExpectedPlayerCount: " + match.ExpectedPlayerCount);
	}

	private void HandleActionMatchStarted(GK_RTM_MatchStartedResult result)
	{
		IOSNativePopUpManager.dismissCurrentAlert();
		if (result.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Match Started", "let's play now\n  Others players count: " + result.Match.Players.Count);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Match Started Error", result.Error.Description);
		}
	}

	private void HandleActionDataReceived(GK_Player player, byte[] data)
	{
		IOSNativePopUpManager.dismissCurrentAlert();
	}
}
