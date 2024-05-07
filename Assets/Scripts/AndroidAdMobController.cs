using System;
using System.Collections.Generic;
using UnityEngine;

public class AndroidAdMobController : SA_Singleton<AndroidAdMobController>, GoogleMobileAdInterface
{
	private const string DEVICES_SEPARATOR = ",";

	private bool _IsInited;

	private Dictionary<int, AndroidADBanner> _banners;

	private string _BannersUunitId;

	private string _InterstisialUnitId;

	private Action _OnInterstitialLoaded = delegate
	{
	};

	private Action _OnInterstitialFailedLoading = delegate
	{
	};

	private Action _OnInterstitialOpened = delegate
	{
	};

	private Action _OnInterstitialClosed = delegate
	{
	};

	private Action _OnInterstitialLeftApplication = delegate
	{
	};

	private Action<string> _OnAdInAppRequest = delegate
	{
	};

	public List<GoogleMobileAdBanner> banners
	{
		get
		{
			List<GoogleMobileAdBanner> list = new List<GoogleMobileAdBanner>();
			if (_banners == null)
			{
				return list;
			}
			foreach (KeyValuePair<int, AndroidADBanner> banner in _banners)
			{
				list.Add(banner.Value);
			}
			return list;
		}
	}

	public bool IsInited
	{
		get
		{
			return _IsInited;
		}
	}

	public string BannersUunitId
	{
		get
		{
			return _BannersUunitId;
		}
	}

	public string InterstisialUnitId
	{
		get
		{
			return _InterstisialUnitId;
		}
	}

	public Action OnInterstitialLoaded
	{
		get
		{
			return _OnInterstitialLoaded;
		}
		set
		{
			_OnInterstitialLoaded = value;
		}
	}

	public Action OnInterstitialFailedLoading
	{
		get
		{
			return _OnInterstitialFailedLoading;
		}
		set
		{
			_OnInterstitialFailedLoading = value;
		}
	}

	public Action OnInterstitialOpened
	{
		get
		{
			return _OnInterstitialOpened;
		}
		set
		{
			_OnInterstitialOpened = value;
		}
	}

	public Action OnInterstitialClosed
	{
		get
		{
			return _OnInterstitialClosed;
		}
		set
		{
			_OnInterstitialClosed = value;
		}
	}

	public Action OnInterstitialLeftApplication
	{
		get
		{
			return _OnInterstitialLeftApplication;
		}
		set
		{
			_OnInterstitialLeftApplication = value;
		}
	}

	public Action<string> OnAdInAppRequest
	{
		get
		{
			return _OnAdInAppRequest;
		}
		set
		{
			_OnAdInAppRequest = value;
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			foreach (KeyValuePair<int, AndroidADBanner> banner in _banners)
			{
				banner.Value.Pause();
			}
			return;
		}
		foreach (KeyValuePair<int, AndroidADBanner> banner2 in _banners)
		{
			banner2.Value.Resume();
		}
	}

	public void Init(string ad_unit_id)
	{
		if (_IsInited)
		{
			Debug.LogWarning("Init shoudl be called only once. Call ignored");
			return;
		}
		_IsInited = true;
		_BannersUunitId = ad_unit_id;
		_InterstisialUnitId = ad_unit_id;
		_banners = new Dictionary<int, AndroidADBanner>();
		AN_GoogleAdProxy.InitMobileAd(ad_unit_id);
	}

	public void Init(string banners_unit_id, string interstisial_unit_id)
	{
		if (_IsInited)
		{
			Debug.LogWarning("Init shoudl be called only once. Call ignored");
			return;
		}
		Init(banners_unit_id);
		SetInterstisialsUnitID(interstisial_unit_id);
	}

	public void SetBannersUnitID(string ad_unit_id)
	{
		_BannersUunitId = ad_unit_id;
		AN_GoogleAdProxy.ChangeBannersUnitID(ad_unit_id);
	}

	public void SetInterstisialsUnitID(string ad_unit_id)
	{
		_InterstisialUnitId = ad_unit_id;
		AN_GoogleAdProxy.ChangeInterstisialsUnitID(ad_unit_id);
	}

