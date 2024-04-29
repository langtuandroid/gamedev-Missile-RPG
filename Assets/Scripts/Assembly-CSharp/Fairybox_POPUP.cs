using Keiwando.BigInteger;
using UnityEngine;

public class Fairybox_POPUP : MonoBehaviour
{
	public int Fairy_ID;

	public bool FairyMAN;

	public GameObject FairyBOX_BOY;

	public GameObject FairyBOX_MAN;

	public UILabel Reward_label;

	public UILabel Normal_BTN_label;

	public UILabel ADs_BTN_label;

	public UILabel ADs_BTN_2_label;

	public UILabel Medal_BTN_label;

	public int Medal_Price;

	public UILabel Medal_price_label;

	public int Stone_Price;

	public UILabel Stone_price_label;

	public BoxFairy target;

	public GameObject ADs_BTN;

	public GameObject Medal_BTN;

	public GameObject ADs_BTN_2;

	public GameObject Normal_BTN;

	public GameObject QUIT_BTN;

	public int GET_ID;

	public BigInteger GET_value;

	public int GET_value_INT;

	private BigInteger Get_gold = new BigInteger(0L);

	public void Setting(int ID)
	{
		Fairy_ID = ID;
		if (Fairy_ID < 4)
		{
			FairyMAN = false;
			FairyBOX_BOY.SetActive(true);
			FairyBOX_MAN.SetActive(false);
			ADs_BTN.SetActive(true);
			Medal_BTN.SetActive(false);
			QUIT_BTN.SetActive(false);
			Normal_BTN.SetActive(true);
			ADs_BTN_2.SetActive(false);
			Normal_BTN_label.text = Localization.Get(string.Format("BOX_Fairy_ANSWER_WORD_Normal_{0:000}", ID));
			ADs_BTN_label.text = Localization.Get(string.Format("BOX_Fairy_ANSWER_WORD_Double_{0:000}", ID));
			if (Now_Data.me.Fairy_Wood_Box_Per > 0f && (float)Random.Range(0, 100) < Now_Data.me.Fairy_Wood_Box_Per)
			{
				Now_Data.me.BOX_Count[0]++;
				UI_Master.me.box_Open_Panel.Setting(0, true);
			}
		}
		else
		{
			FairyMAN = true;
			FairyBOX_BOY.SetActive(false);
			FairyBOX_MAN.SetActive(true);
			ADs_BTN.SetActive(false);
			Medal_BTN.SetActive(true);
			QUIT_BTN.SetActive(true);
			Normal_BTN.SetActive(false);
			ADs_BTN_2.SetActive(true);
			ADs_BTN_2_label.text = Localization.Get(string.Format("BOX_Fairy_ANSWER_WORD_Normal_{0:000}", ID));
			Medal_BTN_label.text = Localization.Get(string.Format("BOX_Fairy_ANSWER_WORD_Double_{0:000}", ID));
			if (Now_Data.me.Fairy_Suply_Box_Per > 0f && (float)Random.Range(0, 100) < Now_Data.me.Fairy_Suply_Box_Per)
			{
				Now_Data.me.BOX_Count[1]++;
				UI_Master.me.box_Open_Panel.Setting(1, true);
			}
		}
		switch (Fairy_ID)
		{
		case 0:
			GET_value = Monster_DB.me.Monster_Gold_by_LV(1, Now_Data.me.LV, false) * 30;
			Reward_label.text = string.Format("{0} {1} {2}", Localization.Get(string.Format("BOX_Fairy_WORD_A_{0:000}", ID)), Now_Data.INT_to_ABC(GET_value), Localization.Get(string.Format("BOX_Fairy_WORD_B_{0:000}", ID)));
			ADs_BTN_label.text = string.Format("{0}\n{1}", Now_Data.INT_to_ABC(GET_value * 20), Localization.Get("MINERAL"));
			UI_Master.me.Popup(base.gameObject);
			break;
		case 1:
			UI_Master.me.Good_MSG(Localization.Get(string.Format("BOX_Fairy_WORD_A_{0:000}", Fairy_ID)));
			GET_value_INT = Random.Range(2, 6);
			UI_Master.me.Skill_Use_EF_Setting(GET_value_INT);
			switch (GET_value_INT)
			{
			case 2:
				if (!Main_Player.me.Cloudshot_ING)
				{
					Main_Player.me.Skill_C();
					Main_Player.me.Now_Cloudshot_Time *= 0.33f;
				}
				break;
			case 3:
				if (!Main_Player.me.Double_Shot_ING)
				{
					Main_Player.me.Skill_D();
					Main_Player.me.Now_Double_Shot_Time *= 0.33f;
				}
				break;
			case 4:
				if (!Main_Player.me.Add_SteamPACK_ING)
				{
					Main_Player.me.Skill_E();
					Main_Player.me.Now_SteamPACK_Time *= 0.33f;
					Main_Player.me.Now_Add_SteamPACK_Time *= 0.33f;
				}
				break;
			case 5:
				if (!Main_Player.me.FAST_MARINE_ING)
				{
					Main_Player.me.Skill_F();
					Main_Player.me.Now_FAST_MARINE_Time *= 0.33f;
				}
				break;
			}
			END();
			break;
		case 2:
			if (Random.Range(0, 10) < 6)
			{
				GET_value_INT = Random.Range(1, 3);
				Reward_label.text = string.Format("{0} {1} {2}", Localization.Get(string.Format("BOX_Fairy_WORD_A_{0:000}", ID)), GET_value_INT, Localization.Get(string.Format("BOX_Fairy_WORD_B_{0:000}", ID)));
				UI_Master.me.Popup(base.gameObject);
			}
			else
			{
				GET_value_INT = Random.Range(1, 3);
				Get_Normal();
				END();
			}
			break;
		case 3:
			Reward_label.text = Localization.Get(string.Format("BOX_Fairy_WORD_A_{0:000}", ID));
			UI_Master.me.Popup(base.gameObject);
			break;
		case 4:
			GET_value = Monster_DB.me.Monster_Gold_by_LV(1, Now_Data.me.LV, false) * 750;
			Reward_label.text = string.Format("{0} {1} {2}", Localization.Get(string.Format("BOX_Fairy_WORD_A_{0:000}", ID)), Now_Data.INT_to_ABC(GET_value), Localization.Get(string.Format("BOX_Fairy_WORD_B_{0:000}", ID)));
			ADs_BTN_2_label.text = string.Format("{0} {1}", Now_Data.INT_to_ABC(GET_value), Localization.Get("MINERAL"));
			Medal_BTN_label.text = string.Format("{0} {1}", Now_Data.INT_to_ABC(GET_value * 40), Localization.Get("MINERAL"));
			Medal_Price = 100;
			Medal_price_label.text = string.Format("-{0}", Medal_Price);
			Stone_Price = 50;
			Stone_price_label.text = string.Format("-{0}", Stone_Price);
			UI_Master.me.Popup(base.gameObject);
			break;
		case 5:
			GET_value_INT = 1;
			Reward_label.text = Localization.Get(string.Format("BOX_Fairy_WORD_A_{0:000}", ID));
			Stone_Price = 50;
			Stone_price_label.text = string.Format("-{0}", Stone_Price);
			Medal_Price = 100;
			Medal_price_label.text = string.Format("-{0}", Medal_Price);
			UI_Master.me.Popup(base.gameObject);
			break;
		case 6:
			Reward_label.text = Localization.Get(string.Format("BOX_Fairy_WORD_A_{0:000}", ID));
			Medal_Price = 250;
			Medal_price_label.text = string.Format("-{0}", Medal_Price);
			Stone_Price = 50;
			Stone_price_label.text = string.Format("-{0}", Stone_Price);
			UI_Master.me.Popup(base.gameObject);
			break;
		case 7:
			Reward_label.text = Localization.Get(string.Format("BOX_Fairy_WORD_A_{0:000}", ID));
			Medal_Price = 100;
			Medal_price_label.text = string.Format("-{0}", Medal_Price);
			Stone_Price = 50;
			Stone_price_label.text = string.Format("-{0}", Stone_Price);
			UI_Master.me.Popup(base.gameObject);
			break;
		}
		SoundManager.me.Fairy_Touch();
	}

