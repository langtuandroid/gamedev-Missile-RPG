using UnityEngine;

public class Tutorial_Manager : MonoBehaviour
{
	public int Tutorial_Step;

	public bool CHECK;

	public GameObject[] Tutorial_IMGs;

	public GameObject Drop_port_Tuto;

	public bool Tuto_ING;

	public static Tutorial_Manager me;

	public GameObject Missile_LOCK;

	public GameObject Marine_LOCK;

	public GameObject Dungeon_LOCK;

	public GameObject Artifact_LOCK;

	public GameObject TIME_ATK_LOCK;

	public void Awake()
	{
		me = this;
		Tutorial_Step = Security.GetInt("Tutorial_Step", 0);
	}

	public void Start()
	{
		if (Tutorial_Step >= 7)
		{
			Missile_LOCK.SetActive(false);
			Dungeon_LOCK.SetActive(false);
			Marine_LOCK.SetActive(false);
			Artifact_LOCK.SetActive(false);
			base.gameObject.SetActive(false);
		}
		else
		{
			if (Tutorial_Step > 1)
			{
				Marine_LOCK.SetActive(false);
			}
			bool flag = false;
			for (int i = 1; i < Now_Data.me.Misile_Parts.Length; i++)
			{
				if (Now_Data.me.Misile_Parts[i] > 0)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				Missile_LOCK.SetActive(false);
			}
			if (Now_Data.me.BEST_LV < 110)
			{
				Dungeon_LOCK.SetActive(true);
			}
			if (Now_Data.me.ALL_DROPPORT > 0)
			{
				Missile_LOCK.SetActive(false);
				Dungeon_LOCK.SetActive(false);
				Marine_LOCK.SetActive(false);
				Artifact_LOCK.SetActive(false);
			}
		}
		if (Now_Data.me.BEST_LV < 310)
		{
			TIME_ATK_LOCK.SetActive(true);
		}
		else
		{
			TIME_ATK_LOCK.SetActive(false);
		}
	}

	public void OnDisable()
	{
		Missile_LOCK.SetActive(false);
		Marine_LOCK.SetActive(false);
		Dungeon_LOCK.SetActive(false);
		Artifact_LOCK.SetActive(false);
	}

	public void FixedUpdate()
	{
		if (!CHECK || !Fight_Master.me.GS.Equals(GameState.Play))
		{
			return;
		}
		switch (Tutorial_Step)
		{
		case 0:
			if (Now_Data.me.LV > 10)
			{
				Tutorial_IMGs[7].SetActive(false);
				if (UI_Master.me.sub_quest_ui[0].Sub_Quest_ID.Equals(0))
				{
					UI_Master.me.sub_quest_ui[0].SET_FIRST_QUEST();
				}
			}
			if (Now_Data.me.Gold_Possible(50))
			{
				Time.timeScale = 0f;
				Tutorial_IMGs[7].SetActive(false);
				Tutorial_IMGs[0].SetActive(true);
				CHECK = false;
				Tuto_ING = true;
			}
			break;
		case 1:
			if (Now_Data.me.LV > 10)
			{
				Tutorial_IMGs[7].SetActive(false);
				if (UI_Master.me.sub_quest_ui[0].Sub_Quest_ID.Equals(0))
				{
					UI_Master.me.sub_quest_ui[0].SET_FIRST_QUEST();
				}
			}
			if (Now_Data.me.Gold_Possible(500))
			{
				Time.timeScale = 0f;
				Tutorial_IMGs[5].SetActive(true);
				CHECK = false;
				Tuto_ING = true;
				Marine_LOCK.SetActive(false);
			}
			break;
		case 2:
			if (Now_Data.me.LV > 22)
			{
				if (Main_Player.me.Autoshot_ING)
				{
					Tutorial_Step = 3;
					Security.SetInt("Tutorial_Step", Tutorial_Step);
					break;
				}
				Time.timeScale = 0f;
				CHECK = false;
				Tuto_ING = true;
				Tutorial_IMGs[16].SetActive(true);
			}
			break;
		case 3:
			if (Now_Data.me.Now_Unit_LV[1] > 0)
			{
				Tutorial_Step = 4;
				Security.SetInt("Tutorial_Step", Tutorial_Step);
			}
			else if (Now_Data.me.Gold_Possible(5000))
			{
				Time.timeScale = 0f;
				Tutorial_IMGs[8].SetActive(true);
				CHECK = false;
				Tuto_ING = true;
				Marine_LOCK.SetActive(false);
			}
			break;
		case 4:
			if (Now_Data.me.Active_Skill_LV[0] > 0)
			{
				Tutorial_Step = 5;
				Security.SetInt("Tutorial_Step", Tutorial_Step);
			}
			else if (Now_Data.me.Gold_Possible(25000))
			{
				Time.timeScale = 0f;
				Tutorial_IMGs[2].SetActive(true);
				CHECK = false;
				Tuto_ING = true;
			}
			break;
		case 5:
			if (Now_Data.me.LV >= 261)
			{
				Time.timeScale = 0f;
				Tutorial_IMGs[18].SetActive(true);
				CHECK = false;
				Tuto_ING = true;
			}
			break;
		case 6:
			if (Now_Data.me.ALL_DROPPORT > 0 && Now_Data.me.LV >= 11)
			{
				if (Now_Data.me.Artifact_Possesion.Count > 0)
				{
					SoundManager.me.Click();
					End_Artifact_tutorial();
					break;
				}
				Time.timeScale = 0f;
				Tutorial_IMGs[20].SetActive(true);
				CHECK = false;
				Tuto_ING = true;
			}
			break;
		}
	}

