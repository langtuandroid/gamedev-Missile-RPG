using Keiwando.BigInteger;
using UnityEngine;

public class Buidling_Popup : MonoBehaviour
{
	public UILabel Building_Name_label;

	public UI2DSprite Building_sprite;

	public UILabel Building_Word_label;

	public UILabel Building_Option_A_label;

	public UILabel Building_Option_B_label;

	public UILabel Building_Option_A_Value_label;

	public UILabel Building_Option_B_Value_label;

	public UISlider Building_Point_Slider;

	public UILabel Building_Point_label;

	public GameObject Upgrade_BTN;

	public UILabel Upgrade_Price_label;

	public UISprite Upgrade_Price_BG;

	public GameObject YET_BTN;

	public BigInteger Upgrade_Price;

	public Building_Icon[] building_Icons;

	public int Selected_ID;

	public void Setting(int ID)
	{
		Selected_ID = ID;
		base.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
		Building_Name_label.text = Localization.Get(string.Format("BUILDING_NAME_{0:000}", ID));
		if (Now_Data.me.Building_LV[ID].Equals(0))
		{
			YET_BTN.SetActive(false);
			Upgrade_BTN.SetActive(false);
			Building_sprite.color = Color.black;
		}
		else
		{
			Building_Name_label.text += string.Format(" LV.{0}", Now_Data.me.Building_LV[ID]);
			Building_sprite.color = Color.white;
		}
		Building_sprite.sprite2D = Sprite_DB.me.Map_Icon[ID];
		Building_Word_label.text = Localization.Get(string.Format("BUILDING_WORD_{0:000}", ID));
		Building_Option_A_label.text = Localization.Get(string.Format("BUILDING_OPTION_A_{0:000}", ID));
		Building_Option_B_label.text = Localization.Get(string.Format("BUILDING_OPTION_B_{0:000}", ID));
		if (Now_Data.me.Building_LV[ID].Equals(0))
		{
			Building_Option_A_Value_label.text = "???";
		}
		else
		{
			Building_Option_A_Value_label.text = Time_Checker.ShowTime_Label_noT(Now_Data.me.Buidling_Working_time_Origin[ID] * (100f - (float)(Now_Data.me.Building_LV[ID] - 1) * 2.5f) / 100f);
		}
		int num = Now_Data.me.Building_LV[ID] / 5 + 1;
		switch (ID)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 7:
			Building_Option_B_Value_label.text = string.Format("+{0}", num);
			break;
		case 5:
		{
			BigInteger target = Monster_DB.me.Monster_Gold_by_LV(1, Now_Data.me.LV, false) * (int)(150f * (100f + (float)(num - 1) * 2.5f)) / 100;
			Building_Option_B_Value_label.text = Now_Data.INT_to_ABC(target);
			break;
		}
		case 6:
			Building_Option_B_Value_label.text = string.Format("{0}", 100 + (num - 1) * 50);
			break;
		}
	}

	public void Upgrade()
	{
		if (Now_Data.me.CRYSTAL_Possible(Upgrade_Price))
		{
			Now_Data.me.Building_Point[Selected_ID] -= 2 + Now_Data.me.Building_LV[Selected_ID];
			Security.SetInt(string.Format("Building_Point_{0:000}", Selected_ID), Now_Data.me.Building_Point[Selected_ID]);
			Now_Data.me.Building_LV[Selected_ID]++;
			Security.SetInt(string.Format("Building_LV_{0:000}", Selected_ID), Now_Data.me.Building_LV[Selected_ID]);
			Now_Data.me.CRYSTAL_Change(-Upgrade_Price);
			Setting(Selected_ID);
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_GAS"));
		}
	}

	public void NO_POINT()
	{
		UI_Master.me.Warning(Localization.Get("NEED_POINT"));
	}
}
