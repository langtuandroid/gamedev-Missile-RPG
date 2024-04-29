using System;
using UnityEngine;

public class PreviewSceneController : MonoBehaviour
{
	public SA_Label title;

	private void Awake()
	{
		title.text = "Android Native Unity3d Plugin (7.5)";
	}

	public void SendMail()
	{
		AndroidSocialGate.SendMail("Send Mail", string.Empty, "Android Native Plugin Question", "stans.assets@gmail.com");
	}

	public void SendBug()
	{
		AN_LicenseManager.OnLicenseRequestResult = (Action<AN_LicenseRequestResult>)Delegate.Combine(AN_LicenseManager.OnLicenseRequestResult, new Action<AN_LicenseRequestResult>(LicenseRequestResult));
		SA_Singleton<AN_LicenseManager>.instance.StartLicenseRequest();
	}

	private void LicenseRequestResult(AN_LicenseRequestResult result)
	{
		Debug.Log("LicenseRequestResult " + result);
	}

	public void OpenDocs()
	{
		string url = "http://goo.gl/pTcIR8";
		Application.OpenURL(url);
	}

	public void OpenAssetStore()
	{
		string url = "http://goo.gl/g8LWlC";
		Application.OpenURL(url);
	}

	public void MorePlugins()
	{
		string url = "http://goo.gl/MgEirV";
		Application.OpenURL(url);
	}
}
