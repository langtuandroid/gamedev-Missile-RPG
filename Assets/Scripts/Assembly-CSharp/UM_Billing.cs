using UnityEngine;

public class UM_Billing : BaseIOSFeaturePreview
{
	public static UM_Billing me;

	public int Item_Number;

	public string[] CONSUMABLE_PRODUCT_ID = new string[6] { "Crystal_1", "Crystal_2", "Crystal_3", "Crystal_4", "Crystal_5", "Crystal_6" };

	public bool Waiting;

	public GameObject Fade;

	public GameObject FailConnection;

	private void Awake()
	{
		if (me == null)
		{
			me = this;
			UM_InAppPurchaseManager.OnPurchaseFlowFinishedAction += OnPurchaseFlowFinishedAction;
			UM_InAppPurchaseManager.OnBillingConnectFinishedAction += OnConnectFinished;
		}
	}

	private new void Start()
	{
		UM_InAppPurchaseManager.OnBillingConnectFinishedAction += OnBillingConnectFinishedAction;
		SA_Singleton<UM_InAppPurchaseManager>.Instance.Init();
	}

	public void Resetting()
	{
		if (Fade != null)
		{
			Fade.SetActive(false);
		}
		Waiting = false;
	}

	public void BuyItem(int Number)
	{
		if (!Waiting)
		{
			if (Fade != null)
			{
				Fade.SetActive(true);
			}
			Waiting = true;
			Item_Number = Number;
			SA_Singleton<UM_InAppPurchaseManager>.instance.Purchase(CONSUMABLE_PRODUCT_ID[Item_Number]);
		}
	}

	public void Restore()
	{
		ISN_Singleton<IOSInAppPurchaseManager>.instance.RestorePurchases();
	}

	private void OnConnectFinished(UM_BillingConnectionResult result)
	{
		if (result.isSuccess)
		{
			Debug.Log("Billing Connect");
			return;
		}
		Debug.Log("Billing Connect fail");
		SoundManager.me.Cancel();
		if (FailConnection != null)
		{
			FailConnection.SetActive(false);
			FailConnection.SetActive(true);
		}
	}

	private void OnPurchaseFlowFinishedAction(UM_PurchaseResult result)
	{
		Debug.Log(result);
		if (result.isSuccess)
		{
			if (Fade != null)
			{
				Fade.SetActive(false);
			}
			Waiting = false;
			Debug.Log("Product " + result.product.id + " purchase Success");
			if (PlayerPrefs.GetString("IAP_Crystal") == "1" || PlayerPrefs.GetString("IAP_Crystal") == string.Empty)
			{
				Debug.Log("ù\ufffd\ufffd\ufffd\ufffd");
			}
			if (Item_Number > 0)
			{
				PlayerPrefs.SetInt("AD_Remove", 1);
			}
			SoundManager.me.Congretu();
			switch (Item_Number)
			{
			case 0:
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			case 5:
				break;
			}
		}
		else
		{
			if (Fade != null)
			{
				Fade.SetActive(false);
			}
			Waiting = false;
			SoundManager.me.Cancel();
			if (FailConnection != null)
			{
				FailConnection.SetActive(false);
				FailConnection.SetActive(true);
			}
		}
	}

	private void OnBillingConnectFinishedAction(UM_BillingConnectionResult result)
	{
		UM_InAppPurchaseManager.OnBillingConnectFinishedAction -= OnBillingConnectFinishedAction;
		if (result.isSuccess)
		{
			Debug.Log("Connected");
			return;
		}
		SoundManager.me.Cancel();
		if (FailConnection != null)
		{
			FailConnection.SetActive(false);
			FailConnection.SetActive(true);
		}
	}
}
