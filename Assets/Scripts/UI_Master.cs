using System.Collections;
using DG.Tweening;
using Keiwando.BigInteger;
using UnityEngine;

public class UI_Master : MonoBehaviour
{
	public GameObject GOLD_Obj;

	public UILabel GOLD_label;

	public UILabel Get_GOLD_label;

	public UILabel CRYSTAL_label;

	public UILabel MEDAL_label;

	public UILabel P_STONE_label;

	public GameObject Event_Candy_Alram;

	public UILabel Misile_DMG;

	public UILabel Unit_DPS;

	public UILabel Stage_label;

	private float check;

	private float check_limit = 0.5f;

	public static UI_Master me;

	public GameObject GameOver_FADE;

	public GameObject Dropport_FADE;

	public GameObject FadeOut;

	public GameObject FadeIN;

	public GameObject GameOver_txt;

	public Misile_Popup Misile_Upgrade_Popup;

	public Skill_Popup skill_Popup;

	public Unit_Explain_panel unit_Explain_panel;

	public Artifact_Panel artifact_Panel;

	public Dungeon_Popup dungeon_Popup;

	public Shop_Panel shop_Panel;

	public UILabel Warining_MSG;

	public GameObject Warining_OBJ;

	public UILabel Good_MSG_Label;

	public GameObject Good_MSG_OBJ;

	public Skill_Use_BTN[] skill_Use_BTNs;

	public ScreenChange_by_ratio screenChange_by_ratio;

	public Fairybox_POPUP fairybox_POPUP;

	public GameObject Cheat_popup;

	public Sub_Quest_UI[] sub_quest_ui;

	public GameObject Up_BTN;

	public GameObject MainIcon_Set;

	public GameObject Skill_Icon_Set;

	public GameObject Top_Menu_Set;

	public Arch_Popup arch_Popup;

	public GameObject PAUSE_POPUP;

	public GameObject RANKING_BTN;

	public UI2DSprite get_parts_sprite;

	public GameObject PAUSE_BTN;

	public Dungeon_Clear_Popup Dungeon_Clear_Popup;

	public Misile_Parts_Manager misile_Parts_Manager;

	public NEW_Misile new_misile;

	public Box_Open_Panel box_Open_Panel;

	public GameObject Go_Shop_panel_Uranium;

	public GameObject Go_Shop_panel_Hellstone;

	public NEW_Misile Upgrade_Missile_Popup;

	public Info_Popup info_popup;

	public GameObject Bazuka_Alram;

	public GameObject Misile_Alram;

	public GameObject Marine_Alram;

	public GameObject Dungeon_Alram;

	public GameObject Artifact_Alram;

	public GameObject PAUSE_Alram;

	public GameObject PORTAL_PARTS_Alram;

	public GameObject Arch_Alram;

	public Time_attack_Panel time_attack_Panel;

	public Event_Shop_Panel event_Shop_Panel;

	public GameObject[] popups;

	public GameObject SelectedTap_Icon;

	public UISprite[] Main_taps;

	public Museum_Popup museum_Popup;

	public Language_Popup language_Popup;

	public UISprite BGM_Sprite;

	public UISprite SOUND_Sprite;

	public GameObject Quest_Change_Popup;

	public int Change_taret_Subquest_Slot;

	public GameObject Start_Screen;

	private bool STARTING;

	public GameObject[] Cartoons;

	public GameObject Box_BTN;

	public UI2DSprite Last_Box_sprite;

	public UILabel Box_Count_Label;

	public UISprite skill_EF_sprite;

	public UILabel skill_EF_label;

	public Uranium_Gift_Popup uranium_Gift_Popup;

	public BigInteger REST_Mineral_Value;

	public UILabel REST_Mineral_Value_Label;

	public GameObject Rest_Mineral_Popup;

	public int LastTime;

	public int REWARD_BOX_ID;

	public GameObject REVIEW_ASK_Popup;

	public GameObject Speed_Popup;

	public GameObject Sample_BTN;

	public GameObject DOUBLE_BTN;

	public GameObject Triple_BTN;

	private int Possible_Speed;

	public UISprite Speed_Sprite;

	public UILabel Speed_Label;

	public GameObject Speed_BTN;

	public GameObject QUIT_Panel;

	public void Awake()
	{
		me = this;
		Screen.sleepTimeout = -1;
	}

	public void CHEAT_OPEN()
	{
		SoundManager.me.Click();
		Popup(Cheat_popup);
	}

	public void ALL_CHEAT()
	{
		Tutorial_Manager.me.gameObject.SetActive(false);
		Now_Data.me.GoldChange(new BigInteger("99999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999"));
		Now_Data.me.CRYSTAL_Change(new BigInteger("1000000"));
		Now_Data.me.MEDAL_Change(new BigInteger("1000000"));
		Now_Data.me.P_STONE_Change(new BigInteger("1000000"));
	}

	public void BAZUKA()
	{
		for (int i = 0; i < Now_Data.me.Bazuka_Possible.Length; i++)
		{
			Now_Data.me.Bazuka_Possible[i] = 1;
		}
	}

	public void GOLD_B()
	{
		Now_Data.me.GoldChange(new BigInteger("1000000"));
	}

	public void GOLD_C()
	{
		Now_Data.me.CRYSTAL_Change(new BigInteger("10000"));
	}

	public void GOLD_D()
	{
		Now_Data.me.MEDAL_Change(new BigInteger("10000"));
	}

	public void GOLD_E()
	{
		Now_Data.me.P_STONE_Change(new BigInteger("10000"));
	}

