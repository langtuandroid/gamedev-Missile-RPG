public class GK_TBM_MatchRemovedResult : ISN_Result
{
	private string _MatchId;

	public string MatchId
	{
		get
		{
			return _MatchId;
		}
	}

	public GK_TBM_MatchRemovedResult(string matchId)
		: base(true)
	{
		_MatchId = matchId;
	}

	public GK_TBM_MatchRemovedResult()
		: base(false)
	{
	}
}
