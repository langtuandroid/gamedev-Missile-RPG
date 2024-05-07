using System;
using UnityEngine;

public class AndroidInAppPurchaseManager : SA_Singleton<AndroidInAppPurchaseManager>
{
	private string _processedSKU;

	private AndroidInventory _inventory;

	private bool _IsConnectingToServiceInProcess;

	private bool _IsProductRetrievingInProcess;

	private bool _IsConnected;

	private bool _IsInventoryLoaded;

	[Obsolete("inventory is deprectaed, plase use Inventory instead")]
	public AndroidInventory inventory
	{
		get
		{
			return _inventory;
		}
	}

	public AndroidInventory Inventory
	{
		get
		{
			return _inventory;
		}
	}

	public bool IsConnectingToServiceInProcess
	{
		get
		{
			return _IsConnectingToServiceInProcess;
		}
	}

	public bool IsProductRetrievingInProcess
	{
		get
		{
			return _IsProductRetrievingInProcess;
		}
	}

	public bool IsConnected
	{
		get
		{
			return _IsConnected;
		}
	}

	public bool IsInventoryLoaded
	{
		get
		{
			return _IsInventoryLoaded;
		}
	}

	public static event Action<BillingResult> ActionProductPurchased;

	public static event Action<BillingResult> ActionProductConsumed;

	public static event Action<BillingResult> ActionBillingSetupFinished;

	public static event Action<BillingResult> ActionRetrieveProducsFinished;

	static AndroidInAppPurchaseManager()
	{
		AndroidInAppPurchaseManager.ActionProductPurchased = delegate
		{
		};
		AndroidInAppPurchaseManager.ActionProductConsumed = delegate
		{
		};
		AndroidInAppPurchaseManager.ActionBillingSetupFinished = delegate
		{
		};
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished = delegate
		{
		};
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		_inventory = new AndroidInventory();
	}

	[Obsolete("addProduct is deprectaed, plase use AddProduct instead")]
	public void addProduct(string SKU)
	{
		AddProduct(SKU);
	}

	public void AddProduct(string SKU)
	{
		GoogleProductTemplate googleProductTemplate = new GoogleProductTemplate();
		googleProductTemplate.SKU = SKU;
		GoogleProductTemplate template = googleProductTemplate;
		AddProduct(template);
	}

	public void AddProduct(GoogleProductTemplate template)
	{
		bool flag = false;
		int index = 0;
		foreach (GoogleProductTemplate product in _inventory.Products)
		{
			if (product.SKU.Equals(template.SKU))
			{
				flag = true;
				index = _inventory.Products.IndexOf(product);
				break;
			}
		}
		if (flag)
		{
			_inventory.Products[index] = template;
		}
		else
		{
			_inventory.Products.Add(template);
		}
	}

	[Obsolete("retrieveProducDetails is deprectaed, plase use RetrieveProducDetails instead")]
	public void retrieveProducDetails()
	{
		RetrieveProducDetails();
	}

	public void RetrieveProducDetails()
	{
		_IsProductRetrievingInProcess = true;
		AN_BillingProxy.RetrieveProducDetails();
	}

	[Obsolete("purchase is deprectaed, plase use Purchase instead")]
	public void purchase(string SKU)
	{
		Purchase(SKU);
	}

	[Obsolete("purchase is deprectaed, plase use Purchase instead")]
	public void purchase(string SKU, string DeveloperPayload)
	{
		Purchase(SKU, DeveloperPayload);
	}

	public void Purchase(string SKU)
	{
		Purchase(SKU, string.Empty);
	}

	public void Purchase(string SKU, string DeveloperPayload)
	{
		_processedSKU = SKU;
		AN_SoomlaGrow.PurchaseStarted(SKU);
		AN_BillingProxy.Purchase(SKU, DeveloperPayload);
	}

	[Obsolete("subscribe is deprectaed, plase use Subscribe instead")]
	public void subscribe(string SKU)
	{
		Subscribe(SKU);
	}

	[Obsolete("subscribe is deprectaed, plase use Subscribe instead")]
	public void subscribe(string SKU, string DeveloperPayload)
	{
		Subscribe(SKU, DeveloperPayload);
	}