	public void RESET()
	{
		Fight_Master.me.Stage_Close();
		Fight_Master.me.stage_Background[0].SetActive(true);
		Now_Data.me.PASWORD_KI_A = 9;
		Security.SetInt("PASWORD_KI_A", 9);
		Now_Data.me.PASWORD_KI_B = 8;
		Security.SetInt("PASWORD_KI_B", 8);
		Now_Data.me.PASWORD_KI_C = 7;
		Security.SetInt("PASWORD_KI_C", 7);
		Now_Data.me.PASWORD_KI_D = 6;
		Security.SetInt("PASWORD_KI_D", 6);
		Now_Data.me.GOLD = 9;
		Now_Data.me.CRYSTAL = 8;
		Now_Data.me.MEDAL = 7;
		Now_Data.me.P_STONE = 6;
		Now_Data.me.GoldChange(0);
		Now_Data.me.CRYSTAL_Change(0);
		Now_Data.me.MEDAL_Change(0);
		Now_Data.me.P_STONE_Change(0);
		Now_Data.me.LV = 10;
		Security.SetInt("LV", 10);
		Now_Data.me.BEST_LV = 10;
		Security.SetInt("BEST_LV", 10);
		Now_Data.me.NOW_Theme = Monster_DB.me.stage_DB[1].Theme;
		Now_Data.me.LV_Misile = 1;
		Security.SetInt("LV_Misile", 1);
		for (int i = 0; i < Now_Data.me.Misile_TIER.Length; i++)
		{
			Now_Data.me.Misile_TIER[i] = 0;
			Security.SetInt(string.Format("Misile_TIER_{0:000}", i), 0);
		}
		Now_Data.me.Misile_TIER[0] = 1;
		Security.SetInt(string.Format("Misile_TIER_{0:000}", 0), 1);
		for (int j = 0; j < Now_Data.me.EQUIP_MISILEs.Length; j++)
		{
			Now_Data.me.EQUIP_MISILEs[j] = 0;
			Security.SetInt(string.Format("EQUIP_MISILEs_{0:000}", j), 0);
		}
		Now_Data.me.EQUIP_MISILEs[0] = 0;
		Security.SetInt(string.Format("EQUIP_MISILEs_{0:000}", 0), 0);
		Now_Data.me.EQUIP_MISILEs[1] = -1;
		Security.SetInt(string.Format("EQUIP_MISILEs_{0:000}", 1), -1);
		Now_Data.me.EQUIP_MISILEs[2] = -1;
		Security.SetInt(string.Format("EQUIP_MISILEs_{0:000}", 2), -1);
		for (int k = 0; k < Now_Data.me.Misile_Parts.Length; k++)
		{
			Now_Data.me.Misile_Parts[k] = 0;
			Security.SetInt(string.Format("Misile_Parts_{0:000}", k), 0);
		}
		for (int l = 0; l < Now_Data.me.Bazuka_Parts.Length; l++)
		{
			Now_Data.me.Bazuka_Parts[l] = 0;
			Security.SetInt(string.Format("Bazuka_Parts_{0:000}", l), 0);
		}
		for (int m = 0; m < Now_Data.me.Active_Skill_LV.Length; m++)
		{
			Now_Data.me.Active_Skill_LV[m] = 0;
			Security.SetInt(string.Format("Active_Skill_LV_{0:000}", m), 0);
		}
		for (int n = 0; n < Now_Data.me.Now_Unit_LV.Length; n++)
		{
			Now_Data.me.Now_Unit_LV[n] = 0;
			Security.SetInt(string.Format("Now_Unit_LV_{0:000}", n), 0);
			Now_Data.me.Now_Unit_Tier[n] = 0;
			Security.SetInt(string.Format("Now_Unit_Tier_{0:000}", n), 0);
		}
		for (int num = 0; num < Now_Data.me.Now_Artifact_LV.Length; num++)
		{
			Now_Data.me.Now_Artifact_LV[num] = 0;
			Security.SetInt(string.Format("Now_Artifact_LV_{0:000}", num), 0);
		}
		for (int num2 = 0; num2 < Now_Data.me.Diablo_Artifact_LV.Length; num2++)
		{
			Now_Data.me.Diablo_Artifact_LV[num2] = 0;
			Security.SetInt(string.Format("Diablo_Artifact_LV_{0:000}", num2), 0);
		}
		for (int num3 = 0; num3 < Now_Data.me.Diablo_Artifact_Parts_count.Length; num3++)
		{
			Now_Data.me.Diablo_Artifact_Parts_count[num3] = 0;
			Security.SetInt(string.Format("Diablo_Artifact_Parts_count_{0:000}", num3), 0);
		}
		for (int num4 = 0; num4 < Now_Data.me.Building_LV.Length; num4++)
		{
			Now_Data.me.Building_LV[num4] = 0;
			Security.SetInt(string.Format("Building_LV_{0:000}", num4), 0);
		}
		for (int num5 = 0; num5 < Now_Data.me.Buidling_Working_time_Check.Length; num5++)
		{
			Now_Data.me.Buidling_Working_time_Check[num5] = 0f;
			Security.SetInt(string.Format("Buidling_Working_time_Check_{0:000}", num5), 0);
		}
		for (int num6 = 0; num6 < Now_Data.me.Building_Point.Length; num6++)
		{
			Now_Data.me.Building_Point[num6] = 0;
			Security.SetInt(string.Format("Building_Point_{0:000}", num6), 0);
		}
		for (int num7 = 0; num7 < Now_Data.me.BOX_Count.Length; num7++)
		{
			Now_Data.me.BOX_Count[num7] = 0;
			Security.SetInt(string.Format("BOX_Count_{0:000}", num7), 0);
		}
		for (int num8 = 0; num8 < Now_Data.me.portal_Parts.Length; num8++)
		{
			for (int num9 = 0; num9 < Now_Data.me.portal_Parts[num8].PORTAL_Parts_count.Length; num9++)
			{
				Now_Data.me.portal_Parts[num8].PORTAL_Parts_count[num9] = 0;
				Security.SetInt(string.Format("Portal_{0:000}_Parts_{1:000}_Count", num8, num9), 0);
			}
		}
		for (int num10 = 0; num10 < Now_Data.me.Archievement_LV.Length; num10++)
		{
			Now_Data.me.Archievement_LV[num10] = 0;
			Security.SetInt(string.Format("Archievement_LV_{0:000}", num10), 0);
		}
		Now_Data.me.Bazuka_ID = 0;
		Security.SetInt(string.Format("Bazuka_ID"), 0);
		for (int num11 = 1; num11 < Now_Data.me.Bazuka_Possible.Length; num11++)
		{
			Now_Data.me.Bazuka_Possible[num11] = 0;
			Security.SetInt(string.Format("Bazuka_Possible_{0:000}", num11), 0);
		}
		Now_Data.me.NOW_Subquest_A_ID = 0;
		Security.SetInt(string.Format("NOW_Subquest_A_ID"), 0);
		Now_Data.me.NOW_Subquest_B_ID = 0;
		Security.SetInt(string.Format("NOW_Subquest_B_ID"), 0);
		sub_quest_ui[0].gameObject.SetActive(false);
		sub_quest_ui[1].gameObject.SetActive(false);
		Now_Data.me.Arch_Star = 0;
		Security.SetInt(string.Format("Arch_Star"), 0);
		Now_Data.me.ALL_TAP = 0;
		Security.SetString(string.Format("ALL_TAP"), "0");
		Now_Data.me.ALL_MINERAL = 0;
		Security.SetString(string.Format("ALL_MINERAL"), "0");
		Now_Data.me.ALL_GAS = 0;
		Security.SetString(string.Format("ALL_GAS"), "0");
		Now_Data.me.ALL_P_STONE = 0;
		Security.SetString(string.Format("ALL_P_STONE"), "0");
		Now_Data.me.ALL_PLAYTIME = 0;
		Security.SetString(string.Format("ALL_PLAYTIME"), "0");
		Now_Data.me.DIABLO_LAPTIME = 0f;
		Security.SetFloat("DIABLO_LAPTIME", Now_Data.me.DIABLO_LAPTIME);
		Now_Data.me.ALL_KILL = 0;
		Security.SetString(string.Format("ALL_KILL"), "0");
		Now_Data.me.ALL_BOSSKILL = 0;
		Security.SetString(string.Format("ALL_BOSSKILL"), "0");
		Now_Data.me.ALL_DUNGEON_CLEAR = 0;
		Security.SetString(string.Format("ALL_DUNGEON_CLEAR"), "0");
		Now_Data.me.ALL_SKILL_USE = 0;
		Security.SetString(string.Format("ALL_SKILL_USE"), "0");
		Now_Data.me.ALL_FAIRY = 0;
		Security.SetString(string.Format("ALL_FAIRY"), "0");
		Now_Data.me.ALL_DROPPORT = 0;
		Security.SetString(string.Format("ALL_DROPPORT"), "0");
		Now_Data.me.ALL_PROMOTE = 0;
		Security.SetInt(string.Format("ALL_PROMOTE"), 0);
		Now_Data.me.ALL_HIDDEN = 0;
		Security.SetInt(string.Format("ALL_HIDDEN"), 0);
		for (int num12 = 0; num12 < Now_Data.me.HIDDEN.Length; num12++)
		{
			Now_Data.me.HIDDEN[num12] = 0;
			Security.SetInt(string.Format("HIDDEN_{0:000}", num12), 0);
		}
		Now_Data.me.NOW_TAP = 0;
		Security.SetInt(string.Format("NOW_TAP"), 0);
		Now_Data.me.NOW_KILL = 0;
		Security.SetInt(string.Format("NOW_KILL"), 0);
		Now_Data.me.NOW_BOSSKILL = 0;
		Security.SetInt(string.Format("NOW_BOSSKILL"), 0);
		Now_Data.me.NOW_PLAYTIME = 0;
		Security.SetInt(string.Format("NOW_PLAYTIME"), 0);
		Now_Data.me.NOW_SKILL_USE = 0;
		Security.SetInt(string.Format("NOW_SKILL_USE"), 0);
		Now_Data.me.NOW_FARIY = 0;
		Security.SetInt(string.Format("NOW_FARIY"), 0);
		Now_Data.me.NOW_DROPPORT = 0;
		Security.SetInt(string.Format("NOW_DROPPORT"), 0);
		Now_Data.me.Autoshot_EXP = 0;
		Security.SetInt(string.Format("Autoshot_EXP"), 0);
		Now_Data.me.Autoshot_LV = 0;
		Security.SetInt(string.Format("Autoshot_LV"), 0);
		Now_Data.me.MINERALBOOM_EXP = 0;
		Security.SetInt(string.Format("MINERALBOOM_EXP"), 0);
		Now_Data.me.MINERALBOOM_LV = 0;
		Security.SetInt(string.Format("MINERALBOOM_LV"), 0);
		Now_Data.me.NOW_DUNGEON_CHAPTER = 1;
		Security.SetInt(string.Format("NOW_DUNGEON_CHAPTER"), 1);
		Now_Data.me.NOW_DUNGEON_LV = 0;
		Security.SetInt(string.Format("NOW_DUNGEON_LV"), 0);
		Now_Data.me.Booster_LOCKON = 0;
		Security.SetInt(string.Format("Booster_LOCKON"), 0);
		Now_Data.me.Booster_MINERAL_MINE = 0;
		Security.SetInt(string.Format("Booster_MINERAL_MINE"), 0);
		Now_Data.me.CLEAR_QUEST = 0;
		Security.SetInt(string.Format("CLEAR_QUEST"), 0);
		Security.SetInt("IAP_GameSpeed", 0);
		if (Tutorial_Manager.me != null)
		{
			Tutorial_Manager.me.Tutorial_Step = 0;
		}
		Security.SetInt("Tutorial_Step", 0);
		Tutorial_Manager.me.Tutorial_IMGs[7].SetActive(true);
		Tutorial_Manager.me.Missile_LOCK.SetActive(true);
		Tutorial_Manager.me.Marine_LOCK.SetActive(true);
		Tutorial_Manager.me.Dungeon_LOCK.SetActive(true);
		Tutorial_Manager.me.Artifact_LOCK.SetActive(true);
		Now_Data.me.IAIP_Items.Clear();
		Security.SetInt(string.Format("IAIP_COUNT"), Now_Data.me.IAIP_Items.Count);
		screenChange_by_ratio.Main_size = 6f;
		screenChange_by_ratio.Setting();
		Popup_Close_All();
		Application.LoadLevel("MAIN_NEW0419");
		Fight_Master.me.All_setting();
		Fight_Master.me.Wave_Start(true);
	}

