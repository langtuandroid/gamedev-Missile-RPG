using System;
using UnityEngine;

public class AN_LicenseManager : SA_Singleton<AN_LicenseManager>
{
	public static Action<AN_LicenseRequestResult> OnLicenseRequestResult = delegate
	{
	};

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void StartLicenseRequest()
	{
		StartLicenseRequest(AndroidNativeSettings.Instance.base64EncodedPublicKey);
	}

	public void StartLicenseRequest(string base64PublicKey)
	{
		AN_LicenseManagerProxy.StartLicenseRequest(base64PublicKey);
	}

	private void OnLicenseRequestRes(string data)
	{
		AN_LicenseRequestResult obj = (AN_LicenseRequestResult)(int)Enum.Parse(typeof(AN_LicenseRequestResult), data);
		OnLicenseRequestResult(obj);
	}
}
