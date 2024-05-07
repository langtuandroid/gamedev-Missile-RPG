using UnityEngine;

public class BillingExample : MonoBehaviour
{
	public DefaultPreviewButton initButton;

	public DefaultPreviewButton[] initBoundButtons;

	public void init()
	{
		GPaymnetManagerExample.init();
	}

	private void FixedUpdate()
	{
		if (GPaymnetManagerExample.isInited)
		{
			initButton.DisabledButton();
			DefaultPreviewButton[] array = initBoundButtons;
			foreach (DefaultPreviewButton defaultPreviewButton in array)
			{
				defaultPreviewButton.EnabledButton();
			}
		}
		else
		{
			initButton.EnabledButton();
			DefaultPreviewButton[] array2 = initBoundButtons;
			foreach (DefaultPreviewButton defaultPreviewButton2 in array2)
			{
				defaultPreviewButton2.DisabledButton();
			}
		}
	}

	public void SuccsesPurchase()
	{
		if (GPaymnetManagerExample.isInited)
		{
			SA_Singleton<AndroidInAppPurchaseManager>.Instance.Purchase("android.test.purchased");
		}
		else
		{
			AndroidMessage.Create("Error", "PaymnetManagerExample not yet inited");
		}
	}

	public void FailPurchase()
	{
		if (GPaymnetManagerExample.isInited)
		{
			SA_Singleton<AndroidInAppPurchaseManager>.Instance.Purchase("android.test.item_unavailable");
		}
		else
		{
			AndroidMessage.Create("Error", "PaymnetManagerExample not yet inited");
		}
	}

	public void ConsumeProduct()
	{
		if (GPaymnetManagerExample.isInited)
		{
			if (SA_Singleton<AndroidInAppPurchaseManager>.instance.Inventory.IsProductPurchased("android.test.purchased"))
			{
				GPaymnetManagerExample.consume("android.test.purchased");
			}
			else
			{
				AndroidMessage.Create("Error", "You do not own product to consume it");
			}
		}
		else
		{
			AndroidMessage.Create("Error", "PaymnetManagerExample not yet inited");
		}
	}
}
