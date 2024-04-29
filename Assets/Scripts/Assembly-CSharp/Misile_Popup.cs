using System.Collections;
using System.Collections.Generic;
using Keiwando.BigInteger;
using UnityEngine;

public class Misile_Popup : MonoBehaviour
{
	public UI2DSprite Bazuka_sprite;

	public UILabel Bazuka_NAME_label;

	public UILabel Misile_LV_label;

	public UILabel Misile_DMG_Value_label;

	public UILabel Misile_MISILE_PER_Value_label;

	public UILabel Misile_CRITICAL_label;

	public UILabel Misile_CRITICAL_DMG_Value_label;

	public UILabel Misile_AUTOSHOT_Value_label;

	public UILabel Misile_Evolution_Icon_Label;

	public GameObject Misile_LV_Flash;

	public GameObject Upgrade_BTN;

	public UILabel Misile_Upgrade_Price_label;

	public UISprite Misile_Upgrade_Sprite;

	public GameObject Upgrade_BTN_TEN;

	public UILabel Misile_Upgrade_Price_label_TEN;

	public UISprite Misile_Upgrade_Sprite_TEN;

	public Skill_Popup skill_popup;

	public GameObject misile_Classup_panel_Manager;

	public Misile_Classup_panel[] misile_Classup_panels;

	public GameObject Try_again;

	public GameObject misile_TIERUP_Manager;

	public Misile_Classup_panel[] misile_TIERUP_panels;

	public UISprite[] Stars;

	public UISprite[] Gages;

	public int Misile_Tier;

	public BigInteger Price_Upgrade;

	public BigInteger Price_Upgrade_TEN;

	public GameObject Drop_Port_BTN;

	public GameObject Drop_Port_YET;

	public BazukaParts_Component_Icon[] bazuka_Parts_Icons;

	public GameObject Bajuka_Change_Popup;

	public Bazuka_SetIcon[] Bazuka_SetIcons;

	public GameObject EQUIP_ICON;

	public GameObject Skill_6_BG;

	public List<int> Target_Parts_IDs;

	public List<int> HIDE_things;

	public List<int> SHOW_things;

	public void Setting()
	{
		Misile_UI_Update(false);
		Upgrade_BTN_TEN.SetActive(false);
		misile_Classup_panel_Manager.SetActive(false);
		if (Now_Data.me.LV > 260)
		{
			Drop_Port_BTN.SetActive(true);
			Drop_Port_YET.SetActive(false);
		}
		else
		{
			Drop_Port_BTN.SetActive(false);
			Drop_Port_YET.SetActive(true);
		}
	}

	public void Misile_UI_Update(bool TEN)
	{
		Bazuka_NAME_label.text = Localization.Get(string.Format("BAZUKA_NAME_{0:000}", Now_Data.me.Bazuka_ID));
		Bazuka_sprite.sprite2D = Sprite_DB.me.Bazuka_Sprite[Now_Data.me.Bazuka_ID];
		Misile_LV_label.text = string.Format("LV.{0}", Now_Data.me.LV_Misile);
		Misile_DMG_Value_label.text = string.Format("{0} : {1}", Localization.Get("STATUS_008_NAME_A"), Now_Data.INT_to_ABC(Misile_DB.me.DMG_by_LV(0, Now_Data.me.LV_Misile)));
		for (int i = 0; i < bazuka_Parts_Icons.Length; i++)
		{
			bazuka_Parts_Icons[i].Setting();
		}
		int num = Now_Data.me.LV_Misile % 20 - 1;
		for (int j = 0; j < Gages.Length; j++)
		{
			if (j > num)
			{
				Gages[j].spriteName = string.Format("UI_Template_UpgradeEmpty");
			}
			else
			{
				Gages[j].spriteName = string.Format("UI_Template_UpgradeSpecial");
			}
		}
		Misile_Evolution_Icon_Label.text = "★";
		if (Now_Data.me.LV_Misile % 100 >= 80)
		{
			Misile_Evolution_Icon_Label.text = "X2";
		}
		if (Now_Data.me.LV_Misile < 999999)
		{
			Upgrade_BTN.SetActive(true);
			Price_Upgrade = Misile_DB.me.Price_by_LV(0, Now_Data.me.LV_Misile);
			Misile_Upgrade_Price_label.text = Now_Data.INT_to_ABC(Price_Upgrade);
			if (Now_Data.me.Gold_Possible(Price_Upgrade))
			{
				Misile_Upgrade_Sprite.spriteName = string.Format("Btn_UpgradeMineral");
			}
			else
			{
				Misile_Upgrade_Sprite.spriteName = string.Format("Btn_UpgradeDisabled");
			}
		}
		else
		{
			Upgrade_BTN.SetActive(false);
		}
		if (TEN)
		{
			if (UI_Master.me.Misile_Upgrade_Popup.gameObject.activeSelf)
			{
				StopAllCoroutines();
				StartCoroutine(Wait_Upgrade());
			}
			if (Now_Data.me.LV_Misile + 10 < 99999)
			{
				Upgrade_BTN_TEN.SetActive(true);
				Price_Upgrade_TEN = new BigInteger(0L);
				for (int k = Now_Data.me.LV_Misile; k < Now_Data.me.LV_Misile + 10; k++)
				{
					Price_Upgrade_TEN += Misile_DB.me.Price_by_LV(0, k);
				}
				Misile_Upgrade_Price_label_TEN.text = Now_Data.INT_to_ABC(Price_Upgrade_TEN);
				if (Now_Data.me.Gold_Possible(Price_Upgrade_TEN))
				{
					Misile_Upgrade_Sprite_TEN.spriteName = string.Format("Btn_UpgradeMineral");
				}
				else
				{
					Misile_Upgrade_Sprite_TEN.spriteName = string.Format("Btn_UpgradeDisabled");
				}
			}
			else
			{
				Upgrade_BTN_TEN.SetActive(false);
			}
		}
		skill_popup.Setting(true);
	}

