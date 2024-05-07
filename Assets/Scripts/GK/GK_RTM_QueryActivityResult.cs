public class GK_RTM_QueryActivityResult : ISN_Result
{
	private int _Activity;

	public int Activity
	{
		get
		{
			return _Activity;
		}
	}

	public GK_RTM_QueryActivityResult(int activity)
		: base(true)
	{
		_Activity = activity;
	}

	public GK_RTM_QueryActivityResult(string errorData)
		: base(errorData)
	{
	}
}