	public void STAGE()
	{
		Now_Data.me.LV += 100;
		Fight_Master.me.Stage_Close();
		Fight_Master.me.stage_Background[0].SetActive(true);
		Fight_Master.me.Wave_Start(true);
	}

	public void STAGE_ONE()
	{
		Now_Data.me.LV++;
		if (Fight_Master.me.BOSS_FIGHT)
		{
			Now_Data.me.GoldChange(Monster_DB.me.Monster_Gold_by_LV(5, Now_Data.me.LV, true) * (int)(Now_Data.me.GOLD_BONUS_PER_ALL + Now_Data.me.GOLD_BONUS_PER_BOSS + (float)Main_Player.me.Mineral_Bonus_ING) / 100);
		}
		else
		{
			Now_Data.me.GoldChange(Monster_DB.me.Monster_Gold_by_LV(22, Now_Data.me.LV, false) * (int)(Now_Data.me.GOLD_BONUS_PER_ALL + Now_Data.me.GOLD_BONUS_PER_PORTAL + (float)Main_Player.me.Mineral_Bonus_ING) / 100);
			Now_Data.me.GoldChange(Monster_DB.me.Monster_Gold_by_LV(1, Now_Data.me.LV, false) * (int)(Now_Data.me.GOLD_BONUS_PER_ALL + Now_Data.me.GOLD_BONUS_PER_NORMAL + (float)Main_Player.me.Mineral_Bonus_ING) / 100 * 30);
		}
		Fight_Master.me.Stage_Close();
		Fight_Master.me.stage_Background[0].SetActive(true);
		Fight_Master.me.Wave_Start(true);
		Fight_Master.me.GS = GameState.Play;
	}

