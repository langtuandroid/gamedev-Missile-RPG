using Keiwando.BigInteger;
using UnityEngine;

public class Status_UI : MonoBehaviour
{
	public int Status_ID;

	public UILabel Name_label;

	public UILabel Word_label;

	public BigInteger Value;

	public float Value_F;

	public void Setting()
	{
		Name_label.text = Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Status_ID));
		Value_F = -999f;
		switch (Status_ID)
		{
		case 1:
			Value_F = Now_Data.me.CRYSTAL_BONUS_PER;
			break;
		case 2:
			Value_F = Now_Data.me.GOLD_BONUS_PER_NORMAL;
			break;
		case 3:
			Value_F = Now_Data.me.GOLD_BONUS_PER_GOLREM;
			break;
		case 4:
			Value_F = Now_Data.me.GOLD_BONUS_PER_BOSS;
			break;
		case 5:
			Value_F = Now_Data.me.GOLD_BONUS_PER_ALL;
			break;
		case 6:
			Value_F = Now_Data.me.GOLD_BONUS_PER_COMEBACK;
			break;
		case 7:
			Value_F = Now_Data.me.GOLD_BONUS_PER_BOX;
			break;
		case 8:
			Value_F = Now_Data.me.MISSILE_DMG_PER;
			break;
		case 9:
			Value_F = Now_Data.me.MISSILE_CRITICAL_PER;
			break;
		case 10:
			Value_F = Now_Data.me.MISSILE_CRITICAL_DMG_PER;
			break;
		case 11:
			Value_F = Now_Data.me.MISSILE_DMG_PER_BY_UNIT;
			break;
		case 14:
			Value_F = Now_Data.me.HUMAN_ATTACK_PER;
			break;
		case 15:
			Value_F = Now_Data.me.Mechanin_ATTACK_PER;
			break;
		case 16:
			Value_F = Now_Data.me.Air_ATTACK_PER;
			break;
		case 17:
			Value_F = Now_Data.me.All_UNIT_ATTACK_PER;
			break;
		case 18:
			Value_F = Now_Data.me.ATTACK_PER_Artifact_Count;
			break;
		case 19:
			Value_F = Now_Data.me.ATTACK_PER_Star_Count;
			break;
		case 20:
			Value_F = Now_Data.me.DMG_PLUS_PER_ALL_TARGET;
			break;
		case 21:
			Value_F = Now_Data.me.DMG_PLUS_PER_NORMAL_TARGET;
			break;
		case 22:
			Value_F = Now_Data.me.DMG_PLUS_PER_BOSS_TARGET;
			break;
		case 23:
			Value_F = Now_Data.me.DMG_PLUS_PER_GOLREM_TARGET;
			break;
		case 24:
			Value_F = Now_Data.me.SKILL_Value_Plus[0];
			break;
		case 25:
			Value_F = Now_Data.me.SKILL_Value_Plus[2];
			break;
		case 26:
			Value_F = Now_Data.me.SKILL_Value_Plus[3];
			break;
		case 27:
			Value_F = Now_Data.me.SKILL_Value_Plus[4];
			break;
		case 28:
			Value_F = Now_Data.me.SKILL_Value_Plus[5];
			break;
		case 29:
			Value_F = Now_Data.me.SKILL_Value_Plus[1];
			break;
		case 30:
			Value_F = Now_Data.me.SKILL_Effectivetime[2];
			break;
		case 31:
			Value_F = Now_Data.me.SKILL_Effectivetime[3];
			break;
		case 32:
			Value_F = Now_Data.me.SKILL_Effectivetime[4];
			break;
		case 33:
			Value_F = Now_Data.me.SKILL_Effectivetime[5];
			break;
		case 34:
			Value_F = Now_Data.me.SKILL_Effectivetime[1];
			break;
		case 35:
			Value_F = Now_Data.me.ALL_SKILL_Effectivetime;
			break;
		case 36:
			Value_F = Now_Data.me.SKILL_Cooltime_Minus[0];
			break;
		case 37:
			Value_F = Now_Data.me.SKILL_Cooltime_Minus[2];
			break;
		case 38:
			Value_F = Now_Data.me.SKILL_Cooltime_Minus[3];
			break;
		case 39:
			Value_F = Now_Data.me.SKILL_Cooltime_Minus[4];
			break;
		case 40:
			Value_F = Now_Data.me.SKILL_Cooltime_Minus[5];
			break;
		case 41:
			Value_F = Now_Data.me.SKILL_Cooltime_Minus[1];
			break;
		case 42:
			Value_F = Now_Data.me.ALL_SKILL_Cooltime_Minus;
			break;
		case 43:
			Value_F = Now_Data.me.Golrem_Per;
			break;
		case 44:
			Value_F = Now_Data.me.BoxFairy_PER_DOUBLE;
			break;
		case 45:
			Value_F = Now_Data.me.Gold_Mineral_PER;
			break;
		case 46:
			Value_F = Now_Data.me.Price_Discount_Unit;
			break;
		case 47:
			Value_F = Now_Data.me.Price_Free_Unit;
			break;
		case 48:
			Value_F = Now_Data.me.Price_Discount_Misile;
			break;
		case 49:
			Value_F = Now_Data.me.Price_Free_Misile;
			break;
		case 50:
			Value_F = Now_Data.me.Portal_Helath_Minus;
			break;
		case 51:
			Value_F = Now_Data.me.Stage_Skip;
			break;
		case 52:
			Value_F = Now_Data.me.FAIRY_URANUM;
			break;
		case 53:
			Value_F = Now_Data.me.BUHAL_SAVE_LV;
			break;
		case 54:
			Value_F = Now_Data.me.FAIRY_Boom;
			break;
		case 57:
			Value_F = Now_Data.me.ATTACK_PER_Enemy_Count;
			break;
		case 58:
			Value_F = Now_Data.me.Artifact_Effect;
			break;
		case 59:
			Value_F = Now_Data.me.COUNT_for_Doubleshot;
			break;
		case 60:
			Value_F = Now_Data.me.COUNT_for_SteamPack;
			break;
		case 61:
			Value_F = Now_Data.me.COUNT_Boom_five;
			break;
		case 62:
			Value_F = Now_Data.me.SteamPACK_Limit_Time;
			break;
		case 63:
			Value_F = Now_Data.me.SteamPACK_Need_tap;
			break;
		case 64:
			Value_F = Now_Data.me.Boss_Save_Gold;
			break;
		case 65:
			Value_F = Now_Data.me.COUNT_BigShot;
			break;
		case 66:
			Value_F = Now_Data.me.JUST_KILL_20;
			break;
		case 67:
			Value_F = Now_Data.me.AUTOShot_delay;
			break;
		case 68:
			Value_F = Now_Data.me.Boss_P_stone;
			break;
		case 69:
			Value_F = Now_Data.me.PIERCING_ATTACK;
			break;
		case 70:
			Value_F = Now_Data.me.ADD_small_Missile;
			break;
		case 71:
			Value_F = Now_Data.me.ADD_Doubleshot_missile;
			break;
		case 73:
			Value_F = Now_Data.me.DMG_PLUS_PER_Portal_TARGET;
			break;
		case 74:
			Value_F = Now_Data.me.DMG_PLUS_PER_DUNGEON_BOSS_TARGET;
			break;
		case 75:
			Value_F = Now_Data.me.Hellsteon_Bonus;
			break;
		case 76:
			Value_F = Now_Data.me.SteamPACK_Fin_Boom;
			break;
		case 77:
			Value_F = Now_Data.me.DMG_PLUS_PER_ALL;
			break;
		case 78:
			Value_F = Now_Data.me.Portal_Parts_Drop_Per;
			break;
		case 79:
			Value_F = Now_Data.me.Portal_Add_Effective;
			break;
		case 80:
			Value_F = Now_Data.me.QUEST_REWARD_PER;
			break;
		case 81:
			Value_F = Now_Data.me.Boss_Helath_Minus;
			break;
		case 82:
		case 83:
		case 84:
		case 85:
		case 86:
		case 87:
			Value_F = Now_Data.me.Misile_Tierup_Discount_Per[Status_ID - 81];
			break;
		case 88:
			Value_F = Now_Data.me.HUMAN_UPGRADE_DISCOUNT;
			break;
		case 89:
			Value_F = Now_Data.me.Mechanin_UPGRADE_DISCOUNT;
			break;
		case 90:
			Value_F = Now_Data.me.Air_UPGRADE_DISCOUNT;
			break;
		case 91:
		case 92:
		case 93:
		case 94:
			Value_F = Now_Data.me.Portal_Add_Effective_by_Color[Status_ID - 90];
			break;
		case 95:
			Value_F = Now_Data.me.Dungeon_A_boss_ADD_DMG_PER;
			break;
		case 96:
			Value_F = Now_Data.me.Dungeon_B_boss_ADD_DMG_PER;
			break;
		case 97:
			Value_F = Now_Data.me.Dungeon_D_boss_ADD_DMG_PER;
			break;
		case 98:
			Value_F = Now_Data.me.Bazuka_compo_double_Per;
			break;
		case 99:
			Value_F = Now_Data.me.Building_Point_double_Per;
			break;
		case 100:
			Value_F = Now_Data.me.NO_ADS_Per;
			break;
		case 101:
			Value_F = Now_Data.me.SKILL_after_ACTION_MAN_PER;
			break;
		case 102:
			Value_F = Now_Data.me.SKILL_after_COOL_RESET_PER;
			break;
		case 103:
			Value_F = Now_Data.me.DUNGEON_KEY_DOUBLE_PER;
			break;
		case 104:
			Value_F = Now_Data.me.Booster_DOUBLE_PER;
			break;
		case 105:
			Value_F = Now_Data.me.CoolReset_Discount;
			break;
		case 106:
			Value_F = Now_Data.me.Diablo_Hurt_PER;
			break;
		case 107:
			Value_F = Now_Data.me.Bazuka_Compo_Bonus;
			break;
		case 108:
			Value_F = Now_Data.me.MISSILE_DMG_PER_FINAL;
			break;
		case 109:
			Value_F = Now_Data.me.DMG_PER_ARTIFACT_FINAL + Now_Data.me.Artifact_Effect;
			break;
		}
		if (Value_F.Equals(-999f))
		{
			Word_label.text = Now_Data.INT_to_ABC(Value);
			return;
		}
		Word_label.text = string.Format("{0}", Value_F);
		Word_label.text += Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Status_ID));
	}
}
