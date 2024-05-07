//using UnityEngine;
//using UnityEngine.Advertisements;

//public class VIDEO_ADS : MonoBehaviour
//{
//	public static VIDEO_ADS me;

//	public int ADNumber;

//	public float AD_time;

//	public bool SHOW_Possible;

//	public bool CHEAT;

//	public bool AD_Show;

//	public int AD_count;

//	private int Imsi;

//	public int TODAY_AD_COUNT;

//	public int reward_ID;

//	public bool isPossible(bool Wating_Time_Ignore)
//	{
//		if (Now_Data.me.Bazuka_Possible[3] > 0)
//		{
//			return true;
//		}
//		if (Advertisement.IsReady() || CHEAT)
//		{
//			return true;
//		}
//		UI_Master.me.Warning(Localization.Get("AD_WARNING"));
//		return false;
//	}

//	public void ShowAD()
//	{
//		if (Now_Data.me.Bazuka_Possible[3] > 0)
//		{
//			UI_Master.me.Good_MSG(Localization.Get("NOAD_BY_WAEPON"));
//		}
//		else if (Now_Data.me.NO_ADS_Per > 0f && (float)Random.Range(0, 100) < Now_Data.me.NO_ADS_Per)
//		{
//			UI_Master.me.Good_MSG(Localization.Get("NOAD"));
//		}
//		else if (Advertisement.IsReady())
//		{
//			AD_Show = true;
//			Advertisement.Show();
//			TODAY_AD_COUNT++;
//			if ((TODAY_AD_COUNT % 4).Equals(0))
//			{
//				UM_Ad.me.Show_FullAD();
//				UM_Ad.me.Load_FullAD();
//			}
//		}
//		else if (UM_Ad.me != null)
//		{
//			AD_Show = true;
//			UM_Ad.me.Show_FullAD();
//			UM_Ad.me.Load_FullAD();
//		}
//	}

//	public void ShowRewardedVideo(int Reward_ID)
//	{
//		reward_ID = Reward_ID;
//		if (Now_Data.me.Bazuka_Possible[3] > 0)
//		{
//			UI_Master.me.Good_MSG(Localization.Get("NOAD_BY_WAEPON"));
//			HandleShowResult(ShowResult.Finished);
//		}
//		else if (Advertisement.IsReady())
//		{
//			if (Now_Data.me.NO_ADS_Per > 0f && (float)Random.Range(0, 100) < Now_Data.me.NO_ADS_Per)
//			{
//				UI_Master.me.Good_MSG(Localization.Get("NOAD"));
//				HandleShowResult(ShowResult.Finished);
//				return;
//			}
//			AD_Show = true;
//			ShowOptions showOptions = new ShowOptions();
//			showOptions.resultCallback = HandleShowResult;
//			Advertisement.Show("rewardedVideo", showOptions);
//			TODAY_AD_COUNT++;
//			if ((TODAY_AD_COUNT % 4).Equals(0))
//			{
//				UM_Ad.me.Show_FullAD();
//				UM_Ad.me.Load_FullAD();
//			}
//		}
//		else
//		{
//			UI_Master.me.Warning(Localization.Get("AD_WARNING"));
//		}
//	}

//	private void Awake()
//	{
//		int num = Random.Range(0, 10);
//		me = this;
//		if (Application.platform == RuntimePlatform.Android)
//		{
//			if (num < 5)
//			{
//				if (!Now_Data.me.VIP_Version)
//				{
//					Advertisement.Initialize("3095828");
//				}
//				else
//				{
//					Advertisement.Initialize("3095881");
//				}
//			}
//			else
//			{
//				Advertisement.Initialize("2742368");
//			}
//		}
//		else if (Application.platform == RuntimePlatform.IPhonePlayer)
//		{
//			if (num < 5)
//			{
//				if (!Now_Data.me.VIP_Version)
//				{
//					Advertisement.Initialize("3095829");
//				}
//				else
//				{
//					Advertisement.Initialize("3095880");
//				}
//			}
//			else
//			{
//				Advertisement.Initialize("2582913");
//			}
//		}
//		Advertisement.IsReady();
//		AD_time = 0f;
//	}

//	public void HandleShowResult(ShowResult result)
//	{
//		switch (result)
//		{
//		case ShowResult.Finished:
//			Debug.Log("Video completed - Offer a reward to the player");
//			switch (reward_ID)
//			{
//			case 0:
//				UI_Master.me.fairybox_POPUP.REWARD_GET(true);
//				break;
//			case 1:
//				UI_Master.me.fairybox_POPUP.REWARD_GET(false);
//				break;
//			case 2:
//				Main_Player.me.Set_AutoShot(true);
//				break;
//			case 3:
//				Main_Player.me.Set_Mineral_Bonus(true);
//				break;
//			case 4:
//				Now_Data.me.GoldChange(UI_Master.me.REST_Mineral_Value * 2);
//				UI_Master.me.Close_Popup();
//				UI_Master.me.BOX_REWARD();
//				break;
//			case 5:
//				UI_Master.me.Speed_Double_Sample_REAL();
//				break;
//			}
//			break;
//		case ShowResult.Skipped:
//			Debug.LogWarning("Video was skipped - Do NOT reward the player");
//			break;
//		case ShowResult.Failed:
//			Debug.LogError("Video failed to show");
//			break;
//		}
//	}
//}