	public void STAGE_TEN()
	{
		for (int i = 0; i < 10; i++)
		{
			STAGE_ONE();
		}
	}

	public void AUTO_UPGRADE()
	{
		bool flag = true;
		me.Open_Marins_Popup();
		int num = me.unit_Explain_panel.unit_upgrade_BTNs.Length - 1;
		int num2 = 100;
		while (flag)
		{
			num2--;
			if (num2 > 0)
			{
				if (me.unit_Explain_panel.gameObject.activeSelf && me.unit_Explain_panel.unit_upgrade_BTNs[num].gameObject.activeSelf && me.unit_Explain_panel.unit_upgrade_BTNs[num].Upgrade_sprite.spriteName.Equals(string.Format("Btn_UpgradeMineral")))
				{
					me.unit_Explain_panel.unit_upgrade_BTNs[num].Upgrade_One();
					continue;
				}
				num--;
				if (num < 0)
				{
					me.Popup_Close_All();
					flag = false;
				}
			}
			else
			{
				me.Popup_Close_All();
				flag = false;
			}
		}
	}

	public void AUTO_MISSILE_UPGRADE()
	{
		bool flag = true;
		me.Open_Misile_Popup();
		while (flag)
		{
			if (me.Misile_Upgrade_Popup.Misile_Upgrade_Sprite.spriteName.Equals(string.Format("Btn_UpgradeMineral")))
			{
				me.Misile_Upgrade_Popup.Misile_Upgrade();
				continue;
			}
			me.Popup_Close_All();
			flag = false;
		}
	}

	public void MISILE_PARTS()
	{
		for (int i = 0; i < Now_Data.me.Misile_Parts.Length; i++)
		{
			Now_Data.me.Misile_Parts[i] += 100;
		}
	}

	public void UNIT_LVUP()
	{
		for (int i = 0; i < Now_Data.me.Now_Unit_LV.Length; i++)
		{
			Now_Data.me.Now_Unit_LV[i] += 10;
		}
	}

	public void MIS_LVUP()
	{
		Now_Data.me.LV_Misile += 100;
		Fight_Master.me.All_setting();
	}

	public void Time_Scale_CHANGE()
	{
		if (Time.timeScale < 3f)
		{
			Time.timeScale = 3f;
			Now_Data.me.Changed_GameSpeed = 3f;
			Good_MSG("3배속");
		}
		else if (Time.timeScale.Equals(3f))
		{
			Time.timeScale = 5f;
			Now_Data.me.Changed_GameSpeed = 5f;
			Good_MSG("5배속");
		}
		else if (Time.timeScale.Equals(5f))
		{
			Time.timeScale = 10f;
			Now_Data.me.Changed_GameSpeed = 10f;
			Good_MSG("10배속");
		}
		else if (Time.timeScale.Equals(10f))
		{
			Time.timeScale = 1f;
			Now_Data.me.Changed_GameSpeed = 1f;
			Good_MSG("일반 속도");
		}
	}

	public void Popup(GameObject target)
	{
		for (int i = 0; i < popups.Length; i++)
		{
			if (popups[i] == null)
			{
				popups[i] = target;
				break;
			}
		}
		target.SetActive(true);
		if (Fight_Master.me.GS.Equals(GameState.Play))
		{
			PAUSE();
		}
	}

	public void Popup_Close_All()
	{
		for (int i = 0; i < popups.Length; i++)
		{
			if (popups[i] != null)
			{
				popups[i].SetActive(false);
				popups[i] = null;
			}
		}
		if (SelectedTap_Icon != null)
		{
			SelectedTap_Icon.SetActive(false);
		}
		PICK_PLAY();
	}

