using Keiwando.BigInteger;
using UnityEngine;

public class Artifact_Upgrde_BTN : MonoBehaviour
{
	public int Atifact_TYPE;

	public int Artifact_ID;

	public UI2DSprite main_sprite;

	public UILabel Artifact_LV_label;

	public UILabel Artifact_Name_label;

	public UILabel Artifact_Word_A_label;

	public UILabel Artifact_Word_B_label;

	public GameObject Upgrade_BTN;

	public GameObject Upgrade_BTN_TEN;

	public GameObject Upgrade_BTN_TENTEN;

	public UISprite Upgrade_sprite;

	public UISprite Upgrade_sprite_TEN;

	public UISprite Upgrade_sprite_TENTEN;

	public UILabel Upgrade_label;

	public UILabel Upgrade_label_TEN;

	public UILabel Upgrade_label_TENTEN;

	public BigInteger MAIN_Value;

	public BigInteger Price_Upgrade;

	public BigInteger Price_Upgrade_TEN;

	public BigInteger Price_Upgrade_TENTEN;

	public GameObject Flash;

	public UISprite Main_BG;

	public GameObject MAXLV_btn;

	private BigInteger Value_A;

	private BigInteger Value_B;

	public void Setting(bool OPEN)
	{
		Clean();
		if (OPEN)
		{
			Text_Update();
			Flash.SetActive(false);
		}
		Price_Update(false);
	}

	public void Clean()
	{
		Upgrade_BTN_TEN.SetActive(false);
		Upgrade_BTN_TENTEN.SetActive(false);
	}

	public void Text_Update()
	{
		if (Now_Data.me.Now_Artifact_LV[Artifact_ID].Equals(0))
		{
			Main_BG.color = new Color32(130, 105, 70, byte.MaxValue);
			Artifact_LV_label.gameObject.SetActive(false);
			Artifact_Name_label.text = "???";
			Artifact_Word_A_label.text = "???";
			Artifact_Word_B_label.text = string.Empty;
			Upgrade_BTN.SetActive(false);
			return;
		}
		Main_BG.color = Color.white;
		Artifact_LV_label.text = string.Format("LV.{0}", Now_Data.me.Now_Artifact_LV[Artifact_ID]);
		Artifact_LV_label.gameObject.SetActive(true);
		Artifact_Name_label.text = Localization.Get(string.Format("ARTIFACT_{0:000}_NAME", Artifact_ID));
		if (Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type != 999)
		{
			switch (Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)
			{
			default:
				Artifact_Word_A_label.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)));
				break;
			case 23:
				Artifact_Word_A_label.text = string.Format("{0}{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)));
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
				Artifact_Word_A_label.text = string.Format("{0}-{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)));
				break;
			}
		}
		if (Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type != 999)
		{
			Artifact_Word_B_label.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)));
		}
		main_sprite.sprite2D = Sprite_DB.me.Artifact_Sprite[Artifact_ID];
		Upgrade_BTN.SetActive(true);
	}

	public void Price_Update(bool TEN)
	{
		if (Now_Data.me.Now_Artifact_LV[Artifact_ID] <= 0)
		{
			return;
		}
		if (Now_Data.me.Now_Artifact_LV[Artifact_ID] < Artifact_DB.me.artifact_DB[Artifact_ID].MAX_LV)
		{
			Price_Upgrade = Artifact_DB.me.Price_by_LV(Artifact_ID, Now_Data.me.Now_Artifact_LV[Artifact_ID]) * 10;
			Upgrade_label.text = Now_Data.INT_to_ABC(Price_Upgrade);
			if (Now_Data.me.CRYSTAL_Possible(Price_Upgrade))
			{
				Upgrade_sprite.spriteName = string.Format("Btn_UpgradeGas");
			}
			else
			{
				Upgrade_sprite.spriteName = string.Format("Btn_UpgradeDisabled");
			}
			Upgrade_BTN.SetActive(true);
			MAXLV_btn.SetActive(false);
		}
		else
		{
			MAXLV_btn.SetActive(true);
			Upgrade_BTN.SetActive(false);
		}
	}

	public void Upgrade_One()
	{
		if (Now_Data.me.CRYSTAL_Possible(Price_Upgrade))
		{
			SoundManager.me.Artifact_Upgrade();
			Now_Data.me.Now_Artifact_LV[Artifact_ID]++;
			Security.SetInt(string.Format("Now_Artifact_LV_{0:000}", Artifact_ID), Now_Data.me.Now_Artifact_LV[Artifact_ID]);
			Now_Data.me.CRYSTAL_Change(-Price_Upgrade);
			UI_Master.me.artifact_Panel.Set_BTNs(false);
			Flash.SetActive(false);
			Flash.SetActive(true);
			Text_Update();
			Price_Update(false);
			Fight_Master.me.All_setting();
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_GAS"));
		}
	}

	public void Upgrade_TEN()
	{
		if (Now_Data.me.CRYSTAL_Possible(Price_Upgrade_TEN))
		{
			SoundManager.me.Congretu();
			Now_Data.me.Now_Artifact_LV[Artifact_ID] += 10;
			Security.SetInt(string.Format("Now_Artifact_LV_{0:000}", Artifact_ID), Now_Data.me.Now_Artifact_LV[Artifact_ID]);
			Now_Data.me.CRYSTAL_Change(-Price_Upgrade_TEN);
			UI_Master.me.artifact_Panel.Set_BTNs(false);
			Text_Update();
			Price_Update(true);
			Fight_Master.me.All_setting();
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_GAS"));
		}
	}

	public void Upgrade_TENTEN()
	{
		if (Now_Data.me.CRYSTAL_Possible(Price_Upgrade_TENTEN))
		{
			SoundManager.me.Congretu();
			Now_Data.me.Now_Artifact_LV[Artifact_ID] += 100;
			Security.SetInt(string.Format("Now_Artifact_LV_{0:000}", Artifact_ID), Now_Data.me.Now_Artifact_LV[Artifact_ID]);
			Now_Data.me.CRYSTAL_Change(-Price_Upgrade_TENTEN);
			UI_Master.me.artifact_Panel.Set_BTNs(false);
			Text_Update();
			Price_Update(true);
			Fight_Master.me.All_setting();
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_GAS"));
		}
	}
}
