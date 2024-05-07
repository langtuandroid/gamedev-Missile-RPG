public class GK_RTM_MatchStartedResult : ISN_Result
{
	private GK_RTM_Match _Match;

	public GK_RTM_Match Match
	{
		get
		{
			return _Match;
		}
	}

	public GK_RTM_MatchStartedResult(GK_RTM_Match match)
		: base(true)
	{
		_Match = match;
	}

	public GK_RTM_MatchStartedResult(string errorData)
		: base(errorData)
	{
	}
}