	public void Close_Popup()
	{
		SoundManager.me.Cancel();
		for (int num = popups.Length - 1; num >= 0; num--)
		{
			if (popups[num] != null)
			{
				popups[num].SetActive(false);
				popups[num] = null;
				break;
			}
		}
		if (popups[0] == null)
		{
			PICK_PLAY();
		}
	}

	public void TAP(int Selected_Tap)
	{
		for (int i = 0; i < Main_taps.Length; i++)
		{
			Main_taps[i].color = Color.gray;
		}
		Main_taps[Selected_Tap].color = Color.white;
		if (SelectedTap_Icon != null)
		{
			SelectedTap_Icon.transform.localPosition = Main_taps[Selected_Tap].transform.parent.localPosition;
			SelectedTap_Icon.SetActive(true);
		}
	}

	public void Open_Misile_Popup()
	{
		SoundManager.me.Click();
		if (!Misile_Upgrade_Popup.gameObject.activeSelf)
		{
			Popup_Close_All();
			Misile_Upgrade_Popup.Setting();
			Popup(Misile_Upgrade_Popup.gameObject);
			TAP(0);
		}
		else
		{
			SoundManager.me.Cancel();
			Popup_Close_All();
		}
	}

	public void Open_Misile_Parts()
	{
		if (Tutorial_Manager.me.Missile_LOCK.activeSelf)
		{
			me.Warning(Localization.Get("TUTO_WARING_A"));
		}
		else if (!misile_Parts_Manager.gameObject.activeSelf)
		{
			SoundManager.me.Click();
			Popup_Close_All();
			misile_Parts_Manager.Full_Setting();
			misile_Parts_Manager.Setting(Now_Data.me.EQUIP_MISILEs[0], false);
			Popup(misile_Parts_Manager.gameObject);
			TAP(1);
		}
		else
		{
			SoundManager.me.Cancel();
			Popup_Close_All();
		}
	}

	public void Open_Marins_Popup()
	{
		SoundManager.me.Click();
		if (Tutorial_Manager.me.Marine_LOCK.activeSelf)
		{
			me.Warning(Localization.Get("TUTO_WARING_B"));
		}
		else if (!unit_Explain_panel.gameObject.activeSelf)
		{
			Popup_Close_All();
			unit_Explain_panel.Setting();
			TAP(2);
		}
		else
		{
			Popup_Close_All();
		}
	}

	public void Open_Portal_Panel()
	{
		if (Now_Data.me.BEST_LV >= 110)
		{
			if (!dungeon_Popup.gameObject.activeSelf)
			{
				SoundManager.me.Click();
				Popup_Close_All();
				dungeon_Popup.Setting();
				TAP(3);
			}
			else
			{
				SoundManager.me.Cancel();
				Popup_Close_All();
			}
		}
		else
		{
			me.Warning(Localization.Get("TUTO_WARING_C"));
		}
	}

	public void Open_Artifact_Panel()
	{
		if (Tutorial_Manager.me.Artifact_LOCK.activeSelf)
		{
			me.Warning(Localization.Get("TUTO_WARING_D"));
		}
		else if (!artifact_Panel.gameObject.activeSelf)
		{
			SoundManager.me.Click();
			Popup_Close_All();
			artifact_Panel.Setting();
			Popup(artifact_Panel.gameObject);
			TAP(4);
		}
		else
		{
			SoundManager.me.Cancel();
			Popup_Close_All();
		}
	}

	public void Open_Shop_Panel()
	{
		if (!shop_Panel.gameObject.activeSelf)
		{
			SoundManager.me.Click();
			Popup_Close_All();
			shop_Panel.Setting();
			TAP(5);
		}
		else
		{
			SoundManager.me.Cancel();
			Popup_Close_All();
		}
	}

	public void Open_Uranium_category()
	{
		if (Dungeon_Clear_Popup.gameObject.activeSelf)
		{
			Dungeon_Clear_Popup.BG.SetActive(false);
			Close_Popup();
			Dungeon_Clear_Popup.Clear_info.SetActive(false);
			SoundManager.me.Click();
			shop_Panel.Setting();
			TAP(5);
			shop_Panel.Tap_C();
		}
		else if (!shop_Panel.gameObject.activeSelf)
		{
			SoundManager.me.Click();
			Popup_Close_All();
			shop_Panel.Setting();
			TAP(5);
			shop_Panel.Tap_C();
		}
		else
		{
			Popup_Close_All();
			Open_Shop_Panel();
			shop_Panel.Tap_C();
		}
	}

	public void Open_BOX_category()
	{
		if (!shop_Panel.gameObject.activeSelf)
		{
			SoundManager.me.Click();
			Popup_Close_All();
			shop_Panel.Setting();
			TAP(5);
			shop_Panel.Tap_B();
		}
		else
		{
			Close_Popup();
			shop_Panel.Tap_B();
		}
	}

	public void Open_PACK_category()
	{
		if (Dungeon_Clear_Popup.gameObject.activeSelf)
		{
			Dungeon_Clear_Popup.BG.SetActive(false);
			Close_Popup();
			Dungeon_Clear_Popup.Clear_info.SetActive(false);
			SoundManager.me.Click();
			shop_Panel.Setting();
			TAP(5);
			shop_Panel.Tap_D();
		}
		else if (!shop_Panel.gameObject.activeSelf)
		{
			SoundManager.me.Click();
			Popup_Close_All();
			shop_Panel.Setting();
			TAP(5);
			shop_Panel.Tap_D();
		}
		else
		{
			Popup_Close_All();
			Open_Shop_Panel();
			shop_Panel.Tap_D();
		}
	}

