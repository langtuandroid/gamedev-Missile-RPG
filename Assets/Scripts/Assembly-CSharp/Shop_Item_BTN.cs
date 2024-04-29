using System.Collections;
using Keiwando.BigInteger;
using UnityEngine;

public class Shop_Item_BTN : MonoBehaviour
{
	public int Item_ID;

	public bool IAP;

	public UILabel name_Label;

	public UILabel word_Label;

	public UILabel Price_Label;

	public BigInteger Price;

	public BigInteger Get_Value;

	public GameObject Btn_BUY;

	public void Setting()
	{
		if (name_Label != null)
		{
			name_Label.text = Localization.Get(string.Format("SHOP_ITEM_NAME_{0:000}", Item_ID));
		}
		if (word_Label != null)
		{
			word_Label.text = Localization.Get(string.Format("SHOP_ITEM_WORD_{0:000}", Item_ID));
		}
		switch (Item_ID)
		{
		case 0:
			Price = 150;
			Get_Value = Monster_DB.me.Monster_Gold_by_LV(1, Now_Data.me.LV, false) * 25000;
			word_Label.text = string.Format("{0}\n{1} {2}", Localization.Get("SHOP_ITEM_0_WORD_A"), Now_Data.INT_to_ABC(Get_Value), Localization.Get("SHOP_ITEM_0_WORD_B"));
			break;
		case 1:
			Price = 500;
			break;
		case 2:
			Price = 1800;
			break;
		case 3:
			Price = 500;
			break;
		case 4:
			Price = 1800;
			break;
		case 5:
			Price = 200;
			break;
		case 6:
			Price = 150;
			break;
		case 7:
			Price = 550;
			break;
		case 8:
			Price = 1100;
			break;
		case 9:
			Price = 2200;
			break;
		case 10:
			Price = 900;
			break;
		case 11:
			Get_Value = 200;
			break;
		case 12:
			Get_Value = 500;
			break;
		case 13:
			Get_Value = 1100;
			break;
		case 14:
			Get_Value = 3600;
			break;
		case 15:
			Get_Value = 7800;
			break;
		case 16:
			Get_Value = 16400;
			break;
		case 17:
			Get_Value = 500;
			break;
		case 18:
			if (Now_Data.me.Bazuka_Possible[1] > 0)
			{
				Btn_BUY.SetActive(false);
			}
			else
			{
				Btn_BUY.SetActive(true);
			}
			Get_Value = 1500;
			break;
		case 19:
			if (Now_Data.me.Bazuka_Possible[2] > 0)
			{
				Btn_BUY.SetActive(false);
			}
			else
			{
				Btn_BUY.SetActive(true);
			}
			Get_Value = 8000;
			break;
		case 20:
			if (Now_Data.me.Bazuka_Possible[3] > 0)
			{
				Btn_BUY.SetActive(false);
				break;
			}
			Get_Value = 20000;
			Btn_BUY.SetActive(true);
			break;
		case 21:
			Price = 0;
			Get_Value = 100;
			break;
		case 22:
			Price = 0;
			Get_Value = 101;
			break;
		}
		if (Price_Label != null)
		{
			if (!IAP)
			{
				Price_Label.text = string.Format("{0}", Price);
			}
			else
			{
				Price_Label.text = Localization.Get(string.Format("IAP_PRICE_{0}", Item_ID));
			}
		}
	}

	public void Buy_Try()
	{
		SoundManager.me.Click();
		switch (Item_ID)
		{
		case 0:
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
			REAL_BUY();
			break;
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
			UI_Master.me.shop_Panel.box_buy_window.Setting(this);
			break;
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
		case 23:
			REAL_BUY();
			break;
		case 17:
		case 18:
		case 19:
		case 20:
			UI_Master.me.shop_Panel.box_buy_window.Setting(this);
			break;
		case 21:
			if (Application.platform.Equals(RuntimePlatform.IPhonePlayer))
			{
				Application.OpenURL("https://itunes.apple.com/us/app/cartoon999/id1099597929");
			}
			else
			{
				Application.OpenURL("https://play.google.com/store/apps/details?id=com.moontm.cartoon999");
			}
			StartCoroutine(Wait_Seconds());
			break;
		case 22:
			if (Application.platform.Equals(RuntimePlatform.IPhonePlayer))
			{
				Application.OpenURL("https://itunes.apple.com/us/app/id1400164025");
			}
			else
			{
				Application.OpenURL("https://play.google.com/store/apps/details?id=purgeday.project.chicken");
			}
			StartCoroutine(Wait_Seconds());
			break;
		}
	}

	public IEnumerator Wait_Seconds()
	{
		Btn_BUY.SetActive(false);
		yield return new WaitForSeconds(2f);
		REAL_BUY();
	}

