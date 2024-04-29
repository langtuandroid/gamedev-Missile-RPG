//using UnityEngine;

//public class UM_NotificationsExample : BaseIOSFeaturePreview
//{
//	private int LastNotificationId;

//	private void OnGUI()
//	{
//		UpdateToStartPos();
//		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Local Notifications API", style);
//		StartY += YLableStep;
//		if (GUI.Button(new Rect(StartX, StartY, buttonWidth + 10, buttonHeight), "Show Notification Popup "))
//		{
//			SA_Singleton<UM_NotificationController>.instance.ShowNotificationPoup("Hello", "Notification popup test");
//		}
//		StartX += XButtonStep;
//		if (GUI.Button(new Rect(StartX, StartY, buttonWidth + 10, buttonHeight), "Schedule Local Notification"))
//		{
//			LastNotificationId = SA_Singleton<UM_NotificationController>.instance.ScheduleLocalNotification("Hello Locacl", "Local Notification Example", 5);
//		}
//		StartX += XButtonStep;
//		if (GUI.Button(new Rect(StartX, StartY, buttonWidth + 10, buttonHeight), "Cansel Last Notification"))
//		{
//			SA_Singleton<UM_NotificationController>.instance.CancelLocalNotification(LastNotificationId);
//		}
//		StartX += XButtonStep;
//		if (GUI.Button(new Rect(StartX, StartY, buttonWidth + 10, buttonHeight), "Cansel All Last Notifications"))
//		{
//			SA_Singleton<UM_NotificationController>.instance.CancelAllLocalNotifications();
//		}
//		StartX = XStartPos;
//		StartY += YButtonStep;
//		StartY += YLableStep;
//		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Push Notifications API", style);
//		StartY += YLableStep;
//		if (GUI.Button(new Rect(StartX, StartY, buttonWidth + 10, buttonHeight), "Retrieve Device PushId "))
//		{
//			UM_NotificationController.OnPushIdLoadResult += OnPushIdLoaded;
//			SA_Singleton<UM_NotificationController>.Instance.RetrieveDevicePushId();
//		}
//	}

//	private void OnPushIdLoaded(UM_PushRegistrationResult res)
//	{
//		if (res.IsSucceeded)
//		{
//			new MobileNativeMessage("Succeeded", "Device Id: " + res.deviceId);
//		}
//		else
//		{
//			new MobileNativeMessage("Failed", "No device id");
//		}
//	}
//}
