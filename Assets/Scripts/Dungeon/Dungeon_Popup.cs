using System.Collections;
using Keiwando.BigInteger;
using UnityEngine;

public class Dungeon_Popup : MonoBehaviour
{
	public GameObject DropPort_ASK_Popup;

	public GameObject DUNGEON_Intro_Popup;

	public Buidling_Popup Building_Intro_Popup;

	public Map_Icon[] Map_Icons;

	public UILabel Now_Dungeon_Chapter_label;

	public UILabel Selected_Stage_ID;

	public UILabel Selected_Stage_Explain;

	public GameObject[] Dungeon_Type_Icons;

	public GameObject GO_BTN;

	public GameObject Already_Clear_BTN;

	public GameObject REWARD_GOURP;

	public UI2DSprite Reward_Building;

	public UILabel Reward_Building_label;

	public UILabel Reward_P_Stone_label;

	public UI2DSprite Reward_BOX_Portal;

	public UI2DSprite Reward_BOX;

	public UILabel Reward_BOX_label;

	private int Target_BOX;

	private int Target_Builing_ID;

	public UI2DSprite Reward_Diablo_sprite;

	public UISprite Reward_PStone_sprite;

	public BigInteger Gas_by_Stage;

	public BigInteger Gas_by_Unit;

	public BigInteger Gas_by_Artifact;

	public BigInteger Total_Get_Gas;

	public UILabel label_Gas_by_Stage;

	public UILabel label_Gas_by_Unit;

	public UILabel label_Gas_by_Artifact;

	public UILabel label_Total_Get_Gas;

	public Color[] BG_Color;

	public UI2DSprite[] Map_BGs;

	public UIPanel Menu_Panel;

	public UIScrollView uiScrollView;

	public GameObject Alram;

	public Vector3 offset_B;

	public int Price_Double;

	public UILabel Price_Double_Label;

	public IEnumerator Snap()
	{
		yield return new WaitForEndOfFrame();
		MinimapSnap();
	}

	public void MinimapSnap()
	{
		offset_B.x = 300f;
		offset_B.y = -116f;
		offset_B.z = 0f;
		SpringPanel.Begin(Menu_Panel.cachedGameObject, offset_B, 6f);
	}

	public void SNAP_to_Building()
	{
		StartCoroutine(Snap());
	}

	public void Setting()
	{
		DUNGEON_Intro_Popup.SetActive(false);
		Building_Intro_Popup.gameObject.SetActive(false);
		for (int i = 0; i < Map_Icons.Length; i++)
		{
			Map_Icons[i].Dungeon_ID = i;
			Map_Icons[i].Setting();
		}
		Alram.transform.parent = Map_Icons[Now_Data.me.NOW_DUNGEON_LV].transform;
		Alram.transform.localPosition = new Vector3(0f, 0f, 0f);
		Now_Dungeon_Chapter_label.text = string.Format("AREA {0}", Now_Data.me.NOW_DUNGEON_CHAPTER);
		UI_Master.me.Popup(base.gameObject);
		for (int j = 0; j < Building_Intro_Popup.building_Icons.Length; j++)
		{
			Building_Intro_Popup.building_Icons[j].Setting();
		}
		int num = (Now_Data.me.NOW_DUNGEON_CHAPTER - 1) % 6;
		for (int k = 0; k < Map_BGs.Length; k++)
		{
			Map_BGs[k].color = BG_Color[num];
		}
	}

