using UnityEngine;

public class UM_PurchaseResult
{
	public bool isSuccess;

	public UM_InAppProduct product = new UM_InAppProduct();

	private int _ResponceCode = -1;

	public GooglePurchaseTemplate Google_PurchaseInfo;

	public IOSStoreKitResult IOS_PurchaseInfo;

	public WP8PurchseResponce WP8_PurchaseInfo;

	public string TransactionId
	{
		get
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return Google_PurchaseInfo.orderId;
			}
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return IOS_PurchaseInfo.TransactionIdentifier;
			}
			if (Application.platform == RuntimePlatform.WP8Player)
			{
				return WP8_PurchaseInfo.TransactionId;
			}
			return string.Empty;
		}
	}

	public int ResponceCode
	{
		get
		{
			return _ResponceCode;
		}
	}

	public void SetResponceCode(int code)
	{
		_ResponceCode = code;
	}
}
