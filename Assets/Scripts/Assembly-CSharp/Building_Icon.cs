using System.Collections.Generic;
using Keiwando.BigInteger;
using UnityEngine;

public class Building_Icon : MonoBehaviour
{
	public int Building_ID;

	public UI2DSprite Building_Icon_sprite;

	public UISprite Cool_Time;

	public UILabel Time_label;

	public bool Possible;

	public GameObject Working_ICON;

	public GameObject Possible_ICON;

	public List<int> Imsi;

	public void Setting()
	{
		if (Now_Data.me.Building_LV[Building_ID] > 0)
		{
			Building_Icon_sprite.sprite2D = Sprite_DB.me.Map_Icon[Building_ID];
		}
		else
		{
			Building_Icon_sprite.sprite2D = Sprite_DB.me.Map_Icon[0];
			Working_ICON.SetActive(false);
			Possible_ICON.SetActive(false);
		}
		if (Possible)
		{
			UI_Master.me.dungeon_Popup.SNAP_to_Building();
		}
	}

	public void FixedUpdate()
	{
		if (!Possible && Now_Data.me.Building_LV[Building_ID] > 0)
		{
			if (Now_Data.me.Buidling_Woriking_Possible[Building_ID])
			{
				Cool_Time.fillAmount = 0f;
				Working_ICON.SetActive(false);
				Possible_ICON.SetActive(true);
				Possible = true;
				UI_Master.me.dungeon_Popup.SNAP_to_Building();
			}
			else
			{
				Cool_Time.fillAmount = Now_Data.me.Buidling_Working_time_Check[Building_ID] / Now_Data.me.Buidling_Working_time[Building_ID];
				Time_label.text = Time_Checker.ShowTime_Label_noT(Now_Data.me.Buidling_Working_time_Check[Building_ID]);
				Working_ICON.SetActive(true);
				Possible_ICON.SetActive(false);
			}
		}
	}