	public void PAUSE()
	{
		if (Fight_Master.me.GS.Equals(GameState.Play))
		{
			Fight_Master.me.GS = GameState.Pause;
			Fight_Master.me.Game_Speed = 0f;
		}
		else
		{
			Fight_Master.me.GS = GameState.Play;
			Fight_Master.me.Game_Speed = 1f;
			Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		}
	}

	public void PICK_PLAY()
	{
		for (int i = 0; i < Main_taps.Length; i++)
		{
			Main_taps[i].color = Color.white;
		}
		Fight_Master.me.GS = GameState.Play;
		Fight_Master.me.Game_Speed = 1f;
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		StartCoroutine(MAKE_Back_Dealy());
	}

	public IEnumerator MAKE_Back_Dealy()
	{
		Fight_Master.me.Back_Delay = true;
		yield return new WaitForSeconds(0.5f);
		Fight_Master.me.Back_Delay = false;
	}

	public void Warning(string msg)
	{
		Good_MSG_OBJ.SetActive(false);
		Warining_MSG.text = msg;
		SoundManager.me.Warning();
		Warining_OBJ.SetActive(false);
		Warining_OBJ.SetActive(true);
	}

	public void Good_MSG(string msg)
	{
		Warining_OBJ.SetActive(false);
		Good_MSG_Label.text = msg;
		SoundManager.me.Congretu();
		Good_MSG_OBJ.SetActive(false);
		Good_MSG_OBJ.SetActive(true);
	}

	public void OPEN_Pause_Popup()
	{
		SoundManager.me.Click();
		Sound_sprite_Check();
		Popup(PAUSE_POPUP);
		if (SA_Singleton<UM_GameServiceManager>.instance.ConnectionSate == UM_ConnectionState.CONNECTED)
		{
			RANKING_BTN.SetActive(true);
		}
		else
		{
			RANKING_BTN.SetActive(false);
		}
		uranium_Gift_Popup.Setting(0);
		StartCoroutine(MAKE_Back_Dealy());
		if (Now_Data.me.IAIP_Items.Count < 1 && Now_Data.me.BEST_LV > 30 && !Now_Data.me.VIP_Version)
		{
/*			UM_Ad.me.Show_FullAD();
			UM_Ad.me.Load_FullAD();*/
		}
	}

	public void Sound_sprite_Check()
	{
		if (SoundManager.me.BGM_Volume.Equals(0f))
		{
			BGM_Sprite.spriteName = "Icon_6MusicOff";
		}
		else
		{
			BGM_Sprite.spriteName = "Icon_6MusicOn";
		}
		if (SoundManager.me.SOUND_Volume.Equals(0f))
		{
			SOUND_Sprite.spriteName = "Icon_7SFXOff";
		}
		else
		{
			SOUND_Sprite.spriteName = "Icon_7SFXOn";
		}
	}

	public void OPEN_Museum_Popup()
	{
		museum_Popup.Setting();
		uranium_Gift_Popup.Setting(1);
		SoundManager.me.Click();
	}

	public void OPEN_Arch_Popup()
	{
		SoundManager.me.Click();
		arch_Popup.Setting();
		uranium_Gift_Popup.Setting(2);
	}

	public void OPEN_Rank()
	{
		SoundManager.me.Click();
		UM_GameService.me.ReportScore(Now_Data.me.BEST_LV);
		UM_GameService.me.Show_Board();
	}

	public void OPEN_Infomation()
	{
		SoundManager.me.Click();
		info_popup.Setting();
		uranium_Gift_Popup.Setting(3);
	}

	public void OPEN_Language()
	{
		SoundManager.me.Click();
		language_Popup.Setting();
		uranium_Gift_Popup.Setting(4);
	}

	public void SOUND_Volume_Change()
	{
		uranium_Gift_Popup.Setting(6);
		SoundManager.me.SOUND_Volume_Change();
		Sound_sprite_Check();
	}

	public void BGM_Volume_Change()
	{
		uranium_Gift_Popup.Setting(5);
		SoundManager.me.BGM_Volume_Change();
		Sound_sprite_Check();
	}

	public void Change_Try_by_Ads()
	{
		sub_quest_ui[Change_taret_Subquest_Slot].Quest_ID_CHOICE();
	}

	public void Change_Try_by_Uranum()
	{
		if (Now_Data.me.MEDAL_Possible(10))
		{
			Now_Data.me.MEDAL_Change(-10);
			me.Close_Popup();
			if (Now_Data.me.CLEAR_QUEST < 6)
			{
				Now_Data.me.CLEAR_QUEST++;
				Security.SetInt(string.Format("CLEAR_QUEST"), 0);
			}
			sub_quest_ui[Change_taret_Subquest_Slot].Quest_ID_CHOICE();
		}
		else
		{
			Warning(Localization.Get("NEEDURANIUM"));
		}
	}

	public void PAUSE_BTN_RESET()
	{
		PAUSE_BTN.SetActive(false);
		PAUSE_BTN.SetActive(true);
	}

	public void Start()
	{
		Start_Screen.SetActive(true);
		MainIcon_Set.transform.localPosition = new Vector3(-330f, 200f, 0f);
		Skill_Icon_Set.transform.localPosition = new Vector3(0f, -420f, 0f);
		Top_Menu_Set.transform.localPosition = new Vector3(0f, 200f, 0f);
		SoundManager.me.BGM_Play(0);
	}

	public void Game_Start()
	{
		if (!STARTING)
		{
			FIST_PLAY_CHECK();
		}
	}

