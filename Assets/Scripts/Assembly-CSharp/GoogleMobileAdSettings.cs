using System.Collections.Generic;
using UnityEngine;

public class GoogleMobileAdSettings : ScriptableObject
{
	public const string VERSION_NUMBER = "7.5";

	public const string PLAY_SERVICE_VERSION = "8487000";

	private const string ISNSettingsAssetName = "GoogleMobileAdSettings";

	private const string ISNSettingsAssetExtension = ".asset";

	public string IOS_InterstisialsUnitId = string.Empty;

	public string IOS_BannersUnitId = string.Empty;

	public string Android_InterstisialsUnitId = string.Empty;

	public string Android_BannersUnitId = string.Empty;

	public string WP8_InterstisialsUnitId = string.Empty;

	public string WP8_BannersUnitId = string.Empty;

	public bool IsIOSSettinsOpened = true;

	public bool IsAndroidSettinsOpened = true;

	public bool IsWP8SettinsOpened = true;

	public bool IsTestSettinsOpened;

	public bool ShowActions;

	public bool KeepManifestClean = true;

	public bool TagForChildDirectedTreatment;

	[SerializeField]
	public List<GADTestDevice> testDevices = new List<GADTestDevice>();

	public bool IsKeywordsOpened;

	public List<string> DefaultKeywords = new List<string>();

	private static GoogleMobileAdSettings instance;

	public static GoogleMobileAdSettings Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load("GoogleMobileAdSettings") as GoogleMobileAdSettings;
				if (instance == null)
				{
					instance = ScriptableObject.CreateInstance<GoogleMobileAdSettings>();
				}
			}
			return instance;
		}
	}

	public void AddDevice(GADTestDevice p)
	{
		testDevices.Add(p);
	}

	public void RemoveDevice(GADTestDevice p)
	{
		testDevices.Remove(p);
	}
}
