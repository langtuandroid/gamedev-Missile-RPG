using System;
using UnityEngine;

public class AN_SoomlaGrow : SA_Singleton<AN_SoomlaGrow>
{
	private static bool _IsInitialized = false;

	public static event Action ActionInitialized;

	public static event Action ActionConnected;

	public static event Action ActionDisconnected;

	static AN_SoomlaGrow()
	{
		AN_SoomlaGrow.ActionInitialized = delegate
		{
		};
		AN_SoomlaGrow.ActionConnected = delegate
		{
		};
		AN_SoomlaGrow.ActionDisconnected = delegate
		{
		};
	}

	public void CreateListner()
	{
	}

	public static void Init()
	{
		if (!_IsInitialized && AndroidNativeSettings.Instance.EnableSoomla)
		{
			SA_Singleton<AN_SoomlaGrow>.Instance.CreateListner();
			AN_SoomlaProxy.Initalize(AndroidNativeSettings.Instance.SoomlaGameKey, AndroidNativeSettings.Instance.SoomlaEnvKey);
			_IsInitialized = true;
		}
	}

	public static void PurchaseStarted(string prodcutId)
	{
		if (CheckState())
		{
			AN_SoomlaProxy.OnMarketPurchaseStarted(prodcutId);
		}
	}

	public static void PurchaseFinished(string prodcutId, long priceInMicros, string currency)
	{
		if (CheckState())
		{
			AN_SoomlaProxy.OnMarketPurchaseFinished(prodcutId, priceInMicros, currency);
		}
	}

	public static void PurchaseCanceled(string prodcutId)
	{
		if (CheckState())
		{
			AN_SoomlaProxy.OnMarketPurchaseCancelled(prodcutId);
		}
	}

	public static void SetPurhsesSupportedState(bool isSupported)
	{
		if (CheckState())
		{
			AN_SoomlaProxy.SetBillingState(isSupported);
		}
	}

	public static void PurchaseError()
	{
		if (CheckState())
		{
			AN_SoomlaProxy.OnMarketPurchaseFailed();
		}
	}

	private static void FriendsRequest(AN_SoomlaEventType eventType, AN_SoomlaSocialProvider provider)
	{
		if (CheckState())
		{
			AN_SoomlaProxy.OnFriendsRequest((int)eventType, (int)provider);
		}
	}

	public static void SocialLogin(AN_SoomlaEventType eventType, AN_SoomlaSocialProvider provider)
	{
		if (CheckState())
		{
			AN_SoomlaProxy.OnSocialLogin((int)eventType, (int)provider);
		}
	}

	public static void SocialLoginFinished(AN_SoomlaSocialProvider provider, string ProfileId)
	{
		if (CheckState())
		{
			AN_SoomlaProxy.OnSocialLoginFinished((int)provider, ProfileId);
		}
	}

	public static void SocialLogOut(AN_SoomlaEventType eventType, AN_SoomlaSocialProvider provider)
	{
		if (CheckState())
		{
			AN_SoomlaProxy.OnSocialLogout((int)eventType, (int)provider);
		}
	}

	public static void SocialShare(AN_SoomlaEventType eventType, AN_SoomlaSocialProvider provider)
	{
		if (CheckState())
		{
			AN_SoomlaProxy.OnSocialShare((int)eventType, (int)provider);
		}
	}

	private static bool CheckState()
	{
		if (AndroidNativeSettings.Instance.EnableSoomla)
		{
			Init();
		}
		return AndroidNativeSettings.Instance.EnableSoomla;
	}

	private void OnInitialized()
	{
		Debug.Log("AN_SOOMAL OnInitialized");
		AN_SoomlaGrow.ActionInitialized();
	}

	private void OnConnected()
	{
		AN_SoomlaGrow.ActionConnected();
	}

	private void OnDisconnected()
	{
		AN_SoomlaGrow.ActionDisconnected();
	}
}
