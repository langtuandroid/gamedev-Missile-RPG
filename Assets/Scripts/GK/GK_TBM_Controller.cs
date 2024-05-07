using System;
using System.Collections.Generic;
using UnityEngine;

public class GK_TBM_Controller : iTBM_Matchmaker
{
	public List<UM_TBM_Match> _Matches = new List<UM_TBM_Match>();

	public List<UM_TBM_Invite> _Invitations = new List<UM_TBM_Invite>();

	public List<UM_TBM_Match> Matches
	{
		get
		{
			return _Matches;
		}
	}

	public List<UM_TBM_Invite> Invitations
	{
		get
		{
			return _Invitations;
		}
	}

	public event Action<UM_TBM_MatchResult> MatchFoundEvent = delegate
	{
	};

	public event Action<UM_TBM_MatchResult> MatchLoadedEvent = delegate
	{
	};

	public event Action<UM_TBM_MatchResult> InvitationAccepted = delegate
	{
	};

	public event Action<string> InvitationDeclined = delegate
	{
	};

	public event Action<UM_TBM_MatchResult> TurnEndedEvent = delegate
	{
	};

	public event Action<UM_TBM_MatchResult> MatchUpdatedEvent = delegate
	{
	};

	public event Action<UM_TBM_MatchesLoadResult> MatchesListLoadedEvent = delegate
	{
	};

	public event Action<List<UM_TBM_Invite>> InvitationsListLoadedEvent = delegate
	{
	};

	public event Action MatchesListUpdated = delegate
	{
	};

	public GK_TBM_Controller()
	{
		GameCenter_TBM.ActionMatchFound += HandleActionMatchFound;
		GameCenter_TBM.ActionRematched += HandleActionRematched;
		GameCenter_TBM.ActionMatchQuit += HandleActionMatchQuit;
		GameCenter_TBM.ActionTrunEnded += HandleActionTrunEnded;
		GameCenter_TBM.ActionMacthEnded += HandleActionMacthEnded;
		GameCenter_TBM.ActionPlayerQuitForMatch += HandleActionPlayerQuitForMatch;
		GameCenter_TBM.ActionTrunReceived += HandleActionTrunReceived;
		GameCenter_TBM.ActionMatchInfoLoaded += HandleActionMatchInfoLoaded;
		GameCenter_TBM.ActionMatchesInfoLoaded += HandleActionMatchesInfoLoaded;
		GameCenter_TBM.ActionMatchRemoved += HandleActionMatchRemoved;
		GameCenter_TBM.ActionMatchInvitationAccepted += HandleActionMatchInvitationAccepted;
		GameCenter_TBM.ActionMatchInvitationDeclined += HandleActionMatchInvitationDeclined;
	}

	private void HandleActionMatchInvitationDeclined(GK_TBM_MatchRemovedResult res)
	{
		RemoveInvitationsFromTheList(res.MatchId);
		this.MatchesListUpdated();
		this.InvitationDeclined(res.MatchId);
	}

	private void HandleActionMatchInvitationAccepted(GK_TBM_MatchInitResult res)
	{
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (res.IsSucceeded)
		{
			RemoveInvitationsFromTheList(res.Match.Id);
			UM_TBM_Match match = new UM_TBM_Match(res.Match);
			uM_TBM_MatchResult.SetMatch(match);
			UpdateMatchData(match);
		}
		this.InvitationAccepted(uM_TBM_MatchResult);
	}

	private void HandleActionMatchRemoved(GK_TBM_MatchRemovedResult res)
	{
		if (res.IsSucceeded)
		{
			RemoveMatchFromTheList(res.MatchId);
		}
	}

