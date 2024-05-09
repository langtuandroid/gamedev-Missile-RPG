using Keiwando.BigInteger;
using UnityEngine;

public class ArchivmentButtonBehaviour : MonoBehaviour
{
	public int Arch_Type_ID;

	public UILabel Arch_Name;

	public UISprite[] Star_sprites;

	public UILabel Value_label;

	public UISlider Value_slider;

	public Quest_Goal_Type arch_goal_type;

	public BigInteger now_value;

	public BigInteger goal_value;

	public UILabel Reward_value_lable;

	public bool Get_Reward_Possible;

	public UISprite Get_Reward_sprite;

	public GameObject Get_Reward_BTN;

	public GameObject MAX_LV_BTN;

	public void Setting()
	{
		Arch_Name.text = Localization.Get(string.Format("Arch_Type_NAME_{0:000}", Arch_Type_ID));
		for (int i = 0; i < Star_sprites.Length; i++)
		{
			if (i < Now_Data.me.Archievement_LV[Arch_Type_ID])
			{
				Star_sprites[i].spriteName = "Prop_Star0";
			}
			else
			{
				Star_sprites[i].spriteName = "Prop_StarEmpty";
			}
		}
		arch_goal_type = ArchivmentDB.me.arch_DB[Arch_Type_ID].Arch[0].GOAL_TYPE;
		Check_Possible(arch_goal_type);
		if (Now_Data.me.Archievement_LV[Arch_Type_ID] < ArchivmentDB.me.arch_DB[Arch_Type_ID].Arch.Length)
		{
			Value_label.text = string.Format("{0}/{1}", Now_Data.INT_to_ABC(now_value), Now_Data.INT_to_ABC(goal_value));
			Value_slider.value = Now_Data.Divide_to_Float(now_value, goal_value);
			if (Get_Reward_Possible)
			{
				Get_Reward_sprite.spriteName = "Btn_UpgradeUranium";
			}
			else
			{
				Get_Reward_sprite.spriteName = "Btn_UpgradeDisabled";
			}
			Reward_value_lable.text = string.Format("{0}", Now_Data.INT_to_ABC(new BigInteger(ArchivmentDB.me.arch_DB[Arch_Type_ID].Arch[Now_Data.me.Archievement_LV[Arch_Type_ID]].REWARD_VALUE)));
			Get_Reward_BTN.SetActive(true);
			MAX_LV_BTN.SetActive(false);
		}
		else
		{
			Get_Reward_BTN.SetActive(false);
			MAX_LV_BTN.SetActive(true);
		}
	}

	public void Check_Possible(Quest_Goal_Type target_goal)
	{
		arch_goal_type = ArchivmentDB.me.arch_DB[Arch_Type_ID].Arch[0].GOAL_TYPE;
		if (!target_goal.Equals(arch_goal_type))
		{
			return;
		}
		if (Now_Data.me.Archievement_LV[Arch_Type_ID] < ArchivmentDB.me.arch_DB[Arch_Type_ID].Arch.Length)
		{
			switch (arch_goal_type)
			{
			case Quest_Goal_Type.ALL_TAP:
				now_value = Now_Data.me.ALL_TAP;
				break;
			case Quest_Goal_Type.ALL_MINERAL:
				now_value = Now_Data.me.ALL_MINERAL;
				break;
			case Quest_Goal_Type.ALL_GAS:
				now_value = Now_Data.me.ALL_GAS;
				break;
			case Quest_Goal_Type.ALL_P_STONE:
				now_value = Now_Data.me.ALL_P_STONE;
				break;
			case Quest_Goal_Type.ARTIFACT_COUNT:
				now_value = Now_Data.me.Artifact_COUNT;
				break;
			case Quest_Goal_Type.BEST_LV:
				now_value = Now_Data.me.BEST_LV;
				break;
			case Quest_Goal_Type.ALL_PLAYTIME:
				now_value = Now_Data.me.ALL_PLAYTIME;
				break;
			case Quest_Goal_Type.ALL_MISSILE_COUNT:
			{
				now_value = 0;
				for (int i = 0; i < Now_Data.me.Misile_TIER.Length; i++)
				{
					if (Now_Data.me.Misile_TIER[i] > 0)
					{
						now_value += (BigInteger)1;
					}
				}
				break;
			}
			case Quest_Goal_Type.ALL_KILL:
				now_value = Now_Data.me.ALL_KILL;
				break;
			case Quest_Goal_Type.ALL_BOSSKILL:
				now_value = Now_Data.me.ALL_BOSSKILL;
				break;
			case Quest_Goal_Type.ALL_DUNGEON_CLEAR:
				now_value = Now_Data.me.ALL_DUNGEON_CLEAR;
				break;
			case Quest_Goal_Type.ALL_SKILL_USE:
				now_value = Now_Data.me.ALL_SKILL_USE;
				break;
			case Quest_Goal_Type.ALL_FAIRY:
				now_value = Now_Data.me.ALL_FAIRY;
				break;
			case Quest_Goal_Type.ALL_DROPPORT:
				now_value = Now_Data.me.ALL_DROPPORT;
				break;
			case Quest_Goal_Type.ALL_PROMOTE:
				now_value = Now_Data.me.ALL_PROMOTE;
				break;
			case Quest_Goal_Type.ALL_HIDDEN:
				now_value = Now_Data.me.ALL_HIDDEN;
				break;
			}
			goal_value = new BigInteger(ArchivmentDB.me.arch_DB[Arch_Type_ID].Arch[Now_Data.me.Archievement_LV[Arch_Type_ID]].TARGET_VALUE);
			if (now_value >= goal_value)
			{
				Get_Reward_Possible = true;
			}
			else
			{
				Get_Reward_Possible = false;
			}
		}
		else
		{
			Get_Reward_Possible = false;
		}
	}

	public void Get_Reward()
	{
		if (Get_Reward_Possible)
		{
			SoundManager.me.Congretu();
			Now_Data.me.MEDAL_Change(new BigInteger(ArchivmentDB.me.arch_DB[Arch_Type_ID].Arch[Now_Data.me.Archievement_LV[Arch_Type_ID]].REWARD_VALUE) * (int)(100f + Now_Data.me.Hellsteon_Bonus) / 100);
			Now_Data.me.Archievement_LV[Arch_Type_ID]++;
			Now_Data.me.Arch_Star += (BigInteger)1;
			Setting();
			Security.SetInt(string.Format("Archievement_LV_{0:000}", Arch_Type_ID), Now_Data.me.Archievement_LV[Arch_Type_ID]);
			Security.SetString("Arch_Star", Now_Data.me.Arch_Star.ToString());
			UI_Master.me.ArchivmentPopup.Arch_LV_Setting();
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NOTYET_ARCHIEVE"));
		}
	}
}
