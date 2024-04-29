using System;
using System.Collections.Generic;
using Keiwando.BigInteger;
using UnityEngine;

public class Now_Data : MonoBehaviour
{
	[Serializable]
	public class PORTAL_Parts
	{
		public int BEST_LEVEL;

		public int[] PORTAL_Parts_count = new int[10];
	}

	public Main_Player mainPlayer;

	[Space]
	[Space]
	public bool VIP_Version;

	[Space]
	[Space]
	public static Now_Data me;

	public int Last_LV;

	public int BEST_LV;

	public int LAST_DIABLO = 1;

	public int LV;

	public int NOW_STAGE;

	public int NOW_Theme;

	public bool BOSS_Failed;

	public BigInteger GOLD = new BigInteger("0");

	public BigInteger CRYSTAL = new BigInteger("0");

	public BigInteger MEDAL = new BigInteger("0");

	public BigInteger P_STONE = new BigInteger("0");

	public int Event_Candy;

	public int Event_Clear_Count;

	public int PASWORD_Event_Candy;

	public int[] EventShopItem_Used;

	public float Missile_Buff_Time;

	public float Mineral_Buff_Time;

	public float Cooltime_Buff_Time;

	public float Mineral_CART_Buff_Time;

	public int PASWORD_KI_A;

	public int PASWORD_KI_B;

	public int PASWORD_KI_C;

	public int PASWORD_KI_D;

	public int LV_Misile = 1;

	public int Misile_TECH_LV = 1;

	public int[] EQUIP_MISILEs;

	public int EQUIP_POSSILBEs_Count = 3;

	public int[] Misile_TIER;

	public int[] Misile_Parts;

	public int[] Bazuka_Parts;

	public int[] Active_Skill_LV;

	public int[] Now_Unit_LV;

	public int[] Now_Unit_Tier;

	public int[] Now_Artifact_LV;

	public int[] Diablo_Artifact_LV;

	public int[] Diablo_Artifact_Parts_count;

	public int[] Building_LV;

	public float[] Buidling_Working_time_Origin;

	public float[] Buidling_Working_time;

	public float[] Buidling_Working_time_Check;

	public bool[] Buidling_Woriking_Possible;

	public int[] Building_Point;

	public int[] BOX_Count;

	public PORTAL_Parts[] portal_Parts;

	public int Total_PotalSet_LV;

	public int[] Archievement_LV;

	public int Archievemnet_Upgrade_LV;

	public int NOW_Subquest_A_ID;

	public int NOW_Subquest_B_ID;

	public BigInteger Arch_Star = new BigInteger("0");

	public BigInteger ALL_TAP = new BigInteger("0");

	public BigInteger ALL_MINERAL = new BigInteger("0");

	public BigInteger ALL_GAS = new BigInteger("0");

	public BigInteger ALL_P_STONE = new BigInteger("0");

	public BigInteger ALL_PLAYTIME = new BigInteger("0");

	public BigInteger ALL_KILL = new BigInteger("0");

	public BigInteger ALL_BOSSKILL = new BigInteger("0");

	public BigInteger ALL_DUNGEON_CLEAR = new BigInteger("0");

	public BigInteger ALL_SKILL_USE = new BigInteger("0");

	public BigInteger ALL_FAIRY = new BigInteger("0");

	public BigInteger ALL_DROPPORT = new BigInteger("0");

	public int ALL_PROMOTE;

	public int ALL_HIDDEN;

	public int[] HIDDEN = new int[10];

	public BigInteger NOW_TAP = new BigInteger("0");

	public BigInteger NOW_KILL = new BigInteger("0");

	public BigInteger NOW_BOSSKILL = new BigInteger("0");

	public BigInteger NOW_PLAYTIME = new BigInteger("0");

	public BigInteger NOW_SKILL_USE = new BigInteger("0");

	public BigInteger NOW_FARIY = new BigInteger("0");

	public BigInteger NOW_DROPPORT = new BigInteger("0");

	public int Autoshot_EXP;

	public int Autoshot_LV;

	public int MINERALBOOM_EXP;

	public int MINERALBOOM_LV;

	public int Autoshot_COUPON;

	public int MINERALBOOM_COUPON;

	public int sub_quest_count = 1;

	public int NOW_DUNGEON_CHAPTER = 1;

	public int NOW_DUNGEON_LV;

	public int NOW_Selected_Dungeon;

	public int DUNGEON_KEY_MAX = 5;

	public int Bazuka_ID;

	public int[] Bazuka_Possible;

	public int Booster_LOCKON;

	public int Booster_MINERAL_MINE;

	public int CLEAR_QUEST;

	public List<int> IAIP_Items;

	public List<int> Artifact_Possesion;

	public BigInteger MISSILE_DMG = 1;

	public float MISSILE_DMG_PER;

	public float MISSILE_DMG_PER_FINAL;

	public float MISSILE_DMG_PER_BY_UNIT;

	public float MISSILE_CRITICAL_PER;

	public float MISSILE_CRITICAL_DMG_PER = 200f;

	public float MISSILE_SPEED = 1f;

	public int SteamPACK_Need_tap = 256;

	public float SteamPACK_Limit_Time = 4f;

	public BigInteger SteamPACK_BUFF = 50;

	public int SteamPACK_Fin_Boom;

	public int LV_GOLD_BONUS;

	public float GOLD_BONUS_PER_NORMAL = 100f;

	public float GOLD_BONUS_PER_GOLREM = 100f;

	public float GOLD_BONUS_PER_BOSS = 100f;

	public float GOLD_BONUS_PER_ALL = 100f;

	public float GOLD_BONUS_PER_COMEBACK;

	public float GOLD_BONUS_PER_BOX;

	public float GOLD_BONUS_PER_PORTAL;

	public float CRYSTAL_BONUS_PER;

	public float P_STONE_BONUS_PER;

	public float[] Unit_DMG_PER = new float[30];

	public float[] Unit_ATTACK_SPEED_PER = new float[30];

	public float[] Unit_Ciritical = new float[30];

	public float HUMAN_ATTACK_PER;

	public float HUMAN_CRITICAL_PER;

	public float HUMAN_CRITICAL_DMG_PER = 100f;

	public float HUMAN_UPGRADE_DISCOUNT;

	public float Mechanin_ATTACK_PER;

	public float Mechanin_CRITICAL_PER;

	public float Mechanin_CRITICAL_DMG_PER = 100f;

	public float Mechanin_UPGRADE_DISCOUNT;

	public float Air_ATTACK_PER;

	public float Air_CRITICAL_PER;

	public float Air_CRITICAL_DMG_PER = 100f;

	public float Air_UPGRADE_DISCOUNT;

	public float All_UNIT_ATTACK_PER;

	public float All_UNIT_CRITICAL_PER;

	public float All_UNIT_CRITICAL_DMG_PER = 200f;

	public float ATTACK_PER_Artifact_Count;

	public float ATTACK_PER_Star_Count;

	public float ATTACK_PER_Missile_Count;

	public float ATTACK_PER_Enemy_Count;

	public float DMG_PLUS_PER_ALL_TARGET;

	public float DMG_PLUS_PER_NORMAL_TARGET;

	public float DMG_PLUS_PER_BOSS_TARGET;

	public float DMG_PLUS_PER_GOLREM_TARGET;

	public float DMG_PLUS_PER_Portal_TARGET;

	public float DMG_PLUS_PER_DUNGEON_BOSS_TARGET;

	public float Hellsteon_Bonus;

	public float DMG_PLUS_PER_ALL;

	public int[] SKILL_Value_Plus = new int[6];

	public float[] SKILL_Cooltime_Minus = new float[6];

	public float[] SKILL_Effectivetime = new float[6];

	public float ALL_SKILL_Cooltime_Minus;

	public float ALL_SKILL_Effectivetime;

	public float Golrem_Per;

	public float BoxFairy_PER_DOUBLE = 1f;

	public float Gold_Mineral_PER;

	public float Boss_Helath_Minus;

	public float Portal_Helath_Minus;

	public float Price_Discount_Misile;

	public float Price_Free_Misile;

	public float Price_Discount_Unit;

	public float Price_Free_Unit;

	public int Stage_Skip;

	public int FAIRY_URANUM;

	public int FAIRY_Boom;

	public float BUHAL_SAVE_LV;

	public int COUNT_for_Doubleshot;

	public int COUNT_for_SteamPack;

	public int COUNT_Boom_five;

	public int COUNT_BigShot;

	public float Boss_Save_Gold;