	public void Get_Normal()
	{
		REWARD_GET(false);
	}

	public void Get_by_ADS()
	{
		//if (VIDEO_ADS.me.isPossible(true))
		//{
		//	VIDEO_ADS.me.ShowRewardedVideo(0);
		//}
		//else
		//{
		//	UI_Master.me.Warning(Localization.Get("AD_WARNING"));
		//}
	}

	public void Get_by_ADS_2()
	{
		//if (VIDEO_ADS.me.isPossible(true))
		//{
		//	VIDEO_ADS.me.ShowRewardedVideo(1);
		//}
		//else
		//{
		//	UI_Master.me.Warning(Localization.Get("AD_WARNING"));
		//}
	}

	public void Get_by_Medal()
	{
		if (Now_Data.me.MEDAL_Possible(Medal_Price))
		{
			Now_Data.me.MEDAL_Change(-Medal_Price);
			REWARD_GET(true);
		}
		else
		{
			UI_Master.me.Open_Uranium_Popup();
		}
	}

	public void Get_by_Stone()
	{
		if (Now_Data.me.P_STONE_Possible(Stone_Price))
		{
			Now_Data.me.P_STONE_Change(-Stone_Price);
			REWARD_GET(false);
		}
		else
		{
			UI_Master.me.Open_BOX_category();
		}
	}

