using System;
using System.Collections.Generic;
using UnityEngine;

public class GameCenterInvitations : ISN_Singleton<GameCenterInvitations>
{
	public static event Action<GK_Player, GK_InviteRecipientResponse> ActionInviteeResponse;

	public static event Action<GK_MatchType, GK_Invite> ActionPlayerAcceptedInvitation;

	public static event Action<GK_MatchType, string[], GK_Player[]> ActionPlayerRequestedMatchWithRecipients;

	static GameCenterInvitations()
	{
		GameCenterInvitations.ActionInviteeResponse = delegate
		{
		};
		GameCenterInvitations.ActionPlayerAcceptedInvitation = delegate
		{
		};
		GameCenterInvitations.ActionPlayerRequestedMatchWithRecipients = delegate
		{
		};
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Init()
	{
	}

	private void OnInviteeResponse(string data)
	{
		Debug.Log("OnInviteeResponse");
		string[] array = data.Split('|');
		GK_Player playerById = GameCenterManager.GetPlayerById(array[0]);
		GK_InviteRecipientResponse arg = (GK_InviteRecipientResponse)Convert.ToInt32(array[1]);
		GameCenterInvitations.ActionInviteeResponse(playerById, arg);
	}

	private void OnPlayerAcceptedInvitation_RTM(string data)
	{
		Debug.Log("OnPlayerAcceptedInvitation_RTM");
		GK_Invite arg = new GK_Invite(data);
		GameCenterInvitations.ActionPlayerAcceptedInvitation(GK_MatchType.RealTime, arg);
	}

	private void OnPlayerRequestedMatchWithRecipients_RTM(string data)
	{
		Debug.Log("OnPlayerRequestedMatchWithRecipients_RTM");
		string[] array = IOSNative.ParseArray(data);
		List<GK_Player> list = new List<GK_Player>();
		string[] array2 = array;
		foreach (string playerID in array2)
		{
			list.Add(GameCenterManager.GetPlayerById(playerID));
		}
		GameCenterInvitations.ActionPlayerRequestedMatchWithRecipients(GK_MatchType.RealTime, array, list.ToArray());
	}

	private void OnPlayerAcceptedInvitation_TBM(string data)
	{
		Debug.Log("OnPlayerAcceptedInvitation_TBM");
		GK_Invite arg = new GK_Invite(data);
		GameCenterInvitations.ActionPlayerAcceptedInvitation(GK_MatchType.TurnBased, arg);
	}

	private void OnPlayerRequestedMatchWithRecipients_TBM(string data)
	{
		Debug.Log("OnPlayerRequestedMatchWithRecipients_TBM");
		string[] array = IOSNative.ParseArray(data);
		List<GK_Player> list = new List<GK_Player>();
		string[] array2 = array;
		foreach (string playerID in array2)
		{
			list.Add(GameCenterManager.GetPlayerById(playerID));
		}
		GameCenterInvitations.ActionPlayerRequestedMatchWithRecipients(GK_MatchType.RealTime, array, list.ToArray());
	}
}
