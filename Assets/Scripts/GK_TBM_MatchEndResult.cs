public class GK_TBM_MatchEndResult : ISN_Result
{
	private GK_TBM_Match _Match;

	public GK_TBM_Match Match
	{
		get
		{
			return _Match;
		}
	}

	public GK_TBM_MatchEndResult(GK_TBM_Match match)
		: base(true)
	{
		_Match = match;
	}

	public GK_TBM_MatchEndResult(string errorData)
		: base(errorData)
	{
	}
}