	public void DUNGEON_Intro_Popup_Setting(int ID)
	{
		Now_Data.me.NOW_Selected_Dungeon = ID;
		Selected_Stage_ID.text = Localization.Get(string.Format("DUNGEON_TYPE_{0:000}", Dungeon_DB.me.dungeon_DB[ID].Dungeon_TYPE));
		for (int i = 1; i < Dungeon_Type_Icons.Length; i++)
		{
			Dungeon_Type_Icons[i].SetActive(false);
		}
		if (Dungeon_DB.me.dungeon_DB[ID].Dungeon_TYPE != 0)
		{
			Dungeon_Type_Icons[Dungeon_DB.me.dungeon_DB[ID].Dungeon_TYPE].SetActive(true);
		}
		if (ID < Now_Data.me.NOW_DUNGEON_LV)
		{
			GO_BTN.SetActive(false);
			Already_Clear_BTN.SetActive(true);
			Selected_Stage_ID.text = Localization.Get("CLEAR_NAME");
			Selected_Stage_Explain.text = Localization.Get("CLEAR_WORD");
			Selected_Stage_Explain.gameObject.SetActive(true);
			for (int j = 1; j < Dungeon_Type_Icons.Length; j++)
			{
				Dungeon_Type_Icons[j].SetActive(false);
			}
			Already_Clear_BTN.SetActive(true);
			REWARD_GOURP.SetActive(false);
		}
		else
		{
			GO_BTN.SetActive(true);
			Already_Clear_BTN.SetActive(false);
			Selected_Stage_Explain.gameObject.SetActive(false);
			REWARD_GOURP.SetActive(true);
			Target_Builing_ID = Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].Building_TYPE;
			Reward_Building.sprite2D = Sprite_DB.me.Map_Icon[Target_Builing_ID];
		}
		DUNGEON_Intro_Popup.SetActive(false);
		DUNGEON_Intro_Popup.SetActive(true);
		Building_Intro_Popup.gameObject.SetActive(false);
	}

	public void DUNGEON_Intro_P_Stone()
	{
		Now_Data.me.NOW_Selected_Dungeon = -1;
		Selected_Stage_ID.text = Localization.Get(string.Format("DUNGEON_TYPE_{0:000}", 5));
		Selected_Stage_Explain.text = Localization.Get(string.Format("DUNGEON_TYPE_EXPLAIN_{0:000}", 5));
		for (int i = 1; i < Dungeon_Type_Icons.Length; i++)
		{
			Dungeon_Type_Icons[i].SetActive(false);
		}
		Dungeon_Type_Icons[5].SetActive(true);
		GO_BTN.SetActive(true);
		Already_Clear_BTN.SetActive(false);
		DUNGEON_Intro_Popup.SetActive(false);
		DUNGEON_Intro_Popup.SetActive(true);
		Building_Intro_Popup.gameObject.SetActive(false);
		REWARD_GOURP.SetActive(false);
		Selected_Stage_Explain.text = Localization.Get("HELL_DUNGEON_WORD");
		Selected_Stage_Explain.gameObject.SetActive(true);
	}

	public void PLAY_TO_DUNGEON()
	{
		UI_Master.me.Popup_Close_All();
		Fight_Master.me.PLAY_TO_Dungeon_Setting();
	}

	public void Already_Gone()
	{
		UI_Master.me.Warning(Localization.Get("ALREADY_CLEAR"));
	}

	public void OPEN_DropPort_ASK_Popup()
	{
		SoundManager.me.Click();
		if (Now_Data.me.LV < 260)
		{
			UI_Master.me.Warning(Localization.Get("DROPPORT_CONDITION"));
			return;
		}
		UI_Master.me.Popup(DropPort_ASK_Popup);
		if (Now_Data.me.LV < 260)
		{
			Gas_by_Stage = new BigInteger(1L);
		}
		else
		{
			Gas_by_Stage = Now_Data.me.LV - 150 + (Now_Data.me.LV - 260) / 10 * (Now_Data.me.LV / 200) + new BigInteger(Unit_DB.me.smooth_ONE_table[(Now_Data.me.LV - 260) / 100]) * 4;
		}
		Gas_by_Unit = 0;
		for (int i = 0; i < Now_Data.me.Now_Unit_LV.Length; i++)
		{
			Gas_by_Unit += new BigInteger(Now_Data.me.Now_Unit_LV[i] / 20);
		}
		Gas_by_Artifact = (Gas_by_Stage + Gas_by_Unit) * (int)((float)Now_Data.me.Artifact_Possesion.Count + Now_Data.me.CRYSTAL_BONUS_PER) / 100;
		Total_Get_Gas = Gas_by_Stage + Gas_by_Unit + Gas_by_Artifact;
		label_Gas_by_Stage.text = Now_Data.INT_to_ABC(Gas_by_Stage);
		label_Gas_by_Unit.text = Now_Data.INT_to_ABC(Gas_by_Unit);
		label_Gas_by_Artifact.text = Now_Data.INT_to_ABC(Gas_by_Artifact);
		label_Total_Get_Gas.text = Now_Data.INT_to_ABC(Total_Get_Gas);
		Price_Double = 500;
		Price_Double_Label.text = string.Format("{0}", Price_Double);
	}

	public int D_value(int STAGE)
	{
		int num = 0;
		return num + (Now_Data.me.LV - 260) / 5;
	}

	public void DropPort_Check()
	{
		if (Now_Data.me.ALL_DROPPORT < 1)
		{
			UI_Master.me.Popup(Tutorial_Manager.me.Drop_port_Tuto);
		}
		else
		{
			USE_Drop_Port();
		}
	}

	public void USE_Drop_Port_URANUM()
	{
		if (Now_Data.me.MEDAL_Possible(Price_Double))
		{
			Now_Data.me.MEDAL_Change(-Price_Double);
			Now_Data.me.CRYSTAL_Change(Total_Get_Gas);
			UI_Master.me.Good_MSG(Localization.Get("SUPERDROPPRT"));
			UI_Master.me.Popup_Close_All();
			Tutorial_Manager.me.Artifact_LOCK.SetActive(false);
			Fight_Master.me.Dropport_Animation_DO();
			Fight_Master.me.BOSS_FIGHT = false;
			Now_Data.me.BOSS_Failed = false;
			Now_Data.me.DIABLO_LAPTIME = 0f;
			Security.SetFloat("DIABLO_LAPTIME", Now_Data.me.DIABLO_LAPTIME);
		}
		else
		{
			UI_Master.me.Open_Uranium_Popup();
		}
	}

	public void USE_Drop_Port()
	{
		Fight_Master.me.Game_Speed = 0f;
		Fight_Master.me.Misile_Clear();
		UI_Master.me.Popup_Close_All();
		Now_Data.me.CRYSTAL_Change(Total_Get_Gas);
		UI_Master.me.Artifact_Alram.SetActive(true);
		Now_Data.me.LV = 10 + (int)((float)Now_Data.me.LV * Now_Data.me.BUHAL_SAVE_LV / 100f);
		Security.SetInt("LV", Now_Data.me.LV);
		Now_Data.me.GoldChange(-(Now_Data.me.GOLD - Now_Data.me.PASWORD_KI_A));
		Now_Data.me.LV_Misile = 1;
		Security.SetInt("LV_Misile", Now_Data.me.LV_Misile);
		for (int i = 0; i < Now_Data.me.Active_Skill_LV.Length; i++)
		{
			Now_Data.me.Active_Skill_LV[i] = 0;
			Security.SetInt(string.Format("Active_Skill_LV_{0:000}", i), Now_Data.me.Active_Skill_LV[i]);
			UI_Master.me.skill_Use_BTNs[i].Setting();
		}
		for (int j = 0; j < Now_Data.me.Now_Unit_LV.Length; j++)
		{
			Now_Data.me.Now_Unit_LV[j] = 0;
			Security.SetInt(string.Format("Now_Unit_LV_{0:000}", j), Now_Data.me.Now_Unit_LV[j]);
		}
		Now_Data.me.DIABLO_LAPTIME = 0f;
		Security.SetFloat("DIABLO_LAPTIME", Now_Data.me.DIABLO_LAPTIME);
		Now_Data.me.NOW_DROPPORT += (BigInteger)1;
		Now_Data.me.ALL_DROPPORT += (BigInteger)1;
		Security.SetString("NOW_DROPPORT", Now_Data.me.NOW_DROPPORT.ToString());
		Security.SetString("ALL_DROPPORT", Now_Data.me.ALL_DROPPORT.ToString());
		Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_DROPPORT);
		for (int k = 0; k < Now_Data.me.sub_quest_count; k++)
		{
			UI_Master.me.sub_quest_ui[k].Txt_Update(Quest_Goal_Type.NOW_DROPPORT);
		}
		for (int l = 0; l < Now_Data.me.Bazuka_Parts.Length; l++)
		{
			Now_Data.me.Bazuka_Parts[l] = 0;
			Security.SetInt(string.Format("Bazuka_Parts_{0:000}", l), 0);
		}
		Tutorial_Manager.me.Artifact_LOCK.SetActive(false);
		Fight_Master.me.Dropport_Animation_DO();
		Fight_Master.me.BOSS_FIGHT = false;
		Now_Data.me.BOSS_Failed = false;
		UI_Master.me.Speed_BTN.SetActive(true);
		if (Now_Data.me.ALL_DROPPORT < 2)
		{
			UM_GameService.me.ReportArchievement("arch_6");
			Security.SetInt("DROP_ARCH", 1);
			return;
		}
		if (Now_Data.me.ALL_DROPPORT < 3)
		{
			UM_GameService.me.ReportArchievement("arch_7");
			Security.SetInt("DROP_ARCH", 2);
			return;
		}
		if (Security.GetInt("DROP_ARCH", 0).Equals(0))
		{
			UM_GameService.me.ReportArchievement("arch_6");
			Security.SetInt("DROP_ARCH", 1);
		}
		if (Security.GetInt("DROP_ARCH", 0).Equals(1))
		{
			UM_GameService.me.ReportArchievement("arch_7");
			Security.SetInt("DROP_ARCH", 2);
		}
	}
}