	public float Boss_P_stone;

	public float JUST_KILL_20;

	public float PIERCING_ATTACK;

	public int ADD_small_Missile;

	public int ADD_Doubleshot_missile;

	public float Portal_Parts_Drop_Per;

	public float Portal_Add_Effective;

	public float[] Portal_Add_Effective_by_Color = new float[5];

	public float QUEST_REWARD_PER;

	public float AUTOShot_delay;

	public float AUTOShot_Count;

	public float Artifact_Effect;

	public float[] Misile_Tierup_Discount_Per = new float[7];

	public float Dungeon_A_boss_ADD_DMG_PER;

	public float Dungeon_B_boss_ADD_DMG_PER;

	public float Dungeon_D_boss_ADD_DMG_PER;

	public float Bazuka_compo_double_Per;

	public float Building_Point_double_Per;

	public float NO_ADS_Per;

	public float SKILL_after_ACTION_MAN_PER;

	public float SKILL_after_COOL_RESET_PER;

	public float DUNGEON_KEY_DOUBLE_PER;

	public float Booster_DOUBLE_PER;

	public float CoolReset_Discount;

	public float Diablo_Hurt_PER;

	public float Bazuka_Compo_Bonus;

	public int Bazuka_Compo_count;

	public float DMG_PER_ARTIFACT_FINAL;

	public float Booster_ADD_Time;

	public float Changed_GameSpeed = 1f;

	public float IAP_GameSpeed;

	public float Dungeon_E_Play_time;

	public float Fairy_Wood_Box_Per;

	public float Fairy_Suply_Box_Per;

	public float DIABLO_LAPTIME;

	public float[] DIABLO_LAPTIME_LV = new float[4] { 1800f, 3600f, 7200f, 21600f };

	public static string[] word_Type = new string[83]
	{
		string.Empty,
		"K",
		"M",
		"B",
		"T",
		"a",
		"b",
		"c",
		"d",
		"e",
		"f",
		"g",
		"h",
		"i",
		"j",
		"k",
		"l",
		"m",
		"n",
		"o",
		"p",
		"q",
		"r",
		"s",
		"t",
		"u",
		"v",
		"w",
		"x",
		"y",
		"z",
		"aa",
		"ab",
		"ac",
		"ad",
		"ae",
		"af",
		"ag",
		"ah",
		"ai",
		"aj",
		"ak",
		"al",
		"am",
		"an",
		"ao",
		"ap",
		"aq",
		"ar",
		"as",
		"at",
		"au",
		"av",
		"aw",
		"ax",
		"ay",
		"az",
		"ba",
		"bb",
		"bc",
		"bd",
		"be",
		"bf",
		"bg",
		"bh",
		"bi",
		"bj",
		"bk",
		"bl",
		"bm",
		"bn",
		"bo",
		"bp",
		"bq",
		"br",
		"bs",
		"bt",
		"bu",
		"bv",
		"bw",
		"bx",
		"by",
		"bz"
	};

	public int Archive_COUNT;

	public int Artifact_COUNT;

	public int Missile_COUNT;

	private int Option_ID;

	private float Option_Default;

	private float Option_Plus;

	private BigInteger Option_Plus_BIG = 0;

	private float PORTAL_EF_VALUE = 2f;

	private int Arch_Type_ID;

	private BigInteger now_value;

	private BigInteger goal_value;

	private bool Get_Reward_Possible;

	private void Awake()
	{
		me = this;
		Status_Chogihwa();
	}

	public void Start()
	{
		DATALOAD();
	}

	public void Buff_Time_Save(float Value)
	{
		Security.SetFloat("Mineral_Buff_Time", Mineral_Buff_Time);
		Security.SetFloat("Missile_Buff_Time", Missile_Buff_Time);
		Security.SetFloat("Cooltime_Buff_Time", Cooltime_Buff_Time);
		Security.SetFloat("Mineral_CART_Buff_Time", Mineral_CART_Buff_Time);
		if (Value > 0f)
		{
			Mineral_Buff_Time -= Value;
			if (Mineral_Buff_Time < 0f)
			{
				Mineral_Buff_Time = 0f;
			}
			Security.SetFloat("Mineral_Buff_Time", Mineral_Buff_Time);
			Missile_Buff_Time -= Value;
			if (Missile_Buff_Time < 0f)
			{
				Missile_Buff_Time = 0f;
			}
			Security.SetFloat("Missile_Buff_Time", Missile_Buff_Time);
			Cooltime_Buff_Time -= Value;
			if (Cooltime_Buff_Time < 0f)
			{
				Cooltime_Buff_Time = 0f;
			}
			Security.SetFloat("Cooltime_Buff_Time", Cooltime_Buff_Time);
			Mineral_CART_Buff_Time -= Value;
			if (Mineral_CART_Buff_Time < 0f)
			{
				Mineral_CART_Buff_Time = 0f;
			}
			Security.SetFloat("Mineral_CART_Buff_Time", Mineral_CART_Buff_Time);
		}
	}

