using System;
using UnityEngine;

public static class UM_AdManager
{
	private static bool _IsInited = false;

	public static bool IsInited
	{
		get
		{
			return _IsInited;
		}
	}

	public static event Action OnInterstitialLoaded;

	public static event Action OnInterstitialLoadFail;

	public static event Action OnInterstitialClosed;

	static UM_AdManager()
	{
		UM_AdManager.OnInterstitialLoaded = delegate
		{
		};
		UM_AdManager.OnInterstitialLoadFail = delegate
		{
		};
		UM_AdManager.OnInterstitialClosed = delegate
		{
		};
	}

	public static void Init()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				GoogleMobileAd.Init();
				GoogleMobileAdInterstitialSubscribe();
			}
			else
			{
				iAdBannerController.InterstitialAdDidLoadAction += InterstitialLoadedHandler;
				iAdBannerController.InterstitialDidFailWithErrorAction += InterstitialLoadFailHandler;
				iAdBannerController.InterstitialAdDidFinishAction += InterstitialClosedHandler;
			}
			break;
		case RuntimePlatform.Android:
			GoogleMobileAd.Init();
			GoogleMobileAdInterstitialSubscribe();
			break;
		case RuntimePlatform.WP8Player:
			GoogleMobileAd.Init();
			GoogleMobileAdInterstitialSubscribe();
			break;
		}
		_IsInited = true;
	}

	public static void ResetActions()
	{
		UM_AdManager.OnInterstitialLoaded = delegate
		{
		};
		UM_AdManager.OnInterstitialLoadFail = delegate
		{
		};
		UM_AdManager.OnInterstitialClosed = delegate
		{
		};
	}

	public static int CreateAdBanner(TextAnchor anchor)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return 0;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				return GoogleMobileAd.CreateAdBanner(anchor, GADBannerSize.BANNER).id;
			}
			return ISN_Singleton<iAdBannerController>.instance.CreateAdBanner(anchor).id;
		case RuntimePlatform.Android:
			return GoogleMobileAd.CreateAdBanner(anchor, GADBannerSize.BANNER).id;
		case RuntimePlatform.WP8Player:
			return GoogleMobileAd.CreateAdBanner(anchor, GADBannerSize.BANNER).id;
		default:
			return 0;
		}
	}

	public static bool IsBannerLoaded(int id)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("IsBannerLoaded shoudl be called only after Init function. Call ignored");
			return false;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				return GoogleMobileAd.GetBanner(id).IsLoaded;
			}
			return ISN_Singleton<iAdBannerController>.instance.GetBanner(id).IsLoaded;
		case RuntimePlatform.Android:
			return GoogleMobileAd.GetBanner(id).IsLoaded;
		case RuntimePlatform.WP8Player:
			return GoogleMobileAd.GetBanner(id).IsLoaded;
		default:
			return false;
		}
	}

	public static bool IsBannerOnScreen(int id)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("IsBannerOnScreen shoudl be called only after Init function. Call ignored");
			return false;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				return GoogleMobileAd.GetBanner(id).IsOnScreen;
			}
			return ISN_Singleton<iAdBannerController>.instance.GetBanner(id).IsOnScreen;
		case RuntimePlatform.Android:
			return GoogleMobileAd.GetBanner(id).IsOnScreen;
		case RuntimePlatform.WP8Player:
			return GoogleMobileAd.GetBanner(id).IsOnScreen;
		default:
			return false;
		}
	}

	public static void DestroyBanner(int id)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("DestroyCurrentBanner shoudl be called only after Init function. Call ignored");
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				GoogleMobileAd.DestroyBanner(id);
			}
			else
			{
				ISN_Singleton<iAdBannerController>.instance.DestroyBanner(id);
			}
			break;
		case RuntimePlatform.Android:
			GoogleMobileAd.DestroyBanner(id);
			break;
		case RuntimePlatform.WP8Player:
			GoogleMobileAd.DestroyBanner(id);
			break;
		}
	}

	public static void HideBanner(int id)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("DestroyCurrentBanner shoudl be called only after Init function. Call ignored");
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				GoogleMobileAd.GetBanner(id).Hide();
			}
			else
			{
				ISN_Singleton<iAdBannerController>.instance.GetBanner(id).Hide();
			}
			break;
		case RuntimePlatform.Android:
			GoogleMobileAd.GetBanner(id).Hide();
			break;
		case RuntimePlatform.WP8Player:
			GoogleMobileAd.GetBanner(id).Hide();
			break;
		}
	}

	public static void ShowBanner(int id)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("DestroyCurrentBanner shoudl be called only after Init function. Call ignored");
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				GoogleMobileAd.GetBanner(id).Show();
			}
			else
			{
				ISN_Singleton<iAdBannerController>.instance.GetBanner(id).Show();
			}
			break;
		case RuntimePlatform.Android:
			GoogleMobileAd.GetBanner(id).Show();
			break;
		case RuntimePlatform.WP8Player:
			GoogleMobileAd.GetBanner(id).Show();
			break;
		}
	}

	public static void RefreshBanner(int id)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("DestroyCurrentBanner shoudl be called only after Init function. Call ignored");
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				GoogleMobileAd.GetBanner(id).Refresh();
			}
			break;
		case RuntimePlatform.Android:
			GoogleMobileAd.GetBanner(id).Refresh();
			break;
		case RuntimePlatform.WP8Player:
			GoogleMobileAd.GetBanner(id).Refresh();
			break;
		}
	}

	public static void StartInterstitialAd()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				GoogleMobileAd.StartInterstitialAd();
			}
			else
			{
				ISN_Singleton<iAdBannerController>.instance.StartInterstitialAd();
			}
			break;
		case RuntimePlatform.Android:
			GoogleMobileAd.StartInterstitialAd();
			break;
		case RuntimePlatform.WP8Player:
			GoogleMobileAd.StartInterstitialAd();
			break;
		}
	}

	public static void LoadInterstitialAd()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				GoogleMobileAd.LoadInterstitialAd();
			}
			else
			{
				ISN_Singleton<iAdBannerController>.instance.LoadInterstitialAd();
			}
			break;
		case RuntimePlatform.Android:
			GoogleMobileAd.LoadInterstitialAd();
			break;
		case RuntimePlatform.WP8Player:
			GoogleMobileAd.LoadInterstitialAd();
			break;
		}
	}

	public static void ShowInterstitialAd()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			if (UltimateMobileSettings.Instance.IOSAdEdngine == UM_IOSAdEngineOprions.GoogleMobileAd)
			{
				GoogleMobileAd.ShowInterstitialAd();
			}
			else
			{
				ISN_Singleton<iAdBannerController>.instance.ShowInterstitialAd();
			}
			break;
		case RuntimePlatform.Android:
			GoogleMobileAd.ShowInterstitialAd();
			break;
		case RuntimePlatform.WP8Player:
			GoogleMobileAd.ShowInterstitialAd();
			break;
		}
	}

	private static void GoogleMobileAdInterstitialSubscribe()
	{
		GoogleMobileAd.OnInterstitialLoaded += InterstitialLoadedHandler;
		GoogleMobileAd.OnInterstitialFailedLoading += InterstitialLoadFailHandler;
		GoogleMobileAd.OnInterstitialClosed += InterstitialClosedHandler;
	}

	private static void InterstitialLoadedHandler()
	{
		UM_AdManager.OnInterstitialLoaded();
	}

	private static void InterstitialLoadFailHandler()
	{
		UM_AdManager.OnInterstitialLoadFail();
	}

	private static void InterstitialClosedHandler()
	{
		UM_AdManager.OnInterstitialClosed();
	}
}
