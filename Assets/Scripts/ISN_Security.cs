using System;
using UnityEngine;

public class ISN_Security : ISN_Singleton<ISN_Security>
{
	public static event Action<ISN_LocalReceiptResult> OnReceiptLoaded;

	public static event Action<ISN_Result> OnReceiptRefreshComplete;

	static ISN_Security()
	{
		ISN_Security.OnReceiptLoaded = delegate
		{
		};
		ISN_Security.OnReceiptRefreshComplete = delegate
		{
		};
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void RetrieveLocalReceipt()
	{
	}

	public void StartReceiptRefreshRequest()
	{
	}

	private void Event_ReceiptLoaded(string data)
	{
		ISN_LocalReceiptResult obj = new ISN_LocalReceiptResult(data);
		ISN_Security.OnReceiptLoaded(obj);
	}

	private void Event_ReceiptRefreshRequestReceived(string data)
	{
		ISN_Result obj = ((!data.Equals("1")) ? new ISN_Result(false) : new ISN_Result(true));
		ISN_Security.OnReceiptRefreshComplete(obj);
	}
}