	public void DATALOAD()
	{
		PASWORD_KI_A = Security.GetInt("PASWORD_KI_A", 9);
		PASWORD_KI_B = Security.GetInt("PASWORD_KI_B", 8);
		PASWORD_KI_C = Security.GetInt("PASWORD_KI_C", 7);
		PASWORD_KI_D = Security.GetInt("PASWORD_KI_D", 6);
		GoldChange(new BigInteger(Security.GetString("GOLD", "9")));
		CRYSTAL_Change(new BigInteger(Security.GetString("CRYSTAL", "8")));
		if (VIP_Version)
		{
			MEDAL_Change(new BigInteger(Security.GetString("MEDAL", "307")));
		}
		else
		{
			MEDAL_Change(new BigInteger(Security.GetString("MEDAL", "7")));
		}
		P_STONE_Change(new BigInteger(Security.GetString("P_STONE", "6")));
		if (GOLD < -100 && Security.GetInt("BUG_RESTORE", 0) < 3)
		{
			Security.SetInt("BUG_RESTORE", 1);
			GOLD *= (BigInteger)(-5);
			GoldChange(0);
			MEDAL_Change(500);
		}
		PASWORD_Event_Candy = Security.GetInt("PASWORD_Event_Candy", 5);
		EVENT_Candy_Change(Security.GetInt("Event_Candy", 5));
		for (int i = 2; i < EventShopItem_Used.Length; i++)
		{
			EventShopItem_Used[i] = Security.GetInt(string.Format("EventShopItem_Used_{0}", i), 0);
		}
		Missile_Buff_Time = Security.GetFloat("Missile_Buff_Time", 0f);
		Mineral_Buff_Time = Security.GetFloat("Mineral_Buff_Time", 0f);
		Cooltime_Buff_Time = Security.GetFloat("Cooltime_Buff_Time", 0f);
		Mineral_CART_Buff_Time = Security.GetFloat("Mineral_CART_Buff_Time", 0f);
		LV = Security.GetInt("LV", 10);
		BEST_LV = Security.GetInt("BEST_LV", 10);
		if (BEST_LV > 210)
		{
			UI_Master.me.Speed_BTN.SetActive(true);
		}
		LAST_DIABLO = Security.GetInt("LAST_DIABLO", 1);
		int num = me.LV / 10;
		if (num < 100)
		{
			NOW_Theme = Monster_DB.me.stage_DB[num].Theme;
		}
		else
		{
			NOW_Theme = UnityEngine.Random.Range(1, 5);
		}
		LV_Misile = Security.GetInt("LV_Misile", 1);
		for (int j = 0; j < me.Misile_TIER.Length; j++)
		{
			me.Misile_TIER[j] = Security.GetInt(string.Format("Misile_TIER_{0:000}", j), 0);
		}
		if (me.Misile_TIER[0].Equals(0))
		{
			me.Misile_TIER[0] = 1;
			Security.SetInt(string.Format("Misile_TIER_{0:000}", 0), 1);
		}
		for (int k = 0; k < me.EQUIP_MISILEs.Length; k++)
		{
			me.EQUIP_MISILEs[k] = Security.GetInt(string.Format("EQUIP_MISILEs_{0:000}", k), 0);
		}
		for (int l = 0; l < me.Misile_Parts.Length; l++)
		{
			me.Misile_Parts[l] = Security.GetInt(string.Format("Misile_Parts_{0:000}", l), 0);
		}
		for (int m = 0; m < me.Bazuka_Parts.Length; m++)
		{
			me.Bazuka_Parts[m] = Security.GetInt(string.Format("Bazuka_Parts_{0:000}", m), 0);
		}
		for (int n = 0; n < me.Active_Skill_LV.Length; n++)
		{
			Active_Skill_LV[n] = Security.GetInt(string.Format("Active_Skill_LV_{0:000}", n), 0);
		}
		for (int num2 = 0; num2 < me.Now_Unit_Tier.Length; num2++)
		{
			Now_Unit_LV[num2] = Security.GetInt(string.Format("Now_Unit_LV_{0:000}", num2), 0);
			Now_Unit_Tier[num2] = Security.GetInt(string.Format("Now_Unit_Tier_{0:000}", num2), 0);
		}
		for (int num3 = 0; num3 < Now_Artifact_LV.Length; num3++)
		{
			Now_Artifact_LV[num3] = Security.GetInt(string.Format("Now_Artifact_LV_{0:000}", num3), 0);
		}
		for (int num4 = 0; num4 < Diablo_Artifact_LV.Length; num4++)
		{
			Diablo_Artifact_LV[num4] = Security.GetInt(string.Format("Diablo_Artifact_LV_{0:000}", num4), 0);
		}
		for (int num5 = 0; num5 < Diablo_Artifact_Parts_count.Length; num5++)
		{
			Diablo_Artifact_Parts_count[num5] = Security.GetInt(string.Format("Diablo_Artifact_Parts_count_{0:000}", num5), 0);
		}
		if (Security.GetInt("DIABLOLV_CHECK", 1).Equals(1))
		{
			for (int num6 = 0; num6 < Diablo_Artifact_Parts_count.Length; num6++)
			{
				if (Diablo_Artifact_Parts_count[num6] > Diablo_Artifact_LV[num6])
				{
					Diablo_Artifact_LV[num6] = Diablo_Artifact_Parts_count[num6];
					Security.SetInt(string.Format("Diablo_Artifact_LV_{0:000}", num6), me.Diablo_Artifact_LV[num6]);
				}
			}
			Security.SetInt("DIABLOLV_CHECK", 2);
		}
		for (int num7 = 0; num7 < Building_LV.Length; num7++)
		{
			Building_LV[num7] = Security.GetInt(string.Format("Building_LV_{0:000}", num7), 0);
		}
		for (int num8 = 0; num8 < Buidling_Working_time_Check.Length; num8++)
		{
			Buidling_Working_time_Check[num8] = Security.GetFloat(string.Format("Buidling_Working_time_Check_{0:000}", num8), 0f);
		}
		for (int num9 = 0; num9 < Building_Point.Length; num9++)
		{
			Building_Point[num9] = Security.GetInt(string.Format("Building_Point_{0:000}", num9), 0);
		}
		for (int num10 = 0; num10 < BOX_Count.Length; num10++)
		{
			BOX_Count[num10] = Security.GetInt(string.Format("BOX_Count_{0:000}", num10), 0);
		}
		for (int num11 = 0; num11 < portal_Parts.Length; num11++)
		{
			for (int num12 = 0; num12 < portal_Parts[num11].PORTAL_Parts_count.Length; num12++)
			{
				portal_Parts[num11].PORTAL_Parts_count[num12] = Security.GetInt(string.Format("Portal_{0:000}_Parts_{1:000}_Count", num11, num12), 0);
			}
		}
		for (int num13 = 0; num13 < Archievement_LV.Length; num13++)
		{
			Archievement_LV[num13] = Security.GetInt(string.Format("Archievement_LV_{0:000}", num13), 0);
		}
		Arch_Star = Security.GetInt(string.Format("Arch_Star"), 0);
		ALL_TAP = new BigInteger(Security.GetString(string.Format("ALL_TAP"), "0"));
		ALL_MINERAL = new BigInteger(Security.GetString(string.Format("ALL_MINERAL"), "0"));
		ALL_GAS = new BigInteger(Security.GetString(string.Format("ALL_GAS"), "0"));
		ALL_P_STONE = new BigInteger(Security.GetString(string.Format("ALL_P_STONE"), "0"));
		ALL_PLAYTIME = new BigInteger(Security.GetString(string.Format("ALL_PLAYTIME"), "0"));
		DIABLO_LAPTIME = Security.GetFloat("DIABLO_LAPTIME", 0f);
		ALL_BOSSKILL = new BigInteger(Security.GetString(string.Format("ALL_BOSSKILL"), "0"));
		ALL_DUNGEON_CLEAR = new BigInteger(Security.GetString(string.Format("ALL_DUNGEON_CLEAR"), "0"));
		ALL_SKILL_USE = new BigInteger(Security.GetString(string.Format("ALL_SKILL_USE"), "0"));
		ALL_FAIRY = new BigInteger(Security.GetString(string.Format("ALL_FAIRY"), "0"));
		ALL_DROPPORT = new BigInteger(Security.GetString(string.Format("ALL_DROPPORT"), "0"));
		if (ALL_DROPPORT > 0)
		{
			UI_Master.me.Speed_BTN.SetActive(true);
		}
		ALL_PROMOTE = Security.GetInt(string.Format("ALL_PROMOTE"), 0);
		ALL_HIDDEN = Security.GetInt(string.Format("ALL_HIDDEN"), 0);
		for (int num14 = 0; num14 < HIDDEN.Length; num14++)
		{
			HIDDEN[num14] = Security.GetInt(string.Format("HIDDEN_{0:000}", num14), 0);
		}
		NOW_TAP = Security.GetInt(string.Format("NOW_TAP"), 0);
		NOW_KILL = Security.GetInt(string.Format("NOW_KILL"), 0);
		NOW_BOSSKILL = Security.GetInt(string.Format("NOW_BOSSKILL"), 0);
		NOW_PLAYTIME = Security.GetInt(string.Format("NOW_PLAYTIME"), 0);
		NOW_SKILL_USE = Security.GetInt(string.Format("NOW_SKILL_USE"), 0);
		NOW_FARIY = Security.GetInt(string.Format("NOW_FARIY"), 0);
		NOW_DROPPORT = Security.GetInt(string.Format("NOW_DROPPORT"), 0);
		Bazuka_ID = Security.GetInt(string.Format("Bazuka_ID"), 0);
		for (int num15 = 1; num15 < Bazuka_Possible.Length; num15++)
		{
			Bazuka_Possible[num15] = Security.GetInt(string.Format("Bazuka_Possible_{0:000}", num15), 0);
		}
		NOW_Subquest_A_ID = Security.GetInt(string.Format("NOW_Subquest_A_ID"), 0);
		NOW_Subquest_B_ID = Security.GetInt(string.Format("NOW_Subquest_B_ID"), 0);
		UI_Master.me.sub_quest_ui[0].Setting(false);
		UI_Master.me.sub_quest_ui[1].Setting(false);
		Autoshot_EXP = Security.GetInt(string.Format("Autoshot_EXP"), 0);
		Autoshot_LV = Security.GetInt(string.Format("Autoshot_LV"), 0);
		MINERALBOOM_EXP = Security.GetInt(string.Format("MINERALBOOM_EXP"), 0);
		MINERALBOOM_LV = Security.GetInt(string.Format("MINERALBOOM_LV"), 0);
		NOW_DUNGEON_CHAPTER = Security.GetInt(string.Format("NOW_DUNGEON_CHAPTER"), 1);
		NOW_DUNGEON_LV = Security.GetInt(string.Format("NOW_DUNGEON_LV"), 0);
		Booster_LOCKON = Security.GetInt(string.Format("Booster_LOCKON"), 0);
		Booster_MINERAL_MINE = Security.GetInt(string.Format("Booster_MINERAL_MINE"), 0);
		CLEAR_QUEST = Security.GetInt(string.Format("CLEAR_QUEST"), 0);
		int @int = Security.GetInt("IAIP_COUNT", 0);
		for (int num16 = 0; num16 < @int; num16++)
		{
			me.IAIP_Items.Add(0);
		}
		if (PlayerPrefs.GetString("Language", "None").Equals("None"))
		{
			switch (Application.systemLanguage)
			{
			default:
				PlayerPrefs.SetString("Language", "English");
				Localization.language = "English";
				break;
			case SystemLanguage.Korean:
				PlayerPrefs.SetString("Language", "Korean");
				Localization.language = "Korean";
				break;
			case SystemLanguage.Japanese:
				PlayerPrefs.SetString("Language", "Japanese");
				Localization.language = "Japanese";
				break;
			case SystemLanguage.Chinese:
			case SystemLanguage.ChineseTraditional:
				PlayerPrefs.SetString("Language", "ChineseTraditional");
				Localization.language = "ChineseTraditional";
				break;
			case SystemLanguage.ChineseSimplified:
				PlayerPrefs.SetString("Language", "ChineseSimplified");
				Localization.language = "ChineseSimplified";
				break;
			case SystemLanguage.Spanish:
				PlayerPrefs.SetString("Language", "Spanish");
				Localization.language = "Spanish";
				break;
			}
		}
		me.IAP_GameSpeed = Security.GetInt("IAP_GameSpeed", 0);
		if (me.IAP_GameSpeed > 0f)
		{
			UI_Master.me.Time_Scale_CHANGE_BY_IAPP();
		}
	}

