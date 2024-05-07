//using System;
//using System.Collections.Generic;
//using ANMiniJSON;
//using UnityEngine;

//public class NotificationsExample : MonoBehaviour
//{
//	public Texture2D bigPicture;

//	private int LastNotificationId;

//	private void Awake()
//	{
//		GoogleCloudMessageService.ActionCMDRegistrationResult += HandleActionCMDRegistrationResult;
//		GoogleCloudMessageService.ActionCouldMessageLoaded += OnMessageLoaded;
//		GoogleCloudMessageService.ActionGCMPushLaunched += HandleActionGCMPushLaunched;
//		GoogleCloudMessageService.ActionGCMPushReceived += HandleActionGCMPushReceived;
//		SA_Singleton<GoogleCloudMessageService>.Instance.Init();
//	}

//	private void HandleActionGCMPushReceived(string message, Dictionary<string, object> data)
//	{
//		Debug.Log("[HandleActionGCMPushReceived]");
//		Debug.Log("Message: " + message);
//		foreach (KeyValuePair<string, object> datum in data)
//		{
//			Debug.Log("Data Entity: " + datum.Key + " " + datum.Value.ToString());
//		}
//		AN_PoupsProxy.showMessage(message, Json.Serialize(data));
//	}

//	private void HandleActionGCMPushLaunched(string message, Dictionary<string, object> data)
//	{
//		Debug.Log("[HandleActionGCMPushLaunched]");
//		Debug.Log("Message: " + message);
//		foreach (KeyValuePair<string, object> datum in data)
//		{
//			Debug.Log("Data Entity: " + datum.Key + " " + datum.Value.ToString());
//		}
//		AN_PoupsProxy.showMessage(message, Json.Serialize(data));
//	}

//	private void Toast()
//	{
//		AndroidToast.ShowToastNotification("Hello Toast", 1);
//	}

//	private void Local()
//	{
//		AndroidNotificationBuilder androidNotificationBuilder = new AndroidNotificationBuilder(SA_IdFactory.NextId, "Local Notification Title", "Big Picture Style Notification for AndroidNative Preview", 3);
//		androidNotificationBuilder.SetBigPicture(bigPicture);
//		SA_Singleton<AndroidNotificationManager>.Instance.ScheduleLocalNotification(androidNotificationBuilder);
//	}

//	private void LoadLaunchNotification()
//	{
//		AndroidNotificationManager instance = SA_Singleton<AndroidNotificationManager>.instance;
//		instance.OnNotificationIdLoaded = (Action<int>)Delegate.Combine(instance.OnNotificationIdLoaded, new Action<int>(OnNotificationIdLoaded));
//		SA_Singleton<AndroidNotificationManager>.instance.LocadAppLaunchNotificationId();
//	}

//	private void CanselLocal()
//	{
//		SA_Singleton<AndroidNotificationManager>.instance.CancelLocalNotification(LastNotificationId);
//	}

//	private void CancelAll()
//	{
//		SA_Singleton<AndroidNotificationManager>.instance.CancelAllLocalNotifications();
//	}

//	private void Reg()
//	{
//		SA_Singleton<GoogleCloudMessageService>.instance.RgisterDevice();
//	}

//	private void LoadLastMessage()
//	{
//		SA_Singleton<GoogleCloudMessageService>.instance.LoadLastMessage();
//	}

//	private void LocalNitificationsListExample()
//	{
//	}

//	private void HandleActionCMDRegistrationResult(GP_GCM_RegistrationResult res)
//	{
//		if (res.IsSucceeded)
//		{
//			AN_PoupsProxy.showMessage("Regstred", "GCM REG ID: " + SA_Singleton<GoogleCloudMessageService>.instance.registrationId);
//		}
//		else
//		{
//			AN_PoupsProxy.showMessage("Reg Failed", "GCM Registration failed :(");
//		}
//	}

//	private void OnNotificationIdLoaded(int notificationid)
//	{
//		AN_PoupsProxy.showMessage("Loaded", "App was laucnhed with notification id: " + notificationid);
//	}

//	private void OnMessageLoaded(string msg)
//	{
//		AN_PoupsProxy.showMessage("Message Loaded", "Last GCM Message: " + SA_Singleton<GoogleCloudMessageService>.instance.lastMessage);
//	}
//}
