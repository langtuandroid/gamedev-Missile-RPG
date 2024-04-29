public class GK_TBM_MatchQuitResult : ISN_Result
{
	private string _MatchId;

	public string MatchId
	{
		get
		{
			return _MatchId;
		}
	}

	public GK_TBM_MatchQuitResult(string matchId)
		: base(true)
	{
		_MatchId = matchId;
	}

	public GK_TBM_MatchQuitResult()
		: base(false)
	{
	}
}