	public void REAL_BUY()
	{
		if (!IAP)
		{
			if (Now_Data.me.MEDAL_Possible(Price))
			{
				Now_Data.me.MEDAL_Change(-Price);
				switch (Item_ID)
				{
				case 0:
					Now_Data.me.GoldChange(Get_Value);
					break;
				case 1:
					Now_Data.me.Booster_LOCKON += 5;
					Security.SetInt("Booster_LOCKON", Now_Data.me.Booster_LOCKON);
					UI_Master.me.Good_MSG(Localization.Get("BOOSTERGET_LOOKON"));
					break;
				case 2:
					Now_Data.me.Booster_LOCKON += 20;
					Security.SetInt("Booster_LOCKON", Now_Data.me.Booster_LOCKON);
					UI_Master.me.Good_MSG(Localization.Get("BOOSTERGET_LOOKON"));
					break;
				case 3:
					Now_Data.me.Booster_MINERAL_MINE += 5;
					Security.SetInt("Booster_MINERAL_MINE", Now_Data.me.Booster_MINERAL_MINE);
					UI_Master.me.Good_MSG(Localization.Get("BOOSTERGET_MINERAL"));
					break;
				case 4:
					Now_Data.me.Booster_MINERAL_MINE += 20;
					Security.SetInt("Booster_MINERAL_MINE", Now_Data.me.Booster_MINERAL_MINE);
					UI_Master.me.Good_MSG(Localization.Get("BOOSTERGET_MINERAL"));
					break;
				case 5:
					UI_Master.me.Good_MSG(Localization.Get("GET_DUNGEONKEY"));
					break;
				case 6:
					UI_Master.me.Popup_Close_All();
					Now_Data.me.BOX_Count[1]++;
					UI_Master.me.box_Open_Panel.Setting(1, true);
					break;
				case 7:
					UI_Master.me.Close_Popup();
					Now_Data.me.BOX_Count[2]++;
					UI_Master.me.box_Open_Panel.Setting(2, true);
					break;
				case 8:
					UI_Master.me.Close_Popup();
					Now_Data.me.BOX_Count[3]++;
					UI_Master.me.box_Open_Panel.Setting(3, true);
					break;
				case 9:
					UI_Master.me.Close_Popup();
					Now_Data.me.BOX_Count[4]++;
					UI_Master.me.box_Open_Panel.Setting(4, true);
					break;
				case 10:
					UI_Master.me.Close_Popup();
					Now_Data.me.BOX_Count[5]++;
					UI_Master.me.box_Open_Panel.Setting(5, true);
					break;
				case 21:
					if (Security.GetInt("DOWNLOAD_AA", 0).Equals(0))
					{
						Security.SetInt("DOWNLOAD_AA", 1);
						Now_Data.me.MEDAL_Change(Get_Value);
						Btn_BUY.SetActive(false);
						UI_Master.me.Good_MSG(Localization.Get("THANKYOU"));
					}
					else
					{
						UI_Master.me.Warning(Localization.Get("ALREADY_USE"));
					}
					break;
				case 22:
					if (Security.GetInt("DOWNLOAD_B", 0).Equals(0))
					{
						Security.SetInt("DOWNLOAD_B", 1);
						Now_Data.me.MEDAL_Change(Get_Value);
						Btn_BUY.SetActive(false);
						UI_Master.me.Good_MSG(Localization.Get("THANKYOU"));
					}
					else
					{
						UI_Master.me.Warning(Localization.Get("ALREADY_USE"));
					}
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
					break;
				}
			}
			else
			{
				UI_Master.me.Open_Uranium_Popup();
			}
		}
		else
		{
			SoundManager.me.Congretu();
			//Parchase.me.BUY_ITEM(Item_ID);
		}
	}

