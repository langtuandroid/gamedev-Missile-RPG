using System;
using UnityEngine;

public class AnOtherFeaturesPreview : MonoBehaviour
{
	public GameObject image;

	public Texture2D helloWorldTexture;

	private void Start()
	{
		LoadNetworkInfo();
	}

	public void SaveToGalalry()
	{
		AndroidCamera instance = SA_Singleton<AndroidCamera>.instance;
		instance.OnImageSaved = (Action<GallerySaveResult>)Delegate.Combine(instance.OnImageSaved, new Action<GallerySaveResult>(OnImageSaved));
		SA_Singleton<AndroidCamera>.instance.SaveImageToGallery(helloWorldTexture, "Screenshot" + AndroidCamera.GetRandomString());
	}

	public void SaveScreenshot()
	{
		AndroidCamera instance = SA_Singleton<AndroidCamera>.instance;
		instance.OnImageSaved = (Action<GallerySaveResult>)Delegate.Combine(instance.OnImageSaved, new Action<GallerySaveResult>(OnImageSaved));
		SA_Singleton<AndroidCamera>.instance.SaveScreenshotToGallery("Screenshot" + AndroidCamera.GetRandomString());
	}

	public void GetImageFromGallery()
	{
		AndroidCamera instance = SA_Singleton<AndroidCamera>.instance;
		instance.OnImagePicked = (Action<AndroidImagePickResult>)Delegate.Combine(instance.OnImagePicked, new Action<AndroidImagePickResult>(OnImagePicked));
		SA_Singleton<AndroidCamera>.instance.GetImageFromGallery();
	}

	public void GetImageFromCamera()
	{
		AndroidCamera instance = SA_Singleton<AndroidCamera>.instance;
		instance.OnImagePicked = (Action<AndroidImagePickResult>)Delegate.Combine(instance.OnImagePicked, new Action<AndroidImagePickResult>(OnImagePicked));
		SA_Singleton<AndroidCamera>.instance.GetImageFromCamera();
	}

	public void CheckForTV()
	{
		TVAppController.DeviceTypeChecked += OnDeviceTypeChecked;
		SA_Singleton<TVAppController>.instance.CheckForATVDevice();
	}

	public void LoadNetworkInfo()
	{
		AndroidNativeUtility.ActionNetworkInfoLoaded += HandleActionNetworkInfoLoaded;
		SA_Singleton<AndroidNativeUtility>.instance.LoadNetworkInfo();
	}

	private void HandleActionNetworkInfoLoaded(AN_NetworkInfo info)
	{
		string empty = string.Empty;
		empty = empty + "IpAddress: " + info.IpAddress + " \n";
		empty = empty + "SubnetMask: " + info.SubnetMask + " \n";
		empty = empty + "MacAddress: " + info.MacAddress + " \n";
		empty = empty + "SSID: " + info.SSID + " \n";
		empty = empty + "BSSID: " + info.BSSID + " \n";
		string text = empty;
		empty = text + "LinkSpeed: " + info.LinkSpeed + " \n";
		text = empty;
		empty = text + "NetworkId: " + info.NetworkId + " \n";
		Debug.Log(empty);
		AndroidNativeUtility.ActionNetworkInfoLoaded -= HandleActionNetworkInfoLoaded;
	}

	public void CheckAppInstalation()
	{
		AndroidNativeUtility.OnPackageCheckResult += OnPackageCheckResult;
		SA_Singleton<AndroidNativeUtility>.instance.CheckIsPackageInstalled("com.google.android.youtube");
	}

	public void RunApp()
	{
		AndroidNativeUtility.OpenSettingsPage("android.settings.APPLICATION_DETAILS_SETTINGS");
	}

	public void CheckAppLicense()
	{
		AN_LicenseManager.OnLicenseRequestResult = (Action<AN_LicenseRequestResult>)Delegate.Combine(AN_LicenseManager.OnLicenseRequestResult, new Action<AN_LicenseRequestResult>(LicenseRequestResult));
		SA_Singleton<AN_LicenseManager>.instance.StartLicenseRequest(AndroidNativeSettings.Instance.base64EncodedPublicKey);
		SA_StatusBar.text = "Get App License Request STARTED";
	}

	private void LicenseRequestResult(AN_LicenseRequestResult result)
	{
		SA_StatusBar.text = "App License Status: " + result;
		AndroidMessage.Create("License Check Result: ", "AN_LicenseRequestResult: " + result);
	}

	private void EnableImmersiveMode()
	{
		SA_Singleton<ImmersiveMode>.instance.EnableImmersiveMode();
	}

	public void GetAndroidId()
	{
		AndroidNativeUtility.OnAndroidIdLoaded += OnAndroidIdLoaded;
		SA_Singleton<AndroidNativeUtility>.instance.LoadAndroidId();
	}

	private void OnAndroidIdLoaded(string id)
	{
		AndroidNativeUtility.OnAndroidIdLoaded -= OnAndroidIdLoaded;
		AndroidMessage.Create("Android Id Loaded", id);
	}

	private void LoadAppInfo()
	{
		AndroidAppInfoLoader.ActionPacakgeInfoLoaded += OnPackageInfoLoaded;
		SA_Singleton<AndroidAppInfoLoader>.instance.LoadPackageInfo();
	}