	public void OnClick()
	{
		UI_Master.me.dungeon_Popup.DUNGEON_Intro_Popup.gameObject.SetActive(false);
		UI_Master.me.dungeon_Popup.Building_Intro_Popup.Setting(Building_ID);
		if (!Now_Data.me.Buidling_Woriking_Possible[Building_ID])
		{
			return;
		}
		bool flag = true;
		int num = Now_Data.me.Building_LV[Building_ID] / 5 + 1;
		switch (Building_ID)
		{
		case 1:
		{
			for (int k = 0; k < num; k++)
			{
				if (((Now_Data.me.LV_Misile + 1) % 20).Equals(0))
				{
					flag = false;
					continue;
				}
				Now_Data.me.LV_Misile++;
				UI_Master.me.Misile_Upgrade_Popup.Misile_CLASS_CHECK(true);
				UI_Master.me.Good_MSG(string.Format("{0} + {1}", Localization.Get("BAZUKA_LV"), num));
			}
			break;
		}
		case 2:
		{
			Imsi.Clear();
			for (int m = 0; m < Now_Data.me.Now_Unit_LV.Length; m++)
			{
				if (Unit_DB.me.unit_DB[m].unit_TYPE.Equals(Unit_Type.Human) && Now_Data.me.Now_Unit_LV[m] > 0)
				{
					Imsi.Add(m);
				}
			}
			if (Imsi.Count > 0)
			{
				int num5 = Imsi[Random.Range(0, Imsi.Count)];
				Now_Data.me.Now_Unit_LV[num5] += num;
				Fight_Master.me.Unit_Setting(num5);
				UI_Master.me.Good_MSG(string.Format("{0} LV + {1}", Localization.Get(string.Format("UNIT_{0:000}_NAME", num5)), num));
			}
			else
			{
				flag = false;
			}
			break;
		}
		case 3:
		{
			Imsi.Clear();
			for (int l = 0; l < Now_Data.me.Now_Unit_LV.Length; l++)
			{
				if (Unit_DB.me.unit_DB[l].unit_TYPE.Equals(Unit_Type.Mechanic) && Now_Data.me.Now_Unit_LV[l] > 0)
				{
					Imsi.Add(l);
				}
			}
			if (Imsi.Count > 0)
			{
				int num4 = Imsi[Random.Range(0, Imsi.Count)];
				Now_Data.me.Now_Unit_LV[num4] += num;
				Fight_Master.me.Unit_Setting(num4);
				UI_Master.me.Good_MSG(string.Format("{0} LV + {1}", Localization.Get(string.Format("UNIT_{0:000}_NAME", num4)), num));
			}
			else
			{
				flag = false;
			}
			break;
		}
		case 4:
		{
			Imsi.Clear();
			for (int j = 0; j < Now_Data.me.Now_Unit_LV.Length; j++)
			{
				if (Unit_DB.me.unit_DB[j].unit_TYPE.Equals(Unit_Type.Air) && Now_Data.me.Now_Unit_LV[j] > 0)
				{
					Imsi.Add(j);
				}
			}
			if (Imsi.Count > 0)
			{
				int num3 = Imsi[Random.Range(0, Imsi.Count)];
				Now_Data.me.Now_Unit_LV[num3] += num;
				Fight_Master.me.Unit_Setting(num3);
				UI_Master.me.Good_MSG(string.Format("{0} LV + {1}", Localization.Get(string.Format("UNIT_{0:000}_NAME", num3)), num));
			}
			else
			{
				flag = false;
			}
			break;
		}
		case 5:
		{
			BigInteger bigInteger = Monster_DB.me.Monster_Gold_by_LV(1, Now_Data.me.LV, false) * (int)(150f * (100f + (float)(num - 1) * 2.5f)) / 100;
			Now_Data.me.GoldChange(bigInteger);
			UI_Master.me.Good_MSG(string.Format("{0}{1}{2}", Localization.Get("MINERAL"), Now_Data.INT_to_ABC(bigInteger), Localization.Get("GETTING")));
			break;
		}
		case 6:
		{
			int num6 = 100;
			Now_Data.me.P_STONE_Change(num6 * (100 + (num - 1) * 50) / 100);
			UI_Master.me.Good_MSG(string.Format("{0}{1}{2}", Localization.Get("HELLSTONE"), num6 * (100 + (num - 1) * 50) / 100, Localization.Get("GETTING")));
			break;
		}
		case 7:
		{
			Imsi.Clear();
			for (int i = 0; i < Now_Data.me.Misile_TIER.Length; i++)
			{
				if (Now_Data.me.Misile_TIER[i] > 0)
				{
					Imsi.Add(i);
				}
			}
			if (Imsi.Count > 0)
			{
				int num2 = Imsi[Random.Range(0, Imsi.Count)];
				Now_Data.me.Misile_Parts[num2] += num;
				Security.SetInt(string.Format("Misile_Parts_{0:000}", num2), Now_Data.me.Misile_Parts[num2]);
				UI_Master.me.new_misile.GET_Misile_IDs.Clear();
				UI_Master.me.new_misile.GET_Misile_IDs.Add(num2);
				UI_Master.me.new_misile.Setting(false);
				UI_Master.me.Good_MSG(string.Format("{0} {1} {2}", Localization.Get(string.Format("MISSILE_{0:000}_NAME", num2)), num, Localization.Get(string.Format("GETTING"))));
			}
			else
			{
				flag = false;
			}
			break;
		}
		}
		if (flag)
		{
			UI_Master.me.Dungeon_Alram.SetActive(false);
			Possible = false;
			Now_Data.me.Buidling_Woriking_Possible[Building_ID] = false;
			Now_Data.me.Buidling_Working_time_Check[Building_ID] = Now_Data.me.Buidling_Working_time[Building_ID];
			Security.SetFloat(string.Format("Buidling_Working_time_Check_{0:000}", Building_ID), Now_Data.me.Buidling_Working_time_Check[Building_ID]);
			Working_ICON.SetActive(true);
			Possible_ICON.SetActive(false);
			Fight_Master.me.All_setting();
		}
		else if (Building_ID.Equals(1))
		{
			UI_Master.me.Warning(Localization.Get("NO_MISILE"));
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NO_UNIT"));
		}
	}
}
