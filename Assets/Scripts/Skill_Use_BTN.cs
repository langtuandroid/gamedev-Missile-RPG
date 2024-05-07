using Keiwando.BigInteger;
using UnityEngine;

public class Skill_Use_BTN : MonoBehaviour
{
	public UILabel Sec_label;

	public UISprite Cool_sprite;

	public GameObject Flash;

	public bool Possible;

	public bool USE_ING;

	public int Skill_ID;

	public float Skill_Cooltime_now;

	public GameObject ING_EF;

	public float SAve_Check;

	public void LOAD()
	{
		if (Now_Data.me.Active_Skill_LV[Skill_ID] > 0)
		{
			Skill_Cooltime_now = Security.GetFloat(string.Format("Skill_Cooltime_now_{0:000}", Skill_ID), 0f);
			if (Skill_Cooltime_now > 0f)
			{
				Sec_label.gameObject.SetActive(true);
			}
			else
			{
				Possible = true;
				Cool_sprite.fillAmount = 0f;
			}
			if (Skill_ID.Equals(0))
			{
				UI_Master.me.Misile_Upgrade_Popup.Skill_6_BG.SetActive(false);
			}
		}
		else
		{
			Sec_label.gameObject.SetActive(false);
			Possible = false;
			Cool_sprite.fillAmount = 1f;
			if (Skill_ID.Equals(0))
			{
				UI_Master.me.Misile_Upgrade_Popup.Skill_6_BG.SetActive(true);
			}
		}
		base.gameObject.SetActive(true);
	}

	public void Setting()
	{
		if (Now_Data.me.Active_Skill_LV[Skill_ID] > 0)
		{
			Skill_Cooltime_now = Security.GetFloat(string.Format("Skill_Cooltime_now_{0:000}", Skill_ID), 0f);
			if (!Possible)
			{
				Possible = true;
				Cool_sprite.fillAmount = 0f;
			}
			if (Skill_ID.Equals(0))
			{
				UI_Master.me.Misile_Upgrade_Popup.Skill_6_BG.SetActive(false);
			}
		}
		else
		{
			Sec_label.gameObject.SetActive(false);
			Possible = false;
			Cool_sprite.fillAmount = 1f;
			if (Skill_ID.Equals(0))
			{
				UI_Master.me.Misile_Upgrade_Popup.Skill_6_BG.SetActive(true);
			}
		}
		Flash.SetActive(false);
		Flash.SetActive(true);
	}

	public void FixedUpdate()
	{
		if (Now_Data.me.Active_Skill_LV[Skill_ID] > 0 && !Possible && !USE_ING)
		{
			SAve_Check += 0.05f * Fight_Master.me.Game_Speed;
			if (SAve_Check >= 5f)
			{
				SAve_Check = 0f;
				Security.SetFloat(string.Format("Skill_Cooltime_now_{0:000}", Skill_ID), Skill_Cooltime_now);
			}
			Skill_Cooltime_now -= 0.05f * Fight_Master.me.Game_Speed;
			Cool_sprite.fillAmount = Skill_Cooltime_now / Misile_DB.me.skill_DB_Basic[Skill_ID].COOL;
			Sec_label.text = string.Format("{0}", Time_Checker.ShowTime_Label_noT(Skill_Cooltime_now));
			if (Skill_Cooltime_now <= 0f)
			{
				Possible = true;
				Flash.SetActive(false);
				Flash.SetActive(true);
				Sec_label.gameObject.SetActive(false);
				Security.SetFloat(string.Format("Skill_Cooltime_now_{0:000}", Skill_ID), Skill_Cooltime_now);
			}
		}
	}

	public void OnClick()
	{
		if (Now_Data.me.Active_Skill_LV[Skill_ID] > 0)
		{
			if (Possible)
			{
				SoundManager.me.Skill_Use(Skill_ID);
				Possible = false;
				Sec_label.gameObject.SetActive(true);
				Skill_Cooltime_now = Misile_DB.me.skill_DB_Basic[Skill_ID].COOL * (100f - Now_Data.me.ALL_SKILL_Cooltime_Minus - Now_Data.me.SKILL_Cooltime_Minus[Skill_ID]) / 100f;
				if (Main_Player.me.Cooltime_Buff_ING)
				{
					Skill_Cooltime_now *= 0.5f;
				}
				if (Now_Data.me.SKILL_after_ACTION_MAN_PER > 0f && (float)Random.Range(0, 100) < Now_Data.me.SKILL_after_ACTION_MAN_PER)
				{
					Fight_Master.me.action_man.Go_Stage_Count = 1;
					Fight_Master.me.action_man.gameObject.SetActive(true);
				}
				if (Now_Data.me.SKILL_after_COOL_RESET_PER > 0f && (float)Random.Range(0, 100) < Now_Data.me.SKILL_after_COOL_RESET_PER)
				{
					Skill_Cooltime_now = 1f;
				}
				Security.SetFloat(string.Format("Skill_Cooltime_now_{0:000}", Skill_ID), Skill_Cooltime_now);
				switch (Skill_ID)
				{
				default:
					Main_Player.me.Skill_A();
					break;
				case 1:
					USE_ING = true;
					ING_EF.SetActive(true);
					Main_Player.me.Skill_B();
					break;
				case 2:
					USE_ING = true;
					ING_EF.SetActive(true);
					Main_Player.me.Skill_C();
					break;
				case 3:
					USE_ING = true;
					ING_EF.SetActive(true);
					Main_Player.me.Skill_D();
					break;
				case 4:
					USE_ING = true;
					ING_EF.SetActive(true);
					Main_Player.me.Skill_E();
					break;
				case 5:
					USE_ING = true;
					ING_EF.SetActive(true);
					Main_Player.me.Skill_F();
					break;
				case 6:
				{
					for (int i = 0; i < Fight_Master.me.Mini_mans.Length; i++)
					{
						Fight_Master.me.Mini_mans[i].Go_Stage_Count = 0;
						Fight_Master.me.Mini_mans[i].gameObject.SetActive(true);
					}
					break;
				}
				}
				Now_Data.me.NOW_SKILL_USE += (BigInteger)1;
				Now_Data.me.ALL_SKILL_USE += (BigInteger)1;
				Security.SetString("NOW_SKILL_USE", Now_Data.me.NOW_SKILL_USE.ToString());
				Security.SetString("ALL_SKILL_USE", Now_Data.me.ALL_SKILL_USE.ToString());
				Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_SKILL_USE);
				for (int j = 0; j < Now_Data.me.sub_quest_count; j++)
				{
					UI_Master.me.sub_quest_ui[j].Txt_Update(Quest_Goal_Type.NOW_SKILL_USE);
				}
				UI_Master.me.Skill_Use_EF_Setting(Skill_ID);
			}
			else
			{
				UI_Master.me.Warning(Localization.Get("WAIT_MORE"));
			}
		}
		else if (Skill_ID.Equals(6))
		{
			UI_Master.me.Warning(Localization.Get("BAZUKA_SKILL"));
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NOTYET_SKILL"));
		}
	}
}
