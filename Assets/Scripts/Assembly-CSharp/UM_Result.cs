public class UM_Result
{
	protected UM_Error _Error;

	protected bool _IsSucceeded = true;

	public bool IsSucceeded
	{
		get
		{
			return _IsSucceeded;
		}
	}

	public bool IsFailed
	{
		get
		{
			return !_IsSucceeded;
		}
	}

	public UM_Error Error
	{
		get
		{
			return _Error;
		}
	}

	public UM_Result()
	{
		_IsSucceeded = true;
	}

	public UM_Result(ISN_Result result)
	{
		_IsSucceeded = result.IsSucceeded;
		if (!_IsSucceeded)
		{
			_Error = new UM_Error(result.Error.Code, result.Error.Description);
		}
	}

	public UM_Result(GooglePlayResult result)
	{
		_IsSucceeded = result.IsSucceeded;
		if (!_IsSucceeded)
		{
			_Error = new UM_Error((int)result.Response, result.Message);
		}
	}

	public void SetError(UM_Error e)
	{
		_Error = e;
		_IsSucceeded = false;
	}
}
