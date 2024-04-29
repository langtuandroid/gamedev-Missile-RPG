using System;
using UnityEngine;

public class GoogleAdsExample : MonoBehaviour
{
	private GUIStyle style;

	private GUIStyle style2;

	private GoogleMobileAdBanner banner1;

	private GoogleMobileAdBanner banner2;

	private void Start()
	{
		GoogleMobileAd.Init();
		GoogleMobileAd.SetGender(GoogleGender.Male);
		GoogleMobileAd.AddKeyword("game");
		GoogleMobileAd.SetBirthday(1989, AndroidMonth.MARCH, 18);
		GoogleMobileAd.TagForChildDirectedTreatment(false);
		GoogleMobileAd.AddTestDevice("733770c317dcbf4675fe870d3df9ca42");
		GoogleMobileAd.OnAdInAppRequest += OnInAppRequest;
		InitStyles();
	}

	private void InitStyles()
	{
		style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.fontSize = 16;
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperLeft;
		style.wordWrap = true;
		style2 = new GUIStyle();
		style2.normal.textColor = Color.white;
		style2.fontSize = 12;
		style2.fontStyle = FontStyle.Italic;
		style2.alignment = TextAnchor.UpperLeft;
		style2.wordWrap = true;
	}

	private void OnGUI()
	{
		float num = 20f;
		float num2 = 10f;
		GUI.Label(new Rect(num2, num, Screen.width, 40f), "Interstisal Example", style);
		num += 40f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Start Interstitial Ad"))
		{
			GoogleMobileAd.StartInterstitialAd();
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Load Interstitial Ad"))
		{
			GoogleMobileAd.LoadInterstitialAd();
		}
		num2 += 170f;
		GUI.enabled = GoogleMobileAd.IsInterstitialReady;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Show Interstitial Ad"))
		{
			GoogleMobileAd.ShowInterstitialAd();
		}
		GUI.enabled = true;
		num += 80f;
		num2 = 10f;
		GUI.Label(new Rect(num2, num, Screen.width, 40f), "Banners Example", style);
		GUI.enabled = false;
		if (banner1 == null)
		{
			GUI.enabled = true;
		}
		num += 40f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Banner Custom Pos"))
		{
			banner1 = GoogleMobileAd.CreateAdBanner(300, 100, GADBannerSize.BANNER);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Banner Top Left"))
		{
			banner1 = GoogleMobileAd.CreateAdBanner(TextAnchor.UpperLeft, GADBannerSize.BANNER);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Banner Top Center"))
		{
			banner1 = GoogleMobileAd.CreateAdBanner(TextAnchor.UpperCenter, GADBannerSize.BANNER);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Banner Top Right"))
		{
			banner1 = GoogleMobileAd.CreateAdBanner(TextAnchor.UpperRight, GADBannerSize.BANNER);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Banner Bottom Left"))
		{
			banner1 = GoogleMobileAd.CreateAdBanner(TextAnchor.LowerLeft, GADBannerSize.BANNER);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Banner Bottom Center"))
		{
			banner1 = GoogleMobileAd.CreateAdBanner(TextAnchor.LowerCenter, GADBannerSize.BANNER);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Banner Bottom Right"))
		{
			banner1 = GoogleMobileAd.CreateAdBanner(TextAnchor.LowerRight, GADBannerSize.BANNER);
		}
		GUI.enabled = false;
		if (banner1 != null && banner1.IsLoaded)
		{
			GUI.enabled = true;
		}
		num += 80f;
		num2 = 10f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Refresh"))
		{
			banner1.Refresh();
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Move To Center"))
		{
			banner1.SetBannerPosition(TextAnchor.MiddleCenter);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "To Radom Coords"))
		{
			banner1.SetBannerPosition(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height));
		}
		GUI.enabled = false;
		if (banner1 != null && banner1.IsLoaded && banner1.IsOnScreen)
		{
			GUI.enabled = true;
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Hide"))
		{
			banner1.Hide();
		}
		GUI.enabled = false;
		if (banner1 != null && banner1.IsLoaded && !banner1.IsOnScreen)
		{
			GUI.enabled = true;
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Show"))
		{
			banner1.Show();
		}
		GUI.enabled = false;
		if (banner1 != null)
		{
			GUI.enabled = true;
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Destroy"))
		{
			GoogleMobileAd.DestroyBanner(banner1.id);
			banner1 = null;
		}
		GUI.enabled = true;
		num += 80f;
		num2 = 10f;
		GUI.Label(new Rect(num2, num, Screen.width, 40f), "Banner 2", style);
		GUI.enabled = false;
		if (banner2 == null)
		{
			GUI.enabled = true;
		}
		num += 40f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Smart Banner"))
		{
			banner2 = GoogleMobileAd.CreateAdBanner(TextAnchor.LowerLeft, GADBannerSize.SMART_BANNER);
			GoogleMobileAdBanner googleMobileAdBanner = banner2;
			googleMobileAdBanner.OnLoadedAction = (Action<GoogleMobileAdBanner>)Delegate.Combine(googleMobileAdBanner.OnLoadedAction, new Action<GoogleMobileAdBanner>(OnBannerLoadedAction));
			banner2.ShowOnLoad = false;
		}
		GUI.enabled = false;
		if (banner2 != null && banner2.IsLoaded)
		{
			GUI.enabled = true;
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Refresh"))
		{
			banner2.Refresh();
		}
		GUI.enabled = false;
		if (banner2 != null && banner2.IsLoaded && banner2.IsOnScreen)
		{
			GUI.enabled = true;
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Hide"))
		{
			banner2.Hide();
		}
		GUI.enabled = false;
		if (banner2 != null && banner2.IsLoaded && !banner2.IsOnScreen)
		{
			GUI.enabled = true;
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Show"))
		{
			banner2.Show();
		}
		GUI.enabled = false;
		if (banner2 != null)
		{
			GUI.enabled = true;
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Destroy"))
		{
			GoogleMobileAd.DestroyBanner(banner2.id);
			banner2 = null;
		}
		GUI.enabled = true;
	}

	private void OnInAppRequest(string productId)
	{
		Debug.Log("In App Request for product Id: " + productId + " received");
		GoogleMobileAd.RecordInAppResolution(GADInAppResolution.RESOLUTION_SUCCESS);
	}

	private void OnInterstitialLoaded()
	{
		Debug.Log("OnInterstitialLoaded catched with C# Actions usage");
	}

	private void OnOpenedAction(GoogleMobileAdBanner banner)
	{
		banner.OnOpenedAction = (Action<GoogleMobileAdBanner>)Delegate.Remove(banner.OnOpenedAction, new Action<GoogleMobileAdBanner>(OnOpenedAction));
		Debug.Log("Banner was just clicked");
	}

	private void OnBannerLoadedAction(GoogleMobileAdBanner banner)
	{
		banner.OnLoadedAction = (Action<GoogleMobileAdBanner>)Delegate.Remove(banner.OnLoadedAction, new Action<GoogleMobileAdBanner>(OnBannerLoadedAction));
		banner.Show();
	}
}
