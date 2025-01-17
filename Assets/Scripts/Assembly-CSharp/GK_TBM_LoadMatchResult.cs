public class GK_TBM_LoadMatchResult : ISN_Result
{
	private GK_TBM_Match _Match;

	public GK_TBM_Match Match
	{
		get
		{
			return _Match;
		}
	}

	public GK_TBM_LoadMatchResult(GK_TBM_Match match)
		: base(true)
	{
		_Match = match;
	}

	public GK_TBM_LoadMatchResult(string errorData)
		: base(errorData)
	{
	}
}
