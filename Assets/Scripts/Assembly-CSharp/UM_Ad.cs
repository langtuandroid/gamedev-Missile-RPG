using System.Collections;
using UnityEngine;

public class UM_Ad : MonoBehaviour
{
	public static UM_Ad me;

	private GUIStyle style;

	private GUIStyle style2;

	private int bannerId1;

	private int bannerId2;

	private bool isLoadInt;

	private float AD_time = 180f;

	public float AD_time_Check;

	public bool AD_Allow = true;

	private bool Show_banner;

	private int banner_warning;

	public bool AD_Impossible;

	private void Awake()
	{
		if (me == null)
		{
			me = this;
			InitializeActions();
			UM_AdManager.Init();
			UM_AdManager.LoadInterstitialAd();
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void Load_FullAD()
	{
		if (!AD_Impossible)
		{
			InitializeActions();
			UM_AdManager.LoadInterstitialAd();
			isLoadInt = true;
		}
	}

	public void Show_FullAD()
	{
		if (!AD_Impossible)
		{
			if (AD_Allow)
			{
				Debug.Log("전면 광고를 띄운다.");
				UM_AdManager.ShowInterstitialAd();
				AD_time_Check = 0f;
				AD_Allow = false;
			}
			else
			{
				Debug.Log("시간이 아직 되지 않음..");
			}
		}
	}

	public void FixedUpdate()
	{
		if (!AD_Allow)
		{
			AD_time_Check += 0.05f;
			if (AD_time_Check >= AD_time)
			{
				AD_Allow = true;
			}
		}
	}

	public void Banner_Top()
	{
		bannerId1 = UM_AdManager.CreateAdBanner(TextAnchor.UpperCenter);
		UM_AdManager.ShowBanner(bannerId2);
	}

	public void Banner_Bot()
	{
		if (!Show_banner)
		{
			bannerId1 = UM_AdManager.CreateAdBanner(TextAnchor.LowerCenter);
			if (bannerId1.Equals(0))
			{
				AD_Impossible = true;
				return;
			}
			AD_Impossible = false;
			Show_banner = true;
			UM_AdManager.ShowBanner(bannerId1);
			StartCoroutine(Frame_Check());
		}
	}

	private IEnumerator Frame_Check()
	{
		yield return new WaitForSeconds(0.5f);
		if (1f / Time.deltaTime < 30f)
		{
			banner_warning++;
			if (banner_warning >= 10)
			{
				AD_Impossible = true;
				Show_banner = false;
				banner_warning = 0;
				if (bannerId1 != 0)
				{
					UM_AdManager.HideBanner(bannerId1);
				}
			}
			else
			{
				StartCoroutine(Frame_Check());
			}
		}
		else
		{
			banner_warning = 0;
			StartCoroutine(Frame_Check());
		}
	}

	public void Banner_Top_Hide()
	{
		UM_AdManager.HideBanner(bannerId2);
	}

	public void Banner_Bot_Hide()
	{
		if (bannerId1 != 0)
		{
			UM_AdManager.HideBanner(bannerId1);
		}
	}

	private void InitializeActions()
	{
		UM_AdManager.ResetActions();
		UM_AdManager.OnInterstitialLoaded += HandleOnInterstitialLoaded;
		UM_AdManager.OnInterstitialLoadFail += HandleOnInterstitialLoadFail;
		UM_AdManager.OnInterstitialClosed += HandleOnInterstitialClosed;
	}

	private void HandleOnInterstitialClosed()
	{
		Debug.Log("Interstitial Ad was closed");
		UM_AdManager.OnInterstitialClosed -= HandleOnInterstitialClosed;
	}

	private void HandleOnInterstitialLoadFail()
	{
		Debug.Log("Interstitial is failed to load");
		UM_AdManager.OnInterstitialLoaded -= HandleOnInterstitialLoaded;
		UM_AdManager.OnInterstitialLoadFail -= HandleOnInterstitialLoadFail;
		UM_AdManager.OnInterstitialClosed -= HandleOnInterstitialClosed;
	}

	private void HandleOnInterstitialLoaded()
	{
		Debug.Log("Interstitial ad content ready");
		UM_AdManager.OnInterstitialLoaded -= HandleOnInterstitialLoaded;
		UM_AdManager.OnInterstitialLoadFail -= HandleOnInterstitialLoadFail;
	}
}
