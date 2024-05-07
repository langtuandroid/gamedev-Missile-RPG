using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidAdMobBannerInterstitial : MonoBehaviour
{
	public string InterstitialUnityId;

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
			if (!SA_Singleton<AndroidAdMobController>.instance.InterstisialUnitId.Equals(InterstitialUnityId))
			{
				SA_Singleton<AndroidAdMobController>.instance.SetInterstisialsUnitID(InterstitialUnityId);
			}
		}
		else
		{
			SA_Singleton<AndroidAdMobController>.instance.Init(InterstitialUnityId);
		}
	}

	private void Start()
	{
		ShowBanner();
	}

	public void ShowBanner()
	{
		SA_Singleton<AndroidAdMobController>.instance.StartInterstitialAd();
	}
}