	public IEnumerator Starting()
	{
		STARTING = true;
		FadeIN.SetActive(false);
		FadeIN.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		Cartoons[0].SetActive(false);
		Start_Screen.SetActive(false);
		FadeIN.SetActive(false);
		FadeOut.SetActive(false);
		FadeOut.SetActive(true);
		Fight_Master.me.PLAY_TO_NORMAL_STAGE_Setting();
		MainIcon_Set.transform.DOLocalMoveX(54f, 1f);
		Skill_Icon_Set.transform.DOLocalMoveY(-300f, 1f);
		Top_Menu_Set.transform.DOLocalMoveY(0f, 1f);
		yield return new WaitForSeconds(0.5f);
		FadeOut.SetActive(false);
		if (Now_Data.me.IAIP_Items.Count < 1 && Now_Data.me.BEST_LV > 30 && !Now_Data.me.VIP_Version)
		{
/*			UM_Ad.me.Show_FullAD();
			UM_Ad.me.Load_FullAD();*/
		}
	}

	public void FIST_PLAY_CHECK()
	{
		SoundManager.me.Click();
		SoundManager.me.Missile_Upgrade();
		if (Now_Data.me.ALL_DROPPORT < 1 && Now_Data.me.LV < 11)
		{
			StartCoroutine(Start_Cartoon());
		}
		else
		{
			StartCoroutine(Starting());
		}
	}

	public IEnumerator Start_Cartoon()
	{
		STARTING = true;
		Cartoons[1].SetActive(false);
		Cartoons[2].SetActive(false);
		Cartoons[3].SetActive(false);
		Cartoons[4].SetActive(false);
		Cartoons[5].SetActive(false);
		Cartoons[0].SetActive(true);
		yield return new WaitForSeconds(1f);
		Start_Screen.SetActive(false);
		Cartoons[1].SetActive(true);
		yield return new WaitForSeconds(1.5f);
		Cartoons[2].SetActive(true);
		yield return new WaitForSeconds(1.5f);
		Cartoons[3].SetActive(true);
		yield return new WaitForSeconds(1.5f);
		Cartoons[4].SetActive(true);
		Cartoons[5].SetActive(true);
		yield return new WaitForSeconds(0.5f);
	}

	public void GO_GAME()
	{
		SoundManager.me.Click();
		SoundManager.me.Missile_Upgrade();
		StartCoroutine(Starting());
	}

	public void OPEN_BOX()
	{
		Cartoons[5].SetActive(false);
		box_Open_Panel.Setting(0, false);
	}

