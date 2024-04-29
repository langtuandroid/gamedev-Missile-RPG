using System;
using System.Collections.Generic;
using UnityEngine;

public class UM_InAppPurchaseManager : SA_Singleton<UM_InAppPurchaseManager>
{
	public const string PREFS_KEY = "UM_InAppPurchaseManager";

	private bool _IsInited;

	public List<UM_InAppProduct> InAppProducts
	{
		get
		{
			return UltimateMobileSettings.Instance.InAppProducts;
		}
	}

	public bool IsInited
	{
		get
		{
			return _IsInited;
		}
	}

	public bool IsPurchasingAvailable
	{
		get
		{
			return IsInited;
		}
	}

	public static event Action<UM_BillingConnectionResult> OnBillingConnectFinishedAction;

	public static event Action<UM_PurchaseResult> OnPurchaseFlowFinishedAction;

	public static event Action<UM_BaseResult> OnPurchasesRestoreFinishedAction;

	static UM_InAppPurchaseManager()
	{
		UM_InAppPurchaseManager.OnBillingConnectFinishedAction = delegate
		{
		};
		UM_InAppPurchaseManager.OnPurchaseFlowFinishedAction = delegate
		{
		};
		UM_InAppPurchaseManager.OnPurchasesRestoreFinishedAction = delegate
		{
		};
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			AndroidInAppPurchaseManager.ActionProductPurchased += Android_ActionProductPurchased;
			AndroidInAppPurchaseManager.ActionProductConsumed += Android_OnProductConsumed;
			AndroidInAppPurchaseManager.ActionBillingSetupFinished += Android_OnBillingConnected;
			AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += Android_OnRetrieveProductsFinised;
			break;
		case RuntimePlatform.IPhonePlayer:
			IOSInAppPurchaseManager.OnStoreKitInitComplete += IOS_OnStoreKitInitComplete;
			IOSInAppPurchaseManager.OnTransactionComplete += IOS_OnTransactionComplete;
			IOSInAppPurchaseManager.OnRestoreComplete += IOS_OnRestoreComplete;
			break;
		case RuntimePlatform.WP8Player:
			WP8InAppPurchasesManager.OnInitComplete += WP8_OnInitComplete;
			WP8InAppPurchasesManager.OnPurchaseFinished += WP8_OnProductPurchased;
			break;
		}
	}

	public void Init()
	{
		if (_IsInited)
		{
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.WindowsEditor:
			Invoke("FakeConnectEvent", 1f);
			break;
		case RuntimePlatform.Android:
			foreach (UM_InAppProduct inAppProduct in UltimateMobileSettings.Instance.InAppProducts)
			{
				SA_Singleton<AndroidInAppPurchaseManager>.Instance.AddProduct(inAppProduct.AndroidId);
			}
			SA_Singleton<AndroidInAppPurchaseManager>.instance.LoadStore();
			break;
		case RuntimePlatform.IPhonePlayer:
			foreach (UM_InAppProduct inAppProduct2 in UltimateMobileSettings.Instance.InAppProducts)
			{
				ISN_Singleton<IOSInAppPurchaseManager>.instance.AddProductId(inAppProduct2.IOSId);
			}
			ISN_Singleton<IOSInAppPurchaseManager>.instance.LoadStore();
			break;
		case RuntimePlatform.WP8Player:
			WP8InAppPurchasesManager.Instance.Init();
			break;
		}
	}

	public void Subscribe(string subscriptionId)
	{
		UM_InAppProduct productById = UltimateMobileSettings.Instance.GetProductById(subscriptionId);
		if (productById != null)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.WindowsEditor:
				FakePurchaseEvent(productById);
				break;
			case RuntimePlatform.Android:
				SA_Singleton<AndroidInAppPurchaseManager>.instance.Subscribe(productById.AndroidId);
				break;
			case RuntimePlatform.IPhonePlayer:
				ISN_Singleton<IOSInAppPurchaseManager>.instance.BuyProduct(productById.IOSId);
				break;
			case RuntimePlatform.WP8Player:
				WP8InAppPurchasesManager.Instance.Purchase(productById.WP8Id);
				break;
			}
		}
		else
		{
			SendNoTemplateEvent();
			Debug.LogWarning("UM_InAppPurchaseManager subscription not found: " + subscriptionId);
		}
	}

	public void Purchase(string productId)
	{
		UM_InAppProduct productById = UltimateMobileSettings.Instance.GetProductById(productId);
		if (productById != null)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.WindowsEditor:
				FakePurchaseEvent(productById);
				break;
			case RuntimePlatform.Android:
				SA_Singleton<AndroidInAppPurchaseManager>.instance.Purchase(productById.AndroidId);
				break;
			case RuntimePlatform.IPhonePlayer:
				ISN_Singleton<IOSInAppPurchaseManager>.instance.BuyProduct(productById.IOSId);
				break;
			case RuntimePlatform.WP8Player:
				WP8InAppPurchasesManager.Instance.Purchase(productById.WP8Id);
				break;
			}
		}
		else
		{
			SendNoTemplateEvent();
			Debug.LogWarning("UM_InAppPurchaseManager product not found: " + productId);
		}
	}

	public bool IsProductPurchased(string id)
	{
		return IsProductPurchased(UltimateMobileSettings.Instance.GetProductById(id));
	}

	public bool IsProductPurchased(UM_InAppProduct product)
	{
		if (product == null)
		{
			return false;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.WindowsEditor:
			return PlayerPrefs.HasKey("UM_InAppPurchaseManager" + product.id);
		case RuntimePlatform.Android:
			if (SA_Singleton<AndroidInAppPurchaseManager>.instance.IsInventoryLoaded)
			{
				return SA_Singleton<AndroidInAppPurchaseManager>.Instance.Inventory.IsProductPurchased(product.AndroidId);
			}
			return PlayerPrefs.HasKey("UM_InAppPurchaseManager" + product.id);
		case RuntimePlatform.IPhonePlayer:
			return PlayerPrefs.HasKey("UM_InAppPurchaseManager" + product.id);
		case RuntimePlatform.WP8Player:
			if (WP8InAppPurchasesManager.Instance.IsInitialized)
			{
				return product.WP8Template.IsPurchased;
			}
			return PlayerPrefs.HasKey("UM_InAppPurchaseManager" + product.id);
		default:
			return false;
		}
	}

	public void DeleteNonConsumablePurchaseRecord(UM_InAppProduct product)
	{
		PlayerPrefs.DeleteKey("UM_InAppPurchaseManager" + product.id);
	}

	public void RestorePurchases()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.WindowsEditor:
			foreach (UM_InAppProduct inAppProduct in InAppProducts)
			{
				FakePurchaseEvent(inAppProduct);
			}
			Invoke("FakeRestoreEvent", 1f);
			break;
		case RuntimePlatform.IPhonePlayer:
			ISN_Singleton<IOSInAppPurchaseManager>.instance.RestorePurchases();
			break;
		}
	}

	public UM_InAppProduct GetProductById(string id)
	{
		return UltimateMobileSettings.Instance.GetProductById(id);
	}

	public UM_InAppProduct GetProductByIOSId(string id)
	{
		return UltimateMobileSettings.Instance.GetProductByIOSId(id);
	}

	public UM_InAppProduct GetProductByAndroidId(string id)
	{
		return UltimateMobileSettings.Instance.GetProductByAndroidId(id);
	}

	public UM_InAppProduct GetProductByWp8Id(string id)
	{
		return UltimateMobileSettings.Instance.GetProductByWp8Id(id);
	}

	private void WP8_OnInitComplete(WP8_InAppsInitResult result)
	{
		_IsInited = true;
		UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
		uM_BillingConnectionResult.message = "Inited";
		uM_BillingConnectionResult.isSuccess = true;
		foreach (UM_InAppProduct inAppProduct in UltimateMobileSettings.Instance.InAppProducts)
		{
			WP8ProductTemplate productById = WP8InAppPurchasesManager.Instance.GetProductById(inAppProduct.WP8Id);
			if (productById != null)
			{
				inAppProduct.SetTemplate(productById);
				if (inAppProduct.WP8Template.IsPurchased && !inAppProduct.IsConsumable)
				{
					SaveNonConsumableItemPurchaseInfo(inAppProduct);
				}
			}
		}
		UM_InAppPurchaseManager.OnBillingConnectFinishedAction(uM_BillingConnectionResult);
	}

	private void WP8_OnProductPurchased(WP8PurchseResponce resp)
	{
		UM_InAppProduct productByWp8Id = UltimateMobileSettings.Instance.GetProductByWp8Id(resp.ProductId);
		if (productByWp8Id != null)
		{
			UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
			uM_PurchaseResult.product = productByWp8Id;
			uM_PurchaseResult.WP8_PurchaseInfo = resp;
			SendPurchaseEvent(uM_PurchaseResult);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	private void IOS_OnTransactionComplete(IOSStoreKitResult responce)
	{
		UM_InAppProduct productByIOSId = UltimateMobileSettings.Instance.GetProductByIOSId(responce.ProductIdentifier);
		if (productByIOSId != null)
		{
			UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
			uM_PurchaseResult.product = productByIOSId;
			uM_PurchaseResult.IOS_PurchaseInfo = responce;
			SendPurchaseEvent(uM_PurchaseResult);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	private void IOS_OnStoreKitInitComplete(ISN_Result res)
	{
		UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
		_IsInited = res.IsSucceeded;
		uM_BillingConnectionResult.isSuccess = res.IsSucceeded;
		if (res.IsSucceeded)
		{
			uM_BillingConnectionResult.message = "Inited";
			foreach (UM_InAppProduct inAppProduct in UltimateMobileSettings.Instance.InAppProducts)
			{
				IOSProductTemplate productById = ISN_Singleton<IOSInAppPurchaseManager>.instance.GetProductById(inAppProduct.IOSId);
				if (productById != null)
				{
					inAppProduct.SetTemplate(productById);
				}
			}
			UM_InAppPurchaseManager.OnBillingConnectFinishedAction(uM_BillingConnectionResult);
		}
		else
		{
			if (res.Error != null)
			{
				uM_BillingConnectionResult.message = res.Error.Description;
			}
			UM_InAppPurchaseManager.OnBillingConnectFinishedAction(uM_BillingConnectionResult);
		}
	}

	private void IOS_OnRestoreComplete(IOSStoreKitRestoreResult res)
	{
		Debug.Log("IOS_OnRestoreComplete");
		UM_BaseResult uM_BaseResult = new UM_BaseResult();
		uM_BaseResult.IsSucceeded = res.IsSucceeded;
		UM_InAppPurchaseManager.OnPurchasesRestoreFinishedAction(uM_BaseResult);
	}

	private void Android_ActionProductPurchased(BillingResult result)
	{
		UM_InAppProduct productByAndroidId = UltimateMobileSettings.Instance.GetProductByAndroidId(result.purchase.SKU);
		if (productByAndroidId != null)
		{
			if (productByAndroidId.IsConsumable && result.isSuccess)
			{
				SA_Singleton<AndroidInAppPurchaseManager>.instance.Consume(result.purchase.SKU);
				return;
			}
			UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
			uM_PurchaseResult.isSuccess = result.isSuccess;
			uM_PurchaseResult.product = productByAndroidId;
			uM_PurchaseResult.SetResponceCode(result.response);
			uM_PurchaseResult.Google_PurchaseInfo = result.purchase;
			SendPurchaseEvent(uM_PurchaseResult);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	private void Android_OnProductConsumed(BillingResult result)
	{
		UM_InAppProduct productByAndroidId = UltimateMobileSettings.Instance.GetProductByAndroidId(result.purchase.SKU);
		if (productByAndroidId != null)
		{
			UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
			uM_PurchaseResult.isSuccess = result.isSuccess;
			uM_PurchaseResult.product = productByAndroidId;
			uM_PurchaseResult.SetResponceCode(result.response);
			uM_PurchaseResult.Google_PurchaseInfo = result.purchase;
			SendPurchaseEvent(uM_PurchaseResult);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	private void Android_OnBillingConnected(BillingResult result)
	{
		if (result.isSuccess)
		{
			AndroidInAppPurchaseManager.ActionBillingSetupFinished -= Android_OnBillingConnected;
			SA_Singleton<AndroidInAppPurchaseManager>.instance.RetrieveProducDetails();
			return;
		}
		UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
		uM_BillingConnectionResult.isSuccess = false;
		uM_BillingConnectionResult.message = result.message;
		UM_InAppPurchaseManager.OnBillingConnectFinishedAction(uM_BillingConnectionResult);
	}

	private void Android_OnRetrieveProductsFinised(BillingResult result)
	{
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= Android_OnRetrieveProductsFinised;
		UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
		uM_BillingConnectionResult.message = result.message;
		uM_BillingConnectionResult.isSuccess = result.isSuccess;
		_IsInited = uM_BillingConnectionResult.isSuccess;
		if (uM_BillingConnectionResult.isSuccess)
		{
			foreach (UM_InAppProduct inAppProduct in UltimateMobileSettings.Instance.InAppProducts)
			{
				GoogleProductTemplate productDetails = SA_Singleton<AndroidInAppPurchaseManager>.Instance.Inventory.GetProductDetails(inAppProduct.AndroidId);
				if (productDetails != null)
				{
					inAppProduct.SetTemplate(productDetails);
					if (inAppProduct.IsConsumable && SA_Singleton<AndroidInAppPurchaseManager>.Instance.Inventory.IsProductPurchased(inAppProduct.AndroidId))
					{
						SA_Singleton<AndroidInAppPurchaseManager>.instance.Consume(inAppProduct.AndroidId);
					}
					if (!inAppProduct.IsConsumable && SA_Singleton<AndroidInAppPurchaseManager>.Instance.Inventory.IsProductPurchased(inAppProduct.AndroidId))
					{
						SaveNonConsumableItemPurchaseInfo(inAppProduct);
					}
				}
			}
		}
		UM_InAppPurchaseManager.OnBillingConnectFinishedAction(uM_BillingConnectionResult);
	}

	private void SendPurchaseEvent(UM_PurchaseResult result)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			switch (result.IOS_PurchaseInfo.State)
			{
			case InAppPurchaseState.Purchased:
			case InAppPurchaseState.Restored:
				result.isSuccess = true;
				break;
			case InAppPurchaseState.Failed:
			case InAppPurchaseState.Deferred:
				result.isSuccess = false;
				break;
			}
			break;
		case RuntimePlatform.WP8Player:
			result.isSuccess = result.WP8_PurchaseInfo.IsSuccses;
			break;
		}
		if (!result.product.IsConsumable && result.isSuccess)
		{
			Debug.Log("UM: Purchase saved to PlayerPrefs");
			SaveNonConsumableItemPurchaseInfo(result.product);
		}
		UM_InAppPurchaseManager.OnPurchaseFlowFinishedAction(result);
	}

	private void SaveNonConsumableItemPurchaseInfo(UM_InAppProduct product)
	{
		PlayerPrefs.SetInt("UM_InAppPurchaseManager" + product.id, 1);
	}

	private void SendNoTemplateEvent()
	{
		Debug.LogWarning("UM: Product tamplate not found");
		UM_PurchaseResult obj = new UM_PurchaseResult();
		UM_InAppPurchaseManager.OnPurchaseFlowFinishedAction(obj);
	}

	private void FakeRestoreEvent()
	{
		UM_BaseResult uM_BaseResult = new UM_BaseResult();
		uM_BaseResult.IsSucceeded = true;
		UM_InAppPurchaseManager.OnPurchasesRestoreFinishedAction(uM_BaseResult);
	}

	private void FakeConnectEvent()
	{
		_IsInited = true;
		UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
		uM_BillingConnectionResult.message = "Inited";
		uM_BillingConnectionResult.isSuccess = true;
		UM_InAppPurchaseManager.OnBillingConnectFinishedAction(uM_BillingConnectionResult);
	}

	private void FakePurchaseEvent(UM_InAppProduct product)
	{
		UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
		uM_PurchaseResult.isSuccess = true;
		uM_PurchaseResult.product = product;
		SendPurchaseEvent(uM_PurchaseResult);
	}
}
