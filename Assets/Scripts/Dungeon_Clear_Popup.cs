using System.Collections;
using Keiwando.BigInteger;
using UnityEngine;

public class Dungeon_Clear_Popup : MonoBehaviour
{
	public GameObject Clear_info;

	public GameObject Fail_Info;

	public UI2DSprite Reward_Building;

	public UILabel Reward_Building_label;

	public UISprite Reward_PStone_sprite;

	public UI2DSprite Reward_Diablo_sprite;

	public UILabel Reward_P_Stone_label;

	public UI2DSprite Reward_BOX;

	public UI2DSprite Reward_BOX_Portal;

	public UILabel Reward_BOX_label;

	public UILabel FAIL_Reward_P_Stone_label;

	public UILabel Choice_reward_A;

	public UI2DSprite Choice_reward_Building;

	public UILabel Choice_reward_word_A;

	public UILabel Choice_reward_B;

	public UILabel Choice_reward_B_value;

	public UILabel Choice_reward_word_B;

	public UILabel Choice_reward_C;

	public UI2DSprite Choice_reward_Box;

	public UILabel Choice_reward_word_C;

	public bool Win;

	public int Double_Value;

	public int Building_Value;

	public int Target_Builing_ID;

	private int Target_Misile;

	private int Target_BOX;

	private int Target_Color;

	private int Target_Parts;

	private int Target_Diablo_ID;

	public GameObject Again_BTN;

	public UILabel Get_stone_Label;

	public UILabel Get_stone_box_Label;

	public GameObject BG;

	public GameObject REAL_AGAIN_BTN;

	public GameObject ONLY_CLOSE_BTN;

	public UILabel Timer_Label;

	public bool Value_Upgrade;

	public bool Diablo_KILL;

	public int Hellstone_Value;

	public int ALL_GET_PRICE;

	public UILabel ALL_GET_PRICE_LABEL;

	private BigInteger Imsi_Int;

	public bool GET_ING;

	public int REWARD_ID;

	public void OnEnable()
	{
		BG.SetActive(true);
		Clear_info.SetActive(false);
		Fail_Info.SetActive(false);
		Again_BTN.SetActive(false);
		Timer_Label.gameObject.SetActive(false);
	}

