using System.Collections.Generic;
using UnityEngine;

public class NEW_Misile : MonoBehaviour
{
	public int Misile_ID;

	public List<int> GET_Misile_IDs = new List<int>();

	public UI2DSprite Misile_sprite;

	public UILabel Buff_A_label;

	public UILabel Buff_B_label;

	public UILabel Buff_C_label;

	public UILabel Buff_A_value_label;

	public UILabel Buff_B_value_label;

	public UILabel Buff_C_value_label;

	public UILabel label_Name;

	public UILabel Rare_Label;

	public UILabel P_Buff_A_label;

	public UILabel P_Buff_B_label;

	public UILabel P_Buff_C_label;

	public UILabel P_Buff_A_value_label;

	public UILabel P_Buff_B_value_label;

	public UILabel P_Buff_C_value_label;

	public UILabel Price_Label;

	public GameObject NEW_obj;

	public UILabel MISSILE_WORD;

	public void Setting(bool NEW)
	{
		if (GET_Misile_IDs.Count <= 0)
		{
			return;
		}
		NEW_obj.SetActive(NEW);
		Misile_ID = GET_Misile_IDs[0];
		GET_Misile_IDs.RemoveAt(0);
		Misile_sprite.color = Color.white;
		Misile_sprite.sprite2D = Sprite_DB.me.Misile_Sprite[Misile_ID];
		Misile_sprite.transform.parent.localPosition = new Vector3((360f - Misile_sprite.sprite2D.textureRect.width) / 4f, 68f, 0f);
		label_Name.text = Localization.Get(string.Format("MISSILE_{0:000}_NAME", Misile_ID));
		Rare_Label.text = Localization.Get(string.Format("RARE_{0:000}", Misile_DB.me.misile_DB[Misile_ID].Rare));
		MISSILE_WORD.text = Localization.Get(string.Format("MISSILE_{0:000}_WORD", Misile_ID));
		if (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A != 999)
		{
			Buff_A_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A));
			switch (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A)
			{
			default:
				Buff_A_value_label.text = string.Empty;
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
				Buff_A_value_label.text = "-";
				break;
			}
			Buff_A_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A)));
		}
		else
		{
			Buff_A_label.text = string.Empty;
			Buff_A_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B != 999)
		{
			Buff_B_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B));
			switch (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B)
			{
			default:
				Buff_B_value_label.text = string.Empty;
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
				Buff_B_value_label.text = "-";
				break;
			}
			Buff_B_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B)));
		}
		else
		{
			Buff_B_label.text = string.Empty;
			Buff_B_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C != 999)
		{
			if (Now_Data.me.Misile_TIER[Misile_ID] >= 5)
			{
				Buff_C_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C));
				switch (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C)
				{
				default:
					Buff_C_value_label.text = string.Empty;
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
					Buff_C_value_label.text = "-";
					break;
				}
				Buff_C_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C)));
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
		if (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A != 999)
		{
			P_Buff_A_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A));
			switch (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A)
			{
			default:
				P_Buff_A_value_label.text = string.Empty;
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
				P_Buff_A_value_label.text = "-";
				break;
			}
			P_Buff_A_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A)));
		}
		else
		{
			P_Buff_A_label.text = string.Empty;
			P_Buff_A_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B != 999)
		{
			if (Now_Data.me.Misile_TIER[Misile_ID] >= 3)
			{
				P_Buff_B_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B));
				switch (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B)
				{
				default:
					P_Buff_B_value_label.text = string.Empty;
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
					P_Buff_B_value_label.text = "-";
					break;
				}
				P_Buff_B_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B)));
			}
			else
			{
				P_Buff_B_label.text = "[767676]???";
				P_Buff_B_value_label.text = "[767676]LV.3";
			}
		}
		else
		{
			P_Buff_B_label.text = string.Empty;
			P_Buff_B_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C != 999)
		{
			if (Now_Data.me.Misile_TIER[Misile_ID] >= 10)
			{
				P_Buff_C_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C));
				switch (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C)
				{
				default:
					P_Buff_C_value_label.text = string.Empty;
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
					P_Buff_C_value_label.text = "-";
					break;
				}
				P_Buff_C_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C)));
			}
			else
			{
				P_Buff_C_label.text = "[767676]???";
				P_Buff_C_value_label.text = "[767676]LV.10";
			}
		}
		else
		{
			P_Buff_C_label.text = string.Empty;
			P_Buff_C_value_label.text = string.Empty;
		}
		SoundManager.me.NEW_MISSILE();
		UI_Master.me.Popup(base.gameObject);
	}

	public void Setting_for_Upgrade(int M_ID)
	{
		Misile_ID = M_ID;
		Misile_sprite.color = Color.white;
		Misile_sprite.sprite2D = Sprite_DB.me.Misile_Sprite[Misile_ID];
		Misile_sprite.transform.parent.localPosition = new Vector3((360f - Misile_sprite.sprite2D.textureRect.width) / 7f, 140f, 0f);
		Price_Label.text = UI_Master.me.misile_Parts_Manager.Need_P_Stone_Label.text;
		label_Name.text = Localization.Get(string.Format("MISSILE_{0:000}_NAME", Misile_ID));
		Rare_Label.text = Localization.Get(string.Format("RARE_{0:000}", Misile_DB.me.misile_DB[Misile_ID].Rare));
		if (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A != 999)
		{
			Buff_A_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A));
			if (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A.Equals(108))
			{
				Buff_A_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Plus * (float)Misile_DB.M_Value[Now_Data.me.Misile_TIER[Misile_ID]] / 100f, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A)));
				Buff_A_value_label.text += "  [67FF00FF]▶ ";
				Buff_A_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Plus * (float)Misile_DB.M_Value[Now_Data.me.Misile_TIER[Misile_ID] + 1] / 100f, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A)));
			}
			else
			{
				switch (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A)
				{
				default:
					Buff_A_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A)));
					Buff_A_value_label.text += "  [67FF00FF]▶ ";
					Buff_A_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Plus * (float)(Now_Data.me.Misile_TIER[Misile_ID] + 1), Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A)));
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
					Buff_A_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A)));
					Buff_A_value_label.text += "  [67FF00FF]▶ ";
					Buff_A_value_label.text += string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A_Plus * (float)(Now_Data.me.Misile_TIER[Misile_ID] + 1), Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_A)));
					break;
				}
			}
		}
		else
		{
			Buff_A_label.text = string.Empty;
			Buff_A_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B != 999)
		{
			Buff_B_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B));
			switch (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B)
			{
			default:
				Buff_B_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B)));
				Buff_B_value_label.text += "  [67FF00FF]▶ ";
				Buff_B_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B_Plus * (float)(Now_Data.me.Misile_TIER[Misile_ID] + 1), Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B)));
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
				Buff_B_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B)));
				Buff_B_value_label.text += "  [67FF00FF]▶ ";
				Buff_B_value_label.text += string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B_Plus * (float)(Now_Data.me.Misile_TIER[Misile_ID] + 1), Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_B)));
				break;
			}
		}
		else
		{
			Buff_B_label.text = string.Empty;
			Buff_B_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C != 999)
		{
			if (Now_Data.me.Misile_TIER[Misile_ID] >= 5)
			{
				Buff_C_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C));
				switch (Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C)
				{
				default:
					Buff_C_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C)));
					Buff_C_value_label.text += "  [67FF00FF]▶ ";
					Buff_C_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C)));
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
					Buff_C_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C)));
					Buff_C_value_label.text += "  [67FF00FF]▶ ";
					Buff_C_value_label.text += string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C_Value + Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].EQUIP_Option_C)));
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
		if (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A != 999)
		{
			if (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A.Equals(108))
			{
				P_Buff_A_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A));
				P_Buff_A_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Plus * (float)Misile_DB.M_Value[Now_Data.me.Misile_TIER[Misile_ID]] / 100f, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A)));
				P_Buff_A_value_label.text += "  [67FF00FF]▶ ";
				P_Buff_A_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Plus * (float)Misile_DB.M_Value[Now_Data.me.Misile_TIER[Misile_ID] + 1] / 100f, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A)));
			}
			else
			{
				P_Buff_A_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A));
				switch (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A)
				{
				default:
					P_Buff_A_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A)));
					P_Buff_A_value_label.text += "  [67FF00FF]▶ ";
					P_Buff_A_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Plus * (float)(Now_Data.me.Misile_TIER[Misile_ID] + 1), Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A)));
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
					P_Buff_A_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A)));
					P_Buff_A_value_label.text += "  [67FF00FF]▶ ";
					P_Buff_A_value_label.text += string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A_Plus * (float)(Now_Data.me.Misile_TIER[Misile_ID] + 1), Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_A)));
					break;
				}
			}
		}
		else
		{
			P_Buff_A_label.text = string.Empty;
			P_Buff_A_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B != 999)
		{
			if (Now_Data.me.Misile_TIER[Misile_ID] >= 3)
			{
				P_Buff_B_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B));
				switch (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B)
				{
				default:
					P_Buff_B_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B)));
					P_Buff_B_value_label.text += "  [67FF00FF]▶ ";
					P_Buff_B_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B));
					P_Buff_B_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B_Plus * (float)(Now_Data.me.Misile_TIER[Misile_ID] + 1), Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B)));
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
					P_Buff_B_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B)));
					P_Buff_B_value_label.text += "  [67FF00FF]▶ ";
					P_Buff_B_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B));
					P_Buff_B_value_label.text += string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B_Plus * (float)(Now_Data.me.Misile_TIER[Misile_ID] + 1), Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_B)));
					break;
				}
			}
			else
			{
				P_Buff_B_label.text = "[767676]???";
				P_Buff_B_value_label.text = "[767676]LV.3";
			}
		}
		else
		{
			P_Buff_B_label.text = string.Empty;
			P_Buff_B_value_label.text = string.Empty;
		}
		if (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C != 999)
		{
			if (Now_Data.me.Misile_TIER[Misile_ID] >= 10)
			{
				P_Buff_C_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C));
				switch (Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C)
				{
				default:
					P_Buff_C_value_label.text = string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C)));
					P_Buff_C_value_label.text += "  [67FF00FF]▶ ";
					P_Buff_C_value_label.text += string.Format("{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C_Plus * (float)(Now_Data.me.Misile_TIER[Misile_ID] + 1), Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C)));
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
					P_Buff_C_value_label.text = string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C_Plus * (float)Now_Data.me.Misile_TIER[Misile_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C)));
					P_Buff_C_value_label.text += "  [67FF00FF]▶ ";
					P_Buff_C_value_label.text += string.Format("-{0}{1}", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C_Value + Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C_Plus * (float)(Now_Data.me.Misile_TIER[Misile_ID] + 1), Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.misile_DB[Misile_ID].BOX_Option_C)));
					break;
				}
			}
			else
			{
				P_Buff_C_label.text = "[767676]???";
				P_Buff_C_value_label.text = "[767676]LV.10";
			}
		}
		else
		{
			P_Buff_C_label.text = string.Empty;
			P_Buff_C_value_label.text = string.Empty;
		}
		UI_Master.me.Popup(base.gameObject);
	}
}