	public void AddKeyword(string keyword)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("AddKeyword shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.AddKeyword(keyword);
		}
	}

	public void SetBirthday(int year, AndroidMonth month, int day)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("SetBirthday shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.SetBirthday(year, (int)month, day);
		}
	}

	public void TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("TagForChildDirectedTreatment shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.TagForChildDirectedTreatment(tagForChildDirectedTreatment);
		}
	}

	public void AddTestDevice(string deviceId)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.AddTestDevice(deviceId);
		}
	}

	public void AddTestDevices(params string[] ids)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
		else if (ids.Length != 0)
		{
			AN_GoogleAdProxy.AddTestDevices(string.Join(",", ids));
		}
	}

	public void SetGender(GoogleGender gender)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("SetGender shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.SetGender((int)gender);
		}
	}

	public GoogleMobileAdBanner CreateAdBanner(TextAnchor anchor, GADBannerSize size)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		AndroidADBanner androidADBanner = new AndroidADBanner(anchor, size, GADBannerIdFactory.nextId);
		_banners.Add(androidADBanner.id, androidADBanner);
		return androidADBanner;
	}

	public GoogleMobileAdBanner CreateAdBanner(int x, int y, GADBannerSize size)
	{
		if (!_IsInited)
		{
			Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		AndroidADBanner androidADBanner = new AndroidADBanner(x, y, size, GADBannerIdFactory.nextId);
		_banners.Add(androidADBanner.id, androidADBanner);
		return androidADBanner;
	}

	public void DestroyBanner(int id)
	{
		if (_banners != null && _banners.ContainsKey(id))
		{
			AndroidADBanner androidADBanner = _banners[id];
			if (androidADBanner.IsLoaded)
			{
				_banners.Remove(id);
				AN_GoogleAdProxy.DestroyBanner(id);
			}
			else
			{
				androidADBanner.DestroyAfterLoad();
			}
		}
	}

	public void StartInterstitialAd()
	{
		if (!_IsInited)
		{
			Debug.LogWarning("StartInterstitialAd shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.StartInterstitialAd();
		}
	}

	public void LoadInterstitialAd()
	{
		if (!_IsInited)
		{
			Debug.LogWarning("LoadInterstitialAd shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.LoadInterstitialAd();
		}
	}

	public void ShowInterstitialAd()
	{
		if (!_IsInited)
		{
			Debug.LogWarning("ShowInterstitialAd shoudl be called only after Init function. Call ignored");
		}
		else
		{
			AN_GoogleAdProxy.ShowInterstitialAd();
		}
	}

	public void RecordInAppResolution(GADInAppResolution resolution)
	{
		AN_GoogleAdProxy.RecordInAppResolution((int)resolution);
	}

	public GoogleMobileAdBanner GetBanner(int id)
	{
		if (_banners.ContainsKey(id))
		{
			return _banners[id];
		}
		Debug.LogWarning("Banner id: " + id + " not found");
		return null;
	}

	private void OnBannerAdLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		int id = Convert.ToInt32(array[0]);
		int w = Convert.ToInt32(array[1]);
		int h = Convert.ToInt32(array[2]);
		AndroidADBanner androidADBanner = GetBanner(id) as AndroidADBanner;
		if (androidADBanner != null)
		{
			androidADBanner.SetDimentions(w, h);
			androidADBanner.OnBannerAdLoaded();
		}
	}

	private void OnBannerAdFailedToLoad(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		AndroidADBanner androidADBanner = GetBanner(id) as AndroidADBanner;
		if (androidADBanner != null)
		{
			androidADBanner.OnBannerAdFailedToLoad();
		}
	}

	private void OnBannerAdOpened(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		AndroidADBanner androidADBanner = GetBanner(id) as AndroidADBanner;
		if (androidADBanner != null)
		{
			androidADBanner.OnBannerAdOpened();
		}
	}

	private void OnBannerAdClosed(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		AndroidADBanner androidADBanner = GetBanner(id) as AndroidADBanner;
		if (androidADBanner != null)
		{
			androidADBanner.OnBannerAdClosed();
		}
	}

	private void OnBannerAdLeftApplication(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		AndroidADBanner androidADBanner = GetBanner(id) as AndroidADBanner;
		if (androidADBanner != null)
		{
			androidADBanner.OnBannerAdLeftApplication();
		}
	}

	private void OnInterstitialAdLoaded()
	{
		_OnInterstitialLoaded();
	}

	private void OnInterstitialAdFailedToLoad()
	{
		_OnInterstitialFailedLoading();
	}

	private void OnInterstitialAdOpened()
	{
		_OnInterstitialOpened();
	}

	private void OnInterstitialAdClosed()
	{
		_OnInterstitialClosed();
	}

	private void OnInterstitialAdLeftApplication()
	{
		_OnInterstitialLeftApplication();
	}

	private void OnInAppPurchaseRequested(string productId)
	{
		_OnAdInAppRequest(productId);
	}
}
