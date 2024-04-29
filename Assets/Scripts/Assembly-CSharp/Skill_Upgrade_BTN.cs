using System.Collections;
using Keiwando.BigInteger;
using UnityEngine;

public class Skill_Upgrade_BTN : MonoBehaviour
{
	public int Skill_ID;

	public UILabel Skill_LV_label;

	public UILabel Skill_Name_label;

	public UILabel Skill_Word_label;

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

	public BigInteger target_GOLD;

	public GameObject Flash;

	public GameObject LOCK_BG;

	public void Setting(bool OPEN)
	{
		if (OPEN)
		{
			Clean();
			Text_Update();
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
		if (Now_Data.me.Active_Skill_LV[Skill_ID].Equals(0))
		{
			LOCK_BG.SetActive(true);
			Skill_LV_label.gameObject.SetActive(false);
			Skill_Name_label.text = "???";
			Skill_Word_label.text = "???";
			return;
		}
		LOCK_BG.SetActive(false);
		Skill_LV_label.text = string.Format("LV.{0}", Now_Data.me.Active_Skill_LV[Skill_ID]);
		Skill_LV_label.gameObject.SetActive(true);
		Skill_Name_label.text = Localization.Get(string.Format("SKILL_{0:000}_NAME", Skill_ID));
		Skill_Word_label.text = Localization.Get(string.Format("SKILL_{0:000}_WORD_A", Skill_ID));
		if (Skill_ID.Equals(0))
		{
			Skill_Word_label.text = Localization.Get(string.Format("SKILL_{0:000}_WORD_B", Skill_ID));
			Skill_Word_label.text += string.Format("{0}", Misile_DB.me.skill_DB[Skill_ID].Skill_LV[Now_Data.me.Active_Skill_LV[Skill_ID]].Value * 100);
			Skill_Word_label.text += Localization.Get(string.Format("SKILL_{0:000}_WORD_C", Skill_ID));
		}
		else if (Skill_ID != 3)
		{
			Skill_Word_label.text += string.Format("{0:N0}", Misile_DB.me.skill_DB_Basic[Skill_ID].Remain_Time * (100f + Now_Data.me.SKILL_Effectivetime[Skill_ID] + Now_Data.me.ALL_SKILL_Effectivetime) / 100f);
			Skill_Word_label.text += Localization.Get(string.Format("SKILL_{0:000}_WORD_B", Skill_ID));
			Skill_Word_label.text += string.Format("{0}", Misile_DB.me.skill_DB[Skill_ID].Skill_LV[Now_Data.me.Active_Skill_LV[Skill_ID]].Value);
			Skill_Word_label.text += Localization.Get(string.Format("SKILL_{0:000}_WORD_C", Skill_ID));
		}
		else
		{
			Skill_Word_label.text += string.Format("{0:N0}", (float)Misile_DB.me.skill_DB[Skill_ID].Skill_LV[Now_Data.me.Active_Skill_LV[Skill_ID]].Value * (100f + Now_Data.me.SKILL_Effectivetime[Skill_ID] + Now_Data.me.ALL_SKILL_Effectivetime) / 100f);
			Skill_Word_label.text += Localization.Get(string.Format("SKILL_{0:000}_WORD_B", Skill_ID));
		}
	}

	public void Price_Update(bool TEN)
	{
		if (Now_Data.me.Active_Skill_LV[Skill_ID] < Misile_DB.me.skill_DB[Skill_ID].Skill_LV.Length - 1)
		{
			Price_Upgrade = new BigInteger(Misile_DB.me.skill_DB[Skill_ID].Skill_LV[Now_Data.me.Active_Skill_LV[Skill_ID]].Price);
			Upgrade_label.text = Now_Data.INT_to_ABC(Price_Upgrade);
			if (Now_Data.me.Gold_Possible(Price_Upgrade))
			{
				Upgrade_sprite.spriteName = string.Format("Btn_UpgradeMineral");
				if (Skill_ID > 0 && Now_Data.me.Active_Skill_LV[Skill_ID - 1] < 1)
				{
					Upgrade_sprite.spriteName = string.Format("Btn_UpgradeDisabled");
				}
			}
			else
			{
				Upgrade_sprite.spriteName = string.Format("Btn_UpgradeDisabled");
			}
			Upgrade_BTN.SetActive(true);
		}
		else
		{
			Upgrade_BTN.SetActive(false);
		}
	}

	public IEnumerator Wait_Upgrade()
	{
		yield return new WaitForSeconds(3f);
		Upgrade_BTN_TEN.SetActive(false);
		Upgrade_BTN_TENTEN.SetActive(false);
	}

	public void Upgrade_One()
	{
		if (Skill_ID > 0 && Now_Data.me.Active_Skill_LV[Skill_ID - 1] < 1)
		{
			UI_Master.me.Warning(Localization.Get("NEED_SKILL"));
		}
		else if (Now_Data.me.Gold_Possible(Price_Upgrade))
		{
			SoundManager.me.Skill_Upgrade();
			Now_Data.me.Active_Skill_LV[Skill_ID]++;
			Security.SetInt(string.Format("Active_Skill_LV_{0:000}", Skill_ID), Now_Data.me.Active_Skill_LV[Skill_ID]);
			Now_Data.me.GoldChange(-Price_Upgrade);
			UI_Master.me.Misile_Upgrade_Popup.Setting();
			Flash.SetActive(false);
			Flash.SetActive(true);
			Text_Update();
			Price_Update(true);
			if (Now_Data.me.Active_Skill_LV[Skill_ID].Equals(1))
			{
				UI_Master.me.skill_Use_BTNs[Skill_ID].Setting();
			}
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_MINERAL"));
		}
	}

	public void Upgrade_TEN()
	{
		if (Now_Data.me.Gold_Possible(Price_Upgrade_TEN))
		{
			Flash.SetActive(false);
			Flash.SetActive(true);
			SoundManager.me.Skill_Upgrade();
			Now_Data.me.Active_Skill_LV[Skill_ID] += 10;
			Now_Data.me.GoldChange(-Price_Upgrade_TEN);
			UI_Master.me.Misile_Upgrade_Popup.Setting();
			Text_Update();
			Price_Update(true);
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_MINERAL"));
		}
	}

	public void Upgrade_TENTEN()
	{
		if (Now_Data.me.Gold_Possible(Price_Upgrade_TENTEN))
		{
			Flash.SetActive(false);
			Flash.SetActive(true);
			SoundManager.me.Skill_Upgrade();
			Now_Data.me.Active_Skill_LV[Skill_ID] += 100;
			Now_Data.me.GoldChange(-Price_Upgrade_TENTEN);
			UI_Master.me.Misile_Upgrade_Popup.Setting();
			Text_Update();
			Price_Update(true);
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_MINERAL"));
		}
	}
}
