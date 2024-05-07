using System;
using UnityEngine;

public class ISN_SoomlaGrow : SA_Singleton<ISN_SoomlaGrow>
{
	private static bool _IsInitialized = false;

	public static bool IsInitialized
	{
		get
		{
			return _IsInitialized;
		}
	}

	public static event Action ActionInitialized;

	public static event Action ActionConnected;

	public static event Action ActionDisconnected;

	static ISN_SoomlaGrow()
	{
		ISN_SoomlaGrow.ActionInitialized = delegate
		{
		};
		ISN_SoomlaGrow.ActionConnected = delegate
		{
		};
		ISN_SoomlaGrow.ActionDisconnected = delegate
		{
		};
	}

	public void CreateObject()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public static void Init()
	{
	}

	private static void HandleOnVerificationComplete(IOSStoreKitVerificationResponse res)
	{
		if (res.status != 0)
		{
			VerificationFailed();
		}
	}

	private static void HandleOnRestoreComplete(IOSStoreKitRestoreResult res)
	{
		RestoreFinished(res.IsSucceeded);
	}

	private static void HandleOnRestoreStarted()
	{
		RestoreStarted();
	}

	private static void HandleOnTransactionStarted(string prodcutId)
	{
		PurchaseStarted(prodcutId);
	}

	private static void HandleOnTransactionComplete(IOSStoreKitResult res)
	{
		switch (res.State)
		{
		case InAppPurchaseState.Purchased:
		{
			IOSProductTemplate productById = ISN_Singleton<IOSInAppPurchaseManager>.Instance.GetProductById(res.ProductIdentifier);
			if (productById != null)
			{
				PurchaseFinished(productById.Id, productById.PriceInMicros.ToString(), productById.CurrencyCode);
			}
			break;
		}
		case InAppPurchaseState.Failed:
			if (res.Error.Code == 2)
			{
				PurchaseCanceled(res.ProductIdentifier);
			}
			else
			{
				PurchaseError();
			}
			break;
		}
	}

	public static void SocialAction(ISN_SoomlaEvent soomlaEvent, ISN_SoomlaAction action, ISN_SoomlaProvider provider)
	{
	}

	private static void PurchaseStarted(string prodcutId)
	{
	}

	private static void PurchaseFinished(string prodcutId, string priceInMicros, string currency)
	{
	}

	private static void PurchaseCanceled(string prodcutId)
	{
	}

	public static void SetPurchasesSupportedState(bool isSupported)
	{
	}

	private static void PurchaseError()
	{
	}

	private static void VerificationFailed()
	{
	}

	private static void RestoreStarted()
	{
	}

	private static void RestoreFinished(bool state)
	{
	}

	private void OnHighWayInitialized()
	{
		ISN_SoomlaGrow.ActionInitialized();
	}

	private void OnHihgWayConnected()
	{
		ISN_SoomlaGrow.ActionConnected();
	}

	private void OnHihgWayDisconnected()
	{
		ISN_SoomlaGrow.ActionDisconnected();
	}

	private static void HandleOnInstagramPostResult(ISN_Result res)
	{
		if (res.IsSucceeded)
		{
			SocialAction(ISN_SoomlaEvent.FINISHED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.INSTAGRAM);
		}
		else
		{
			SocialAction(ISN_SoomlaEvent.FAILED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.INSTAGRAM);
		}
	}

	private static void HandleOnTwitterPostResult(ISN_Result res)
	{
		if (res.IsSucceeded)
		{
			SocialAction(ISN_SoomlaEvent.FINISHED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.TWITTER);
		}
		else
		{
			SocialAction(ISN_SoomlaEvent.FAILED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.TWITTER);
		}
	}

	private static void HandleOnInstagramPostStart()
	{
		SocialAction(ISN_SoomlaEvent.STARTED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.INSTAGRAM);
	}

	private static void HandleOnTwitterPostStart()
	{
		SocialAction(ISN_SoomlaEvent.STARTED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.TWITTER);
	}

	private static void HandleOnFacebookPostStart()
	{
		SocialAction(ISN_SoomlaEvent.STARTED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.FACEBOOK);
	}

	private static void HandleOnFacebookPostResult(ISN_Result res)
	{
		if (res.IsSucceeded)
		{
			SocialAction(ISN_SoomlaEvent.FINISHED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.FACEBOOK);
		}
		else
		{
			SocialAction(ISN_SoomlaEvent.CANCELLED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.FACEBOOK);
		}
	}
}
