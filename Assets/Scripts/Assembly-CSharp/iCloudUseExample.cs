using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class iCloudUseExample : BaseIOSFeaturePreview
{
	private float v = 1.1f;

	private void Awake()
	{
		iCloudManager.OnCloudInitAction += OnCloundInitAction;
		iCloudManager.OnCloudDataReceivedAction += OnCloudDataReceivedAction;
		iCloudManager.OnStoreDidChangeExternally += HandleOnStoreDidChangeExternally;
		ISN_Singleton<iCloudManager>.Instance.init();
	}

	private void HandleOnStoreDidChangeExternally(List<iCloudData> changedData)
	{
		foreach (iCloudData changedDatum in changedData)
		{
			Debug.Log("Cloud data with key:  " + changedDatum.key + " was chnaged");
		}
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(170f, 70f, 150f, 50f), "Set String"))
		{
			ISN_Singleton<iCloudManager>.Instance.setString("TestStringKey", "Hello World");
		}
		if (GUI.Button(new Rect(170f, 130f, 150f, 50f), "Get String"))
		{
			ISN_Singleton<iCloudManager>.Instance.requestDataForKey("TestStringKey");
		}
		if (GUI.Button(new Rect(330f, 70f, 150f, 50f), "Set Float"))
		{
			v += 1.1f;
			ISN_Singleton<iCloudManager>.Instance.setFloat("TestFloatKey", v);
		}
		if (GUI.Button(new Rect(330f, 130f, 150f, 50f), "Get Float"))
		{
			ISN_Singleton<iCloudManager>.Instance.requestDataForKey("TestFloatKey");
		}
		if (GUI.Button(new Rect(490f, 70f, 150f, 50f), "Set Bytes"))
		{
			string s = "hello world";
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			byte[] bytes = uTF8Encoding.GetBytes(s);
			ISN_Singleton<iCloudManager>.Instance.setData("TestByteKey", bytes);
		}
		if (GUI.Button(new Rect(490f, 130f, 150f, 50f), "Get Bytes"))
		{
			ISN_Singleton<iCloudManager>.Instance.requestDataForKey("TestByteKey");
		}
		if (GUI.Button(new Rect(170f, 500f, 150f, 50f), "TestConnection"))
		{
			LoadLevel("Peer-To-PeerGameExample");
		}
	}

	private void OnCloundInitAction(ISN_Result result)
	{
		if (result.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("iCloud", "Initialization Success!");
		}
		else
		{
			IOSNativePopUpManager.showMessage("iCloud", "Initialization Failed!");
		}
	}

	private void OnCloudDataReceivedAction(iCloudData data)
	{
		if (data.IsEmpty)
		{
			IOSNativePopUpManager.showMessage(data.key, "data is empty");
		}
		else
		{
			IOSNativePopUpManager.showMessage(data.key, data.stringValue);
		}
	}

	private void OnDestroy()
	{
		iCloudManager.OnCloudInitAction -= OnCloundInitAction;
		iCloudManager.OnCloudDataReceivedAction -= OnCloudDataReceivedAction;
	}
}
