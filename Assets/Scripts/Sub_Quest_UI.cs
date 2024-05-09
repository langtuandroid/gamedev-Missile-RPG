using Keiwando.BigInteger;
using UnityEngine;

public class Sub_Quest_UI : MonoBehaviour
{
	public int Slot;

	public int Sub_Quest_ID;

	public Quest_Goal_Type Sub_Quest_Goal_Type;

	public UILabel Goal_Name_label;

	public UILabel Goal_label;

	public UISlider Slider;

	public BigInteger now_value;

	public BigInteger goal_value;

	public GameObject Clear_BTN;

	public GameObject Change_BTN;

	public void Quest_ID_CHOICE()
	{
		if (Now_Data.me.CLEAR_QUEST < 6)
		{
			switch (Now_Data.me.CLEAR_QUEST)
			{
			case 1:
				if (Slot.Equals(0))
				{
					Now_Data.me.NOW_Subquest_A_ID = 1;
				}
				else
				{
					Now_Data.me.NOW_Subquest_B_ID = 1;
				}
				break;
			case 2:
				if (Slot.Equals(0))
				{
					Now_Data.me.NOW_Subquest_A_ID = 9;
				}
				else
				{
					Now_Data.me.NOW_Subquest_B_ID = 9;
				}
				break;
			case 3:
				if (Slot.Equals(0))
				{
					Now_Data.me.NOW_Subquest_A_ID = 5;
				}
				else
				{
					Now_Data.me.NOW_Subquest_B_ID = 5;
				}
				break;
			case 4:
				if (Slot.Equals(0))
				{
					Now_Data.me.NOW_Subquest_A_ID = 6;
				}
				else
				{
					Now_Data.me.NOW_Subquest_B_ID = 6;
				}
				break;
			case 5:
				if (Slot.Equals(0))
				{
					Now_Data.me.NOW_Subquest_A_ID = 18;
				}
				else
				{
					Now_Data.me.NOW_Subquest_B_ID = 18;
				}
				break;
			}
		}
		else if (Slot.Equals(0))
		{
			Quest_Goal_Type gOAL_TYPE = ArchivmentDB.me.SUB_arch_DB[Now_Data.me.NOW_Subquest_A_ID].GOAL_TYPE;
			Now_Data.me.NOW_Subquest_A_ID = Random.Range(1, ArchivmentDB.me.SUB_arch_DB.Length);
			while (ArchivmentDB.me.SUB_arch_DB[Now_Data.me.NOW_Subquest_A_ID].GOAL_TYPE.Equals(gOAL_TYPE) || ArchivmentDB.me.SUB_arch_DB[Now_Data.me.NOW_Subquest_A_ID].GOAL_TYPE.Equals(ArchivmentDB.me.SUB_arch_DB[Now_Data.me.NOW_Subquest_B_ID].GOAL_TYPE))
			{
				Now_Data.me.NOW_Subquest_A_ID = Random.Range(1, ArchivmentDB.me.SUB_arch_DB.Length);
			}
		}
		else
		{
			Quest_Goal_Type gOAL_TYPE2 = ArchivmentDB.me.SUB_arch_DB[Now_Data.me.NOW_Subquest_B_ID].GOAL_TYPE;
			Now_Data.me.NOW_Subquest_B_ID = Random.Range(1, ArchivmentDB.me.SUB_arch_DB.Length);
			while (ArchivmentDB.me.SUB_arch_DB[Now_Data.me.NOW_Subquest_B_ID].GOAL_TYPE.Equals(gOAL_TYPE2) || ArchivmentDB.me.SUB_arch_DB[Now_Data.me.NOW_Subquest_B_ID].GOAL_TYPE.Equals(ArchivmentDB.me.SUB_arch_DB[Now_Data.me.NOW_Subquest_A_ID].GOAL_TYPE))
			{
				Now_Data.me.NOW_Subquest_B_ID = Random.Range(1, ArchivmentDB.me.SUB_arch_DB.Length);
			}
		}
		Setting(true);
	}

	public void SET_FIRST_QUEST()
	{
		if (Slot.Equals(0))
		{
			Now_Data.me.NOW_Subquest_A_ID = 6;
		}
		Setting(true);
	}