	public void Setting(bool Clear_or_NOT)
	{
		if (!Clear_or_NOT)
		{
			Fight_Master.me.All_KILL();
		}
		else
		{
			Now_Data.me.ALL_DUNGEON_CLEAR += (BigInteger)1;
			Security.SetString("ALL_DUNGEON_CLEAR", Now_Data.me.ALL_DUNGEON_CLEAR.ToString());
			Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_DUNGEON_CLEAR);
		}
		Double_Value = 1;
		UI_Master.me.Popup(base.gameObject);
		Fight_Master.me.GS = GameState.End;
		StartCoroutine(Show_Popup(Clear_or_NOT));
	}

	public void AGAIN_Setting()
	{
		SoundManager.me.Fail();
		Win = false;
		Fight_Master.me.GS = GameState.End;
		Get_stone_Label.text = string.Format("{0}", Fight_Master.me.Dungeon_E_Hell_stone_Count);
		Get_stone_box_Label.text = string.Format("{0}", Fight_Master.me.Dungeon_E_Hell_stone_BOX_Count);
		Fight_Master.me.Game_Speed = 0f;
		UI_Master.me.Popup(base.gameObject);
		StartCoroutine(Show_H());
	}

	public IEnumerator Show_H()
	{
		yield return new WaitForSeconds(0.8f);
		if (Fight_Master.me.Live_Enemies.Count.Equals(0))
		{
			REAL_AGAIN_BTN.SetActive(false);
			ONLY_CLOSE_BTN.SetActive(true);
		}
		else
		{
			REAL_AGAIN_BTN.SetActive(true);
			ONLY_CLOSE_BTN.SetActive(false);
		}
		Again_BTN.SetActive(true);
	}

	public void START_SETTING()
	{
		Win = true;
		Fight_Master.me.GS = GameState.End;
		Fight_Master.me.Game_Speed = 0f;
		base.gameObject.SetActive(true);
		StartCoroutine(Start_Again());
	}

	public IEnumerator Start_Again()
	{
		Timer_Label.text = "3";
		Timer_Label.transform.gameObject.SetActive(false);
		Timer_Label.transform.gameObject.SetActive(true);
		SoundManager.me.Unit_Upgrade();
		yield return new WaitForSeconds(1f);
		Timer_Label.text = "2";
		Timer_Label.transform.gameObject.SetActive(false);
		Timer_Label.transform.gameObject.SetActive(true);
		SoundManager.me.Unit_Upgrade();
		yield return new WaitForSeconds(1f);
		Timer_Label.text = "1";
		Timer_Label.transform.gameObject.SetActive(false);
		Timer_Label.transform.gameObject.SetActive(true);
		SoundManager.me.Unit_Upgrade();
		yield return new WaitForSeconds(1f);
		Timer_Label.text = "GO!";
		SoundManager.me.Fairy_Touch();
		Timer_Label.transform.gameObject.SetActive(false);
		Timer_Label.transform.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.2f);
		Timer_Label.transform.gameObject.SetActive(false);
		UI_Master.me.Popup_Close_All();
		Fight_Master.me.Dungeon_E_Time = 15f;
		Fight_Master.me.GS = GameState.Play;
		Fight_Master.me.Game_Speed = 1f;
		base.gameObject.SetActive(false);
	}

	public IEnumerator Show_Popup(bool Clear_or_NOT)
	{
		yield return new WaitForSeconds(1f);
		if (Clear_or_NOT)
		{
			Win = true;
			REWARD_ID = 0;
			SoundManager.me.Congretu();
			Clear_info.SetActive(true);
			Fail_Info.SetActive(false);
			Now_Data.me.NOW_DUNGEON_LV = Now_Data.me.NOW_Selected_Dungeon + 1;
			Debug.Log(Now_Data.me.NOW_DUNGEON_CHAPTER + " - " + Now_Data.me.NOW_DUNGEON_LV + "을 클리어했습니다!");
			if (Now_Data.me.NOW_DUNGEON_LV >= Dungeon_DB.me.dungeon_DB.Length)
			{
				Now_Data.me.NOW_DUNGEON_CHAPTER++;
				Now_Data.me.NOW_DUNGEON_LV = 0;
				UI_Master.me.Good_MSG(Localization.Get("DIABLO_CLEAR"));
				Diablo_KILL = true;
				ALL_GET_PRICE = 999;
			}
			else
			{
				Diablo_KILL = false;
				ALL_GET_PRICE = 300;
			}
			ALL_GET_PRICE_LABEL.text = ALL_GET_PRICE.ToString();
			Security.SetInt("NOW_DUNGEON_CHAPTER", Now_Data.me.NOW_DUNGEON_CHAPTER);
			Security.SetInt("NOW_DUNGEON_LV", Now_Data.me.NOW_DUNGEON_LV);
			if (Random.Range(0, 100) < 15)
			{
				Value_Upgrade = true;
			}
			else
			{
				Value_Upgrade = false;
			}
			if (!Diablo_KILL)
			{
				Target_Builing_ID = Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].Building_TYPE;
				if (Target_Builing_ID.Equals(8))
				{
					Target_Builing_ID = Random.Range(1, 8);
				}
				Choice_reward_Building.sprite2D = Sprite_DB.me.Map_Icon[Target_Builing_ID];
				Choice_reward_A.text = Localization.Get(string.Format("BUILDING_NAME_{0:000}", Target_Builing_ID));
				Choice_reward_word_A.text = Localization.Get(string.Format("BUILDING_UP_WORD"));
			}
			else
			{
				Target_Builing_ID = 0;
				Choice_reward_Building.sprite2D = Sprite_DB.me.Map_Icon[Target_Builing_ID];
				Choice_reward_A.text = Localization.Get(string.Format("BUILDING_NAME_{0:000}", Target_Builing_ID));
				Choice_reward_word_A.text = Localization.Get(string.Format("BUILDING_ALLUP_WORD"));
			}
			Choice_reward_B.text = Localization.Get("HELLSTONE");
			Choice_reward_word_B.text = Localization.Get(string.Format("HELLSTONE_WORD"));
			if (!Diablo_KILL)
			{
				if (!Value_Upgrade)
				{
					Hellstone_Value = 500;
				}
				else
				{
					Hellstone_Value = 750;
				}
			}
			else
			{
				Hellstone_Value = 2000;
			}
			Choice_reward_B_value.text = string.Format("{0}", Hellstone_Value);
			if (!Diablo_KILL)
			{
				if (!Value_Upgrade)
				{
					Target_BOX = 1;
					Choice_reward_C.text = Localization.Get("SHOP_ITEM_NAME_006");
					Choice_reward_word_C.text = Localization.Get("SHOP_ITEM_WORD_006");
				}
				else
				{
					Target_BOX = 2;
					Choice_reward_C.text = Localization.Get("SHOP_ITEM_NAME_007");
					Choice_reward_word_C.text = Localization.Get("SHOP_ITEM_WORD_007");
				}
			}
			else
			{
				Target_BOX = 3;
				Choice_reward_C.text = Localization.Get("SHOP_ITEM_NAME_008");
				Choice_reward_word_C.text = Localization.Get("SHOP_ITEM_WORD_008");
			}
			Choice_reward_Box.sprite2D = Sprite_DB.me.BOX_Icon[Target_BOX];
			GET_ING = false;
		}
		else
		{
			SoundManager.me.Fail();
			Win = false;
			Clear_info.SetActive(false);
			Imsi_Int = new BigInteger(Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].REWARD_A_Value) / 4;
			if (Imsi_Int < 5)
			{
				Imsi_Int = 5;
			}
			Fail_Info.SetActive(true);
		}
		Debug.Log(Clear_or_NOT);
	}

	public void Reward_Get()
	{
		Double_Value = 1;
		UI_Master.me.Popup_Close_All();
	}

	public void Reward_Double()
	{
		Double_Value = 2;
		UI_Master.me.Popup_Close_All();
	}

	public void Select_A()
	{
		bool flag = false;
		if (GET_ING)
		{
			return;
		}
		if (!Target_Builing_ID.Equals(0))
		{
			flag = ((Now_Data.me.Building_LV[Target_Builing_ID] < 20) ? true : false);
		}
		else
		{
			flag = true;
			for (int i = 0; i < Now_Data.me.Building_LV.Length; i++)
			{
				if (Now_Data.me.Building_LV[Target_Builing_ID] > 19)
				{
					flag = false;
				}
			}
		}
		if (flag)
		{
			REWARD_ID = 1;
			GET_ING = true;
			UI_Master.me.Popup_Close_All();
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NO_BUILDING"));
		}
	}

	public void Select_B()
	{
		if (!GET_ING)
		{
			REWARD_ID = 2;
			GET_ING = true;
			UI_Master.me.Popup_Close_All();
		}
	}

	public void Select_C()
	{
		if (!GET_ING)
		{
			REWARD_ID = 3;
			GET_ING = true;
			UI_Master.me.Popup_Close_All();
		}
	}

	public void Select_ALL()
	{
		if (Now_Data.me.MEDAL_Possible(ALL_GET_PRICE))
		{
			Now_Data.me.MEDAL_Change(-ALL_GET_PRICE);
			if (!GET_ING)
			{
				REWARD_ID = 4;
				GET_ING = true;
				UI_Master.me.Popup_Close_All();
			}
		}
		else
		{
			UI_Master.me.Open_Uranium_Popup();
		}
	}

	public void Fail_Reward()
	{
		UI_Master.me.Popup_Close_All();
	}

	public void OnDisable()
	{
		Fight_Master.me.PLAY_TO_NORMAL_STAGE_Setting();
		if (Win)
		{
			switch (REWARD_ID)
			{
			case 1:
				GET_REWARD_A();
				break;
			case 2:
				GET_REWARD_B();
				break;
			case 3:
				GET_REWARD_C();
				break;
			case 4:
				GET_REWARD_A();
				GET_REWARD_B();
				GET_REWARD_C();
				break;
			}
			if (Now_Data.me.NOW_DUNGEON_CHAPTER.Equals(1) && Now_Data.me.NOW_DUNGEON_LV.Equals(2))
			{
				Fight_Master.me.REVIEW_CHECK();
			}
		}
		UI_Master.me.Box_Checking();
	}

	public void GET_REWARD_A()
	{
		if (Target_Builing_ID.Equals(0))
		{
			for (int i = 1; i < Now_Data.me.Building_LV.Length; i++)
			{
				Now_Data.me.Building_LV[i]++;
				if (Now_Data.me.Building_LV[i] > 21)
				{
					Now_Data.me.Building_LV[i] = 21;
				}
				Security.SetInt(string.Format("Building_LV_{0:000}", i), Now_Data.me.Building_LV[i]);
			}
		}
		else
		{
			Now_Data.me.Building_LV[Target_Builing_ID]++;
			Security.SetInt(string.Format("Building_LV_{0:000}", Target_Builing_ID), Now_Data.me.Building_LV[Target_Builing_ID]);
		}
	}

	public void GET_REWARD_B()
	{
		Now_Data.me.P_STONE_Change(new BigInteger(Hellstone_Value));
	}

	public void GET_REWARD_C()
	{
		Now_Data.me.BOX_Count[Target_BOX]++;
		Security.SetInt(string.Format("BOX_Count_{0:000}", Target_BOX), Now_Data.me.BOX_Count[Target_BOX]);
	}
}
