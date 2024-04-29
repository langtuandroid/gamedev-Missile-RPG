public class GK_TBM_MatchTurnResult : ISN_Result
{
	private GK_TBM_Match _Match;

	public GK_TBM_Match Match
	{
		get
		{
			return _Match;
		}
	}

	public GK_TBM_MatchTurnResult(GK_TBM_Match match)
		: base(true)
	{
		_Match = match;
	}

	public GK_TBM_MatchTurnResult(string errorData)
		: base(errorData)
	{
	}
}
