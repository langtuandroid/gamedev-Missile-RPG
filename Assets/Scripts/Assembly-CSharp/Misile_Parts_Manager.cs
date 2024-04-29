using System.Collections.Generic;
using Keiwando.BigInteger;
using UnityEngine;

public class Misile_Parts_Manager : MonoBehaviour
{
	public Mislie_BTN[] misile_BTN;

	public int Selected_Misile_ID;

	public UILabel Rare_Label;

	public UILabel Buff_A_label;

	public UILabel Buff_B_label;

	public UILabel Buff_C_label;

	public UILabel Buff_A_value_label;

	public UILabel Buff_B_value_label;

	public UILabel Buff_C_value_label;

	public UILabel Buff_SET_A_label;

	public UILabel Buff_SET_B_label;

	public UILabel Buff_SET_C_label;

	public UILabel Buff_SET_A_value_label;

	public UILabel Buff_SET_B_value_label;

	public UILabel Buff_SET_C_value_label;

	public UILabel label_Name;

	public UILabel MISSILE_WORD;

	public int Need_Parts_Count;

	public BigInteger Need_P_Stone;

	public UILabel Need_P_Stone_Label;

	public UISprite Need_P_stone_sprite;

	public List<int> SHOW_things;

	public GameObject Selected_Flash;

	public UI2DSprite Misile_sprite;

	public GameObject Equips_Panel;

	public Mislie_BTN[] EQUIP_misile_BTN;

	public GameObject Misile_Info_Card;

	public GameObject Upgrade_BTN;

	public GameObject Equip_BTN;

	public void Full_Setting()
	{
		Equips_Panel.SetActive(false);
		SHOW_things.Clear();
		for (int i = 0; i < Now_Data.me.Misile_TIER.Length; i++)
		{
			if (Now_Data.me.Misile_TIER[i] > 0)
			{
				SHOW_things.Add(i);
			}
		}
		for (int j = 0; j < misile_BTN.Length; j++)
		{
			if (j < SHOW_things.Count)
			{
				misile_BTN[j].Setting(SHOW_things[j]);
				misile_BTN[j].gameObject.SetActive(true);
			}
			else
			{
				misile_BTN[j].gameObject.SetActive(false);
			}
		}
	}

