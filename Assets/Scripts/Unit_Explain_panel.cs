using System.Collections;
using Keiwando.BigInteger;
using UnityEngine;

public class Unit_Explain_panel : MonoBehaviour
{
	public int Unit_ID;

	public UILabel label_NAME;

	public UILabel label_LV;

	public GameObject Tier_tip;

	public UILabel label_Tier_Buff;

	public UILabel[] label_each_LV;

	public UILabel[] label_each_word;

	public UILabel label_Tierup_Price;

	public Unit_Upgrade_BTN[] unit_upgrade_BTNs;

	public GameObject Flash;

	public GameObject Skill_Unlock_Flash;

	public GameObject TierUp_BTN;

	public UISprite Rank_sprite;

	public UISprite Rank_sprite_BG;

	public UIPanel Menu_Panel;

	public UIScrollView uiScrollView;

	public GameObject Last_Unit;

	public Vector3 offset_B;

	public BigInteger Price_Tierup;

	public void Setting()
	{
		Set_BTNs(true);
		Setting_ID(Unit_ID);
		UI_Master.me.Popup(base.gameObject);
		if (Now_Data.me.Now_Unit_LV[3].Equals(0))
		{
			StartCoroutine(Snap());
		}
	}

	public IEnumerator Snap()
	{
		yield return new WaitForEndOfFrame();
		MinimapSnap();
	}

	public void MinimapSnap()
	{
		for (int i = 0; i < unit_upgrade_BTNs.Length; i++)
		{
			if (unit_upgrade_BTNs[i].gameObject.activeSelf)
			{
				Last_Unit = unit_upgrade_BTNs[i].gameObject;
			}
		}
		offset_B.x = 871f;
		offset_B.y = -111f;
		offset_B.z = 0f;
		SpringPanel.Begin(Menu_Panel.cachedGameObject, offset_B, 6f);
	}

	public void Set_BTNs(bool OPEN)
	{
		for (int i = 0; i < unit_upgrade_BTNs.Length; i++)
		{
			unit_upgrade_BTNs[i].Unit_ID = i;
			unit_upgrade_BTNs[i].Setting(OPEN);
			if (i > 0)
			{
				if (Now_Data.me.Now_Unit_LV[i - 1] > 0)
				{
					unit_upgrade_BTNs[i].gameObject.SetActive(true);
				}
				else
				{
					unit_upgrade_BTNs[i].gameObject.SetActive(false);
				}
			}
		}
	}

