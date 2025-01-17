using System;

public class UM_TBM_Invite
{
	private string _Id;

	private DateTime _CreationTimestamp;

	private UM_TBM_Participant _Inviter;

	public string Id
	{
		get
		{
			return _Id;
		}
	}

	public DateTime CreationTimestamp
	{
		get
		{
			return _CreationTimestamp;
		}
	}

	public UM_TBM_Participant Inviter
	{
		get
		{
			return _Inviter;
		}
	}

	public UM_TBM_Invite(GP_Invite invite)
	{
		_Id = invite.Id;
		_CreationTimestamp = UnixTimeStampToDateTime(invite.CreationTimestamp);
		_Inviter = new UM_TBM_Participant(invite.Participant);
	}

	public UM_TBM_Invite(GK_TBM_Match match)
	{
		_Id = match.Id;
		_CreationTimestamp = match.CreationTimestamp;
		_Inviter = new UM_TBM_Participant(match.Participants[0]);
	}

	public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
	{
		return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(unixTimeStamp).ToLocalTime();
	}
}