	public IEnumerator Wait_Upgrade()
	{
		yield return new WaitForSeconds(3f);
		Upgrade_BTN_TEN.SetActive(false);
	}

	public void Misile_Upgrade()
	{
		if (Now_Data.me.Gold_Possible(Price_Upgrade))
		{
			if (Random.Range(0f, 100f) < Now_Data.me.Price_Free_Misile)
			{
				UI_Master.me.Good_MSG(Localization.Get("FREE_UP"));
			}
			else
			{
				Now_Data.me.GoldChange(-Price_Upgrade);
			}
			Now_Data.me.LV_Misile++;
			Misile_CLASS_CHECK(true);
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_MINERAL"));
		}
	}

	public void Misile_Upgrade_TEN()
	{
		if (Now_Data.me.Gold_Possible(Price_Upgrade_TEN))
		{
			if (Random.Range(0f, 100f) < Now_Data.me.Price_Free_Misile)
			{
				UI_Master.me.Warning(Localization.Get("FREE_UP"));
			}
			else
			{
				Now_Data.me.GoldChange(-Price_Upgrade_TEN);
			}
			for (int i = 0; i < 10; i++)
			{
				Now_Data.me.LV_Misile++;
				Misile_CLASS_CHECK(false);
			}
			Misile_CLASS_CHECK(true);
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_MINERAL"));
		}
	}

	public void Misile_CLASS_CHECK(bool FIN)
	{
		if (FIN)
		{
			Main_Player.me.Status_Setting();
			SoundManager.me.Congretu();
			Misile_UI_Update(true);
			Misile_LV_Flash.SetActive(false);
			Misile_LV_Flash.SetActive(true);
			Security.SetInt("LV_Misile", Now_Data.me.LV_Misile);
		}
		if ((Now_Data.me.LV_Misile % 20).Equals(0))
		{
			if ((Now_Data.me.LV_Misile % 100).Equals(0))
			{
				UI_Master.me.Good_MSG(Localization.Get("ATK_DOUBLE"));
			}
			Random_Setting();
			UI_Master.me.Popup(misile_Classup_panel_Manager);
		}
	}

	public void Random_Setting()
	{
		Target_Parts_IDs.Clear();
		for (int i = 0; i < 7; i++)
		{
			Target_Parts_IDs.Add(i);
		}
		int index = Random.Range(0, Target_Parts_IDs.Count);
		misile_Classup_panels[0].Setting(Target_Parts_IDs[index]);
		Target_Parts_IDs.RemoveAt(index);
		index = Random.Range(0, Target_Parts_IDs.Count);
		misile_Classup_panels[1].Setting(Target_Parts_IDs[index]);
		Target_Parts_IDs.RemoveAt(index);
		index = Random.Range(0, Target_Parts_IDs.Count);
		misile_Classup_panels[2].Setting(Target_Parts_IDs[index]);
		Target_Parts_IDs.RemoveAt(index);
		misile_Classup_panels[0].gameObject.SetActive(false);
		misile_Classup_panels[0].gameObject.SetActive(true);
		misile_Classup_panels[1].gameObject.SetActive(false);
		misile_Classup_panels[1].gameObject.SetActive(true);
		misile_Classup_panels[2].gameObject.SetActive(false);
		misile_Classup_panels[2].gameObject.SetActive(true);
	}

	public void Class_UP(int ID)
	{
	}

	public void Get_Parts(int ID)
	{
		Now_Data.me.Bazuka_Parts[ID]++;
		if ((float)Random.Range(0, 100) < Now_Data.me.Bazuka_compo_double_Per)
		{
			UI_Master.me.Good_MSG(Localization.Get("DOUBLE_GET"));
			Now_Data.me.Bazuka_Parts[ID]++;
		}
		Security.SetInt(string.Format("Bazuka_Parts_{0:000}", ID), Now_Data.me.Bazuka_Parts[ID]);
		Fight_Master.me.All_setting();
		Setting();
	}

	public void All_Unlock()
	{
		for (int i = 0; i < UI_Master.me.skill_Use_BTNs.Length; i++)
		{
			Now_Data.me.Active_Skill_LV[i] += 10;
			UI_Master.me.skill_Use_BTNs[i].Setting();
		}
		Setting();
	}

	public void Open_Change_Bazuka()
	{
		Update_Bazuka_popup();
		UI_Master.me.Popup(Bajuka_Change_Popup);
		UI_Master.me.uranium_Gift_Popup.Setting(7);
	}

	public void Update_Bazuka_popup()
	{
		for (int i = 0; i < Bazuka_SetIcons.Length; i++)
		{
			Bazuka_SetIcons[i].Setting();
		}
		EQUIP_ICON.gameObject.SetActive(false);
		EQUIP_ICON.transform.localPosition = Bazuka_SetIcons[Now_Data.me.Bazuka_ID].transform.localPosition;
		EQUIP_ICON.gameObject.SetActive(true);
	}
}
