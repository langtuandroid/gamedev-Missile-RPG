using System;
using System.Collections.Generic;
using UnityEngine;

public class GoogleMobileAd
{
	public static GoogleMobileAdInterface controller;

	private static bool _IsInited = false;

	private static bool _IsInterstitialReady = false;

	public static bool IsInited
	{
		get
		{
			return _IsInited;
		}
	}

	public static string BannersUunitId
	{
		get
		{
			return controller.BannersUunitId;
		}
	}

	public static string InterstisialUnitId
	{
		get
		{
			return controller.InterstisialUnitId;
		}
	}

	public static bool IsInterstitialReady
	{
		get
		{
			return _IsInterstitialReady;
		}
	}

	public static event Action OnInterstitialLoaded;

	public static event Action OnInterstitialFailedLoading;

	public static event Action OnInterstitialOpened;

	public static event Action OnInterstitialClosed;

	public static event Action OnInterstitialLeftApplication;

	public static event Action<string> OnAdInAppRequest;

	static GoogleMobileAd()
	{
		GoogleMobileAd.OnInterstitialLoaded = delegate
		{
		};
		GoogleMobileAd.OnInterstitialFailedLoading = delegate
		{
		};
		GoogleMobileAd.OnInterstitialOpened = delegate
		{
		};
		GoogleMobileAd.OnInterstitialClosed = delegate
		{
		};
		GoogleMobileAd.OnInterstitialLeftApplication = delegate
		{
		};
		GoogleMobileAd.OnAdInAppRequest = delegate
		{
		};
	}

