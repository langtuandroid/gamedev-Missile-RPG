using System.Collections;
using Keiwando.BigInteger;
using UnityEngine;

public class Unit_Upgrade_BTN : MonoBehaviour
{
	public int Unit_ID;

	public int REAL_Unit_ID;

	public UILabel Unit_LV_label;

	public UILabel Unit_Tier_label;

	public UILabel Unit_Name_label;

	public UILabel Unit_DPS_label;

	public UILabel Upgrade_or_Join_Label;

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

	public UISprite[] Unlock_Buff_Icons;

	public UISprite Unit_Type_ICON;

	public GameObject LOCK_BG;

	public UISprite MAIN_BG;

	public UILabel NEXT_DPS_label;

	public bool Super_Upgrade;

	private BigInteger DPS;

	private BigInteger NEXT_DPS;

	private bool ALL_CHECK_NEED;

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
		if (Now_Data.me.Now_Unit_LV[Unit_ID].Equals(0))
		{
			LOCK_BG.SetActive(true);
			Unit_LV_label.gameObject.SetActive(false);
			Unit_Tier_label.gameObject.SetActive(false);
			Unit_Name_label.text = "???";
			Unit_DPS_label.text = Localization.Get("NO_MEMBER");
			Upgrade_or_Join_Label.text = Localization.Get("JOIN");
			if (Unit_ID > 0)
			{
				if (Now_Data.me.Now_Unit_LV[Unit_ID - 1].Equals(0))
				{
					Upgrade_BTN.SetActive(false);
				}
				else if (Now_Data.me.Now_Unit_LV[Unit_ID - 1] >= 1)
				{
					Upgrade_BTN.SetActive(true);
					Price_Update(false);
				}
			}
		}
		else
		{
			if (Now_Data.me.Now_Unit_LV[Unit_ID] <= Unit_DB.me.LV_Point[8])
			{
				Super_Upgrade = false;
			}
			else
			{
				Super_Upgrade = true;
			}
			LOCK_BG.SetActive(false);
			Unit_LV_label.text = string.Format("LV.{0}", Now_Data.me.Now_Unit_LV[Unit_ID]);
			Unit_LV_label.gameObject.SetActive(false);
			Unit_LV_label.gameObject.SetActive(true);
			if (Now_Data.me.Now_Unit_Tier[Unit_ID] > 0)
			{
				Unit_Tier_label.text = string.Format("+{0}", Now_Data.me.Now_Unit_Tier[Unit_ID]);
				Unit_Tier_label.gameObject.SetActive(true);
			}
			else
			{
				Unit_Tier_label.gameObject.SetActive(false);
			}
			Unit_Name_label.text = Localization.Get(string.Format("UNIT_{0:000}_NAME", Unit_ID));
			if (Unit_DB.me.unit_DB[Unit_ID].ATK_SPEED >= 1f)
			{
				DPS = Unit_DB.me.DMG_by_LV(Unit_ID, Now_Data.me.Now_Unit_LV[Unit_ID]) * 10 / new BigInteger((int)(Unit_DB.me.unit_DB[Unit_ID].ATK_SPEED * 10f));
				NEXT_DPS = Unit_DB.me.DMG_by_LV(Unit_ID, Now_Data.me.Now_Unit_LV[Unit_ID] + 1) * 10 / new BigInteger((int)(Unit_DB.me.unit_DB[Unit_ID].ATK_SPEED * 10f));
			}
			Unit_DPS_label.text = string.Format("DPS : {0}", Now_Data.INT_to_ABC(DPS));
			Upgrade_or_Join_Label.text = string.Format("+{0} DPS", Now_Data.INT_to_ABC(NEXT_DPS - DPS));
		}
		if (!Super_Upgrade)
		{
			for (int i = 0; i < Unlock_Buff_Icons.Length; i++)
			{
				if (Now_Data.me.Now_Unit_LV[Unit_ID] >= Unit_DB.me.LV_Point[i])
				{
					Unlock_Buff_Icons[i].spriteName = "UI_Template_UpgradeSpecial";
				}
				else
				{
					Unlock_Buff_Icons[i].spriteName = "UI_Template_UpgradeEmpty";
				}
			}
		}
		else
		{
			for (int j = 0; j < Unlock_Buff_Icons.Length; j++)
			{
				if (Now_Data.me.Now_Unit_LV[Unit_ID] >= Unit_DB.me.LV_Point[j] + Unit_DB.me.LV_Point[8])
				{
					Unlock_Buff_Icons[j].spriteName = "UI_Template_UpgradeSpecial";
				}
				else
				{
					Unlock_Buff_Icons[j].spriteName = "UI_Template_UpgradeEmpty";
				}
			}
		}
		switch (Unit_DB.me.unit_DB[Unit_ID].unit_TYPE)
		{
		case Unit_Type.Human:
			Unit_Type_ICON.spriteName = "Prop_Type_Infantry";
			break;
		case Unit_Type.Mechanic:
			Unit_Type_ICON.spriteName = "Prop_Type_Armor";
			break;
		case Unit_Type.Air:
			Unit_Type_ICON.spriteName = "Prop_Type_Air";
			break;
		}
	}

	public void Price_Update(bool TEN)
	{
		if (Now_Data.me.Now_Unit_LV[Unit_ID] < Unit_DB.me.LV_Point[8])
		{
			REAL_Unit_ID = Unit_ID;
			MAIN_BG.spriteName = "UI_Template_Upgrade";
		}
		else if (Now_Data.me.Now_Unit_LV[Unit_ID].Equals(Unit_DB.me.LV_Point[8]))
		{
			REAL_Unit_ID = Unit_ID;
			MAIN_BG.spriteName = "UI_Template_Upgrade";
		}
		else
		{
			REAL_Unit_ID = Unit_ID + 27;
			MAIN_BG.spriteName = "UI_Template_UpgradeSuper";
		}
		if (!Now_Data.me.Now_Unit_LV[Unit_ID].Equals(Unit_DB.me.LV_Point[8]) && !Now_Data.me.Now_Unit_LV[Unit_ID].Equals(Unit_DB.me.LV_Point[8] * 2))
		{
			bool flag = false;
			if (REAL_Unit_ID.Equals(Unit_ID))
			{
				for (int i = 0; i < Unit_DB.me.LV_Point.Length; i++)
				{
					if ((Now_Data.me.Now_Unit_LV[Unit_ID] + 1).Equals(Unit_DB.me.LV_Point[i]))
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < Unit_DB.me.LV_Point.Length; j++)
				{
					if ((Now_Data.me.Now_Unit_LV[Unit_ID] + 1 - Unit_DB.me.LV_Point[8]).Equals(Unit_DB.me.LV_Point[j]))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				Upgrade_sprite.color = new Color32(236, byte.MaxValue, 54, byte.MaxValue);
			}
			else
			{
				Upgrade_sprite.color = Color.white;
			}
			Price_Upgrade = Unit_DB.me.Price_by_LV(Unit_ID, Now_Data.me.Now_Unit_LV[Unit_ID]);
			Upgrade_label.text = Now_Data.INT_to_ABC(Price_Upgrade);
			if (Now_Data.me.Gold_Possible(Price_Upgrade))
			{
				Upgrade_sprite.spriteName = string.Format("Btn_UpgradeMineral");
			}
			else
			{
				Upgrade_sprite.spriteName = string.Format("Btn_UpgradeDisabled");
			}
			Upgrade_BTN.SetActive(true);
			if (Unit_ID > 0 && Now_Data.me.Now_Unit_LV[Unit_ID - 1].Equals(0))
			{
				Upgrade_BTN.SetActive(false);
			}
		}
		else if (REAL_Unit_ID > 27)
		{
			Upgrade_BTN.SetActive(false);
		}
		else
		{
			Price_Upgrade = Unit_DB.me.Price_by_LV(Unit_ID, Unit_DB.me.LV_Point[8] + 1);
			Upgrade_label.text = Now_Data.INT_to_ABC(Price_Upgrade);
			if (Now_Data.me.Gold_Possible(Price_Upgrade))
			{
				Upgrade_sprite.spriteName = string.Format("Btn_UpgradeMineral");
			}
			else
			{
				Upgrade_sprite.spriteName = string.Format("Btn_UpgradeDisabled");
			}
			Upgrade_or_Join_Label.text = Localization.Get("SUPERUP");
		}
		if (!TEN)
		{
			return;
		}
		if (UI_Master.me.unit_Explain_panel.gameObject.activeSelf)
		{
			StopAllCoroutines();
			StartCoroutine(Wait_Upgrade());
		}
		Upgrade_BTN_TEN.SetActive(false);
		if (Super_Upgrade)
		{
			if (Now_Data.me.Now_Unit_LV[Unit_ID] + 10 < Unit_DB.me.LV_Point[8] * 2)
			{
				Price_Upgrade_TEN = new BigInteger(0L);
				for (int k = Now_Data.me.Now_Unit_LV[Unit_ID]; k < Now_Data.me.Now_Unit_LV[Unit_ID] + 10; k++)
				{
					Price_Upgrade_TEN += Unit_DB.me.Price_by_LV(Unit_ID, k);
				}
				Upgrade_label_TEN.text = Now_Data.INT_to_ABC(Price_Upgrade_TEN);
				if (Now_Data.me.Gold_Possible(Price_Upgrade_TEN))
				{
					Upgrade_sprite_TEN.spriteName = string.Format("Btn_UpgradeMineral");
				}
				else
				{
					Upgrade_sprite_TEN.spriteName = string.Format("Btn_UpgradeDisabled");
				}
				Upgrade_BTN_TEN.SetActive(true);
			}
		}
		else if (Now_Data.me.Now_Unit_LV[Unit_ID] + 10 < Unit_DB.me.LV_Point[8])
		{
			Price_Upgrade_TEN = new BigInteger(0L);
			for (int l = Now_Data.me.Now_Unit_LV[Unit_ID]; l < Now_Data.me.Now_Unit_LV[Unit_ID] + 10; l++)
			{
				Price_Upgrade_TEN += Unit_DB.me.Price_by_LV(Unit_ID, l);
			}
			Upgrade_label_TEN.text = Now_Data.INT_to_ABC(Price_Upgrade_TEN);
			if (Now_Data.me.Gold_Possible(Price_Upgrade_TEN))
			{
				Upgrade_sprite_TEN.spriteName = string.Format("Btn_UpgradeMineral");
			}
			else
			{
				Upgrade_sprite_TEN.spriteName = string.Format("Btn_UpgradeDisabled");
			}
			Upgrade_BTN_TEN.SetActive(true);
		}
		Upgrade_BTN_TENTEN.SetActive(false);
		if (Super_Upgrade)
		{
			if (Now_Data.me.Now_Unit_LV[Unit_ID] + 100 < Unit_DB.me.LV_Point[8] * 2)
			{
				Price_Upgrade_TENTEN = new BigInteger(0L);
				for (int m = Now_Data.me.Now_Unit_LV[Unit_ID]; m < Now_Data.me.Now_Unit_LV[Unit_ID] + 100; m++)
				{
					Price_Upgrade_TENTEN += Unit_DB.me.Price_by_LV(Unit_ID, m);
				}
				Upgrade_label_TENTEN.text = Now_Data.INT_to_ABC(Price_Upgrade_TENTEN);
				if (Now_Data.me.Gold_Possible(Price_Upgrade_TENTEN))
				{
					Upgrade_sprite_TENTEN.spriteName = string.Format("Btn_UpgradeMineral");
				}
				else
				{
					Upgrade_sprite_TENTEN.spriteName = string.Format("Btn_UpgradeDisabled");
				}
				Upgrade_BTN_TENTEN.SetActive(true);
			}
		}
		else if (Now_Data.me.Now_Unit_LV[Unit_ID] + 100 < Unit_DB.me.LV_Point[8])
		{
			Price_Upgrade_TENTEN = new BigInteger(0L);
			for (int n = Now_Data.me.Now_Unit_LV[Unit_ID]; n < Now_Data.me.Now_Unit_LV[Unit_ID] + 100; n++)
			{
				Price_Upgrade_TENTEN += Unit_DB.me.Price_by_LV(Unit_ID, n);
			}
			Upgrade_label_TENTEN.text = Now_Data.INT_to_ABC(Price_Upgrade_TENTEN);
			if (Now_Data.me.Gold_Possible(Price_Upgrade_TENTEN))
			{
				Upgrade_sprite_TENTEN.spriteName = string.Format("Btn_UpgradeMineral");
			}
			else
			{
				Upgrade_sprite_TENTEN.spriteName = string.Format("Btn_UpgradeDisabled");
			}
			Upgrade_BTN_TENTEN.SetActive(true);
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
		if (Unit_ID > 0 && Now_Data.me.Now_Unit_LV[Unit_ID - 1] < 1)
		{
			UI_Master.me.Warning(Localization.Get("NEED_TEN"));
			return;
		}
		if (Now_Data.me.Gold_Possible(Price_Upgrade))
		{
			bool flag = false;
			if (Now_Data.me.Now_Unit_LV[Unit_ID] >= Unit_DB.me.LV_Point[8])
			{
				if (Now_Data.me.Now_Unit_LV[26] > 0)
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				UI_Master.me.Warning(Localization.Get("NEED_LAST_UNIT"));
			}
			else
			{
				if (Random.Range(0f, 100f) < Now_Data.me.Price_Free_Unit)
				{
					SoundManager.me.Congretu();
					UI_Master.me.Good_MSG(Localization.Get("FREE_UP"));
				}
				else
				{
					Now_Data.me.GoldChange(-Price_Upgrade);
				}
				SoundManager.me.Unit_Upgrade();
				Now_Data.me.Now_Unit_LV[Unit_ID]++;
				UI_Master.me.unit_Explain_panel.Set_BTNs(false);
				ALL_CHECK_NEED = false;
				if (!Super_Upgrade)
				{
					for (int i = 0; i < Unit_DB.me.LV_Point.Length; i++)
					{
						if (Now_Data.me.Now_Unit_LV[Unit_ID].Equals(Unit_DB.me.LV_Point[i]))
						{
							ALL_CHECK_NEED = true;
							SoundManager.me.Unit_Upgrade_Unlock_Ability();
							UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.transform.position = UI_Master.me.unit_Explain_panel.label_each_word[i].transform.parent.position;
							UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.SetActive(false);
							UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.SetActive(true);
							break;
						}
					}
				}
				else
				{
					for (int j = 0; j < Unit_DB.me.LV_Point.Length; j++)
					{
						if ((Now_Data.me.Now_Unit_LV[Unit_ID] - Unit_DB.me.LV_Point[8]).Equals(Unit_DB.me.LV_Point[j]))
						{
							ALL_CHECK_NEED = true;
							SoundManager.me.Unit_Upgrade_Unlock_Ability();
							UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.transform.position = UI_Master.me.unit_Explain_panel.label_each_word[j].transform.parent.position;
							UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.SetActive(false);
							UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.SetActive(true);
							break;
						}
					}
				}
				if (ALL_CHECK_NEED)
				{
					Fight_Master.me.All_setting();
				}
				else
				{
					if (Now_Data.me.Now_Unit_LV[Unit_ID].Equals(1))
					{
						if (Now_Data.me.Now_Unit_LV[16] > 0)
						{
							UI_Master.me.screenChange_by_ratio.Main_size = 7.5f;
						}
						else if (Now_Data.me.Now_Unit_LV[7] > 0)
						{
							UI_Master.me.screenChange_by_ratio.Main_size = 7f;
						}
						else if (Now_Data.me.Now_Unit_LV[4] > 0)
						{
							UI_Master.me.screenChange_by_ratio.Main_size = 6.5f;
						}
						UI_Master.me.screenChange_by_ratio.Setting();
						if (Now_Data.me.ALL_DROPPORT == 0)
						{
							UI_Master.me.Popup_Close_All();
						}
					}
					Fight_Master.me.Unit_Setting(Unit_ID);
				}
				Text_Update();
				Price_Update(true);
			}
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_MINERAL"));
		}
		UI_Master.me.unit_Explain_panel.Setting_ID(Unit_ID);
	}

	public void Upgrade_TEN()
	{
		if (Now_Data.me.Gold_Possible(Price_Upgrade_TEN))
		{
			if (Random.Range(0f, 100f) < Now_Data.me.Price_Free_Unit)
			{
				UI_Master.me.Good_MSG(Localization.Get("FREE_UP"));
			}
			else
			{
				Now_Data.me.GoldChange(-Price_Upgrade_TEN);
			}
			SoundManager.me.Unit_Upgrade();
			UI_Master.me.unit_Explain_panel.Set_BTNs(false);
			for (int i = 0; i < 10; i++)
			{
				Now_Data.me.Now_Unit_LV[Unit_ID]++;
				for (int j = 0; j < Unit_DB.me.LV_Point.Length; j++)
				{
					if (Now_Data.me.Now_Unit_LV[Unit_ID].Equals(Unit_DB.me.LV_Point[j]))
					{
						ALL_CHECK_NEED = true;
						SoundManager.me.Unit_Upgrade_Unlock_Ability();
						UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.transform.position = UI_Master.me.unit_Explain_panel.label_each_word[j].transform.parent.position;
						UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.SetActive(false);
						UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.SetActive(true);
						break;
					}
				}
			}
			Text_Update();
			Price_Update(true);
			Fight_Master.me.Unit_Setting(Unit_ID);
			if (ALL_CHECK_NEED)
			{
				Fight_Master.me.All_setting();
			}
			else
			{
				Fight_Master.me.Unit_Setting(Unit_ID);
			}
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_MINERAL"));
		}
		UI_Master.me.unit_Explain_panel.Setting_ID(Unit_ID);
	}

	public void Upgrade_TENTEN()
	{
		if (Now_Data.me.Gold_Possible(Price_Upgrade_TENTEN))
		{
			if (Random.Range(0f, 100f) < Now_Data.me.Price_Free_Unit)
			{
				UI_Master.me.Good_MSG(Localization.Get("FREE_UP"));
			}
			else
			{
				Now_Data.me.GoldChange(-Price_Upgrade_TENTEN);
			}
			SoundManager.me.Unit_Upgrade();
			UI_Master.me.unit_Explain_panel.Set_BTNs(false);
			for (int i = 0; i < 100; i++)
			{
				Now_Data.me.Now_Unit_LV[Unit_ID]++;
				for (int j = 0; j < Unit_DB.me.LV_Point.Length; j++)
				{
					if (Now_Data.me.Now_Unit_LV[Unit_ID].Equals(Unit_DB.me.LV_Point[j]))
					{
						ALL_CHECK_NEED = true;
						SoundManager.me.Unit_Upgrade_Unlock_Ability();
						UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.transform.position = UI_Master.me.unit_Explain_panel.label_each_word[j].transform.parent.position;
						UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.SetActive(false);
						UI_Master.me.unit_Explain_panel.Skill_Unlock_Flash.SetActive(true);
						break;
					}
				}
			}
			Text_Update();
			Price_Update(true);
			if (ALL_CHECK_NEED)
			{
				Fight_Master.me.All_setting();
			}
			else
			{
				Fight_Master.me.Unit_Setting(Unit_ID);
			}
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_MINERAL"));
		}
		UI_Master.me.unit_Explain_panel.Setting_ID(Unit_ID);
	}

	public void OnClick()
	{
		UI_Master.me.unit_Explain_panel.Setting_ID(Unit_ID);
	}
}
