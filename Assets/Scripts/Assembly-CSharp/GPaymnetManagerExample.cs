using UnityEngine;

public class GPaymnetManagerExample : MonoBehaviour
{
	public const string ANDROID_TEST_PURCHASED = "android.test.purchased";

	public const string ANDROID_TEST_CANCELED = "android.test.canceled";

	public const string ANDROID_TEST_REFUNDED = "android.test.refunded";

	public const string ANDROID_TEST_ITEM_UNAVAILABLE = "android.test.item_unavailable";

	private static bool _isInited;

	public static bool isInited
	{
		get
		{
			return _isInited;
		}
	}

	public static void init()
	{
		SA_Singleton<AndroidInAppPurchaseManager>.instance.AddProduct("android.test.purchased");
		SA_Singleton<AndroidInAppPurchaseManager>.instance.AddProduct("android.test.canceled");
		SA_Singleton<AndroidInAppPurchaseManager>.instance.AddProduct("android.test.refunded");
		SA_Singleton<AndroidInAppPurchaseManager>.instance.AddProduct("android.test.item_unavailable");
		AndroidInAppPurchaseManager.ActionProductPurchased += OnProductPurchased;
		AndroidInAppPurchaseManager.ActionProductConsumed += OnProductConsumed;
		AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;
		SA_Singleton<AndroidInAppPurchaseManager>.Instance.LoadStore();
	}

	public static void purchase(string SKU)
	{
		SA_Singleton<AndroidInAppPurchaseManager>.Instance.Purchase(SKU);
	}

	public static void consume(string SKU)
	{
		SA_Singleton<AndroidInAppPurchaseManager>.Instance.Consume(SKU);
	}

	private static void OnProcessingPurchasedProduct(GooglePurchaseTemplate purchase)
	{
	}

	private static void OnProcessingConsumeProduct(GooglePurchaseTemplate purchase)
	{
	}

	private static void OnProductPurchased(BillingResult result)
	{
		if (result.isSuccess)
		{
			AndroidMessage.Create("Product Purchased", result.purchase.SKU + "\n Full Response: " + result.purchase.originalJson);
			OnProcessingPurchasedProduct(result.purchase);
		}
		else
		{
			AndroidMessage.Create("Product Purchase Failed", result.response + " " + result.message);
		}
		Debug.Log("Purchased Responce: " + result.response + " " + result.message);
		Debug.Log(result.purchase.originalJson);
	}

	private static void OnProductConsumed(BillingResult result)
	{
		if (result.isSuccess)
		{
			AndroidMessage.Create("Product Consumed", result.purchase.SKU + "\n Full Response: " + result.purchase.originalJson);
			OnProcessingConsumeProduct(result.purchase);
		}
		else
		{
			AndroidMessage.Create("Product Cousume Failed", result.response + " " + result.message);
		}
		Debug.Log("Cousume Responce: " + result.response + " " + result.message);
	}

	private static void OnBillingConnected(BillingResult result)
	{
		AndroidInAppPurchaseManager.ActionBillingSetupFinished -= OnBillingConnected;
		if (result.isSuccess)
		{
			SA_Singleton<AndroidInAppPurchaseManager>.Instance.RetrieveProducDetails();
			AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += OnRetrieveProductsFinised;
		}
		AndroidMessage.Create("Connection Responce", result.response + " " + result.message);
		Debug.Log("Connection Responce: " + result.response + " " + result.message);
	}

	private static void OnRetrieveProductsFinised(BillingResult result)
	{
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetrieveProductsFinised;
		if (result.isSuccess)
		{
			_isInited = true;
			AndroidMessage.Create("Success", "Billing init complete inventory contains: " + SA_Singleton<AndroidInAppPurchaseManager>.instance.Inventory.Purchases.Count + " products");
			Debug.Log("Loaded products names");
			foreach (GoogleProductTemplate product in SA_Singleton<AndroidInAppPurchaseManager>.instance.Inventory.Products)
			{
				Debug.Log(product.Title);
				Debug.Log(product.OriginalJson);
			}
		}
		else
		{
			AndroidMessage.Create("Connection Responce", result.response + " " + result.message);
		}
		Debug.Log("Connection Responce: " + result.response + " " + result.message);
	}
}
