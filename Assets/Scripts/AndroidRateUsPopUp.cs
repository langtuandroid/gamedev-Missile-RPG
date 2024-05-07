using System;
using UnityEngine;

public class AndroidRateUsPopUp : BaseAndroidPopup
{
	public string yes;

	public string later;

	public string no;

	public string url;

	public static AndroidRateUsPopUp Create(string title, string message, string url)
	{
		return Create(title, message, url, "Rate app", "Later", "No, thanks");
	}

	public static AndroidRateUsPopUp Create(string title, string message, string url, string yes, string later, string no)
	{
		AndroidRateUsPopUp androidRateUsPopUp = new GameObject("AndroidRateUsPopUp").AddComponent<AndroidRateUsPopUp>();
		androidRateUsPopUp.title = title;
		androidRateUsPopUp.message = message;
		androidRateUsPopUp.url = url;
		androidRateUsPopUp.yes = yes;
		androidRateUsPopUp.later = later;
		androidRateUsPopUp.no = no;
		androidRateUsPopUp.init();
		return androidRateUsPopUp;
	}

	public void init()
	{
		AN_PoupsProxy.showRateDialog(title, message, yes, later, no);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		switch (Convert.ToInt16(buttonIndex))
		{
		case 0:
			AN_PoupsProxy.OpenAppRatePage(url);
			DispatchAction(AndroidDialogResult.RATED);
			break;
		case 1:
			DispatchAction(AndroidDialogResult.REMIND);
			break;
		case 2:
			DispatchAction(AndroidDialogResult.DECLINED);
			break;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
