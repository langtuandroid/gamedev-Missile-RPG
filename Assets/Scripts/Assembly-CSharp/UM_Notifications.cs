public class UM_Notifications : BaseIOSFeaturePreview
{
	public static UM_Notifications me;

	private int LastNotificationId;

	private void Awake()
	{
		if (me == null)
		{
			me = this;
		}
	}

	public void CancelAll()
	{
		//SA_Singleton<UM_NotificationController>.Instance.CancelAllLocalNotifications();
	}

	public void CancelLast()
	{
		//SA_Singleton<UM_NotificationController>.Instance.CancelLocalNotification(LastNotificationId);
	}

	public void Noti(string wording, int time)
	{
		//LastNotificationId = SA_Singleton<UM_NotificationController>.Instance.ScheduleLocalNotification(Localization.Get("MISSILERPG"), wording, time);
	}

	private void OnPushIdLoaded(UM_PushRegistrationResult res)
	{
		if (res.IsSucceeded)
		{
			new MobileNativeMessage("Succeeded", "Device Id: " + res.deviceId);
		}
		else
		{
			new MobileNativeMessage("Failed", "No device id");
		}
	}
}
