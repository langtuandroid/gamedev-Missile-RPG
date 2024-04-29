public class GK_TBM_MatchDataUpdateResult : ISN_Result
{
	private GK_TBM_Match _Match;

	public GK_TBM_Match Match
	{
		get
		{
			return _Match;
		}
	}

	public GK_TBM_MatchDataUpdateResult()
		: base(false)
	{
	}

	public GK_TBM_MatchDataUpdateResult(GK_TBM_Match updatedMatch)
		: base(true)
	{
		_Match = updatedMatch;
	}

	public GK_TBM_MatchDataUpdateResult(string errorData)
		: base(errorData)
	{
	}
}