	public void Go_to_Misile_Tutorial()
	{
		SoundManager.me.Click();
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		UI_Master.me.Open_Misile_Popup();
		Tutorial_IMGs[0].SetActive(false);
		Tutorial_IMGs[1].SetActive(true);
		CHECK = true;
		Tutorial_Step = 1;
		Security.SetInt("Tutorial_Step", Tutorial_Step);
	}

	public void End_Misile_Tutorial()
	{
		SoundManager.me.Click();
		SoundManager.me.Congretu();
		UI_Master.me.Misile_Upgrade_Popup.Misile_Upgrade();
		Tutorial_IMGs[1].SetActive(false);
		Tuto_ING = false;
	}

	public void Go_to_Unit()
	{
		SoundManager.me.Click();
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		UI_Master.me.Open_Marins_Popup();
		Tutorial_IMGs[5].SetActive(false);
		Tutorial_IMGs[6].SetActive(true);
		CHECK = true;
		Tutorial_Step = 2;
		Security.SetInt("Tutorial_Step", Tutorial_Step);
	}

	public void End_Unit_Tutorial()
	{
		SoundManager.me.Click();
		UI_Master.me.unit_Explain_panel.unit_upgrade_BTNs[0].Upgrade_One();
		Tutorial_IMGs[6].SetActive(false);
		Tuto_ING = false;
	}

	public void Go_Booster_tuto()
	{
		SoundManager.me.Click();
		Now_Data.me.Booster_LOCKON++;
		Main_Player.me.Open_Autoshot_Popup();
		Tutorial_IMGs[16].SetActive(false);
		Tutorial_IMGs[17].SetActive(true);
		CHECK = true;
		Tuto_ING = false;
		Tutorial_Step = 3;
		Security.SetInt("Tutorial_Step", Tutorial_Step);
	}

	public void End_Booster_tuto()
	{
		SoundManager.me.Click();
		Main_Player.me.Set_AutoShot_by_Booster();
		Tutorial_IMGs[17].SetActive(false);
		Tuto_ING = false;
	}

	public void Go_to_Unit_B()
	{
		SoundManager.me.Click();
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		UI_Master.me.Open_Marins_Popup();
		Tutorial_IMGs[8].SetActive(false);
		Tutorial_IMGs[9].SetActive(true);
		CHECK = true;
		Tuto_ING = false;
		Tutorial_Step = 4;
		Security.SetInt("Tutorial_Step", Tutorial_Step);
	}

	public void End_Unit_B_Tutorial()
	{
		SoundManager.me.Missile_Upgrade();
		SoundManager.me.Click();
		UI_Master.me.unit_Explain_panel.unit_upgrade_BTNs[1].Upgrade_One();
		Tutorial_IMGs[9].SetActive(false);
		Tuto_ING = false;
	}

	public void Go_to_Skill()
	{
		SoundManager.me.Missile_Upgrade();
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		UI_Master.me.Open_Misile_Popup();
		Tutorial_IMGs[2].SetActive(false);
		Tutorial_IMGs[3].SetActive(true);
		CHECK = true;
		Tutorial_Step = 5;
		Security.SetInt("Tutorial_Step", Tutorial_Step);
	}

	public void End_Skill_Tutorial()
	{
		SoundManager.me.Missile_Upgrade();
		UI_Master.me.skill_Popup.Skill_Upgrade_BTNs[0].Upgrade_One();
		Tutorial_IMGs[3].SetActive(false);
		UI_Master.me.Popup_Close_All();
		Tutorial_IMGs[4].SetActive(true);
		Time.timeScale = 0f;
		Tuto_ING = false;
	}