	public void REAL_GET()
	{
		UI_Master.me.Good_MSG(Localization.Get("IAP_THANKYOU"));
		Debug.Log(Item_ID);
		switch (Item_ID)
		{
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
			Now_Data.me.MEDAL_Change(Get_Value);
			break;
		case 17:
			Now_Data.me.MEDAL_Change(Get_Value);
			Now_Data.me.Booster_LOCKON += 50;
			Security.SetInt("Booster_LOCKON", Now_Data.me.Booster_LOCKON);
			Now_Data.me.Booster_MINERAL_MINE += 50;
			Security.SetInt("Booster_MINERAL_MINE", Now_Data.me.Booster_MINERAL_MINE);
			break;
		case 18:
			Now_Data.me.MEDAL_Change(Get_Value);
			Now_Data.me.BOX_Count[2] += 3;
			Security.SetInt(string.Format("BOX_Count_{0:000}", 2), Now_Data.me.BOX_Count[2]);
			UI_Master.me.Box_Checking();
			Now_Data.me.Bazuka_Possible[1]++;
			Security.SetInt("Bazuka_Possible_001", Now_Data.me.Bazuka_Possible[1]);
			Now_Data.me.Bazuka_ID = 1;
			Security.SetInt("Bazuka_ID", Now_Data.me.Bazuka_ID);
			UI_Master.me.skill_Use_BTNs[6].Skill_Cooltime_now = 0f;
			Security.SetFloat(string.Format("Skill_Cooltime_now_{0:000}", 6), 0f);
			Fight_Master.me.All_setting();
			Btn_BUY.SetActive(false);
			if (!UI_Master.me.Dungeon_Clear_Popup.gameObject.activeSelf)
			{
				UI_Master.me.Popup_Close_All();
			}
			break;
		case 19:
			Now_Data.me.MEDAL_Change(Get_Value);
			Now_Data.me.BOX_Count[3] += 3;
			Security.SetInt(string.Format("BOX_Count_{0:000}", 3), Now_Data.me.BOX_Count[3]);
			UI_Master.me.Box_Checking();
			Now_Data.me.Bazuka_Possible[2]++;
			Security.SetInt("Bazuka_Possible_002", Now_Data.me.Bazuka_Possible[2]);
			Now_Data.me.Bazuka_ID = 2;
			Security.SetInt("Bazuka_ID", Now_Data.me.Bazuka_ID);
			Fight_Master.me.All_setting();
			Now_Data.me.sub_quest_count = 2;
			UI_Master.me.sub_quest_ui[1].Quest_ID_CHOICE();
			Btn_BUY.SetActive(false);
			if (!UI_Master.me.Dungeon_Clear_Popup.gameObject.activeSelf)
			{
				UI_Master.me.Popup_Close_All();
			}
			break;
		case 20:
			Now_Data.me.MEDAL_Change(Get_Value);
			Now_Data.me.BOX_Count[4] += 3;
			Security.SetInt(string.Format("BOX_Count_{0:000}", 4), Now_Data.me.BOX_Count[4]);
			UI_Master.me.Box_Checking();
			Now_Data.me.Bazuka_Possible[3]++;
			Security.SetInt("Bazuka_Possible_003", Now_Data.me.Bazuka_Possible[3]);
			Now_Data.me.Bazuka_ID = 3;
			Security.SetInt("Bazuka_ID", Now_Data.me.Bazuka_ID);
			Fight_Master.me.All_setting();
			Btn_BUY.SetActive(false);
			if (!UI_Master.me.Dungeon_Clear_Popup.gameObject.activeSelf)
			{
				UI_Master.me.Popup_Close_All();
			}
			break;
		case 21:
			Now_Data.me.MEDAL_Change(Get_Value);
			Now_Data.me.BOX_Count[4] += 3;
			Security.SetInt(string.Format("BOX_Count_{0:000}", 4), Now_Data.me.BOX_Count[4]);
			UI_Master.me.Box_Checking();
			Now_Data.me.Bazuka_Possible[3]++;
			Security.SetInt("Bazuka_Possible_003", Now_Data.me.Bazuka_Possible[3]);
			Now_Data.me.Bazuka_ID = 3;
			Security.SetInt("Bazuka_ID", Now_Data.me.Bazuka_ID);
			Fight_Master.me.All_setting();
			Btn_BUY.SetActive(false);
			if (!UI_Master.me.Dungeon_Clear_Popup.gameObject.activeSelf)
			{
				UI_Master.me.Popup_Close_All();
			}
			break;
		case 23:
			Now_Data.me.MEDAL_Change(100);
			Now_Data.me.BOX_Count[4] += 3;
			Security.SetInt(string.Format("BOX_Count_{0:000}", 4), Now_Data.me.BOX_Count[4]);
			UI_Master.me.Box_Checking();
			Security.SetInt("IAP_GameSpeed", 1);
			Now_Data.me.IAP_GameSpeed = 1f;
			UI_Master.me.Popup_Close_All();
			UI_Master.me.Time_Scale_CHANGE_BY_IAPP();
			break;
		case 24:
			Now_Data.me.MEDAL_Change(500);
			Now_Data.me.BOX_Count[4] += 3;
			Security.SetInt(string.Format("BOX_Count_{0:000}", 4), Now_Data.me.BOX_Count[4]);
			UI_Master.me.Box_Checking();
			Security.SetInt("IAP_GameSpeed", 2);
			Now_Data.me.IAP_GameSpeed = 2f;
			UI_Master.me.Popup_Close_All();
			UI_Master.me.Time_Scale_CHANGE_BY_IAPP();
			break;
		case 22:
			break;
		}
	}
}