	private void LoadAdressBook()
	{
		SA_Singleton<AddressBookController>.Instance.LoadContacts();
		AddressBookController.OnContactsLoadedAction += OnContactsLoaded;
	}

	private void OnDeviceTypeChecked()
	{
		AN_PoupsProxy.showMessage("Check for a TV Device Result", SA_Singleton<TVAppController>.instance.IsRuningOnTVDevice.ToString());
		TVAppController.DeviceTypeChecked -= OnDeviceTypeChecked;
	}

	private void OnPackageCheckResult(AN_PackageCheckResult res)
	{
		if (res.IsSucceeded)
		{
			AN_PoupsProxy.showMessage("On Package Check Result", "Application  " + res.packageName + " is installed on this device");
		}
		else
		{
			AN_PoupsProxy.showMessage("On Package Check Result", "Application  " + res.packageName + " is not installed on this device");
		}
		AndroidNativeUtility.OnPackageCheckResult -= OnPackageCheckResult;
	}

	private void OnContactsLoaded()
	{
		AddressBookController.OnContactsLoadedAction -= OnContactsLoaded;
		AN_PoupsProxy.showMessage("On Contacts Loaded", "Andress book has " + SA_Singleton<AddressBookController>.instance.contacts.Count + " Contacts");
	}

	private void OnImagePicked(AndroidImagePickResult result)
	{
		Debug.Log("OnImagePicked");
		if (result.IsSucceeded)
		{
			AN_PoupsProxy.showMessage("Image Pick Rsult", "Succeeded, path: " + result.ImagePath);
			image.GetComponent<Renderer>().material.mainTexture = result.Image;
		}
		else
		{
			AN_PoupsProxy.showMessage("Image Pick Rsult", "Failed");
		}
		AndroidCamera instance = SA_Singleton<AndroidCamera>.instance;
		instance.OnImagePicked = (Action<AndroidImagePickResult>)Delegate.Remove(instance.OnImagePicked, new Action<AndroidImagePickResult>(OnImagePicked));
	}

	private void OnImageSaved(GallerySaveResult result)
	{
		AndroidCamera instance = SA_Singleton<AndroidCamera>.instance;
		instance.OnImageSaved = (Action<GallerySaveResult>)Delegate.Remove(instance.OnImageSaved, new Action<GallerySaveResult>(OnImageSaved));
		if (result.IsSucceeded)
		{
			AN_PoupsProxy.showMessage("Saved", "Image saved to gallery \nPath: " + result.imagePath);
			SA_StatusBar.text = "Image saved to gallery";
		}
		else
		{
			AN_PoupsProxy.showMessage("Failed", "Image save to gallery failed");
			SA_StatusBar.text = "Image save to gallery failed";
		}
	}

	private void OnPackageInfoLoaded(PackageAppInfo PacakgeInfo)
	{
		AndroidAppInfoLoader.ActionPacakgeInfoLoaded -= OnPackageInfoLoaded;
		string empty = string.Empty;
		empty = empty + "versionName: " + PacakgeInfo.versionName + "\n";
		empty = empty + "versionCode: " + PacakgeInfo.versionCode + "\n";
		empty = empty + "packageName" + PacakgeInfo.packageName + "\n";
		empty = empty + "lastUpdateTime:" + Convert.ToString(PacakgeInfo.lastUpdateTime) + "\n";
		empty = empty + "sharedUserId" + PacakgeInfo.sharedUserId + "\n";
		empty = empty + "sharedUserLabel" + PacakgeInfo.sharedUserLabel;
		AN_PoupsProxy.showMessage("App Info Loaded", empty);
	}

	public void LoadInternal()
	{
		AndroidNativeUtility.InternalStoragePathLoaded += InternalStoragePathLoaded;
		SA_Singleton<AndroidNativeUtility>.instance.GetInternalStoragePath();
	}

	public void LoadExternal()
	{
		AndroidNativeUtility.ExternalStoragePathLoaded += ExternalStoragePathLoaded;
		SA_Singleton<AndroidNativeUtility>.instance.GetExternalStoragePath();
	}

	public void LoadLocaleInfo()
	{
		AndroidNativeUtility.LocaleInfoLoaded += LocaleInfoLoaded;
		SA_Singleton<AndroidNativeUtility>.instance.LoadLocaleInfo();
	}

	private void LocaleInfoLoaded(AN_Locale locale)
	{
		AN_PoupsProxy.showMessage("Locale Indo:", locale.CountryCode + "/" + locale.DisplayCountry + "  :   " + locale.LanguageCode + "/" + locale.DisplayLanguage);
		AndroidNativeUtility.LocaleInfoLoaded -= LocaleInfoLoaded;
	}

	private void ExternalStoragePathLoaded(string path)
	{
		AN_PoupsProxy.showMessage("External Storage Path:", path);
		AndroidNativeUtility.ExternalStoragePathLoaded -= ExternalStoragePathLoaded;
	}

	private void InternalStoragePathLoaded(string path)
	{
		AN_PoupsProxy.showMessage("Internal Storage Path:", path);
		AndroidNativeUtility.InternalStoragePathLoaded -= InternalStoragePathLoaded;
	}
}