	public void SetGroup(int playerGroup)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.SetPlayerGroup(playerGroup);
	}

	public void SetMask(int mask)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.SetPlayerAttributes(mask);
	}

	public void FindMatch(int minPlayers, int maxPlayers, string[] recipients = null)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.FindMatch(minPlayers, maxPlayers, string.Empty, recipients);
	}

	public void ShowNativeFindMatchUI(int minPlayers, int maxPlayers)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.FindMatchWithNativeUI(minPlayers, maxPlayers, string.Empty);
	}

	public void LoadMatchesInfo()
	{
		ISN_Singleton<GameCenter_TBM>.Instance.LoadMatchesInfo();
	}

	public void LoadMatch(string matchId)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.LoadMatch(matchId);
	}

	public void RemoveMatch(string matchId)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.RemoveMatch(matchId);
	}

	public void TakeTurn(string matchId, byte[] matchData, UM_TBM_Participant nextParticipant)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.EndTurn(matchId, matchData, nextParticipant.Id);
	}

	public void FinishMatch(string matchId, byte[] matchData, params UM_TMB_ParticipantResult[] results)
	{
		foreach (UM_TMB_ParticipantResult uM_TMB_ParticipantResult in results)
		{
			int outcome = 1;
			switch (uM_TMB_ParticipantResult.Outcome)
			{
			case UM_TBM_Outcome.Won:
				outcome = 2;
				break;
			case UM_TBM_Outcome.Lost:
				outcome = 3;
				break;
			case UM_TBM_Outcome.Tied:
				outcome = 4;
				break;
			case UM_TBM_Outcome.Disconnected:
				outcome = 1;
				break;
			case UM_TBM_Outcome.None:
				outcome = 0;
				break;
			}
			ISN_Singleton<GameCenter_TBM>.Instance.UpdateParticipantOutcome(matchId, outcome, uM_TMB_ParticipantResult.ParticipantId);
		}
		ISN_Singleton<GameCenter_TBM>.Instance.EndMatch(matchId, matchData);
	}

	public void Rematch(string matchId)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.Rematch(matchId);
	}

	public void AcceptInvite(UM_TBM_Invite invite)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.AcceptInvite(invite.Id);
	}

	public void DeclineInvite(UM_TBM_Invite invite)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.DeclineInvite(invite.Id);
	}

	public void QuitInTurn(string matchId, UM_TBM_Participant nextParticipant)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.QuitInTurn(matchId, GK_TurnBasedMatchOutcome.Quit, nextParticipant.Id, new byte[0]);
	}

	public void QuitOutOfTurn(string matchId)
	{
		ISN_Singleton<GameCenter_TBM>.Instance.QuitOutOfTurn(matchId, GK_TurnBasedMatchOutcome.Quit);
	}

	public void SendMatchUpdateEvent(ISN_Result res, GK_TBM_Match match)
	{
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (match != null)
		{
			UM_TBM_Match match2 = new UM_TBM_Match(match);
			uM_TBM_MatchResult.SetMatch(match2);
			UpdateMatchData(match2);
		}
		this.MatchUpdatedEvent(uM_TBM_MatchResult);
	}

	private void UpdateMatchData(UM_TBM_Match match)
	{
		bool flag = false;
		for (int i = 0; i < Matches.Count; i++)
		{
			if (Matches[i].Id.Equals(match.Id))
			{
				flag = true;
				Matches[i] = match;
			}
		}
		if (!flag)
		{
			Matches.Add(match);
		}
		this.MatchesListUpdated();
	}

	private void RemoveMatchFromTheList(string matchId)
	{
		foreach (UM_TBM_Match match in _Matches)
		{
			if (match.Id.Equals(matchId))
			{
				_Matches.Remove(match);
				this.MatchesListUpdated();
				break;
			}
		}
	}

	private void RemoveInvitationsFromTheList(string inviteId)
	{
		foreach (UM_TBM_Invite invitation in _Invitations)
		{
			if (invitation.Id.Equals(inviteId))
			{
				_Invitations.Remove(invitation);
				break;
			}
		}
	}

	private void HandleActionMatchFound(GK_TBM_MatchInitResult res)
	{
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (res.Match != null)
		{
			UM_TBM_Match match = new UM_TBM_Match(res.Match);
			uM_TBM_MatchResult.SetMatch(match);
			UpdateMatchData(match);
		}
		this.MatchFoundEvent(uM_TBM_MatchResult);
	}

	private void HandleActionRematched(GK_TBM_RematchResult res)
	{
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (res.Match != null)
		{
			UM_TBM_Match match = new UM_TBM_Match(res.Match);
			uM_TBM_MatchResult.SetMatch(match);
			UpdateMatchData(match);
		}
		this.MatchFoundEvent(uM_TBM_MatchResult);
	}

	private void HandleActionTrunReceived(GK_TBM_MatchTurnResult res)
	{
		Debug.Log("GK_TBM_Controller::HandleActionTrunReceived");
		SendMatchUpdateEvent(res, res.Match);
	}

	private void HandleActionPlayerQuitForMatch(GK_TBM_Match match)
	{
		Debug.Log("GK_TBM_Controller::HandleActionPlayerQuitForMatch");
		ISN_Result res = new ISN_Result(true);
		SendMatchUpdateEvent(res, match);
	}

	private void HandleActionMatchQuit(GK_TBM_MatchQuitResult res)
	{
		Debug.Log("GK_TBM_Controller::HandleActionMatchQuit");
		LoadMatchesInfo();
	}

	private void HandleActionMacthEnded(GK_TBM_MatchEndResult res)
	{
		SendMatchUpdateEvent(res, res.Match);
	}

	private void HandleActionMatchInfoLoaded(GK_TBM_LoadMatchResult res)
	{
		Debug.Log("GK_TBM_Controller::HandleActionMatchInfoLoaded");
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (res.Match != null)
		{
			UM_TBM_Match match = new UM_TBM_Match(res.Match);
			UpdateMatchData(match);
			uM_TBM_MatchResult.SetMatch(match);
		}
		this.MatchLoadedEvent(uM_TBM_MatchResult);
	}

	private void HandleActionTrunEnded(GK_TBM_EndTrunResult res)
	{
		UM_TBM_MatchResult uM_TBM_MatchResult = new UM_TBM_MatchResult(res);
		if (res.Match != null)
		{
			UM_TBM_Match match = new UM_TBM_Match(res.Match);
			UpdateMatchData(match);
			uM_TBM_MatchResult.SetMatch(match);
		}
		this.TurnEndedEvent(uM_TBM_MatchResult);
	}

	private void HandleActionMatchesInfoLoaded(GK_TBM_LoadMatchesResult res)
	{
		UM_TBM_MatchesLoadResult uM_TBM_MatchesLoadResult = new UM_TBM_MatchesLoadResult(res);
		if (res.IsSucceeded)
		{
			_Matches.Clear();
			_Invitations.Clear();
			foreach (KeyValuePair<string, GK_TBM_Match> loadedMatch in res.LoadedMatches)
			{
				GK_TBM_Match value = loadedMatch.Value;
				if (value.LocalParticipant != null)
				{
					if (value.LocalParticipant.Status == GK_TurnBasedParticipantStatus.Invited)
					{
						UM_TBM_Invite item = new UM_TBM_Invite(value);
						_Invitations.Add(item);
					}
					else
					{
						UM_TBM_Match item2 = new UM_TBM_Match(value);
						_Matches.Add(item2);
					}
					uM_TBM_MatchesLoadResult.SetMatches(_Matches);
					uM_TBM_MatchesLoadResult.SetInvitations(_Invitations);
				}
			}
		}
		this.MatchesListUpdated();
		this.MatchesListLoadedEvent(uM_TBM_MatchesLoadResult);
	}
}
