using System;
using System.Collections.Generic;
using UnityEngine;

public class IOSAdMobController : SA_Singleton<IOSAdMobController>, GoogleMobileAdInterface
{
	private const string DEVICES_SEPARATOR = ",";

	private bool _IsInited;

	private Dictionary<int, IOSADBanner> _banners;

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
			foreach (KeyValuePair<int, IOSADBanner> banner in _banners)
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
		_banners = new Dictionary<int, IOSADBanner>();
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
	}

	public void SetInterstisialsUnitID(string ad_unit_id)
	{
		_InterstisialUnitId = ad_unit_id;
	}

	public GoogleMobileAdBanner CreateAdBanner(TextAnchor anchor, GADBannerSize size)
	{
		if (!IsInited)
		{
			Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		IOSADBanner iOSADBanner = new IOSADBanner(anchor, size, GADBannerIdFactory.nextId);
		_banners.Add(iOSADBanner.id, iOSADBanner);
		return iOSADBanner;
	}

	public GoogleMobileAdBanner CreateAdBanner(int x, int y, GADBannerSize size)
	{
		if (!IsInited)
		{
			Debug.LogWarning("CreateBannerAd shoudl be called only after Init function. Call ignored");
			return null;
		}
		IOSADBanner iOSADBanner = new IOSADBanner(x, y, size, GADBannerIdFactory.nextId);
		_banners.Add(iOSADBanner.id, iOSADBanner);
		return iOSADBanner;
	}

	public void DestroyBanner(int id)
	{
		if (_banners != null && _banners.ContainsKey(id))
		{
			IOSADBanner iOSADBanner = _banners[id];
			if (iOSADBanner.IsLoaded)
			{
				_banners.Remove(id);
			}
			else
			{
				iOSADBanner.DestroyAfterLoad();
			}
		}
	}

	public void DirectBannerDestory(int id)
	{
	}

	public void RecordInAppResolution(GADInAppResolution resolution)
	{
	}

	public void AddKeyword(string keyword)
	{
		if (!IsInited)
		{
			Debug.LogWarning("AddKeyword shoudl be called only after Init function. Call ignored");
		}
	}

	public void AddTestDevice(string deviceId)
	{
		if (!IsInited)
		{
			Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
	}

	public void AddTestDevices(params string[] ids)
	{
		if (!IsInited)
		{
			Debug.LogWarning("AddTestDevice shoudl be called only after Init function. Call ignored");
		}
		else if (ids.Length != 0)
		{
		}
	}

	public void SetGender(GoogleGender gender)
	{
		if (!IsInited)
		{
			Debug.LogWarning("SetGender shoudl be called only after Init function. Call ignored");
		}
	}

	public void SetBirthday(int year, AndroidMonth month, int day)
	{
		if (!IsInited)
		{
			Debug.LogWarning("SetBirthday shoudl be called only after Init function. Call ignored");
		}
	}

	public void TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
	{
		if (!IsInited)
		{
			Debug.LogWarning("TagForChildDirectedTreatment shoudl be called only after Init function. Call ignored");
		}
	}

	public void StartInterstitialAd()
	{
		if (!IsInited)
		{
			Debug.LogWarning("StartInterstitialAd shoudl be called only after Init function. Call ignored");
		}
	}

	public void LoadInterstitialAd()
	{
		if (!IsInited)
		{
			Debug.LogWarning("LoadInterstitialAd shoudl be called only after Init function. Call ignored");
		}
	}

	public void ShowInterstitialAd()
	{
		if (!IsInited)
		{
			Debug.LogWarning("ShowInterstitialAd shoudl be called only after Init function. Call ignored");
		}
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
		IOSADBanner iOSADBanner = GetBanner(id) as IOSADBanner;
		if (iOSADBanner != null)
		{
			iOSADBanner.SetDimentions(w, h);
			iOSADBanner.OnBannerAdLoaded();
		}
	}

	private void OnBannerAdFailedToLoad(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		IOSADBanner iOSADBanner = GetBanner(id) as IOSADBanner;
		if (iOSADBanner != null)
		{
			iOSADBanner.OnBannerAdFailedToLoad();
		}
	}

	private void OnBannerAdOpened(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		IOSADBanner iOSADBanner = GetBanner(id) as IOSADBanner;
		if (iOSADBanner != null)
		{
			iOSADBanner.OnBannerAdOpened();
		}
	}

	private void OnBannerAdClosed(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		IOSADBanner iOSADBanner = GetBanner(id) as IOSADBanner;
		if (iOSADBanner != null)
		{
			iOSADBanner.OnBannerAdClosed();
		}
	}

	private void OnBannerAdLeftApplication(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		IOSADBanner iOSADBanner = GetBanner(id) as IOSADBanner;
		if (iOSADBanner != null)
		{
			iOSADBanner.OnBannerAdLeftApplication();
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