	public void End_End_Skill_Tutorial()
	{
		SoundManager.me.Missile_Upgrade();
		SoundManager.me.Click();
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		Tutorial_IMGs[4].SetActive(false);
		Tuto_ING = false;
	}

	public void Go_to_DropPort_tuto()
	{
		SoundManager.me.Missile_Upgrade();
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		UI_Master.me.Open_Misile_Popup();
		Tutorial_IMGs[18].SetActive(false);
		Tutorial_IMGs[19].SetActive(true);
		CHECK = true;
		Tutorial_Step = 6;
		Security.SetInt("Tutorial_Step", Tutorial_Step);
	}

	public void End_DropPort_tuto()
	{
		SoundManager.me.Missile_Upgrade();
		Tutorial_IMGs[19].SetActive(false);
		UI_Master.me.dungeon_Popup.OPEN_DropPort_ASK_Popup();
		Tuto_ING = false;
	}

	public void Go_to_Artifact_Tuto()
	{
		SoundManager.me.Missile_Upgrade();
		Time.timeScale = Now_Data.me.Changed_GameSpeed;
		UI_Master.me.Open_Artifact_Panel();
		Tutorial_IMGs[20].SetActive(false);
		CHECK = true;
		End_Artifact_tutorial();
	}

	public void End_Artifact_tutorial()
	{
		SoundManager.me.Missile_Upgrade();
		Tutorial_Step = 7;
		Security.SetInt("Tutorial_Step", Tutorial_Step);
		base.gameObject.SetActive(false);
	}

	public void Set_Dungeon_Tuto()
	{
		SoundManager.me.Missile_Upgrade();
		Time.timeScale = 0f;
		Tutorial_IMGs[10].SetActive(true);
		Dungeon_LOCK.SetActive(false);
		CHECK = false;
		Tuto_ING = true;
	}

	public void Go_Dungeon_Tuto()
	{
		SoundManager.me.Missile_Upgrade();
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		UI_Master.me.Open_Portal_Panel();
		Tutorial_IMGs[10].SetActive(false);
		Tutorial_IMGs[11].SetActive(true);
	}

	public void Go_Dungeon_Tuto_B()
	{
		SoundManager.me.Missile_Upgrade();
		UI_Master.me.dungeon_Popup.Map_Icons[0].OnClick();
		Tutorial_IMGs[11].SetActive(false);
		Tutorial_IMGs[12].SetActive(true);
		CHECK = true;
	}

	public void End_Dungeon_Tuto()
	{
		SoundManager.me.Missile_Upgrade();
		Tutorial_IMGs[12].SetActive(false);
		UI_Master.me.dungeon_Popup.PLAY_TO_DUNGEON();
	}

	public void Set_Missile_Tuto()
	{
		SoundManager.me.Missile_Upgrade();
		Time.timeScale = 0f;
		Tutorial_IMGs[13].SetActive(true);
		Missile_LOCK.SetActive(false);
		CHECK = false;
		Tuto_ING = true;
	}

	public void Go_Misile_Tuto()
	{
		SoundManager.me.Missile_Upgrade();
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		UI_Master.me.Open_Misile_Parts();
		Tutorial_IMGs[13].SetActive(false);
		Tutorial_IMGs[14].SetActive(true);
	}

	public void Go_Misile_Tuto_B()
	{
		SoundManager.me.Missile_Upgrade();
		UI_Master.me.misile_Parts_Manager.misile_BTN[1].OnClick();
		Tutorial_IMGs[14].SetActive(false);
		Tutorial_IMGs[15].SetActive(true);
		CHECK = true;
	}

	public void End_Misile_Tuto()
	{
		SoundManager.me.Missile_Upgrade();
		Tutorial_IMGs[15].SetActive(false);
		UI_Master.me.misile_Parts_Manager.Equip();
	}

	public void Set_TIME_ATK_Tuto()
	{
		base.gameObject.SetActive(true);
		SoundManager.me.Missile_Upgrade();
		Time.timeScale = 0f;
		Tutorial_IMGs[21].SetActive(true);
		CHECK = false;
		Tuto_ING = true;
		TIME_ATK_LOCK.SetActive(false);
	}

	public void Go_TIME_ATK_Tuto()
	{
		SoundManager.me.Missile_Upgrade();
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		UI_Master.me.OPEN_TIME_ATK();
		Tutorial_IMGs[21].SetActive(false);
	}
}