	public void RESET_DATA()
	{
		GoldChange(new BigInteger(0L));
		CRYSTAL_Change(new BigInteger(0L));
		MEDAL_Change(new BigInteger(0L));
		P_STONE_Change(new BigInteger(0L));
		LV = 10;
		LV_Misile = 1;
		ALL_TAP = new BigInteger("0");
		ALL_MINERAL = new BigInteger("0");
		ALL_GAS = new BigInteger("0");
		ALL_P_STONE = new BigInteger("0");
		ALL_PLAYTIME = new BigInteger("0");
		ALL_KILL = new BigInteger("0");
		ALL_BOSSKILL = new BigInteger("0");
		ALL_DUNGEON_CLEAR = new BigInteger("0");
		ALL_SKILL_USE = new BigInteger("0");
		ALL_FAIRY = new BigInteger("0");
		ALL_DROPPORT = new BigInteger("0");
		NOW_TAP = new BigInteger("0");
		NOW_KILL = new BigInteger("0");
		NOW_BOSSKILL = new BigInteger("0");
		NOW_PLAYTIME = new BigInteger("0");
		NOW_SKILL_USE = new BigInteger("0");
		NOW_FARIY = new BigInteger("0");
		NOW_DROPPORT = new BigInteger("0");
	}

	public static string INT_to_ABC(BigInteger target)
	{
		BigInteger bigInteger = target;
		string text = string.Format("{0}", bigInteger);
		if (target < 1000)
		{
			return text;
		}
		int num = 0;
		while (bigInteger >= 1000)
		{
			bigInteger /= (BigInteger)1000;
			num++;
		}
		string text2 = string.Format("{0}", bigInteger);
		return string.Format("{0}.{1}{2}", text2, text.Substring(text2.Length, 2), word_Type[num]);
	}

	public static float Divide_to_Float(BigInteger A, BigInteger B)
	{
		if (B <= 0)
		{
			B = 1;
		}
		if (A <= 0)
		{
			A = 0;
		}
		float num = 0f;
		string text = string.Format("{0}", A);
		string text2 = string.Format("{0}", B);
		if (A >= B)
		{
			return 1f;
		}
		if (text2.Length - text.Length > 3)
		{
			return 0.001f;
		}
		if (text2.Length < 6)
		{
			num = float.Parse(text) / float.Parse(text2);
		}
		else
		{
			int num2 = text.Length - 3;
			num = float.Parse(text.Substring(0, text.Length - num2)) / float.Parse(text2.Substring(0, text2.Length - num2));
		}
		if (num < 0f)
		{
			num = 0f;
		}
		return num;
	}

	public void GoldChange(BigInteger value)
	{
		me.GOLD -= (BigInteger)PASWORD_KI_A;
		PASWORD_KI_A = UnityEngine.Random.Range(1, 99);
		Security.SetInt("PASWORD_KI_A", PASWORD_KI_A);
		me.GOLD += (BigInteger)PASWORD_KI_A;
		if (Main_Player.me != null && value > 0 && Main_Player.me.Mineral_CART_ING)
		{
			value *= (BigInteger)2;
		}
		GOLD += value;
		if (value > 0)
		{
			UI_Master.me.Get_GOLD_label.text = string.Format("+ {0}", INT_to_ABC(value));
			UI_Master.me.Get_GOLD_label.gameObject.SetActive(false);
			UI_Master.me.Get_GOLD_label.gameObject.SetActive(true);
			me.ALL_MINERAL += value;
			Security.SetString("ALL_MINERAL", me.ALL_MINERAL.ToString());
		}
		UI_Master.me.GOLD_Obj.SetActive(false);
		UI_Master.me.GOLD_Obj.SetActive(true);
		UI_Master.me.GOLD_label.text = INT_to_ABC(GOLD - PASWORD_KI_A);
		Security.SetString("GOLD", string.Format("{0}", GOLD));
		Check_Possible(Quest_Goal_Type.ALL_MINERAL);
		UI_Master.me.Bazuka_Alram.SetActive(false);
		for (int i = 0; i < 6; i++)
		{
			if (me.Active_Skill_LV[i].Equals(0))
			{
				if (Gold_Possible(new BigInteger(Misile_DB.me.skill_DB[i].Skill_LV[0].Price)))
				{
					UI_Master.me.Bazuka_Alram.SetActive(true);
				}
				break;
			}
		}
		UI_Master.me.Marine_Alram.SetActive(false);
		for (int j = 0; j < Now_Unit_LV.Length; j++)
		{
			if (Now_Unit_LV[j].Equals(0))
			{
				if (Gold_Possible(Unit_DB.me.Price_by_LV(j, 0)))
				{
					UI_Master.me.Marine_Alram.SetActive(true);
				}
				break;
			}
		}
	}

	public bool Gold_Possible(BigInteger value)
	{
		if (GOLD - PASWORD_KI_A >= value)
		{
			return true;
		}
		return false;
	}

	public void CRYSTAL_Change(BigInteger value)
	{
		CRYSTAL -= (BigInteger)PASWORD_KI_B;
		PASWORD_KI_B = UnityEngine.Random.Range(1, 99);
		Security.SetInt("PASWORD_KI_B", PASWORD_KI_B);
		CRYSTAL += (BigInteger)PASWORD_KI_B;
		CRYSTAL += value;
		if (value > 0)
		{
			me.ALL_GAS += value;
			Security.SetString("ALL_GAS", me.ALL_GAS.ToString());
			Check_Possible(Quest_Goal_Type.ALL_GAS);
		}
		UI_Master.me.CRYSTAL_label.text = string.Format("{0}", CRYSTAL - PASWORD_KI_B);
		Security.SetString("CRYSTAL", CRYSTAL.ToString());
	}

	public bool CRYSTAL_Possible(BigInteger value)
	{
		if (CRYSTAL - PASWORD_KI_B >= value)
		{
			return true;
		}
		return false;
	}

	public void MEDAL_Change(BigInteger value)
	{
		MEDAL -= (BigInteger)PASWORD_KI_C;
		PASWORD_KI_C = UnityEngine.Random.Range(1, 99);
		Security.SetInt("PASWORD_KI_C", PASWORD_KI_C);
		MEDAL += (BigInteger)PASWORD_KI_C;
		MEDAL += value;
		UI_Master.me.MEDAL_label.text = string.Format("{0}", MEDAL - PASWORD_KI_C);
		Security.SetString("MEDAL", MEDAL.ToString());
	}

	public bool MEDAL_Possible(BigInteger value)
	{
		if (MEDAL - PASWORD_KI_C >= value)
		{
			return true;
		}
		return false;
	}

	public void P_STONE_Change(BigInteger value)
	{
		P_STONE -= (BigInteger)PASWORD_KI_D;
		PASWORD_KI_D = UnityEngine.Random.Range(1, 99);
		Security.SetInt("PASWORD_KI_D", PASWORD_KI_D);
		P_STONE += (BigInteger)PASWORD_KI_D;
		P_STONE += value;
		UI_Master.me.P_STONE_label.text = string.Format("{0}", P_STONE - PASWORD_KI_D);
		Security.SetString("P_STONE", P_STONE.ToString());
		if (value > 0)
		{
			me.ALL_P_STONE += value;
			Security.SetString("ALL_P_STONE", me.ALL_P_STONE.ToString());
			Check_Possible(Quest_Goal_Type.ALL_P_STONE);
		}
		UI_Master.me.misile_Parts_Manager.AnyMisile_PossibleCheck();
	}

	public bool P_STONE_Possible(BigInteger value)
	{
		if (P_STONE - PASWORD_KI_D >= value)
		{
			return true;
		}
		return false;
	}

	public bool EVENT_Candy_Possible(int value)
	{
		if (Event_Candy - PASWORD_Event_Candy >= value)
		{
			return true;
		}
		return false;
	}