	public void Box_Checking()
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < Now_Data.me.BOX_Count.Length; i++)
		{
			num += Now_Data.me.BOX_Count[i];
			if (Now_Data.me.BOX_Count[i] > 0)
			{
				num2 = i;
			}
		}
		Last_Box_sprite.sprite2D = Sprite_DB.me.BOX_Icon[num2];
		if (num > 0)
		{
			Box_Count_Label.text = string.Format("X {0}", num);
			Box_BTN.SetActive(true);
		}
		else
		{
			Box_BTN.SetActive(false);
		}
	}

	public void Open_Uranium_Popup()
	{
		Popup(Go_Shop_panel_Uranium);
	}

	public void Open_Hellstone_Popup()
	{
		Popup(Go_Shop_panel_Hellstone);
	}

	public void Skill_Use_EF_Setting(int Id)
	{
		skill_EF_sprite.spriteName = string.Format("Btn_Skill_{0:00}", Id + 1);
		skill_EF_label.text = Localization.Get(string.Format("SKILL_{0:000}_NAME", Id));
		skill_EF_sprite.transform.parent.gameObject.SetActive(false);
		skill_EF_sprite.transform.parent.gameObject.SetActive(true);
	}

	public void Rest_Mineral_Setting()
	{
		U_time_leader.me.Time_Load();
		StartCoroutine(REST_MINERAL_POPUP_OPEN());
	}

	public IEnumerator REST_MINERAL_POPUP_OPEN()
	{
		yield return new WaitForSeconds(0.2f);
		int Fail_Count = 50;
		while (!U_time_leader.me.Time_reading)
		{
			U_time_leader.me.Time_Load();
			Fail_Count--;
			if (Fail_Count < 0)
			{
				break;
			}
			yield return new WaitForSeconds(0.2f);
		}
		if (U_time_leader.me.Time_reading)
		{
			LastTime = Time_Checker.Time_LOAD("LAST_PLAY_TIME", false);
			if (Time_Checker.DAY_LOAD("LAST_PLAY_TIME") > 0)
			{
				for (int i = 2; i < Now_Data.me.EventShopItem_Used.Length; i++)
				{
					Now_Data.me.EventShopItem_Used[i] = 0;
					Security.SetInt(string.Format("EventShopItem_Used_{0}", i), 0);
				}
			}
			REWARD_BOX_ID = 0;
			if (LastTime >= 79200)
			{
				switch (Security.GetInt("PUSH_REWARD_LV", 0))
				{
				case 0:
					REWARD_BOX_ID = 1;
					break;
				case 1:
					REWARD_BOX_ID = 2;
					break;
				case 2:
					REWARD_BOX_ID = 3;
					break;
				default:
					REWARD_BOX_ID = 4;
					break;
				}
			}
			if (LastTime > 86400)
			{
				LastTime = 86400;
			}
			if (LastTime > 300)
			{
				Time_Checker.Time_Save("LAST_PLAY_TIME");
				REST_Mineral_Value = Monster_DB.me.Monster_Gold_by_LV(1, Now_Data.me.LV, false) * (LastTime / 100);
				REST_Mineral_Value *= (BigInteger)(int)(Now_Data.me.GOLD_BONUS_PER_COMEBACK + 100f);
				REST_Mineral_Value /= (BigInteger)100;
				if (REST_Mineral_Value < 100)
				{
					REST_Mineral_Value = 100;
				}
				REST_Mineral_Value_Label.text = Now_Data.INT_to_ABC(REST_Mineral_Value);
				me.Popup(Rest_Mineral_Popup);
			}
			else if (LastTime.Equals(-999999999))
			{
				Time_Checker.Time_Save("LAST_PLAY_TIME");
			}
			if (LastTime > 0)
			{
				Now_Data.me.Buff_Time_Save(LastTime);
			}
		}
		else
		{
			Debug.Log("시간불러오기 실패. - 시간 관련된 사항 모두 제거하기.");
			Now_Data.me.Mineral_Buff_Time = 0f;
			Now_Data.me.Missile_Buff_Time = 0f;
			Now_Data.me.Cooltime_Buff_Time = 0f;
			Now_Data.me.Mineral_CART_Buff_Time = 0f;
		}
	}

	public void REST_MINERAL_GET()
	{
		Now_Data.me.GoldChange(REST_Mineral_Value);
		Close_Popup();
		BOX_REWARD();
	}

	public void REST_MINERAL_GET_by_ADS()
	{
		//if (VIDEO_ADS.me.isPossible(true))
		//{
		//	VIDEO_ADS.me.ShowRewardedVideo(4);
		//}
		//else
		//{
		//	me.Warning(Localization.Get("AD_WARNING"));
		//}
	}

	public void BOX_REWARD()
	{
		Security.SetInt("PUSH_REWARD_LV", REWARD_BOX_ID);
		switch (REWARD_BOX_ID)
		{
		case 1:
		case 4:
			Good_MSG(Localization.Get("PUSH_REWARD"));
			Now_Data.me.BOX_Count[2]++;
			box_Open_Panel.Setting(2, true);
			break;
		case 2:
			Good_MSG(Localization.Get("PUSH_REWARD"));
			Now_Data.me.BOX_Count[3]++;
			box_Open_Panel.Setting(3, true);
			break;
		case 3:
			Good_MSG(Localization.Get("PUSH_REWARD"));
			Now_Data.me.BOX_Count[7]++;
			box_Open_Panel.Setting(7, true);
			break;
		}
	}

	public void OPEN_REVIEW_PAGE()
	{
		if (Application.platform.Equals(RuntimePlatform.IPhonePlayer))
		{
			Application.OpenURL("https://itunes.apple.com/us/app/id1411133511");
		}
		else
		{
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.dd.mk");
		}
	}

	public void OPEN_TIME_ATK()
	{
		if (Now_Data.me.BEST_LV < 310)
		{
			Warning(Localization.Get("TIME_WARNING"));
			return;
		}
		SoundManager.me.Click();
		time_attack_Panel.Setting();
		Popup(time_attack_Panel.gameObject);
	}

	public void Speed_Change()
	{
		if (Now_Data.me.BEST_LV < 210)
		{
			Warning(Localization.Get("TIME_WARNING_SPEED"));
			return;
		}
		Possible_Speed = Security.GetInt("IAP_GameSpeed", 0);
		if (Security.GetInt("SPEED_SAMPLE", 0).Equals(0))
		{
			Sample_BTN.SetActive(true);
		}
		else
		{
			Sample_BTN.SetActive(false);
		}
		if (Possible_Speed.Equals(0))
		{
			Popup(Speed_Popup);
		}
		else
		{
			Time_Scale_CHANGE_BY_IAPP();
		}
	}

	public void Time_Scale_CHANGE_BY_IAPP()
	{
		Possible_Speed = Security.GetInt("IAP_GameSpeed", 0);
		if (Possible_Speed.Equals(1))
		{
			if (Now_Data.me.IAP_GameSpeed.Equals(1f))
			{
				Now_Data.me.IAP_GameSpeed = 0f;
				Fight_Master.me.All_setting();
				Good_MSG(Localization.Get("SPEED_X1"));
				Speed_Sprite.spriteName = "AD_BuffSpeed1";
				Speed_Label.text = "X 1";
			}
			else
			{
				Now_Data.me.IAP_GameSpeed = 1f;
				Fight_Master.me.All_setting();
				Good_MSG(Localization.Get("SPEED_X2"));
				Speed_Sprite.spriteName = "AD_BuffSpeed2";
				Speed_Label.text = "X 2";
			}
		}
		else if (Now_Data.me.IAP_GameSpeed.Equals(0f))
		{
			Now_Data.me.IAP_GameSpeed = 1f;
			Fight_Master.me.All_setting();
			Good_MSG(Localization.Get("SPEED_X2"));
			Speed_Sprite.spriteName = "AD_BuffSpeed2";
			Speed_Label.text = "X 2";
		}
		else if (Now_Data.me.IAP_GameSpeed.Equals(1f))
		{
			Now_Data.me.IAP_GameSpeed = 2f;
			Fight_Master.me.All_setting();
			Good_MSG(Localization.Get("SPEED_X3"));
			Speed_Sprite.spriteName = "AD_BuffSpeed3";
			Speed_Label.text = "X 3";
		}
		else
		{
			Now_Data.me.IAP_GameSpeed = 0f;
			Fight_Master.me.All_setting();
			Good_MSG(Localization.Get("SPEED_X1"));
			Speed_Sprite.spriteName = "AD_BuffSpeed1";
			Speed_Label.text = "X 1";
		}
	}

	public void Speed_Double_Sample()
	{
		//VIDEO_ADS.me.ShowRewardedVideo(5);
	}

	public void Speed_Double_Sample_REAL()
	{
		Security.SetInt("SPEED_SAMPLE", 1);
		Popup_Close_All();
		Now_Data.me.IAP_GameSpeed = 1f;
		Fight_Master.me.All_setting();
		Good_MSG(Localization.Get("SPEED_X2"));
		Speed_Sprite.spriteName = "AD_BuffSpeed2";
		Speed_Label.text = "X 2";
		StartCoroutine(Speed_Double_Sample_CO());
	}

	public IEnumerator Speed_Double_Sample_CO()
	{
		yield return new WaitForSeconds(360f);
		Now_Data.me.IAP_GameSpeed = 0f;
		Fight_Master.me.All_setting();
		Good_MSG(Localization.Get("SPEED_X1"));
		Speed_Sprite.spriteName = "AD_BuffSpeed1";
		Speed_Label.text = "X 1";
	}

	public void REAL_QUIT()
	{
		Application.Quit();
	}
}