	public void REWARD_GET(bool Double)
	{
		UI_Master.me.Popup_Close_All();
		switch (Fairy_ID)
		{
		case 0:
			if (!Double)
			{
				Now_Data.me.GoldChange(GET_value);
			}
			else
			{
				Now_Data.me.GoldChange(GET_value * 20);
			}
			break;
		case 1:
			break;
		case 2:
			if (!Double)
			{
				Now_Data.me.MEDAL_Change(GET_value_INT);
			}
			else
			{
				Now_Data.me.MEDAL_Change(GET_value_INT * 5);
			}
			break;
		case 3:
			if (!Double)
			{
				Main_Player.me.Now_MINERAL_GOLREM_Time = 1;
				Main_Player.me.MINERAL_GOLREM_ING = true;
			}
			else
			{
				Main_Player.me.Now_MINERAL_GOLREM_Time = 2;
				Main_Player.me.MINERAL_GOLREM_ING = true;
			}
			break;
		case 4:
			if (!Double)
			{
				Now_Data.me.GoldChange(GET_value);
			}
			else
			{
				Now_Data.me.GoldChange(GET_value * 40);
			}
			break;
		case 5:
			if (!Double)
			{
				Now_Data.me.Booster_LOCKON++;
				Security.SetInt("Booster_LOCKON", Now_Data.me.Booster_LOCKON);
			}
			else
			{
				Now_Data.me.Booster_LOCKON += 5;
				Security.SetInt("Booster_LOCKON", Now_Data.me.Booster_LOCKON);
			}
			break;
		case 6:
			if (!Double)
			{
				Now_Data.me.BOX_Count[0]++;
				UI_Master.me.box_Open_Panel.Setting(0, true);
			}
			else
			{
				Now_Data.me.BOX_Count[7]++;
				UI_Master.me.box_Open_Panel.Setting(7, true);
			}
			break;
		case 7:
			if (!Double)
			{
				Fight_Master.me.action_man.Go_Stage_Count = 5;
			}
			else
			{
				Fight_Master.me.action_man.Go_Stage_Count = 50;
			}
			Fight_Master.me.action_man.gameObject.SetActive(true);
			break;
		}
	}

	public void OnDisable()
	{
		END();
	}

	public void END()
	{
		Get_gold = new BigInteger(0L);
		Get_gold = Monster_DB.me.Monster_Gold_by_LV(1, Now_Data.me.LV, false) * 30 * (int)(100f + Now_Data.me.GOLD_BONUS_PER_BOX) / 100;
		Now_Data.me.GoldChange(Get_gold);
		for (int i = 0; i < 5; i++)
		{
			Make_GOLDCoin();
		}
		if (FairyMAN && Now_Data.me.FAIRY_URANUM > 0)
		{
			UI_Master.me.Good_MSG(Localization.Get("FAIRY_URANIUM"));
			Now_Data.me.MEDAL_Change(Now_Data.me.FAIRY_URANUM);
		}
		if (Now_Data.me.FAIRY_Boom > 0)
		{
			Main_Player.me.SmallShot_FROM_FARIY(Now_Data.me.FAIRY_Boom);
		}
		target.EXIT();
		Now_Data.me.NOW_FARIY += (BigInteger)1;
		Now_Data.me.ALL_FAIRY += (BigInteger)1;
		Security.SetString("NOW_FARIY", Now_Data.me.NOW_FARIY.ToString());
		Security.SetString("ALL_FAIRY", Now_Data.me.ALL_FAIRY.ToString());
		Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_FAIRY);
		for (int j = 0; j < Now_Data.me.sub_quest_count; j++)
		{
			UI_Master.me.sub_quest_ui[j].Txt_Update(Quest_Goal_Type.NOW_FARIY);
		}
	}

	public void Make_GOLDCoin()
	{
		if (OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GET_Possible)
		{
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Get();
		}
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Item_Number = 0;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].sprite_img.sprite = Sprite_DB.me.Mineral;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.position = target.transform.position;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(true);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.localScale = Vector3.zero;
		OBJ_Pool.Coin_Number++;
		if (OBJ_Pool.Coin_Number >= OBJ_Pool.me.Coin.Length)
		{
			OBJ_Pool.Coin_Number = 0;
		}
	}
}