	public void EVENT_Candy_Change(int value)
	{
		Event_Candy -= PASWORD_Event_Candy;
		PASWORD_Event_Candy = UnityEngine.Random.Range(1, 99);
		Security.SetInt("PASWORD_Event_Candy", PASWORD_Event_Candy);
		Event_Candy += PASWORD_Event_Candy;
		Event_Candy += value;
		if (Event_Candy - PASWORD_Event_Candy > 999)
		{
			Event_Candy = 999 + PASWORD_Event_Candy;
		}
		else if (value > 0)
		{
			UI_Master.me.Event_Candy_Alram.SetActive(false);
			UI_Master.me.Event_Candy_Alram.SetActive(true);
		}
		Security.SetInt("Event_Candy", Event_Candy);
	}

	public void Save_Stage()
	{
	}

	public void Status_Chogihwa()
	{
		MISSILE_DMG = 0;
		MISSILE_DMG_PER = 0f;
		MISSILE_DMG_PER_FINAL = 0f;
		MISSILE_DMG_PER_BY_UNIT = 0f;
		MISSILE_CRITICAL_PER = 0f;
		MISSILE_CRITICAL_DMG_PER = 200f;
		SteamPACK_Need_tap = 256;
		SteamPACK_Limit_Time = 4f;
		SteamPACK_BUFF = 50;
		SteamPACK_Fin_Boom = 0;
		LV_GOLD_BONUS = 0;
		GOLD_BONUS_PER_NORMAL = 0f;
		GOLD_BONUS_PER_GOLREM = 0f;
		GOLD_BONUS_PER_BOSS = 0f;
		GOLD_BONUS_PER_ALL = 0f;
		GOLD_BONUS_PER_COMEBACK = 0f;
		GOLD_BONUS_PER_BOX = 0f;
		GOLD_BONUS_PER_PORTAL = 0f;
		CRYSTAL_BONUS_PER = 0f;
		P_STONE_BONUS_PER = 0f;
		for (int i = 0; i < Unit_DMG_PER.Length; i++)
		{
			Unit_DMG_PER[i] = 0f;
			Unit_Ciritical[i] = 0f;
			Unit_ATTACK_SPEED_PER[i] = 0f;
		}
		HUMAN_ATTACK_PER = 0f;
		HUMAN_CRITICAL_PER = 0f;
		HUMAN_CRITICAL_DMG_PER = 100f;
		HUMAN_UPGRADE_DISCOUNT = 0f;
		Mechanin_ATTACK_PER = 0f;
		Mechanin_CRITICAL_PER = 0f;
		Mechanin_CRITICAL_DMG_PER = 100f;
		Mechanin_UPGRADE_DISCOUNT = 0f;
		Air_ATTACK_PER = 0f;
		Air_CRITICAL_PER = 0f;
		Air_CRITICAL_DMG_PER = 100f;
		Air_UPGRADE_DISCOUNT = 0f;
		All_UNIT_ATTACK_PER = 0f;
		All_UNIT_CRITICAL_PER = 0f;
		All_UNIT_CRITICAL_DMG_PER = 200f;
		ATTACK_PER_Artifact_Count = 0f;
		ATTACK_PER_Star_Count = 0f;
		ATTACK_PER_Missile_Count = 0f;
		ATTACK_PER_Enemy_Count = 0f;
		DMG_PLUS_PER_ALL_TARGET = 0f;
		DMG_PLUS_PER_NORMAL_TARGET = 0f;
		DMG_PLUS_PER_BOSS_TARGET = 0f;
		DMG_PLUS_PER_GOLREM_TARGET = 0f;
		DMG_PLUS_PER_Portal_TARGET = 0f;
		DMG_PLUS_PER_DUNGEON_BOSS_TARGET = 0f;
		Hellsteon_Bonus = 0f;
		DMG_PLUS_PER_ALL = 0f;
		for (int j = 0; j < SKILL_Value_Plus.Length; j++)
		{
			SKILL_Value_Plus[j] = 0;
			SKILL_Cooltime_Minus[j] = 0f;
			SKILL_Effectivetime[j] = 0f;
		}
		ALL_SKILL_Cooltime_Minus = 0f;
		ALL_SKILL_Effectivetime = 0f;
		Golrem_Per = 0f;
		BoxFairy_PER_DOUBLE = 1f;
		Gold_Mineral_PER = 0f;
		Boss_Helath_Minus = 0f;
		Portal_Helath_Minus = 0f;
		Price_Discount_Misile = 0f;
		Price_Free_Misile = 0f;
		Price_Discount_Unit = 0f;
		Price_Free_Unit = 0f;
		Stage_Skip = 0;
		FAIRY_URANUM = 0;
		FAIRY_Boom = 0;
		BUHAL_SAVE_LV = 0f;
		COUNT_for_Doubleshot = 0;
		COUNT_for_SteamPack = 0;
		COUNT_Boom_five = 0;
		COUNT_BigShot = 0;
		Boss_Save_Gold = 0f;
		Boss_P_stone = 0f;
		JUST_KILL_20 = 0f;
		PIERCING_ATTACK = 0f;
		ADD_small_Missile = 0;
		ADD_Doubleshot_missile = 0;
		Portal_Parts_Drop_Per = 0f;
		Portal_Add_Effective = 0f;
		for (int k = 0; k < Portal_Add_Effective_by_Color.Length; k++)
		{
			Portal_Add_Effective_by_Color[k] = 0f;
		}
		QUEST_REWARD_PER = 0f;
		AUTOShot_delay = 0f;
		AUTOShot_Count = 0f;
		Artifact_Effect = 0f;
		for (int l = 0; l < Misile_Tierup_Discount_Per.Length; l++)
		{
			Misile_Tierup_Discount_Per[l] = 0f;
		}
		Dungeon_A_boss_ADD_DMG_PER = 0f;
		Dungeon_B_boss_ADD_DMG_PER = 0f;
		Dungeon_D_boss_ADD_DMG_PER = 0f;
		Bazuka_compo_double_Per = 0f;
		Building_Point_double_Per = 0f;
		NO_ADS_Per = 0f;
		SKILL_after_ACTION_MAN_PER = 0f;
		SKILL_after_COOL_RESET_PER = 0f;
		DUNGEON_KEY_DOUBLE_PER = 0f;
		Booster_DOUBLE_PER = 0f;
		CoolReset_Discount = 0f;
		Diablo_Hurt_PER = 0f;
		Bazuka_Compo_Bonus = 0f;
		Bazuka_Compo_count = 0;
		for (int m = 0; m < Bazuka_Parts.Length; m++)
		{
			Bazuka_Compo_count += Bazuka_Parts[m];
		}
		DMG_PER_ARTIFACT_FINAL = 0f;
		Booster_ADD_Time = 0f;
		Changed_GameSpeed = 1f;
		Dungeon_E_Play_time = 0f;
		Fairy_Wood_Box_Per = 0f;
		Fairy_Suply_Box_Per = 0f;
		Archive_COUNT = 0;
		for (int n = 0; n < me.Archievement_LV.Length; n++)
		{
			Archive_COUNT += me.Archievement_LV[n];
		}
		Artifact_COUNT = 0;
		for (int num = 0; num < me.Now_Artifact_LV.Length; num++)
		{
			if (me.Now_Artifact_LV[num] > 0)
			{
				Artifact_COUNT++;
			}
		}
		Missile_COUNT = 0;
		for (int num2 = 0; num2 < me.Misile_TIER.Length; num2++)
		{
			if (me.Misile_TIER[num2] > 0)
			{
				Missile_COUNT++;
			}
		}
	}