	public void Subscribe(string SKU)
	{
		Subscribe(SKU, string.Empty);
	}

	public void Subscribe(string SKU, string DeveloperPayload)
	{
		_processedSKU = SKU;
		AN_SoomlaGrow.PurchaseStarted(SKU);
		AN_BillingProxy.Subscribe(SKU, DeveloperPayload);
	}

	[Obsolete("consume is deprectaed, plase use Consume instead")]
	public void consume(string SKU)
	{
		Consume(SKU);
	}

	public void Consume(string SKU)
	{
		_processedSKU = SKU;
		AN_BillingProxy.Consume(SKU);
	}

	[Obsolete("loadStore is deprectaed, plase use LoadStore instead")]
	public void loadStore()
	{
		LoadStore();
	}

	[Obsolete("loadStore is deprectaed, plase use LoadStore instead")]
	public void loadStore(string base64EncodedPublicKey)
	{
		LoadStore(base64EncodedPublicKey);
	}

	public void LoadStore()
	{
		if (AndroidNativeSettings.Instance.IsBase64KeyWasReplaced)
		{
			LoadStore(AndroidNativeSettings.Instance.base64EncodedPublicKey);
			_IsConnectingToServiceInProcess = true;
		}
		else
		{
			Debug.LogError("Replace base64EncodedPublicKey in Androdi Native Setting menu");
		}
	}

	public void LoadStore(string base64EncodedPublicKey)
	{
		foreach (GoogleProductTemplate inAppProduct in AndroidNativeSettings.Instance.InAppProducts)
		{
			AddProduct(inAppProduct.SKU);
		}
		string text = string.Empty;
		int count = AndroidNativeSettings.Instance.InAppProducts.Count;
		for (int i = 0; i < count; i++)
		{
			if (i != 0)
			{
				text += ",";
			}
			text += AndroidNativeSettings.Instance.InAppProducts[i].SKU;
		}
		AN_BillingProxy.Connect(text, base64EncodedPublicKey);
	}

	public void OnPurchaseFinishedCallback(string data)
	{
		Debug.Log(data);
		string[] array = data.Split("|"[0]);
		int num = Convert.ToInt32(array[0]);
		GooglePurchaseTemplate googlePurchaseTemplate = new GooglePurchaseTemplate();
		if (num == 0)
		{
			googlePurchaseTemplate.SKU = array[2];
			googlePurchaseTemplate.packageName = array[3];
			googlePurchaseTemplate.developerPayload = array[4];
			googlePurchaseTemplate.orderId = array[5];
			googlePurchaseTemplate.SetState(array[6]);
			googlePurchaseTemplate.token = array[7];
			googlePurchaseTemplate.signature = array[8];
			googlePurchaseTemplate.time = Convert.ToInt64(array[9]);
			googlePurchaseTemplate.originalJson = array[10];
			if (_inventory != null)
			{
				_inventory.addPurchase(googlePurchaseTemplate);
			}
		}
		else
		{
			googlePurchaseTemplate.SKU = _processedSKU;
		}
		switch (num)
		{
		case 0:
		{
			GoogleProductTemplate productDetails = Inventory.GetProductDetails(googlePurchaseTemplate.SKU);
			if (productDetails != null)
			{
				AN_SoomlaGrow.PurchaseFinished(productDetails.SKU, productDetails.PriceAmountMicros, productDetails.PriceCurrencyCode);
			}
			else
			{
				AN_SoomlaGrow.PurchaseFinished(googlePurchaseTemplate.SKU, 0L, "USD");
			}
			break;
		}
		case -1005:
			AN_SoomlaGrow.PurchaseCanceled(googlePurchaseTemplate.SKU);
			break;
		default:
			AN_SoomlaGrow.PurchaseError();
			break;
		}
		BillingResult obj = new BillingResult(num, array[1], googlePurchaseTemplate);
		AndroidInAppPurchaseManager.ActionProductPurchased(obj);
	}