	public static void Init()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			controller = SA_Singleton<IOSAdMobController>.instance;
			controller.Init(GoogleMobileAdSettings.Instance.IOS_BannersUnitId);
			if (!GoogleMobileAdSettings.Instance.IOS_InterstisialsUnitId.Equals(string.Empty))
			{
				controller.SetInterstisialsUnitID(GoogleMobileAdSettings.Instance.IOS_InterstisialsUnitId);
			}
			break;
		case RuntimePlatform.Android:
			controller = SA_Singleton<AndroidAdMobController>.instance;
			controller.Init(GoogleMobileAdSettings.Instance.Android_BannersUnitId);
			if (!GoogleMobileAdSettings.Instance.Android_InterstisialsUnitId.Equals(string.Empty))
			{
				controller.SetInterstisialsUnitID(GoogleMobileAdSettings.Instance.Android_InterstisialsUnitId);
			}
			break;
		}
		controller.OnInterstitialLoaded = OnInterstitialLoadedListner;
		controller.OnInterstitialFailedLoading = OnInterstitialFailedLoadingListner;
		controller.OnInterstitialOpened = OnInterstitialOpenedListner;
		controller.OnInterstitialClosed = OnInterstitialClosedListner;
		controller.OnInterstitialLeftApplication = OnInterstitialLeftApplicationListner;
		controller.OnAdInAppRequest = OnAdInAppRequestListner;
		_IsInited = true;
		if (GoogleMobileAdSettings.Instance.testDevices.Count > 0)
		{
			List<string> list = new List<string>();
			foreach (GADTestDevice testDevice in GoogleMobileAdSettings.Instance.testDevices)
			{
				list.Add(testDevice.ID);
			}
			AddTestDevices(list.ToArray());
		}
		TagForChildDirectedTreatment(GoogleMobileAdSettings.Instance.TagForChildDirectedTreatment);
		foreach (string defaultKeyword in GoogleMobileAdSettings.Instance.DefaultKeywords)
		{
			AddKeyword(defaultKeyword);
		}
	}

	public static void SetBannersUnitID(string android_unit_id, string ios_unit_id, string wp8_unit_id)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("ChangeBannersUnitID shoudl be called only after Init function. Call ignored");
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			controller.SetBannersUnitID(ios_unit_id);
			break;
		case RuntimePlatform.Android:
			controller.SetBannersUnitID(android_unit_id);
			break;
		default:
			controller.SetBannersUnitID(wp8_unit_id);
			break;
		}
	}

	public static void SetInterstisialsUnitID(string android_unit_id, string ios_unit_id, string wp8_unit_id)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("ChangeInterstisialsUnitID shoudl be called only after Init function. Call ignored");
			return;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			controller.SetInterstisialsUnitID(ios_unit_id);
			break;
		case RuntimePlatform.Android:
			controller.SetInterstisialsUnitID(android_unit_id);
			break;
		default:
			controller.SetInterstisialsUnitID(wp8_unit_id);
			break;
		}
	}

	public static GoogleMobileAdBanner CreateAdBanner(TextAnchor anchor, GADBannerSize size)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		return controller.CreateAdBanner(anchor, size);
	}

	public static GoogleMobileAdBanner CreateAdBanner(int x, int y, GADBannerSize size)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		return controller.CreateAdBanner(x, y, size);
	}

	public static GoogleMobileAdBanner GetBanner(int id)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("GetBanner shoudl be called only after Init function. Call ignored");
			return null;
		}
		return controller.GetBanner(id);
	}

	public static void DestroyBanner(int id)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("DestroyCurrentBanner shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.DestroyBanner(id);
		}
	}

	public static void AddKeyword(string keyword)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("AddKeyword shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.AddKeyword(keyword);
		}
	}

	public static void SetBirthday(int year, AndroidMonth month, int day)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("SetBirthday shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.SetBirthday(year, month, day);
		}
	}

	public static void TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("TagForChildDirectedTreatment shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.TagForChildDirectedTreatment(tagForChildDirectedTreatment);
		}
	}

	public static void AddTestDevice(string deviceId)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.AddTestDevice(deviceId);
		}
	}

	public static void AddTestDevices(params string[] ids)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.AddTestDevices(ids);
		}
	}

	public static void SetGender(GoogleGender gender)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("SetGender shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.SetGender(gender);
		}
	}

	public static void StartInterstitialAd()
	{
		if (!_IsInited)
		{
			Debug.LogWarning("StartInterstitialAd shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.StartInterstitialAd();
		}
	}

	public static void LoadInterstitialAd()
	{
		if (!_IsInited)
		{
			Debug.LogWarning("LoadInterstitialAd shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.LoadInterstitialAd();
		}
	}

	public static void ShowInterstitialAd()
	{
		if (_IsInterstitialReady)
		{
			_IsInterstitialReady = false;
			if (!_IsInited)
			{
				Debug.LogWarning("ShowInterstitialAd shoudl be called only after Init function. Call ignored");
			}
			else
			{
				controller.ShowInterstitialAd();
			}
		}
		else
		{
			Debug.LogWarning("ShowInterstitialAd shoudl be called only what  Interstitial Ad is Ready ");
		}
	}

	public static void RecordInAppResolution(GADInAppResolution resolution)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("RecordInAppResolution shoudl be called only after Init function. Call ignored");
		}
		else
		{
			controller.RecordInAppResolution(resolution);
		}
	}

	private static void OnInterstitialLoadedListner()
	{
		_IsInterstitialReady = true;
		GoogleMobileAd.OnInterstitialLoaded();
	}

	private static void OnInterstitialFailedLoadingListner()
	{
		_IsInterstitialReady = false;
		GoogleMobileAd.OnInterstitialFailedLoading();
	}

	private static void OnInterstitialOpenedListner()
	{
		_IsInterstitialReady = false;
		GoogleMobileAd.OnInterstitialOpened();
	}

	private static void OnInterstitialClosedListner()
	{
		_IsInterstitialReady = false;
		GoogleMobileAd.OnInterstitialClosed();
	}

	private static void OnInterstitialLeftApplicationListner()
	{
		GoogleMobileAd.OnInterstitialLeftApplication();
	}

	private static void OnAdInAppRequestListner(string productId)
	{
		GoogleMobileAd.OnAdInAppRequest(productId);
	}
}
