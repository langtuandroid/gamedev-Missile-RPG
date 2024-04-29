using UnityEngine;

public class UM_BillingExample : BaseIOSFeaturePreview
{
	public const string CONSUMABLE_PRODUCT_ID = "coins_pack";

	public const string NON_CONSUMABLE_PRODUCT_ID = "coins_bonus";

	private void Awake()
	{
		UM_ExampleStatusBar.text = "Unified billing exmple scene loaded";
		UM_InAppPurchaseManager.OnPurchaseFlowFinishedAction += OnPurchaseFlowFinishedAction;
		UM_InAppPurchaseManager.OnBillingConnectFinishedAction += OnConnectFinished;
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "In-App Purchases", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Init"))
		{
			UM_InAppPurchaseManager.OnBillingConnectFinishedAction += OnBillingConnectFinishedAction;
			SA_Singleton<UM_InAppPurchaseManager>.instance.Init();
			UM_ExampleStatusBar.text = "Initializing billing...";
		}
		if (SA_Singleton<UM_InAppPurchaseManager>.instance.IsInited)
		{
			GUI.enabled = true;
		}
		else
		{
			GUI.enabled = false;
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Buy Consumable Item"))
		{
			SA_Singleton<UM_InAppPurchaseManager>.instance.Purchase("coins_pack");
			UM_ExampleStatusBar.text = "Start purchsing coins_pack product";
		}
		StartX += XButtonStep;
		bool flag = GUI.enabled;
		string empty = string.Empty;
		if (SA_Singleton<UM_InAppPurchaseManager>.instance.IsProductPurchased("coins_bonus"))
		{
			empty = "Already purchased";
			GUI.enabled = false;
		}
		else
		{
			empty = "Not yet purchased";
		}
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Buy Non-Consumable Item \n" + empty))
		{
			UM_ExampleStatusBar.text = "Start purchsing coins_bonus product";
			SA_Singleton<UM_InAppPurchaseManager>.instance.Purchase("coins_bonus");
		}
		GUI.enabled = flag;
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Restore Purshases \n For IOS Only"))
		{
			ISN_Singleton<IOSInAppPurchaseManager>.instance.RestorePurchases();
		}
	}

	private void OnConnectFinished(UM_BillingConnectionResult result)
	{
		if (result.isSuccess)
		{
			UM_ExampleStatusBar.text = "Billing init Success";
		}
		else
		{
			UM_ExampleStatusBar.text = "Billing init Failed";
		}
	}

	private void OnPurchaseFlowFinishedAction(UM_PurchaseResult result)
	{
		UM_InAppPurchaseManager.OnPurchaseFlowFinishedAction -= OnPurchaseFlowFinishedAction;
		if (result.isSuccess)
		{
			UM_ExampleStatusBar.text = "Product " + result.product.id + " purchase Success";
		}
		else
		{
			UM_ExampleStatusBar.text = "Product " + result.product.id + " purchase Failed";
		}
	}

	private void OnBillingConnectFinishedAction(UM_BillingConnectionResult result)
	{
		UM_InAppPurchaseManager.OnBillingConnectFinishedAction -= OnBillingConnectFinishedAction;
		if (result.isSuccess)
		{
			Debug.Log("Connected");
		}
		else
		{
			Debug.Log("Failed to connect");
		}
	}
}