	public void All_Buff_Counting()
	{
		for (int i = 0; i < EQUIP_POSSILBEs_Count; i++)
		{
			if (EQUIP_MISILEs[i] == -1)
			{
				continue;
			}
			Option_ID = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_A;
			if (Option_ID != 999)
			{
				Option_Default = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_A_Value;
				if (!Option_ID.Equals(108))
				{
					Option_Plus = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_A_Plus * (float)Misile_TIER[EQUIP_MISILEs[i]];
				}
				else
				{
					Option_Plus = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_A_Plus * (float)Misile_DB.M_Value[Misile_TIER[EQUIP_MISILEs[i]]] / 100f;
				}
				Option_PLUS(i);
			}
			Option_ID = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_B;
			if (Option_ID != 999)
			{
				Option_Default = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_B_Value;
				if (!Option_ID.Equals(108))
				{
					Option_Plus = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_B_Plus * (float)Misile_TIER[EQUIP_MISILEs[i]];
				}
				else
				{
					Option_Plus = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_B_Plus * (float)Misile_DB.M_Value[Misile_TIER[EQUIP_MISILEs[i]]] / 100f;
				}
				Option_PLUS(i);
			}
			if (me.Misile_TIER[EQUIP_MISILEs[i]] < 5)
			{
				continue;
			}
			Option_ID = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_C;
			if (Option_ID != 999)
			{
				Option_Default = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_C_Value;
				if (!Option_ID.Equals(108))
				{
					Option_Plus = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_C_Plus * (float)Misile_TIER[EQUIP_MISILEs[i]];
				}
				else
				{
					Option_Plus = Misile_DB.me.misile_DB[EQUIP_MISILEs[i]].EQUIP_Option_C_Plus * (float)Misile_DB.M_Value[Misile_TIER[EQUIP_MISILEs[i]]] / 100f;
				}
				Option_PLUS(i);
			}
		}
		for (int j = 0; j < Misile_TIER.Length; j++)
		{
			bool flag = false;
			if (Misile_TIER[j] > 0)
			{
				flag = true;
			}
			if (!flag)
			{
				continue;
			}
			Option_ID = Misile_DB.me.misile_DB[j].BOX_Option_A;
			if (Option_ID != 999)
			{
				Option_Default = Misile_DB.me.misile_DB[j].BOX_Option_A_Value;
				if (!Option_ID.Equals(108))
				{
					Option_Plus = Misile_DB.me.misile_DB[j].BOX_Option_A_Plus * (float)Misile_TIER[j];
				}
				else
				{
					Option_Plus = Misile_DB.me.misile_DB[j].BOX_Option_A_Plus * (float)Misile_DB.M_Value[Misile_TIER[j]] / 100f;
				}
				Option_PLUS(j);
			}
			if (me.Misile_TIER[j] >= 3)
			{
				Option_ID = Misile_DB.me.misile_DB[j].BOX_Option_B;
				if (Option_ID != 999)
				{
					Option_Default = Misile_DB.me.misile_DB[j].BOX_Option_B_Value;
					if (!Option_ID.Equals(108))
					{
						Option_Plus = Misile_DB.me.misile_DB[j].BOX_Option_B_Plus * (float)Misile_TIER[j];
					}
					else
					{
						Option_Plus = Misile_DB.me.misile_DB[j].BOX_Option_B_Plus * (float)Misile_DB.M_Value[Misile_TIER[j]] / 100f;
					}
					Option_PLUS(j);
				}
			}
			if (me.Misile_TIER[j] < 10)
			{
				continue;
			}
			Option_ID = Misile_DB.me.misile_DB[j].BOX_Option_C;
			if (Option_ID != 999)
			{
				Option_Default = Misile_DB.me.misile_DB[j].BOX_Option_C_Value;
				if (!Option_ID.Equals(108))
				{
					Option_Plus = Misile_DB.me.misile_DB[j].BOX_Option_C_Plus * (float)Misile_TIER[j];
				}
				else
				{
					Option_Plus = Misile_DB.me.misile_DB[j].BOX_Option_C_Plus * (float)Misile_DB.M_Value[Misile_TIER[j]] / 100f;
				}
				Option_PLUS(j);
			}
		}
		for (int k = 0; k < Bazuka_Parts.Length; k++)
		{
			bool flag2 = false;
			if (Bazuka_Parts[k] > 0)
			{
				flag2 = true;
			}
			if (flag2)
			{
				Option_ID = Misile_DB.me.Bazuka_Parts_DB[k].EQUIP_Option_A;
				if (Option_ID != 999)
				{
					Option_Default = 0f;
					Option_Plus = Misile_DB.me.Bazuka_Parts_DB[k].EQUIP_Option_A_Plus * (float)Bazuka_Parts[k];
					Option_PLUS(k);
				}
			}
		}
		Option_Plus = 0f;
		for (int l = 0; l < Now_Unit_LV.Length; l++)
		{
			if (Now_Unit_LV[l] > Unit_DB.me.LV_Point[8])
			{
				for (int m = 0; m < Unit_DB.me.LV_Point.Length && Now_Unit_LV[l] - Unit_DB.me.LV_Point[8] >= Unit_DB.me.LV_Point[m]; m++)
				{
					Option_ID = Unit_DB.me.unit_DB[l].BUFF_ID[m];
					Option_Default = Unit_DB.me.unit_DB[l].BUFF_Value[m];
					Option_PLUS(l);
				}
			}
			else
			{
				for (int n = 0; n < Unit_DB.me.LV_Point.Length && Now_Unit_LV[l] >= Unit_DB.me.LV_Point[n]; n++)
				{
					Option_ID = Unit_DB.me.unit_DB[l].BUFF_ID[n];
					Option_Default = Unit_DB.me.unit_DB[l].BUFF_Value[n];
					Option_PLUS(l);
				}
			}
		}
		me.Artifact_Possesion.Clear();
		Option_Plus = 0f;
		for (int num = 0; num < Now_Artifact_LV.Length; num++)
		{
			if (Now_Artifact_LV[num] > 0)
			{
				me.Artifact_Possesion.Add(num);
				Option_ID = Artifact_DB.me.artifact_DB[num].BUFF_A_Type;
				if (Option_ID != 999)
				{
					Option_Default = Artifact_DB.me.artifact_DB[num].BUFF_A_Value;
					Option_Plus = Artifact_DB.me.artifact_DB[num].BUFF_A_Value_PLUS * (float)Now_Artifact_LV[num];
					Option_PLUS(num);
				}
				Option_ID = Artifact_DB.me.artifact_DB[num].BUFF_B_Type;
				if (Option_ID != 999)
				{
					Option_Default = Artifact_DB.me.artifact_DB[num].BUFF_B_Value * (100f + Artifact_Effect) / 100f;
					Option_Plus = Artifact_DB.me.artifact_DB[num].BUFF_B_Value_PLUS * (float)Now_Artifact_LV[num] * (100f + Artifact_Effect) / 100f;
					Option_PLUS(num);
				}
			}
		}
		Option_Plus = 0f;
		for (int num2 = 0; num2 < Diablo_Artifact_LV.Length; num2++)
		{
			if (Diablo_Artifact_LV[num2] <= 0)
			{
				continue;
			}
			Option_ID = Artifact_DB.me.diablo_artifact_DB[num2].BUFF_A_Type;
			if (Option_ID != 999)
			{
				if (Option_ID.Equals(77))
				{
					Option_Default = Artifact_DB.me.diablo_artifact_DB[num2].BUFF_A_Value;
					Option_Plus = Option_Default;
					for (int num3 = 0; num3 < Diablo_Artifact_LV[num2]; num3++)
					{
						Option_Plus *= 2f;
					}
					Option_Default = 0f;
				}
				Option_Default = Artifact_DB.me.diablo_artifact_DB[num2].BUFF_A_Value;
				Option_Plus = Artifact_DB.me.diablo_artifact_DB[num2].BUFF_A_Value_PLUS * (float)Diablo_Artifact_LV[num2];
				Option_PLUS(num2);
			}
			Option_ID = Artifact_DB.me.diablo_artifact_DB[num2].BUFF_B_Type;
			if (Option_ID == 999)
			{
				continue;
			}
			if (Option_ID.Equals(77))
			{
				Option_Default = Artifact_DB.me.diablo_artifact_DB[num2].BUFF_B_Value;
				Option_Plus = Option_Default;
				for (int num4 = 0; num4 < Diablo_Artifact_LV[num2]; num4++)
				{
					Option_Plus *= 2f;
				}
				Option_Default = 0f;
			}
			Option_Default = Artifact_DB.me.diablo_artifact_DB[num2].BUFF_B_Value;
			Option_Plus = Artifact_DB.me.diablo_artifact_DB[num2].BUFF_B_Value_PLUS * (float)Diablo_Artifact_LV[num2];
			Option_PLUS(num2);
		}
		Portal_BEST_Count();
		float num5 = 0f;
		int num6 = 0;
		for (int num7 = 1; num7 < me.portal_Parts.Length; num7++)
		{
			for (int num8 = 0; num8 < me.portal_Parts[num7].PORTAL_Parts_count.Length; num8++)
			{
				num6 += me.portal_Parts[num7].PORTAL_Parts_count[num8];
			}
			num5 = ((me.portal_Parts[num7].BEST_LEVEL <= 0) ? ((float)num6 * PORTAL_EF_VALUE * (100f + me.Portal_Add_Effective) / 100f) : ((me.Total_PotalSet_LV <= 0) ? ((float)num6 * PORTAL_EF_VALUE * (float)(me.portal_Parts[num7].BEST_LEVEL * 4)) : ((float)num6 * PORTAL_EF_VALUE * (float)(me.portal_Parts[num7].BEST_LEVEL * 4) * (float)me.Total_PotalSet_LV * 4f)));
			switch (num7)
			{
			case 1:
				HUMAN_ATTACK_PER += (int)(num5 * (100f + me.Portal_Add_Effective_by_Color[num7])) / 100;
				break;
			case 2:
				Mechanin_ATTACK_PER += (int)(num5 * (100f + me.Portal_Add_Effective_by_Color[num7])) / 100;
				break;
			case 3:
				Air_ATTACK_PER += (int)(num5 * (100f + me.Portal_Add_Effective_by_Color[num7])) / 100;
				break;
			case 4:
				MISSILE_DMG_PER += (int)(num5 * (100f + me.Portal_Add_Effective_by_Color[num7])) / 100;
				break;
			}
		}
		for (int num9 = 0; num9 < me.Buidling_Working_time.Length; num9++)
		{
			me.Buidling_Working_time[num9] = me.Buidling_Working_time_Origin[num9] * (100f - (float)(me.Building_LV[num9] - 1) * 2.5f) / 100f;
		}
	}

