using System.Collections.Generic;
using UnityEngine;

public class PermissionsAPI_Sample_By_moontm : MonoBehaviour
{
	public static PermissionsAPI_Sample_By_moontm me;

	private void Awake()
	{
		me = this;
		PermissionsManager.ActionPermissionsRequestCompleted += HandleActionPermissionsRequestCompleted;
	}

	public void CheckPermission()
	{
		Debug.Log("CheckPermission");
		bool flag = PermissionsManager.IsPermissionGranted(AN_ManifestPermission.WRITE_EXTERNAL_STORAGE);
		Debug.Log(flag);
		flag = PermissionsManager.IsPermissionGranted(AN_ManifestPermission.INTERNET);
		Debug.Log(flag);
	}

	public void RequestPermission()
	{
		SA_Singleton<PermissionsManager>.Instance.RequestPermissions(AN_ManifestPermission.WRITE_EXTERNAL_STORAGE, AN_ManifestPermission.CAMERA);
	}

	private void HandleActionPermissionsRequestCompleted(AN_GrantPermissionsResult res)
	{
		Debug.Log("HandleActionPermissionsRequestCompleted");
		foreach (KeyValuePair<AN_ManifestPermission, AN_PermissionState> item in res.RequestedPermissionsState)
		{
			Debug.Log(item.Key.GetFullName() + " / " + item.Value);
		}
	}

	public void Request_STORAGE_and_ACCOUNT()
	{
		if (!PermissionsManager.IsPermissionGranted(AN_ManifestPermission.WRITE_EXTERNAL_STORAGE))
		{
			SA_Singleton<PermissionsManager>.Instance.RequestPermissions(AN_ManifestPermission.WRITE_EXTERNAL_STORAGE);
		}
		if (!PermissionsManager.IsPermissionGranted(AN_ManifestPermission.GET_ACCOUNTS))
		{
			SA_Singleton<PermissionsManager>.Instance.RequestPermissions(AN_ManifestPermission.GET_ACCOUNTS);
		}
	}
}