	public void Setting(int ID, bool Show_Again)
	{
		Selected_Misile_ID = ID;
		for (int i = 0; i < misile_BTN.Length; i++)
		{
			if (misile_BTN[i].Misile_ID.Equals(Selected_Misile_ID))
			{
				Selected_Flash.transform.position = misile_BTN[i].transform.position;
				break;
			}
		}
		Misile_sprite.sprite2D = Sprite_DB.me.Misile_Sprite[ID];
		Misile_sprite.transform.parent.localPosition = new Vector3((360f - Misile_sprite.sprite2D.textureRect.width) / 7f, 110f, 0f);
		label_Name.text = Localization.Get(string.Format("MISSILE_{0:000}_NAME", ID));
		Rare_Label.text = Localization.Get(string.Format("RARE_{0:000}", Misile_DB.me.misile_DB[ID].Rare));
		MISSILE_WORD.text = Localization.Get(string.Format("MISSILE_{0:000}_WORD", ID));
		if (Misile_DB.me.misile_DB[ID].EQUIP_Option_A != 999)
		{
			Buff_A_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[ID].EQUIP_Option_A));
			switch (Misile_DB.me.misile_DB[ID].EQUIP_Option_A)
			{
			default:
				Buff_A_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_A_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_A_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_A)));
				break;
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 41:
			case 42:
			case 46:
			case 48:
			case 50:
			case 51:
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 105:
				Buff_A_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_A_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_A_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_A)));
				break;
			case 108:
				Buff_A_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_A_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_A_Plus * (float)Misile_DB.M_Value[Now_Data.me.Misile_TIER[ID]] / 100f, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_A)));
				break;
			}
		}
		else
		{
			Buff_A_label.text = string.Empty;
			Buff_A_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[ID].EQUIP_Option_B != 999)
		{
			Buff_B_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[ID].EQUIP_Option_B));
			switch (Misile_DB.me.misile_DB[ID].EQUIP_Option_B)
			{
			default:
				Buff_B_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_B_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_B_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_B)));
				break;
			case 23:
				Buff_B_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_B_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_B_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_B)));
				break;
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 41:
			case 42:
			case 46:
			case 48:
			case 50:
			case 51:
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 105:
				Buff_B_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_B_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_B_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_B)));
				break;
			case 108:
				Buff_B_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_B_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_B_Plus * (float)Misile_DB.M_Value[Now_Data.me.Misile_TIER[ID]] / 100f, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_B)));
				break;
			}
		}
		else
		{
			Buff_B_label.text = string.Empty;
			Buff_B_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[ID].EQUIP_Option_C != 999)
		{
			if (Now_Data.me.Misile_TIER[ID] >= 5)
			{
				Buff_C_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[ID].EQUIP_Option_C));
				switch (Misile_DB.me.misile_DB[ID].EQUIP_Option_C)
				{
				default:
					Buff_C_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_C_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_C_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_C)));
					break;
				case 23:
					Buff_C_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_C_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_C_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_C)));
					break;
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 46:
				case 48:
				case 50:
				case 51:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 105:
					Buff_C_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_C_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_C_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_C)));
					break;
				case 108:
					Buff_C_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].EQUIP_Option_C_Value + Misile_DB.me.misile_DB[ID].EQUIP_Option_C_Plus * (float)Misile_DB.M_Value[Now_Data.me.Misile_TIER[ID]] / 100f, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].EQUIP_Option_C)));
					break;
				}
			}
			else
			{
				Buff_C_label.text = "[767676]???";
				Buff_C_value_label.text = "[767676]LV.5";
			}
		}
		else
		{
			Buff_C_label.text = string.Empty;
			Buff_C_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[ID].BOX_Option_A != 999)
		{
			Buff_SET_A_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[ID].BOX_Option_A));
			switch (Misile_DB.me.misile_DB[ID].BOX_Option_A)
			{
			default:
				Buff_SET_A_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].BOX_Option_A_Value + Misile_DB.me.misile_DB[ID].BOX_Option_A_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].BOX_Option_A)));
				break;
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 41:
			case 42:
			case 46:
			case 48:
			case 50:
			case 51:
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 105:
				Buff_SET_A_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[ID].BOX_Option_A_Value + Misile_DB.me.misile_DB[ID].BOX_Option_A_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].BOX_Option_A)));
				break;
			case 108:
				Buff_SET_A_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].BOX_Option_A_Value + Misile_DB.me.misile_DB[ID].BOX_Option_A_Plus * (float)Misile_DB.M_Value[Now_Data.me.Misile_TIER[ID]] / 100f, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].BOX_Option_A)));
				break;
			}
		}
		else
		{
			Buff_SET_A_label.text = string.Empty;
			Buff_SET_A_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[ID].BOX_Option_B != 999)
		{
			if (Now_Data.me.Misile_TIER[ID] >= 3)
			{
				Buff_SET_B_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[ID].BOX_Option_B));
				switch (Misile_DB.me.misile_DB[ID].BOX_Option_B)
				{
				default:
					Buff_SET_B_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].BOX_Option_B_Value + Misile_DB.me.misile_DB[ID].BOX_Option_B_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].BOX_Option_B)));
					break;
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 46:
				case 48:
				case 50:
				case 51:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 105:
					Buff_SET_B_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[ID].BOX_Option_B_Value + Misile_DB.me.misile_DB[ID].BOX_Option_B_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].BOX_Option_B)));
					break;
				case 108:
					Buff_SET_B_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].BOX_Option_B_Value + Misile_DB.me.misile_DB[ID].BOX_Option_B_Plus * (float)Misile_DB.M_Value[Now_Data.me.Misile_TIER[ID]] / 100f, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].BOX_Option_B)));
					break;
				}
			}
			else
			{
				Buff_SET_B_label.text = "[767676]???";
				Buff_SET_B_value_label.text = "[767676]LV.3";
			}
		}
		else
		{
			Buff_SET_B_label.text = string.Empty;
			Buff_SET_B_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[ID].BOX_Option_C != 999)
		{
			if (Now_Data.me.Misile_TIER[ID] >= 10)
			{
				Buff_SET_C_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[ID].BOX_Option_C));
				switch (Misile_DB.me.misile_DB[ID].BOX_Option_C)
				{
				default:
					Buff_SET_C_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].BOX_Option_C_Value + Misile_DB.me.misile_DB[ID].BOX_Option_C_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].BOX_Option_C)));
					break;
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 46:
				case 48:
				case 50:
				case 51:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 105:
					Buff_SET_C_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[ID].BOX_Option_C_Value + Misile_DB.me.misile_DB[ID].BOX_Option_C_Plus * (float)Now_Data.me.Misile_TIER[ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].BOX_Option_C)));
					break;
				case 108:
					Buff_SET_C_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[ID].BOX_Option_C_Value + Misile_DB.me.misile_DB[ID].BOX_Option_C_Plus * (float)Misile_DB.M_Value[Now_Data.me.Misile_TIER[ID]] / 100f, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[ID].BOX_Option_C)));
					break;
				}
			}
			else
			{
				Buff_SET_C_label.text = "[767676]???";
				Buff_SET_C_value_label.text = "[767676]LV.10";
			}
		}
		else
		{
			Buff_SET_C_label.text = string.Empty;
			Buff_SET_C_value_label.text = string.Empty;
		}
		if (Now_Data.me.Misile_TIER[ID] >= 20)
		{
			Upgrade_BTN.SetActive(false);
		}
		else
		{
			switch (Misile_DB.me.misile_DB[ID].Rare)
			{
			case 1:
				Need_P_Stone = 40;
				break;
			case 2:
				Need_P_Stone = 60;
				break;
			case 3:
				Need_P_Stone = 80;
				break;
			case 4:
				Need_P_Stone = 100;
				break;
			case 5:
				Need_P_Stone = 150;
				break;
			case 6:
				Need_P_Stone = 200;
				break;
			}
			switch (Misile_DB.me.misile_DB[ID].Rare)
			{
			case 1:
				Need_P_Stone += (BigInteger)((Now_Data.me.Misile_TIER[ID] - 1) * 20);
				break;
			case 2:
				Need_P_Stone += (BigInteger)((Now_Data.me.Misile_TIER[ID] - 1) * 30);
				break;
			case 3:
				Need_P_Stone += (BigInteger)((Now_Data.me.Misile_TIER[ID] - 1) * 40);
				break;
			case 4:
				Need_P_Stone += (BigInteger)((Now_Data.me.Misile_TIER[ID] - 1) * 50);
				break;
			case 5:
				Need_P_Stone += (BigInteger)((Now_Data.me.Misile_TIER[ID] - 1) * 75);
				break;
			case 6:
				Need_P_Stone += (BigInteger)((Now_Data.me.Misile_TIER[ID] - 1) * 100);
				break;
			}
			Need_P_Stone *= (BigInteger)(int)(100f - Now_Data.me.Misile_Tierup_Discount_Per[Misile_DB.me.misile_DB[ID].Rare]);
			Need_P_Stone /= (BigInteger)100;
			Need_P_Stone_Label.text = Now_Data.INT_to_ABC(Need_P_Stone);
			Need_Parts_Count = Misile_DB.me.RARE_misile_DB[Misile_DB.me.misile_DB[Selected_Misile_ID].Rare].Need_Parts[Now_Data.me.Misile_TIER[Selected_Misile_ID]];
			if (Now_Data.me.P_STONE_Possible(Need_P_Stone) && Now_Data.me.Misile_Parts[Selected_Misile_ID] >= Need_Parts_Count)
			{
				Need_P_stone_sprite.spriteName = "Btn_UpgradeUranium";
				if (Show_Again)
				{
					Show_Upgrade();
				}
			}
			else
			{
				Need_P_stone_sprite.spriteName = "Btn_UpgradeDisabled";
			}
			Upgrade_BTN.SetActive(true);
		}
		bool flag = false;
		for (int j = 0; j < Now_Data.me.EQUIP_POSSILBEs_Count; j++)
		{
			if (Now_Data.me.EQUIP_MISILEs[j].Equals(Selected_Misile_ID))
			{
				flag = true;
				break;
			}
		}
		Equip_BTN.SetActive(!flag);
		Misile_Info_Card.SetActive(false);
		Misile_Info_Card.SetActive(true);
	}

	public void Upgrade()
	{
		SoundManager.me.Click();
		Need_Parts_Count = Misile_DB.me.RARE_misile_DB[Misile_DB.me.misile_DB[Selected_Misile_ID].Rare].Need_Parts[Now_Data.me.Misile_TIER[Selected_Misile_ID]];
		if (Now_Data.me.Misile_Parts[Selected_Misile_ID] >= Need_Parts_Count)
		{
			if (Now_Data.me.P_STONE_Possible(Need_P_Stone))
			{
				Need_Parts_Count = Misile_DB.me.RARE_misile_DB[Misile_DB.me.misile_DB[Selected_Misile_ID].Rare].Need_Parts[Now_Data.me.Misile_TIER[Selected_Misile_ID]];
				SoundManager.me.Missile_Upgrade();
				Now_Data.me.P_STONE_Change(-Need_P_Stone);
				Now_Data.me.Misile_TIER[Selected_Misile_ID]++;
				Security.SetInt(string.Format("Misile_TIER_{0:000}", Selected_Misile_ID), Now_Data.me.Misile_TIER[Selected_Misile_ID]);
				Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_MISSILE_COUNT);
				Now_Data.me.Misile_Parts[Selected_Misile_ID] -= Misile_DB.me.RARE_misile_DB[Misile_DB.me.misile_DB[Selected_Misile_ID].Rare].Need_Parts[Now_Data.me.Misile_TIER[Selected_Misile_ID] - 1];
				Security.SetInt(string.Format("Misile_Parts_{0:000}", Selected_Misile_ID), Now_Data.me.Misile_Parts[Selected_Misile_ID]);
				for (int i = 0; i < misile_BTN.Length; i++)
				{
					if (misile_BTN[i].Misile_ID.Equals(Selected_Misile_ID))
					{
						misile_BTN[i].Setting(Selected_Misile_ID);
						break;
					}
				}
				UI_Master.me.Close_Popup();
				Setting(Selected_Misile_ID, true);
				Fight_Master.me.All_setting();
				AnyMisile_PossibleCheck();
			}
			else
			{
				UI_Master.me.Warning(Localization.Get("NEED_HELLSTONE"));
			}
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_PARTS"));
		}
	}

	public void Show_Upgrade()
	{
		SoundManager.me.Click();
		Need_Parts_Count = Misile_DB.me.RARE_misile_DB[Misile_DB.me.misile_DB[Selected_Misile_ID].Rare].Need_Parts[Now_Data.me.Misile_TIER[Selected_Misile_ID]];
		if (Now_Data.me.Misile_Parts[Selected_Misile_ID] >= Need_Parts_Count)
		{
			if (Now_Data.me.P_STONE_Possible(Need_P_Stone))
			{
				UI_Master.me.Upgrade_Missile_Popup.Setting_for_Upgrade(Selected_Misile_ID);
			}
			else
			{
				UI_Master.me.Warning(Localization.Get("NEED_HELLSTONE"));
			}
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_PARTS"));
		}
	}

	public void Equip()
	{
		SoundManager.me.Click();
		bool flag = false;
		for (int i = 0; i < Now_Data.me.EQUIP_POSSILBEs_Count; i++)
		{
			if (!Now_Data.me.EQUIP_MISILEs[i].Equals(-1))
			{
				continue;
			}
			flag = true;
			Now_Data.me.EQUIP_MISILEs[i] = Selected_Misile_ID;
			Security.SetInt(string.Format("EQUIP_MISILEs_{0:000}", i), Now_Data.me.EQUIP_MISILEs[i]);
			for (int j = 0; j < misile_BTN.Length; j++)
			{
				if (misile_BTN[j].Misile_ID.Equals(Selected_Misile_ID))
				{
					misile_BTN[j].Setting(Selected_Misile_ID);
					break;
				}
			}
			UI_Master.me.Good_MSG(string.Format("{0}{1}", Localization.Get(string.Format("MISSILE_{0:000}_NAME", Selected_Misile_ID)), Localization.Get("Equiped")));
			Setting(Selected_Misile_ID, false);
			Fight_Master.me.All_setting();
			break;
		}
		if (!flag)
		{
			Equip_Panel_Show();
		}
	}

	public void Equip_Panel_Show()
	{
		for (int i = 0; i < Now_Data.me.EQUIP_POSSILBEs_Count; i++)
		{
			EQUIP_misile_BTN[i].Setting(Now_Data.me.EQUIP_MISILEs[i]);
		}
		Equips_Panel.SetActive(true);
	}

	public void Equip_Change_Target(int Taget_Misile)
	{
		SoundManager.me.Click();
		for (int i = 0; i < Now_Data.me.EQUIP_POSSILBEs_Count; i++)
		{
			if (!Now_Data.me.EQUIP_MISILEs[i].Equals(Taget_Misile))
			{
				continue;
			}
			Now_Data.me.EQUIP_MISILEs[i] = Selected_Misile_ID;
			Security.SetInt(string.Format("EQUIP_MISILEs_{0:000}", i), Now_Data.me.EQUIP_MISILEs[i]);
			UI_Master.me.Good_MSG(string.Format("{0}{1}", Localization.Get(string.Format("MISSILE_{0:000}_NAME", Selected_Misile_ID)), Localization.Get("Equiped")));
			for (int j = 0; j < misile_BTN.Length; j++)
			{
				if (misile_BTN[j].Misile_ID.Equals(Selected_Misile_ID))
				{
					misile_BTN[j].Setting(Selected_Misile_ID);
					break;
				}
			}
			for (int k = 0; k < misile_BTN.Length; k++)
			{
				if (misile_BTN[k].Misile_ID.Equals(Taget_Misile))
				{
					misile_BTN[k].Setting(Taget_Misile);
					break;
				}
			}
			break;
		}
		Setting(Selected_Misile_ID, false);
		Equips_Panel.SetActive(false);
		Fight_Master.me.All_setting();
	}

	public void Close_Equip_Change()
	{
		Equips_Panel.SetActive(false);
	}

	public void AnyMisile_PossibleCheck()
	{
		bool active = false;
		UI_Master.me.Misile_Alram.SetActive(false);
		for (int i = 0; i < Now_Data.me.Misile_TIER.Length; i++)
		{
			if (Now_Data.me.Misile_TIER[i] < 20 && Now_Data.me.Misile_TIER[i] > 0)
			{
				switch (Misile_DB.me.misile_DB[i].Rare)
				{
				case 1:
					Need_P_Stone = 40 + (Now_Data.me.Misile_TIER[i] - 1) * 20;
					break;
				case 2:
					Need_P_Stone = 60 + (Now_Data.me.Misile_TIER[i] - 1) * 30;
					break;
				case 3:
					Need_P_Stone = 80 + (Now_Data.me.Misile_TIER[i] - 1) * 40;
					break;
				case 4:
					Need_P_Stone = 100 + (Now_Data.me.Misile_TIER[i] - 1) * 50;
					break;
				case 5:
					Need_P_Stone = 150 + (Now_Data.me.Misile_TIER[i] - 1) * 75;
					break;
				case 6:
					Need_P_Stone = 200 + (Now_Data.me.Misile_TIER[i] - 1) * 100;
					break;
				}
				Need_P_Stone *= (BigInteger)(int)(100f - Now_Data.me.Misile_Tierup_Discount_Per[Misile_DB.me.misile_DB[i].Rare]);
				Need_P_Stone /= (BigInteger)100;
				Need_Parts_Count = Misile_DB.me.RARE_misile_DB[Misile_DB.me.misile_DB[i].Rare].Need_Parts[Now_Data.me.Misile_TIER[i]];
				if (Now_Data.me.P_STONE_Possible(Need_P_Stone) && Now_Data.me.Misile_Parts[i] >= Need_Parts_Count)
				{
					active = true;
					break;
				}
			}
		}
		UI_Master.me.Misile_Alram.SetActive(active);
	}
}
