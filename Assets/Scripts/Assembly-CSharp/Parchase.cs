//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.Purchasing;
//using UnityEngine.Purchasing.Security;

//public class Parchase : MonoBehaviour, IStoreListener
//{
//	public static Parchase me;

//	private static IStoreController storeController;

//	private static IExtensionProvider extensionProvider;

//	private string[] productIds = new string[25]
//	{
//		string.Empty,
//		string.Empty,
//		string.Empty,
//		string.Empty,
//		string.Empty,
//		string.Empty,
//		string.Empty,
//		string.Empty,
//		string.Empty,
//		string.Empty,
//		string.Empty,
//		"mk_product_1",
//		"mk_product_2",
//		"mk_product_3",
//		"mk_product_4",
//		"mk_product_5",
//		"mk_product_6",
//		"mk_product_7",
//		"mk_product_8",
//		"mk_product_9",
//		"mk_product_10",
//		"NO",
//		"NO",
//		"mk_product_11",
//		"mk_product_12"
//	};

//	public bool Wating;

//	public float Waiting_time;

//	public bool Super;

//	public int Just_Get_Item;

//	public GameObject Waiting_Panel;

//	public GameObject Fail_Panel;

//	private int Need_Crystal;

//	public Shop_Item_BTN Target_Item_BTN;

//	public void Awake()
//	{
//		if (me == null)
//		{
//			me = this;
//		}
//	}

//	private void Start()
//	{
//		InitializePurchasing();
//	}

//	private bool IsInitialized()
//	{
//		return storeController != null && extensionProvider != null;
//	}

//	public void InitializePurchasing()
//	{
//		if (IsInitialized())
//		{
//			return;
//		}
//		StandardPurchasingModule first = StandardPurchasingModule.Instance();
//		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(first);
//		for (int i = 11; i < productIds.Length; i++)
//		{
//			if (!productIds[i].Equals("NO"))
//			{
//				configurationBuilder.AddProduct(productIds[i], ProductType.Consumable, new IDs
//				{
//					{
//						productIds[i],
//						"AppleAppStore"
//					},
//					{
//						productIds[i],
//						"GooglePlay"
//					}
//				});
//			}
//		}
//		UnityPurchasing.Initialize(this, configurationBuilder);
//	}

//	public void BuyProductID(int product_Number)
//	{
//		try
//		{
//			if (IsInitialized())
//			{
//				Debug.Log(product_Number);
//				Product product = storeController.products.WithID(productIds[product_Number]);
//				if (product != null && product.availableToPurchase)
//				{
//					Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
//					storeController.InitiatePurchase(product);
//				}
//				else
//				{
//					Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//					UI_Master.me.Warning(Localization.Get("IMPOSSILBE_BUY"));
//					Waiting_Panel.SetActive(false);
//				}
//			}
//			else
//			{
//				Debug.Log("BuyProductID FAIL. Not initialized.");
//				UI_Master.me.Warning(Localization.Get("IMPOSSILBE_BUY"));
//				Waiting_Panel.SetActive(false);
//			}
//		}
//		catch (Exception ex)
//		{
//			Debug.Log("BuyProductID: FAIL. Exception during purchase. " + ex);
//			UI_Master.me.Warning(Localization.Get("IMPOSSILBE_BUY"));
//			Waiting_Panel.SetActive(false);
//		}
//	}

//	public void RestorePurchase()
//	{
//		if (!IsInitialized())
//		{
//			Debug.Log("RestorePurchases FAIL. Not initialized.");
//		}
//		else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
//		{
//			Debug.Log("RestorePurchases started ...");
//			IAppleExtensions extension = extensionProvider.GetExtension<IAppleExtensions>();
//			extension.RestoreTransactions(delegate(bool result)
//			{
//				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//			});
//		}
//		else
//		{
//			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
//		}
//	}

//	public void OnInitialized(IStoreController sc, IExtensionProvider ep)
//	{
//		Debug.Log("OnInitialized : PASS");
//		storeController = sc;
//		extensionProvider = ep;
//	}

//	public void OnInitializeFailed(InitializationFailureReason reason)
//	{
//		Debug.Log("OnInitializeFailed InitializationFailureReason:" + reason);
//	}

//	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//	{
//		Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//		bool flag = true;
//		CrossPlatformValidator crossPlatformValidator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.bundleIdentifier);
//		try
//		{
//			IPurchaseReceipt[] array = crossPlatformValidator.Validate(args.purchasedProduct.receipt);
//			Debug.Log("Receipt is valid. Contents:");
//			IPurchaseReceipt[] array2 = array;
//			foreach (IPurchaseReceipt purchaseReceipt in array2)
//			{
//				Debug.Log(purchaseReceipt.productID);
//				Debug.Log(purchaseReceipt.purchaseDate);
//				Debug.Log(purchaseReceipt.transactionID);
//			}
//		}
//		catch (IAPSecurityException)
//		{
//			Debug.Log("Invalid receipt, not unlocking content");
//			flag = false;
//		}
//		if (flag)
//		{
//			Debug.Log(productIds[Just_Get_Item] + "==" + args.purchasedProduct.definition.id);
//			IAP_SUCCESS();
//		}
//		return PurchaseProcessingResult.Complete;
//	}

//	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//	{
//		Fail();
//		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//	}

//	public void BUY_ITEM(int ID)
//	{
//		StartCoroutine(Fail_check());
//		Just_Get_Item = ID;
//		Wating = true;
//		Waiting_Panel.SetActive(true);
//		Waiting_time = 0f;
//		if (Super)
//		{
//			IAP_SUCCESS();
//		}
//		else
//		{
//			me.BuyProductID(Just_Get_Item);
//		}
//	}

//	public IEnumerator Fail_check()
//	{
//		yield return new WaitForSeconds(15f);
//		if (Wating)
//		{
//			Fail();
//		}
//	}

//	public void IAP_SUCCESS()
//	{
//		Debug.Log(Just_Get_Item + "번 결제성공.");
//		Wating = false;
//		Waiting_Panel.SetActive(false);
//		StopCoroutine(Fail_check());
//		Now_Data.me.IAIP_Items.Add(Just_Get_Item);
//		Security.SetString("IAIP", string.Format("{0}/{1}", Security.GetString("IAIP", string.Empty), Just_Get_Item));
//		Security.SetInt("IAIP_COUNT", Now_Data.me.IAIP_Items.Count);
//		GETTING(Just_Get_Item);
//	}

//	public void Fail()
//	{
//		Waiting_Panel.SetActive(false);
//		UI_Master.me.Popup(Fail_Panel);
//	}

//	public void GETTING(int ID)
//	{
//		Just_Get_Item = ID;
//		Target_Item_BTN = UI_Master.me.shop_Panel.Shop_Item_BTNs[ID];
//		Target_Item_BTN.REAL_GET();
//		Waiting_Panel.SetActive(false);
//	}
//}
