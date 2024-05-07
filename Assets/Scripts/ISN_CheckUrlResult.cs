public class ISN_CheckUrlResult : ISN_Result
{
	private string _url;

	public string url
	{
		get
		{
			return _url;
		}
	}

	public ISN_CheckUrlResult(string checkedUrl, bool IsResultSucceeded)
		: base(IsResultSucceeded)
	{
		_url = checkedUrl;
	}
}
