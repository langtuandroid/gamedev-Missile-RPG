using UnityEngine;

public class Push_Ctrl : MonoBehaviour
{
	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			UM_Notifications.me.CancelAll();
			switch (Security.GetInt("PUSH_REWARD_LV", 0))
			{
			case 0:
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_001")), 79200);
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_001")), 165600);
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_001")), 511200);
				break;
			case 1:
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_002")), 79200);
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_002")), 165600);
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_002")), 511200);
				break;
			case 2:
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_003")), 79200);
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_003")), 165600);
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_003")), 511200);
				break;
			default:
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_001")), 79200);
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_001")), 165600);
				UM_Notifications.me.Noti(Localization.Get(string.Format("PUSH_001")), 511200);
				break;
			}
			if (!Fight_Master.me.GS.Equals(GameState.Play) || !(UI_Master.me.popups[0] == null)  )
			{
				return;
			}
			if (Tutorial_Manager.me != null)
			{
				if (!Tutorial_Manager.me.Tuto_ING)
				{
					UI_Master.me.OPEN_Pause_Popup();
				}
			}
			else
			{
				UI_Master.me.OPEN_Pause_Popup();
			}
		}
		else
		{
			//VIDEO_ADS.me.AD_Show = false;
			UM_Notifications.me.CancelAll();
		}
	}
}
