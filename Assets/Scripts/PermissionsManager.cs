using System;
using System.Collections.Generic;
using UnityEngine;

public class PermissionsManager : SA_Singleton<PermissionsManager>
{
	private const string PM_CLASS_NAME = "com.androidnative.features.permissions.PermissionsManager";

	public static event Action<AN_GrantPermissionsResult> ActionPermissionsRequestCompleted;

	static PermissionsManager()
	{
		PermissionsManager.ActionPermissionsRequestCompleted = delegate
		{
		};
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public static bool IsPermissionGranted(AN_ManifestPermission permission)
	{
		return IsPermissionGranted(permission.GetFullName());
	}

	public static bool IsPermissionGranted(string permission)
	{
		return AN_ProxyPool.CallStatic<bool>("com.androidnative.features.permissions.PermissionsManager", "checkForPermission", new object[1] { permission });
	}

	public void RequestPermissions(params AN_ManifestPermission[] permissions)
	{
		List<string> list = new List<string>();
		foreach (AN_ManifestPermission permission in permissions)
		{
			list.Add(permission.GetFullName());
		}
		RequestPermissions(list.ToArray());
	}

	public void RequestPermissions(params string[] permissions)
	{
		AN_ProxyPool.CallStatic("com.androidnative.features.permissions.PermissionsManager", "requestPermissions", AndroidNative.ArrayToString(permissions));
	}

	private void OnPermissionsResult(string data)
	{
		Debug.Log("OnPermissionsResult:" + data);
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string[] array2 = AndroidNative.StringToArray(array[0]);
		string[] array3 = AndroidNative.StringToArray(array[1]);
		string[] array4 = array2;
		foreach (string message in array4)
		{
			Debug.Log(message);
		}
		string[] array5 = array3;
		foreach (string message2 in array5)
		{
			Debug.Log(message2);
		}
		AN_GrantPermissionsResult obj = new AN_GrantPermissionsResult(array2, array3);
		PermissionsManager.ActionPermissionsRequestCompleted(obj);
	}

	public static AN_ManifestPermission GetPermissionByName(string fullName)
	{
		foreach (int value in Enum.GetValues(typeof(AN_ManifestPermission)))
		{
			if (((AN_ManifestPermission)value).GetFullName().Equals(fullName))
			{
				return (AN_ManifestPermission)value;
			}
		}
		return AN_ManifestPermission.UNDEFINED;
	}
}
