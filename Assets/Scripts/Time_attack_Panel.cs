using UnityEngine;

public class Time_attack_Panel : MonoBehaviour
{
	public UILabel Goal_Label;

	public UILabel Time_Label;

	public UISlider Time_Slider;

	public Reward_Icon[] reward_Icons;

	public GameObject[] Lock_img;

	public UILabel[] goaltime_labels;

	private int NOW_GOAL_DIABLO;

	private float NOW_DIABLOR_RECORD;

	public void Setting()
	{
		NOW_GOAL_DIABLO = (Now_Data.me.LV - 10) / 1000 + 1;
		NOW_DIABLOR_RECORD = Security.GetFloat(string.Format("DIABLO_{0:000}_RECORD", NOW_GOAL_DIABLO), -1f);
		Goal_Label.text = string.Format("{0} {1} {2}", Localization.Get("TIME_ATK_GOAL_A"), NOW_GOAL_DIABLO * 1000, Localization.Get("TIME_ATK_GOAL_B"));
		Time_Label.text = string.Format("{0} {1}", Localization.Get("REMAIN_TIME"), Time_Checker.ShowTime_Label_noT(Now_Data.me.DIABLO_LAPTIME));
		Time_Slider.value = Now_Data.me.DIABLO_LAPTIME / 36000f;
		reward_Icons[0].Setting_Box(Dungeon_DB.me.dungeon_DB_for_REWARD[NOW_GOAL_DIABLO].REWARD_A_ID, Dungeon_DB.me.dungeon_DB_for_REWARD[NOW_GOAL_DIABLO].REWARD_A_Value);
		reward_Icons[1].Setting_Box(Dungeon_DB.me.dungeon_DB_for_REWARD[NOW_GOAL_DIABLO].REWARD_B_ID, Dungeon_DB.me.dungeon_DB_for_REWARD[NOW_GOAL_DIABLO].REWARD_B_Value);
		reward_Icons[2].Setting_Box(Dungeon_DB.me.dungeon_DB_for_REWARD[NOW_GOAL_DIABLO].REWARD_C_ID, Dungeon_DB.me.dungeon_DB_for_REWARD[NOW_GOAL_DIABLO].REWARD_C_Value);
		reward_Icons[3].Setting_Box(Dungeon_DB.me.dungeon_DB_for_REWARD[NOW_GOAL_DIABLO].REWARD_D_ID, Dungeon_DB.me.dungeon_DB_for_REWARD[NOW_GOAL_DIABLO].REWARD_D_Value);
		reward_Icons[4].Setting_Box(Dungeon_DB.me.dungeon_DB_for_REWARD[NOW_GOAL_DIABLO].REWARD_E_ID, Dungeon_DB.me.dungeon_DB_for_REWARD[NOW_GOAL_DIABLO].REWARD_E_Value);
		Lock_img[0].SetActive(false);
		Lock_img[1].SetActive(false);
		Lock_img[2].SetActive(false);
		Lock_img[3].SetActive(false);
		Lock_img[4].SetActive(false);
		goaltime_labels[0].text = string.Format("{0} 1 {1}", Localization.Get("IN_CLEAR_A"), Localization.Get("IN_CLEAR_B"));
		goaltime_labels[1].text = string.Format("{0} 2 {1}", Localization.Get("IN_CLEAR_A"), Localization.Get("IN_CLEAR_B"));
		goaltime_labels[2].text = string.Format("{0} 4 {1}", Localization.Get("IN_CLEAR_A"), Localization.Get("IN_CLEAR_B"));
		goaltime_labels[3].text = string.Format("{0} 8 {1}", Localization.Get("IN_CLEAR_A"), Localization.Get("IN_CLEAR_B"));
		goaltime_labels[4].text = string.Format("{0} 24 {1}", Localization.Get("IN_CLEAR_A"), Localization.Get("IN_CLEAR_B"));
		if (!(Now_Data.me.DIABLO_LAPTIME < Now_Data.me.DIABLO_LAPTIME_LV[0]))
		{
			if (Now_Data.me.DIABLO_LAPTIME < Now_Data.me.DIABLO_LAPTIME_LV[1])
			{
				Lock_img[0].SetActive(true);
				goaltime_labels[0].text = Localization.Get("GET_FAIL");
			}
			else if (Now_Data.me.DIABLO_LAPTIME < Now_Data.me.DIABLO_LAPTIME_LV[2])
			{
				Lock_img[0].SetActive(true);
				Lock_img[1].SetActive(true);
				goaltime_labels[0].text = Localization.Get("GET_FAIL");
				goaltime_labels[1].text = Localization.Get("GET_FAIL");
			}
			else if (Now_Data.me.DIABLO_LAPTIME < Now_Data.me.DIABLO_LAPTIME_LV[3])
			{
				Lock_img[0].SetActive(true);
				Lock_img[1].SetActive(true);
				Lock_img[2].SetActive(true);
				goaltime_labels[0].text = Localization.Get("GET_FAIL");
				goaltime_labels[1].text = Localization.Get("GET_FAIL");
				goaltime_labels[2].text = Localization.Get("GET_FAIL");
			}
			else
			{
				Lock_img[0].SetActive(true);
				Lock_img[1].SetActive(true);
				Lock_img[2].SetActive(true);
				Lock_img[3].SetActive(true);
				goaltime_labels[0].text = Localization.Get("GET_FAIL");
				goaltime_labels[1].text = Localization.Get("GET_FAIL");
				goaltime_labels[2].text = Localization.Get("GET_FAIL");
				goaltime_labels[3].text = Localization.Get("GET_FAIL");
			}
		}
		if (NOW_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[0] && NOW_DIABLOR_RECORD > 0f)
		{
			Lock_img[0].SetActive(true);
			Lock_img[1].SetActive(true);
			Lock_img[2].SetActive(true);
			Lock_img[3].SetActive(true);
			Lock_img[4].SetActive(true);
			goaltime_labels[0].text = Localization.Get("GET_ALREADY");
			goaltime_labels[1].text = Localization.Get("GET_ALREADY");
			goaltime_labels[2].text = Localization.Get("GET_ALREADY");
			goaltime_labels[3].text = Localization.Get("GET_ALREADY");
			goaltime_labels[4].text = Localization.Get("GET_ALREADY");
		}
		else if (NOW_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[1] && NOW_DIABLOR_RECORD > 0f)
		{
			Lock_img[1].SetActive(true);
			Lock_img[2].SetActive(true);
			Lock_img[3].SetActive(true);
			Lock_img[4].SetActive(true);
			goaltime_labels[1].text = Localization.Get("GET_ALREADY");
			goaltime_labels[2].text = Localization.Get("GET_ALREADY");
			goaltime_labels[3].text = Localization.Get("GET_ALREADY");
			goaltime_labels[4].text = Localization.Get("GET_ALREADY");
		}
		else if (NOW_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[2] && NOW_DIABLOR_RECORD > 0f)
		{
			Lock_img[2].SetActive(true);
			Lock_img[3].SetActive(true);
			Lock_img[4].SetActive(true);
			goaltime_labels[2].text = Localization.Get("GET_ALREADY");
			goaltime_labels[3].text = Localization.Get("GET_ALREADY");
			goaltime_labels[4].text = Localization.Get("GET_ALREADY");
		}
		else if (NOW_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[3] && NOW_DIABLOR_RECORD > 0f)
		{
			Lock_img[3].SetActive(true);
			Lock_img[4].SetActive(true);
			goaltime_labels[3].text = Localization.Get("GET_ALREADY");
			goaltime_labels[4].text = Localization.Get("GET_ALREADY");
		}
		else if (NOW_DIABLOR_RECORD > 0f)
		{
			Lock_img[4].SetActive(true);
			goaltime_labels[4].text = Localization.Get("GET_ALREADY");
		}
	}

	public void Retry()
	{
		SoundManager.me.Click();
		UI_Master.me.Good_MSG(Localization.Get("RETRY_MSG"));
		UI_Master.me.Popup_Close_All();
		UI_Master.me.Open_Misile_Popup();
	}
}