	public void Setting_ID(int ID)
	{
		if (!ID.Equals(Unit_ID))
		{
			Flash.SetActive(false);
			Flash.SetActive(true);
		}
		Unit_ID = ID;
		label_NAME.text = Localization.Get(string.Format("UNIT_{0:000}_NAME", ID));
		label_LV.text = string.Format("LV.{0}", Now_Data.me.Now_Unit_LV[ID]);
		Rank_sprite.spriteName = string.Format("Prop_Rank_{0}", Now_Data.me.Now_Unit_Tier[ID]);
		Rank_sprite.color = Color.white;
		Rank_sprite.width = Rank_sprite.GetAtlasSprite().width;
		Rank_sprite.height = Rank_sprite.GetAtlasSprite().height;
		if (Now_Data.me.Now_Unit_Tier[ID] >= 19)
		{
			Rank_sprite_BG.spriteName = string.Format("Prop_RankBar_{0}", 4);
		}
		else if (Now_Data.me.Now_Unit_Tier[ID] >= 13)
		{
			Rank_sprite_BG.spriteName = string.Format("Prop_RankBar_{0}", 3);
		}
		else if (Now_Data.me.Now_Unit_Tier[ID] >= 10)
		{
			Rank_sprite_BG.spriteName = string.Format("Prop_RankBar_{0}", 2);
		}
		else if (Now_Data.me.Now_Unit_Tier[ID] >= 5)
		{
			Rank_sprite_BG.spriteName = string.Format("Prop_RankBar_{0}", 1);
		}
		else
		{
			Rank_sprite_BG.spriteName = string.Format("Prop_RankBar_{0}", 0);
		}
		if (Now_Data.me.Now_Unit_Tier[Unit_ID] > 0)
		{
			int num = 0;
			for (int i = 0; i < Now_Data.me.Now_Unit_Tier[ID]; i++)
			{
				num += Unit_DB.Tier_Buff[i];
			}
			label_Tier_Buff.text = string.Format("{0}+{1}%", Localization.Get("DPS"), num);
			Tier_tip.gameObject.SetActive(true);
		}
		else
		{
			Tier_tip.gameObject.SetActive(false);
		}
		if (Now_Data.me.Now_Unit_LV[Unit_ID] <= Unit_DB.me.LV_Point[8])
		{
			for (int j = 0; j < label_each_LV.Length; j++)
			{
				label_each_LV[j].text = string.Format("LV.{0}", Unit_DB.me.LV_Point[j]);
				if (Unit_DB.me.unit_DB[Unit_ID].BUFF_Value[j] > 0f)
				{
					label_each_word[j].text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Unit_DB.me.unit_DB[Unit_ID].BUFF_ID[j])), Unit_DB.me.unit_DB[Unit_ID].BUFF_Value[j], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Unit_DB.me.unit_DB[Unit_ID].BUFF_ID[j])));
				}
				else
				{
					label_each_word[j].text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Unit_DB.me.unit_DB[Unit_ID].BUFF_ID[j])), Unit_DB.me.unit_DB[Unit_ID].BUFF_Value[j], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Unit_DB.me.unit_DB[Unit_ID].BUFF_ID[j])));
				}
				if (Now_Data.me.Now_Unit_LV[Unit_ID] >= Unit_DB.me.LV_Point[j])
				{
					label_each_LV[j].color = new Color32(byte.MaxValue, 155, 0, byte.MaxValue);
					label_each_word[j].color = Color.white;
				}
				else
				{
					label_each_LV[j].color = Color.gray;
					label_each_word[j].color = Color.gray;
				}
			}
		}
		else
		{
			int num2 = Unit_ID + 27;
			for (int k = 0; k < label_each_LV.Length; k++)
			{
				label_each_LV[k].text = string.Format("LV.{0}", Unit_DB.me.LV_Point[k] + Unit_DB.me.LV_Point[8]);
				if (Unit_DB.me.unit_DB[num2].BUFF_Value[k] > 0f)
				{
					label_each_word[k].text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Unit_DB.me.unit_DB[num2].BUFF_ID[k])), Unit_DB.me.unit_DB[num2].BUFF_Value[k], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Unit_DB.me.unit_DB[num2].BUFF_ID[k])));
				}
				else
				{
					label_each_word[k].text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Unit_DB.me.unit_DB[num2].BUFF_ID[k])), Unit_DB.me.unit_DB[num2].BUFF_Value[k], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Unit_DB.me.unit_DB[num2].BUFF_ID[k])));
				}
				if (Now_Data.me.Now_Unit_LV[Unit_ID] >= Unit_DB.me.LV_Point[k] + Unit_DB.me.LV_Point[8])
				{
					label_each_LV[k].color = new Color32(byte.MaxValue, 155, 0, byte.MaxValue);
					label_each_word[k].color = Color.white;
				}
				else
				{
					label_each_LV[k].color = Color.gray;
					label_each_word[k].color = Color.gray;
				}
			}
		}
		if (Now_Data.me.Now_Unit_Tier[Unit_ID] >= 22)
		{
			TierUp_BTN.SetActive(false);
			return;
		}
		Price_Tierup = Unit_DB.TierPrice[Now_Data.me.Now_Unit_Tier[Unit_ID]] * (Unit_ID + 10) * 100 / 1000;
		label_Tierup_Price.text = Now_Data.INT_to_ABC(Price_Tierup);
		TierUp_BTN.SetActive(true);
	}

	public void TierUp_Upgrade()
	{
		if (Now_Data.me.Now_Unit_Tier[Unit_ID] >= 19)
		{
			if (Now_Data.me.Now_Unit_LV[Unit_ID] >= Unit_DB.me.LV_Point[8])
			{
				Tierup_Check();
			}
			else
			{
				UI_Master.me.Warning(Localization.Get("CONDITION_A"));
			}
		}
		else if (Now_Data.me.Now_Unit_Tier[Unit_ID] >= 13)
		{
			if (Now_Data.me.Now_Unit_LV[Unit_ID] >= Unit_DB.me.LV_Point[7])
			{
				Tierup_Check();
			}
			else
			{
				UI_Master.me.Warning(Localization.Get("CONDITION_B"));
			}
		}
		else
		{
			Tierup_Check();
		}
	}

	public void Tierup_Check()
	{
		if (Now_Data.me.P_STONE_Possible(Price_Tierup))
		{
			SoundManager.me.Congretu();
			Now_Data.me.Now_Unit_Tier[Unit_ID]++;
			Security.SetInt(string.Format("Now_Unit_Tier_{0:000}", Unit_ID), Now_Data.me.Now_Unit_Tier[Unit_ID]);
			Now_Data.me.P_STONE_Change(-Price_Tierup);
			unit_upgrade_BTNs[Unit_ID].Setting(true);
			Now_Data.me.ALL_PROMOTE++;
			Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_PROMOTE);
			Security.SetInt("ALL_PROMOTE", Now_Data.me.ALL_PROMOTE);
			Setting_ID(Unit_ID);
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_HELLSTONE"));
		}
	}

	public void All_Unlock()
	{
		for (int i = 0; i < Now_Data.me.Now_Unit_LV.Length; i++)
		{
			Now_Data.me.Now_Unit_LV[i] = 1;
			Fight_Master.me.All_setting();
			Fight_Master.me.Unit_Setting(i);
			UI_Master.me.Popup_Close_All();
			UI_Master.me.screenChange_by_ratio.Main_size = 7.5f;
			UI_Master.me.screenChange_by_ratio.Setting();
		}
	}

	public void TIER_CHECK()
	{
		int num = 0;
		for (int i = 0; i < Now_Data.me.Now_Unit_Tier[Unit_ID]; i++)
		{
			num += Unit_DB.Tier_Buff[i];
		}
		label_Tier_Buff.text = string.Format("{0}+{1}%", Localization.Get("DPS"), num);
		Tier_tip.gameObject.SetActive(false);
		Tier_tip.gameObject.SetActive(true);
	}
}