	public void Portal_BEST_Count()
	{
		for (int i = 1; i < portal_Parts.Length; i++)
		{
			portal_Parts[i].BEST_LEVEL = 99999;
			for (int j = 0; j < portal_Parts[i].PORTAL_Parts_count.Length; j++)
			{
				if (portal_Parts[i].PORTAL_Parts_count[j] < portal_Parts[i].BEST_LEVEL)
				{
					portal_Parts[i].BEST_LEVEL = portal_Parts[i].PORTAL_Parts_count[j];
				}
			}
		}
		Total_PotalSet_LV = 99999;
		for (int k = 1; k < portal_Parts.Length; k++)
		{
			if (portal_Parts[k].BEST_LEVEL < Total_PotalSet_LV)
			{
				Total_PotalSet_LV = portal_Parts[k].BEST_LEVEL;
			}
		}
	}

	public void Check_Possible(Quest_Goal_Type target_goal)
	{
		if (UI_Master.me.Arch_Alram.activeSelf)
		{
			return;
		}
		switch (target_goal)
		{
		case Quest_Goal_Type.ALL_TAP:
			Arch_Type_ID = 1;
			now_value = ALL_TAP;
			break;
		case Quest_Goal_Type.ALL_MINERAL:
			Arch_Type_ID = 2;
			now_value = ALL_MINERAL;
			break;
		case Quest_Goal_Type.ALL_GAS:
			Arch_Type_ID = 3;
			now_value = ALL_GAS;
			break;
		case Quest_Goal_Type.ALL_P_STONE:
			Arch_Type_ID = 4;
			now_value = me.ALL_P_STONE;
			break;
		case Quest_Goal_Type.ARTIFACT_COUNT:
			Arch_Type_ID = 5;
			now_value = me.Artifact_COUNT;
			break;
		case Quest_Goal_Type.BEST_LV:
			Arch_Type_ID = 6;
			now_value = me.BEST_LV;
			break;
		case Quest_Goal_Type.ALL_PLAYTIME:
			Arch_Type_ID = 7;
			now_value = me.ALL_PLAYTIME;
			break;
		case Quest_Goal_Type.ALL_MISSILE_COUNT:
		{
			Arch_Type_ID = 8;
			now_value = 0;
			for (int i = 0; i < me.Misile_TIER.Length; i++)
			{
				if (me.Misile_TIER[i] > 0)
				{
					now_value += (BigInteger)1;
				}
			}
			break;
		}
		case Quest_Goal_Type.ALL_KILL:
			Arch_Type_ID = 9;
			now_value = me.ALL_KILL;
			break;
		case Quest_Goal_Type.ALL_BOSSKILL:
			Arch_Type_ID = 10;
			now_value = me.ALL_BOSSKILL;
			break;
		case Quest_Goal_Type.ALL_DUNGEON_CLEAR:
			Arch_Type_ID = 11;
			now_value = me.ALL_DUNGEON_CLEAR;
			break;
		case Quest_Goal_Type.ALL_SKILL_USE:
			Arch_Type_ID = 12;
			now_value = me.ALL_SKILL_USE;
			break;
		case Quest_Goal_Type.ALL_FAIRY:
			Arch_Type_ID = 13;
			now_value = me.ALL_FAIRY;
			break;
		case Quest_Goal_Type.ALL_DROPPORT:
			Arch_Type_ID = 14;
			now_value = me.ALL_DROPPORT;
			break;
		case Quest_Goal_Type.ALL_PROMOTE:
			Arch_Type_ID = 15;
			now_value = me.ALL_PROMOTE;
			break;
		case Quest_Goal_Type.ALL_HIDDEN:
			Arch_Type_ID = 16;
			now_value = me.ALL_HIDDEN;
			break;
		}
		if (me.Archievement_LV[Arch_Type_ID] < 9)
		{
			goal_value = new BigInteger(Arch_DB.me.arch_DB[Arch_Type_ID].Arch[me.Archievement_LV[Arch_Type_ID]].TARGET_VALUE);
			if (now_value >= goal_value)
			{
				Get_Reward_Possible = true;
				UI_Master.me.PAUSE_Alram.SetActive(true);
				UI_Master.me.Arch_Alram.SetActive(true);
			}
			else
			{
				Get_Reward_Possible = false;
			}
		}
	}

