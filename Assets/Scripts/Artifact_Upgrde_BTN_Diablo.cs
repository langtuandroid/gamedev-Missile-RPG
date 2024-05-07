using Keiwando.BigInteger;
using UnityEngine;

public class Artifact_Upgrde_BTN_Diablo : MonoBehaviour
{
	public int Artifact_ID;

	public UI2DSprite main_sprite;

	public UILabel Artifact_LV_label;

	public UILabel Artifact_Name_label;

	public UILabel Artifact_Word_A_label;

	public UILabel Artifact_Word_B_label;

	public GameObject Parts_Count_BTN;

	public GameObject Upgrade_BTN;

	public GameObject MAXLV_btn;

	public UISprite Upgrade_sprite;

	public UILabel Upgrade_label;

	public UILabel Parts_count_label;

	public BigInteger MAIN_Value;

	public BigInteger Price_Upgrade;

	public int Need_Parts_counts;

	public BigInteger target_GOLD;

	public GameObject Flash;

	public UISprite Main_BG;

	private BigInteger Value_A;

	private BigInteger Value_B;

	public void Setting(bool OPEN)
	{
		Upgrade_BTN.SetActive(false);
		if (OPEN)
		{
			Text_Update();
			Flash.SetActive(false);
		}
		Price_Update(false);
	}

	public void Text_Update()
	{
		if (Now_Data.me.Diablo_Artifact_LV[Artifact_ID].Equals(0))
		{
			Main_BG.color = new Color32(130, 105, 70, byte.MaxValue);
			Artifact_LV_label.gameObject.SetActive(false);
			Artifact_Name_label.text = "???";
			Artifact_Word_A_label.text = "???";
			Artifact_Word_B_label.text = string.Empty;
			Upgrade_BTN.SetActive(false);
			Parts_Count_BTN.SetActive(false);
			main_sprite.sprite2D = Sprite_DB.me.Diablo_Artifact_Sprite[Artifact_ID];
			main_sprite.color = Color.black;
			return;
		}
		Main_BG.color = Color.white;
		Artifact_LV_label.text = string.Format("LV.{0}", Now_Data.me.Diablo_Artifact_LV[Artifact_ID]);
		Artifact_LV_label.gameObject.SetActive(true);
		Artifact_Name_label.text = Localization.Get(string.Format("ARTIFACT_NAME_DIABLO_{0:000}", Artifact_ID));
		if (Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type != 999)
		{
			switch (Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)
			{
			default:
				Artifact_Word_A_label.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)Now_Data.me.Diablo_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)));
				break;
			case 77:
			{
				float num = Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value;
				for (int i = 0; i < Now_Data.me.Diablo_Artifact_LV[Artifact_ID]; i++)
				{
					num *= 2f;
				}
				Artifact_Word_A_label.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)), num, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)));
				break;
			}
			case 51:
			case 52:
			case 54:
			case 65:
				Artifact_Word_A_label.text = string.Format("{0} {1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)Now_Data.me.Diablo_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)));
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
				Artifact_Word_A_label.text = string.Format("{0} -{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)Now_Data.me.Diablo_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)));
				break;
			}
		}
		else
		{
			Artifact_Word_B_label.text = string.Empty;
		}
		if (Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type != 999)
		{
			switch (Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)
			{
			default:
				Artifact_Word_B_label.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)Now_Data.me.Diablo_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)));
				break;
			case 77:
			{
				float num2 = Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value;
				for (int j = 0; j < Now_Data.me.Diablo_Artifact_LV[Artifact_ID]; j++)
				{
					num2 *= 2f;
				}
				Artifact_Word_B_label.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)), num2, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)));
				break;
			}
			case 51:
			case 52:
			case 54:
			case 65:
				Artifact_Word_B_label.text = string.Format("{0} {1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)Now_Data.me.Diablo_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)));
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
				Artifact_Word_B_label.text = string.Format("{0} -{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)Now_Data.me.Diablo_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)));
				break;
			}
		}
		else
		{
			Artifact_Word_B_label.text = string.Empty;
		}
		main_sprite.sprite2D = Sprite_DB.me.Diablo_Artifact_Sprite[Artifact_ID];
		Upgrade_BTN.SetActive(true);
	}

	public void Price_Update(bool TEN)
	{
		if (Now_Data.me.Diablo_Artifact_LV[Artifact_ID] <= 0)
		{
			return;
		}
		if (Now_Data.me.Diablo_Artifact_LV[Artifact_ID] < Artifact_DB.me.diablo_artifact_DB[Artifact_ID].MAX_LV)
		{
			Need_Parts_counts = Now_Data.me.Diablo_Artifact_LV[Artifact_ID] + 1;
			Parts_count_label.text = string.Format("{0}/{1}", Now_Data.me.Diablo_Artifact_Parts_count[Artifact_ID], Need_Parts_counts);
			switch (Now_Data.me.Diablo_Artifact_LV[Artifact_ID])
			{
			default:
				Price_Upgrade = 1000000;
				break;
			case 1:
				Price_Upgrade = 500;
				break;
			case 2:
				Price_Upgrade = 2000;
				break;
			case 3:
				Price_Upgrade = 5000;
				break;
			case 4:
				Price_Upgrade = 10000;
				break;
			case 5:
				Price_Upgrade = 20000;
				break;
			case 6:
				Price_Upgrade = 50000;
				break;
			case 7:
				Price_Upgrade = 200000;
				break;
			case 8:
				Price_Upgrade = 500000;
				break;
			}
			Upgrade_label.text = Price_Upgrade.ToString();
			if (Now_Data.me.CRYSTAL_Possible(Price_Upgrade))
			{
				Upgrade_sprite.spriteName = string.Format("Btn_UpgradeUranium");
			}
			else
			{
				Upgrade_sprite.spriteName = string.Format("Btn_UpgradeDisabled");
			}
			if (Now_Data.me.Diablo_Artifact_Parts_count[Artifact_ID] >= Need_Parts_counts)
			{
				Upgrade_BTN.SetActive(true);
				Parts_Count_BTN.SetActive(false);
			}
			else
			{
				Upgrade_BTN.SetActive(false);
				Parts_Count_BTN.SetActive(true);
			}
		}
		else
		{
			MAXLV_btn.SetActive(true);
			Upgrade_BTN.SetActive(false);
			Parts_Count_BTN.SetActive(false);
		}
	}

	public void Upgrade_One()
	{
		if (Now_Data.me.P_STONE_Possible(Price_Upgrade))
		{
			SoundManager.me.Artifact_Upgrade();
			Now_Data.me.Diablo_Artifact_LV[Artifact_ID]++;
			Security.SetInt(string.Format("Diablo_Artifact_LV_{0:000}", Artifact_ID), Now_Data.me.Diablo_Artifact_LV[Artifact_ID]);
			Now_Data.me.P_STONE_Change(-Price_Upgrade);
			UI_Master.me.artifact_Panel.Set_BTNs(false);
			Flash.SetActive(false);
			Flash.SetActive(true);
			Text_Update();
			Price_Update(false);
			Fight_Master.me.All_setting();
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_HELLSTONE"));
		}
	}
}