	public void OnConsumeFinishedCallBack(string data)
	{
		string[] array = data.Split("|"[0]);
		int num = Convert.ToInt32(array[0]);
		GooglePurchaseTemplate googlePurchaseTemplate = null;
		if (num == 0)
		{
			googlePurchaseTemplate = new GooglePurchaseTemplate();
			googlePurchaseTemplate.SKU = array[2];
			googlePurchaseTemplate.packageName = array[3];
			googlePurchaseTemplate.developerPayload = array[4];
			googlePurchaseTemplate.orderId = array[5];
			googlePurchaseTemplate.SetState(array[6]);
			googlePurchaseTemplate.token = array[7];
			googlePurchaseTemplate.signature = array[8];
			googlePurchaseTemplate.time = Convert.ToInt64(array[9]);
			googlePurchaseTemplate.originalJson = array[10];
			if (_inventory != null)
			{
				_inventory.removePurchase(googlePurchaseTemplate);
			}
		}
		BillingResult obj = new BillingResult(num, array[1], googlePurchaseTemplate);
		AndroidInAppPurchaseManager.ActionProductConsumed(obj);
	}

	public void OnBillingSetupFinishedCallback(string data)
	{
		string[] array = data.Split("|"[0]);
		int code = Convert.ToInt32(array[0]);
		_IsConnected = true;
		_IsConnectingToServiceInProcess = false;
		BillingResult billingResult = new BillingResult(code, array[1]);
		AN_SoomlaGrow.SetPurhsesSupportedState(billingResult.isSuccess);
		AndroidInAppPurchaseManager.ActionBillingSetupFinished(billingResult);
	}

	public void OnQueryInventoryFinishedCallBack(string data)
	{
		string[] array = data.Split("|"[0]);
		int code = Convert.ToInt32(array[0]);
		BillingResult obj = new BillingResult(code, array[1]);
		_IsInventoryLoaded = true;
		_IsProductRetrievingInProcess = false;
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished(obj);
	}

	public void OnPurchasesRecive(string data)
	{
		if (data.Equals(string.Empty))
		{
			Debug.Log("InAppPurchaseManager, no purchases avaiable");
			return;
		}
		string[] array = data.Split("|"[0]);
		for (int i = 0; i < array.Length; i += 9)
		{
			GooglePurchaseTemplate googlePurchaseTemplate = new GooglePurchaseTemplate();
			googlePurchaseTemplate.SKU = array[i];
			googlePurchaseTemplate.packageName = array[i + 1];
			googlePurchaseTemplate.developerPayload = array[i + 2];
			googlePurchaseTemplate.orderId = array[i + 3];
			googlePurchaseTemplate.SetState(array[i + 4]);
			googlePurchaseTemplate.token = array[i + 5];
			googlePurchaseTemplate.signature = array[i + 6];
			googlePurchaseTemplate.time = Convert.ToInt64(array[i + 7]);
			googlePurchaseTemplate.originalJson = array[i + 8];
			_inventory.addPurchase(googlePurchaseTemplate);
		}
		Debug.Log("InAppPurchaseManager, total purchases loaded: " + _inventory.Purchases.Count);
	}

	public void OnProducttDetailsRecive(string data)
	{
		if (data.Equals(string.Empty))
		{
			Debug.Log("InAppPurchaseManager, no products avaiable");
			return;
		}
		string[] array = data.Split("|"[0]);
		for (int i = 0; i < array.Length; i += 7)
		{
			GoogleProductTemplate googleProductTemplate = _inventory.GetProductDetails(array[i]);
			if (googleProductTemplate == null)
			{
				googleProductTemplate = new GoogleProductTemplate();
				googleProductTemplate.SKU = array[i];
				_inventory.Products.Add(googleProductTemplate);
			}
			googleProductTemplate.LocalizedPrice = array[i + 1];
			googleProductTemplate.Title = array[i + 2];
			googleProductTemplate.Description = array[i + 3];
			googleProductTemplate.PriceAmountMicros = Convert.ToInt64(array[i + 4]);
			googleProductTemplate.PriceCurrencyCode = array[i + 5];
			googleProductTemplate.OriginalJson = array[i + 6];
			Debug.Log("Prodcut originalJson: " + googleProductTemplate.OriginalJson);
		}
		Debug.Log("InAppPurchaseManager, total products loaded: " + _inventory.Products.Count);
	}
}
