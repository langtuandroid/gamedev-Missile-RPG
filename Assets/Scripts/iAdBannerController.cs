using System;
using System.Collections.Generic;
using UnityEngine;

public class iAdBannerController : ISN_Singleton<iAdBannerController>
{
	private static int _nextId = 0;

	private static iAdBannerController _instance;

	private Dictionary<int, iAdBanner> _banners;

	private bool _IsInterstisialsAdReady;

	public static int nextId
	{
		get
		{
			_nextId++;
			return _nextId;
		}
	}

	public bool IsInterstisialsAdReady
	{
		get
		{
			return _IsInterstisialsAdReady;
		}
	}

	public List<iAdBanner> banners
	{
		get
		{
			List<iAdBanner> list = new List<iAdBanner>();
			if (_banners == null)
			{
				return list;
			}
			foreach (KeyValuePair<int, iAdBanner> banner in _banners)
			{
				list.Add(banner.Value);
			}
			return list;
		}
	}

	public static event Action InterstitialDidFailWithErrorAction;

	public static event Action InterstitialAdWillLoadAction;

	public static event Action InterstitialAdDidLoadAction;

	public static event Action InterstitialAdDidFinishAction;

	static iAdBannerController()
	{
		iAdBannerController.InterstitialDidFailWithErrorAction = delegate
		{
		};
		iAdBannerController.InterstitialAdWillLoadAction = delegate
		{
		};
		iAdBannerController.InterstitialAdDidLoadAction = delegate
		{
		};
		iAdBannerController.InterstitialAdDidFinishAction = delegate
		{
		};
	}

	private void Awake()
	{
		_banners = new Dictionary<int, iAdBanner>();
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public iAdBanner CreateAdBanner(TextAnchor anchor)
	{
		iAdBanner iAdBanner2 = new iAdBanner(anchor, nextId);
		_banners.Add(iAdBanner2.id, iAdBanner2);
		return iAdBanner2;
	}

	public iAdBanner CreateAdBanner(int x, int y)
	{
		iAdBanner iAdBanner2 = new iAdBanner(x, y, nextId);
		_banners.Add(iAdBanner2.id, iAdBanner2);
		return iAdBanner2;
	}

	public void DestroyBanner(int id)
	{
		if (_banners != null && _banners.ContainsKey(id))
		{
			_banners.Remove(id);
		}
	}

	public void StartInterstitialAd()
	{
	}

	public void LoadInterstitialAd()
	{
	}

	public void ShowInterstitialAd()
	{
		if (_IsInterstisialsAdReady)
		{
			Invoke("interstitialAdActionDidFinish", 1f);
			_IsInterstisialsAdReady = false;
		}
	}

	public iAdBanner GetBanner(int id)
	{
		if (_banners.ContainsKey(id))
		{
			return _banners[id];
		}
		if (!IOSNativeSettings.Instance.DisablePluginLogs)
		{
			Debug.LogWarning("Banner id: " + id + " not found");
		}
		return null;
	}

	private void didFailToReceiveAdWithError(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		iAdBanner banner = GetBanner(id);
		if (banner != null)
		{
			banner.didFailToReceiveAdWithError();
		}
	}

	private void bannerViewDidLoadAd(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		iAdBanner banner = GetBanner(id);
		if (banner != null)
		{
			banner.bannerViewDidLoadAd();
		}
	}

	private void bannerViewWillLoadAd(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		iAdBanner banner = GetBanner(id);
		if (banner != null)
		{
			banner.bannerViewWillLoadAd();
		}
	}

	private void bannerViewActionDidFinish(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		iAdBanner banner = GetBanner(id);
		if (banner != null)
		{
			banner.bannerViewActionDidFinish();
		}
	}

	private void bannerViewActionShouldBegin(string bannerID)
	{
		int id = Convert.ToInt32(bannerID);
		iAdBanner banner = GetBanner(id);
		if (banner != null)
		{
			banner.bannerViewActionShouldBegin();
		}
	}

	private void interstitialdidFailWithError(string data)
	{
		iAdBannerController.InterstitialDidFailWithErrorAction();
		_IsInterstisialsAdReady = false;
	}

	private void interstitialAdWillLoad(string data)
	{
		iAdBannerController.InterstitialAdWillLoadAction();
		_IsInterstisialsAdReady = false;
	}

	private void interstitialAdDidLoad(string data)
	{
		iAdBannerController.InterstitialAdDidLoadAction();
		_IsInterstisialsAdReady = true;
	}

	private void interstitialAdActionDidFinish()
	{
		iAdBannerController.InterstitialAdDidFinishAction();
	}
}