	public void Setting(bool NEW_Q)
	{
		if (Slot.Equals(0))
		{
			Sub_Quest_ID = Now_Data.me.NOW_Subquest_A_ID;
			Security.SetInt("NOW_Subquest_A_ID", Now_Data.me.NOW_Subquest_A_ID);
		}
		else
		{
			Sub_Quest_ID = Now_Data.me.NOW_Subquest_B_ID;
			Security.SetInt("NOW_Subquest_B_ID", Now_Data.me.NOW_Subquest_B_ID);
		}
		if (Sub_Quest_ID.Equals(0))
		{
			base.gameObject.SetActive(false);
			return;
		}
		goal_value = new BigInteger(ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].TARGET_VALUE);
		Sub_Quest_Goal_Type = ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].GOAL_TYPE;
		switch (Sub_Quest_Goal_Type)
		{
		case Quest_Goal_Type.NOW_TAP:
			if (NEW_Q)
			{
				Now_Data.me.NOW_TAP = 0;
				Security.SetString("NOW_TAP", "0");
			}
			now_value = Now_Data.me.NOW_TAP;
			break;
		case Quest_Goal_Type.NOW_KILL:
			if (NEW_Q)
			{
				Now_Data.me.NOW_KILL = 0;
				Security.SetString("NOW_KILL", "0");
			}
			now_value = Now_Data.me.NOW_KILL;
			break;
		case Quest_Goal_Type.NOW_BOSSKILL:
			if (NEW_Q)
			{
				Now_Data.me.NOW_BOSSKILL = 0;
				Security.SetString("NOW_BOSSKILL", "0");
			}
			now_value = Now_Data.me.NOW_BOSSKILL;
			break;
		case Quest_Goal_Type.NOW_PLAYTIME:
			if (NEW_Q)
			{
				Now_Data.me.NOW_PLAYTIME = 0;
				Security.SetString("NOW_PLAYTIME", "0");
			}
			now_value = Now_Data.me.NOW_PLAYTIME;
			break;
		case Quest_Goal_Type.NOW_SKILL_USE:
			if (NEW_Q)
			{
				Now_Data.me.NOW_SKILL_USE = 0;
				Security.SetString("NOW_SKILL_USE", "0");
			}
			now_value = Now_Data.me.NOW_SKILL_USE;
			break;
		case Quest_Goal_Type.NOW_FARIY:
			if (NEW_Q)
			{
				Now_Data.me.NOW_FARIY = 0;
				Security.SetString("NOW_FARIY", "0");
			}
			now_value = Now_Data.me.NOW_FARIY;
			break;
		case Quest_Goal_Type.NOW_DROPPORT:
			if (NEW_Q)
			{
				Now_Data.me.NOW_DROPPORT = 0;
				Security.SetString("NOW_DROPPORT", "0");
			}
			now_value = Now_Data.me.NOW_DROPPORT;
			break;
		}
		base.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
		LANG_CHANGE();
		Txt_Update(Sub_Quest_Goal_Type);
	}

	public void LANG_CHANGE()
	{
		Goal_Name_label.text = Localization.Get(string.Format("SUBQUEST_NAME_{0:000}", Sub_Quest_ID));
	}

	public void Txt_Update(Quest_Goal_Type target_goal)
	{
		if (Sub_Quest_Goal_Type.Equals(target_goal))
		{
			switch (Sub_Quest_Goal_Type)
			{
			case Quest_Goal_Type.NOW_TAP:
				now_value = Now_Data.me.NOW_TAP;
				break;
			case Quest_Goal_Type.NOW_KILL:
				now_value = Now_Data.me.NOW_KILL;
				break;
			case Quest_Goal_Type.NOW_BOSSKILL:
				now_value = Now_Data.me.NOW_BOSSKILL;
				break;
			case Quest_Goal_Type.NOW_PLAYTIME:
				now_value = Now_Data.me.NOW_PLAYTIME;
				break;
			case Quest_Goal_Type.NOW_SKILL_USE:
				now_value = Now_Data.me.NOW_SKILL_USE;
				break;
			case Quest_Goal_Type.NOW_FARIY:
				now_value = Now_Data.me.NOW_FARIY;
				break;
			case Quest_Goal_Type.NOW_DROPPORT:
				now_value = Now_Data.me.NOW_DROPPORT;
				break;
			}
			if (now_value >= goal_value)
			{
				now_value = goal_value;
				Clear_BTN.SetActive(false);
				Clear_BTN.SetActive(true);
				Change_BTN.SetActive(false);
				Goal_Name_label.gameObject.SetActive(false);
				Goal_label.gameObject.SetActive(false);
			}
			else
			{
				Goal_Name_label.gameObject.SetActive(true);
				Goal_label.gameObject.SetActive(true);
				Clear_BTN.SetActive(false);
				Change_BTN.SetActive(true);
			}
			Goal_label.text = string.Format("{0}/{1}", now_value, goal_value);
			Slider.value = Now_Data.Divide_to_Float(now_value, goal_value);
		}
	}

	public void Change_Popup_Open()
	{
		UI_Master.me.Change_taret_Subquest_Slot = Slot;
		UI_Master.me.Popup(UI_Master.me.Quest_Change_Popup);
	}

	public void Get_Reward()
	{
		SoundManager.me.Congretu();
		switch (ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].REWARD_TYPE)
		{
		case "HELLSTONE":
			Now_Data.me.P_STONE_Change(new BigInteger(ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].REWARD_VALUE) * (int)(100f + Now_Data.me.Hellsteon_Bonus) / 100);
			UI_Master.me.Good_MSG(string.Format("{0} {1} {2}", Localization.Get("HELLSTONE"), ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].REWARD_VALUE, Localization.Get("GETTING")));
			break;
		case "URANIUM":
			Now_Data.me.MEDAL_Change(new BigInteger(ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].REWARD_VALUE));
			UI_Master.me.Good_MSG(string.Format("{0} {1} {2}", Localization.Get("URANIUM"), ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].REWARD_VALUE, Localization.Get("GETTING")));
			break;
		case "STAR":
			Now_Data.me.Arch_Star += new BigInteger(ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].REWARD_VALUE);
			Security.SetString("Arch_Star", Now_Data.me.Arch_Star.ToString());
			UI_Master.me.Good_MSG(string.Format("{0} {1} {2}", Localization.Get("STAR"), ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].REWARD_VALUE, Localization.Get("GETTING")));
			break;
		case "BOX":
			Now_Data.me.BOX_Count[int.Parse(ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].REWARD_VALUE)]++;
			UI_Master.me.box_Open_Panel.Setting(int.Parse(ArchivmentDB.me.SUB_arch_DB[Sub_Quest_ID].REWARD_VALUE), true);
			break;
		}
		Now_Data.me.CLEAR_QUEST++;
		Security.SetInt("CLEAR_QUEST", Now_Data.me.CLEAR_QUEST);
		Quest_ID_CHOICE();
	}
}
