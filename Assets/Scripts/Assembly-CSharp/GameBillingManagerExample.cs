using UnityEngine;

public class GameBillingManagerExample : MonoBehaviour
{
	public const string COINS_ITEM = "small_coins_bag";

	public const string COINS_BOOST = "coins_bonus";

	private static bool _isInited;

	private static bool ListnersAdded;

	public static bool isInited
	{
		get
		{
			return _isInited;
		}
	}

	public static void init()
	{
		if (!ListnersAdded)
		{
			SA_Singleton<AndroidInAppPurchaseManager>.instance.AddProduct("small_coins_bag");
			SA_Singleton<AndroidInAppPurchaseManager>.instance.AddProduct("coins_bonus");
			AndroidInAppPurchaseManager.ActionProductPurchased += OnProductPurchased;
			AndroidInAppPurchaseManager.ActionProductConsumed += OnProductConsumed;
			AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;
			SA_Singleton<AndroidInAppPurchaseManager>.Instance.LoadStore();
			ListnersAdded = true;
		}
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
		switch (purchase.SKU)
		{
		case "small_coins_bag":
			consume("small_coins_bag");
			break;
		case "coins_bonus":
			GameDataExample.EnableCoinsBoost();
			break;
		}
	}

	private static void OnProcessingConsumeProduct(GooglePurchaseTemplate purchase)
	{
		switch (purchase.SKU)
		{
		case "small_coins_bag":
			GameDataExample.AddCoins(100);
			break;
		}
	}

	private static void OnProductPurchased(BillingResult result)
	{
		if (result.isSuccess)
		{
			OnProcessingPurchasedProduct(result.purchase);
		}
		else
		{
			AndroidMessage.Create("Product Purchase Failed", result.response + " " + result.message);
		}
		Debug.Log("Purchased Responce: " + result.response + " " + result.message);
	}

	private static void OnProductConsumed(BillingResult result)
	{
		if (result.isSuccess)
		{
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
			AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += OnRetrieveProductsFinised;
			SA_Singleton<AndroidInAppPurchaseManager>.Instance.RetrieveProducDetails();
		}
		AndroidMessage.Create("Connection Responce", result.response + " " + result.message);
		Debug.Log("Connection Responce: " + result.response + " " + result.message);
	}

	private static void OnRetrieveProductsFinised(BillingResult result)
	{
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetrieveProductsFinised;
		if (result.isSuccess)
		{
			UpdateStoreData();
			_isInited = true;
		}
		else
		{
			AndroidMessage.Create("Connection Responce", result.response + " " + result.message);
		}
	}

	private static void UpdateStoreData()
	{
		foreach (GoogleProductTemplate product in SA_Singleton<AndroidInAppPurchaseManager>.instance.Inventory.Products)
		{
			Debug.Log("Loaded product: " + product.Title);
		}
		if (SA_Singleton<AndroidInAppPurchaseManager>.instance.Inventory.IsProductPurchased("small_coins_bag"))
		{
			consume("small_coins_bag");
		}
		if (SA_Singleton<AndroidInAppPurchaseManager>.instance.Inventory.IsProductPurchased("coins_bonus"))
		{
			GameDataExample.EnableCoinsBoost();
		}
	}
}