	public void Option_PLUS(int Self_ID)
	{
		switch (Option_ID)
		{
		case 1:
			CRYSTAL_BONUS_PER += (int)(Option_Default + Option_Plus);
			break;
		case 2:
			GOLD_BONUS_PER_NORMAL += (int)(Option_Default + Option_Plus);
			break;
		case 3:
			GOLD_BONUS_PER_GOLREM += (int)(Option_Default + Option_Plus);
			break;
		case 4:
			GOLD_BONUS_PER_BOSS += (int)(Option_Default + Option_Plus);
			break;
		case 5:
			GOLD_BONUS_PER_ALL += (int)(Option_Default + Option_Plus);
			break;
		case 6:
			GOLD_BONUS_PER_COMEBACK += (int)(Option_Default + Option_Plus);
			break;
		case 7:
			GOLD_BONUS_PER_BOX += (int)(Option_Default + Option_Plus);
			break;
		case 8:
			MISSILE_DMG_PER += Option_Default + Option_Plus;
			break;
		case 9:
			MISSILE_CRITICAL_PER += Option_Default + Option_Plus;
			break;
		case 10:
			MISSILE_CRITICAL_DMG_PER += Option_Default + Option_Plus;
			break;
		case 11:
			MISSILE_DMG_PER_BY_UNIT += Option_Default + Option_Plus;
			break;
		case 12:
			Unit_DMG_PER[Self_ID] += Option_Default + Option_Plus;
			break;
		case 13:
			Unit_Ciritical[Self_ID] += Option_Default + Option_Plus;
			break;
		case 14:
			HUMAN_ATTACK_PER += (int)(Option_Default + Option_Plus);
			break;
		case 15:
			Mechanin_ATTACK_PER += (int)(Option_Default + Option_Plus);
			break;
		case 16:
			Air_ATTACK_PER += (int)(Option_Default + Option_Plus);
			break;
		case 17:
			All_UNIT_ATTACK_PER += (int)(Option_Default + Option_Plus);
			break;
		case 18:
			ATTACK_PER_Artifact_Count += (int)(Option_Default + Option_Plus);
			break;
		case 19:
			ATTACK_PER_Star_Count += (int)(Option_Default + Option_Plus);
			break;
		case 20:
			DMG_PLUS_PER_ALL_TARGET += Option_Default + Option_Plus;
			break;
		case 21:
			DMG_PLUS_PER_NORMAL_TARGET += Option_Default + Option_Plus;
			break;
		case 22:
			DMG_PLUS_PER_BOSS_TARGET += (int)(Option_Default + Option_Plus);
			break;
		case 23:
			DMG_PLUS_PER_GOLREM_TARGET += (int)(Option_Default + Option_Plus);
			break;
		case 24:
			SKILL_Value_Plus[0] += (int)(Option_Default + Option_Plus);
			break;
		case 25:
			SKILL_Value_Plus[2] += (int)(Option_Default + Option_Plus);
			break;
		case 26:
			SKILL_Value_Plus[3] += (int)(Option_Default + Option_Plus);
			break;
		case 27:
			SKILL_Value_Plus[4] += (int)(Option_Default + Option_Plus);
			break;
		case 28:
			SKILL_Value_Plus[5] += (int)(Option_Default + Option_Plus);
			break;
		case 29:
			SKILL_Value_Plus[1] += (int)(Option_Default + Option_Plus);
			break;
		case 30:
			SKILL_Effectivetime[2] += (int)(Option_Default + Option_Plus);
			break;
		case 31:
			SKILL_Effectivetime[3] += (int)(Option_Default + Option_Plus);
			break;
		case 32:
			SKILL_Effectivetime[4] += (int)(Option_Default + Option_Plus);
			break;
		case 33:
			SKILL_Effectivetime[5] += (int)(Option_Default + Option_Plus);
			break;
		case 34:
			SKILL_Effectivetime[1] += (int)(Option_Default + Option_Plus);
			break;
		case 35:
			ALL_SKILL_Effectivetime += (int)(Option_Default + Option_Plus);
			break;
		case 36:
			SKILL_Cooltime_Minus[0] += (int)(Option_Default + Option_Plus);
			break;
		case 37:
			SKILL_Cooltime_Minus[2] += (int)(Option_Default + Option_Plus);
			break;
		case 38:
			SKILL_Cooltime_Minus[3] += (int)(Option_Default + Option_Plus);
			break;
		case 39:
			SKILL_Cooltime_Minus[4] += (int)(Option_Default + Option_Plus);
			break;
		case 40:
			SKILL_Cooltime_Minus[5] += (int)(Option_Default + Option_Plus);
			break;
		case 41:
			SKILL_Cooltime_Minus[1] += (int)(Option_Default + Option_Plus);
			break;
		case 42:
			ALL_SKILL_Cooltime_Minus += (int)(Option_Default + Option_Plus);
			break;
		case 43:
			Golrem_Per += Option_Default + Option_Plus;
			break;
		case 44:
			BoxFairy_PER_DOUBLE += Option_Default + Option_Plus;
			break;
		case 45:
			Gold_Mineral_PER += Option_Default + Option_Plus;
			break;
		case 46:
			Price_Discount_Unit += Option_Default + Option_Plus;
			break;
		case 47:
			Price_Free_Unit += Option_Default + Option_Plus;
			break;
		case 48:
			Price_Discount_Misile += Option_Default + Option_Plus;
			break;
		case 49:
			Price_Free_Misile += Option_Default + Option_Plus;
			break;
		case 50:
			Portal_Helath_Minus += (int)(Option_Default + Option_Plus);
			break;
		case 51:
			Stage_Skip += (int)(Option_Default + Option_Plus);
			break;
		case 52:
			FAIRY_URANUM += (int)(Option_Default + Option_Plus);
			break;
		case 53:
			BUHAL_SAVE_LV += Option_Default + Option_Plus;
			break;
		case 54:
			FAIRY_Boom += (int)(Option_Default + Option_Plus);
			break;
		case 56:
			break;
		case 57:
			ATTACK_PER_Enemy_Count += Option_Default + Option_Plus;
			break;
		case 58:
			Artifact_Effect += Option_Default + Option_Plus;
			break;
		case 59:
			COUNT_for_Doubleshot += (int)(Option_Default + Option_Plus);
			break;
		case 60:
			COUNT_for_SteamPack += (int)(Option_Default + Option_Plus);
			break;
		case 61:
			COUNT_Boom_five += (int)(Option_Default + Option_Plus);
			break;
		case 62:
			SteamPACK_Limit_Time = SteamPACK_Limit_Time * (100f + Option_Default + Option_Plus) / 100f;
			break;
		case 63:
			SteamPACK_Need_tap += (int)(Option_Default + Option_Plus);
			break;
		case 64:
			Boss_Save_Gold += Option_Default + Option_Plus;
			break;
		case 65:
			COUNT_BigShot += (int)(Option_Default + Option_Plus);
			break;
		case 66:
			JUST_KILL_20 += Option_Default + Option_Plus;
			break;
		case 67:
			AUTOShot_delay += Option_Default + Option_Plus;
			break;
		case 68:
			Boss_P_stone += Option_Default + Option_Plus;
			break;
		case 69:
			PIERCING_ATTACK += Option_Default + Option_Plus;
			break;
		case 70:
			ADD_small_Missile += (int)(Option_Default + Option_Plus);
			break;
		case 71:
			ADD_Doubleshot_missile += (int)(Option_Default + Option_Plus);
			break;
		case 72:
			Unit_ATTACK_SPEED_PER[Self_ID] += Option_Default + Option_Plus;
			break;
		case 73:
			DMG_PLUS_PER_Portal_TARGET += (int)(Option_Default + Option_Plus);
			break;
		case 74:
			DMG_PLUS_PER_DUNGEON_BOSS_TARGET += (int)(Option_Default + Option_Plus);
			break;
		case 75:
			Hellsteon_Bonus += Option_Default + Option_Plus;
			break;
		case 76:
			SteamPACK_Fin_Boom += (int)(Option_Default + Option_Plus);
			break;
		case 77:
			DMG_PLUS_PER_ALL += Option_Default + Option_Plus;
			break;
		case 78:
			Portal_Parts_Drop_Per += Option_Default + Option_Plus;
			break;
		case 79:
			Portal_Add_Effective += Option_Default + Option_Plus;
			break;
		case 80:
			QUEST_REWARD_PER += Option_Default + Option_Plus;
			break;
		case 81:
			Boss_Helath_Minus += Option_Default + Option_Plus;
			break;
		case 82:
		case 83:
		case 84:
		case 85:
		case 86:
		case 87:
			Misile_Tierup_Discount_Per[Option_ID - 81] += Option_Default + Option_Plus;
			break;
		case 88:
			HUMAN_UPGRADE_DISCOUNT += Option_Default + Option_Plus;
			break;
		case 89:
			Mechanin_UPGRADE_DISCOUNT += Option_Default + Option_Plus;
			break;
		case 90:
			Air_UPGRADE_DISCOUNT += Option_Default + Option_Plus;
			break;
		case 91:
		case 92:
		case 93:
		case 94:
			Portal_Add_Effective_by_Color[Option_ID - 90] += Option_Default + Option_Plus;
			break;
		case 95:
			Dungeon_A_boss_ADD_DMG_PER += Option_Default + Option_Plus;
			break;
		case 96:
			Dungeon_B_boss_ADD_DMG_PER += Option_Default + Option_Plus;
			break;
		case 97:
			Dungeon_D_boss_ADD_DMG_PER += Option_Default + Option_Plus;
			break;
		case 98:
			Bazuka_compo_double_Per += Option_Default + Option_Plus;
			break;
		case 99:
			Building_Point_double_Per += Option_Default + Option_Plus;
			break;
		case 100:
			NO_ADS_Per += Option_Default + Option_Plus;
			break;
		case 101:
			SKILL_after_ACTION_MAN_PER += Option_Default + Option_Plus;
			break;
		case 102:
			SKILL_after_COOL_RESET_PER += Option_Default + Option_Plus;
			break;
		case 103:
			DUNGEON_KEY_DOUBLE_PER += Option_Default + Option_Plus;
			break;
		case 104:
			Booster_DOUBLE_PER += Option_Default + Option_Plus;
			break;
		case 105:
			CoolReset_Discount += Option_Default + Option_Plus;
			break;
		case 106:
			Diablo_Hurt_PER += Option_Default + Option_Plus;
			break;
		case 107:
			Bazuka_Compo_Bonus += Option_Default + Option_Plus;
			break;
		case 108:
			MISSILE_DMG_PER_FINAL += Option_Default + Option_Plus;
			break;
		case 109:
			DMG_PER_ARTIFACT_FINAL += Option_Default + Option_Plus + Artifact_Effect;
			break;
		case 110:
			Booster_ADD_Time += Option_Default + Option_Plus;
			break;
		case 111:
			Changed_GameSpeed += Option_Default + Option_Plus;
			Changed_GameSpeed = (100f + Changed_GameSpeed) / 100f;
			break;
		case 112:
			Dungeon_E_Play_time += Option_Default + Option_Plus;
			break;
		case 113:
			Fairy_Wood_Box_Per += Option_Default + Option_Plus;
			break;
		case 114:
			Fairy_Suply_Box_Per += Option_Default + Option_Plus;
			break;
		case 55:
			break;
		}
	}
}
