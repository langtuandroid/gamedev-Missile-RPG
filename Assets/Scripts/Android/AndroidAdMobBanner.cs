using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidAdMobBanner : MonoBehaviour
{
	public string BannersUnityId;

	public GADBannerSize size = GADBannerSize.SMART_BANNER;

	public TextAnchor anchor = TextAnchor.LowerCenter;

	private static Dictionary<string, GoogleMobileAdBanner> _refisterdBanners;

	public static Dictionary<string, GoogleMobileAdBanner> registerdBanners
	{
		get
		{
			if (_refisterdBanners == null)
			{
				_refisterdBanners = new Dictionary<string, GoogleMobileAdBanner>();
			}
			return _refisterdBanners;
		}
	}

	public string sceneBannerId
	{
		get
		{
			return SceneManager.GetActiveScene().name + "_" + base.gameObject.name;
		}
	}

	private void Awake()
	{
		if (SA_Singleton<AndroidAdMobController>.instance.IsInited)
		{
			if (!SA_Singleton<AndroidAdMobController>.instance.BannersUunitId.Equals(BannersUnityId))
			{
				SA_Singleton<AndroidAdMobController>.instance.SetBannersUnitID(BannersUnityId);
			}
		}
		else
		{
			SA_Singleton<AndroidAdMobController>.instance.Init(BannersUnityId);
		}
	}

	private void Start()
	{
		ShowBanner();
	}

	private void OnDestroy()
	{
		HideBanner();
	}

	public void ShowBanner()
	{
		GoogleMobileAdBanner googleMobileAdBanner;
		if (registerdBanners.ContainsKey(sceneBannerId))
		{
			googleMobileAdBanner = registerdBanners[sceneBannerId];
		}
		else
		{
			googleMobileAdBanner = SA_Singleton<AndroidAdMobController>.instance.CreateAdBanner(anchor, size);
			registerdBanners.Add(sceneBannerId, googleMobileAdBanner);
		}
		if (googleMobileAdBanner.IsLoaded && !googleMobileAdBanner.IsOnScreen)
		{
			googleMobileAdBanner.Show();
		}
	}

	public void HideBanner()
	{
		if (!registerdBanners.ContainsKey(sceneBannerId))
		{
			return;
		}
		GoogleMobileAdBanner googleMobileAdBanner = registerdBanners[sceneBannerId];
		if (googleMobileAdBanner.IsLoaded)
		{
			if (googleMobileAdBanner.IsOnScreen)
			{
				googleMobileAdBanner.Hide();
			}
		}
		else
		{
			googleMobileAdBanner.ShowOnLoad = false;
		}
	}
}
