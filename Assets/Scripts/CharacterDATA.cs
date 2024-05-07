using System;
using Keiwando.BigInteger;

[Serializable]
public class CharacterDATA
{
	public string Name;

	public Tribe_Type Tribe;

	public float Size = 1f;

	public int INFI_COUNT = 1;

	public int Base_Class_LV;

	public int[] Base_Class_Number = new int[5];

	public int[] NEXT_Class_Number = new int[2];

	public int[] Open_Skill = new int[12];

	public int[] Open_Skill_LV = new int[12];

	public float Battle_Point;

	public BigInteger HP;

	public BigInteger MP;

	public BigInteger ATK;

	public BigInteger DEF;

	public BigInteger INT;

	public BigInteger DEX;

	public float MP_Heal;

	public float HP_Heal;

	public float Lucky;

	public BigInteger Critical_DMG;

	public int ATK_TYPE;

	public float ATK_Speed;

	public float ATK_Range;

	public float Move_Speed;

	public float Misile_Speed;

	public float Charge_Delay;

	public float Knockback;

	public float KB_Regist;

	public float HP_Drain;

	public float MP_Drain;

	public float Death_HP_Drain;

	public float Death_MP_Drain;

	public Attack_Type attack_type;

	public Misile_Type misile_type;

	public Idle_Type idle_type;

	public Heal_Type heal_type;

	public BigInteger Gold;

	public BigInteger GETEXP;

	public BigInteger NEED_MP;

	public float Drop_Per;

	public int[] Item_Number = new int[14];

	public int Q_Item;

	public int ATK_Element;

	public int DEF_Element;

	public float MINUS_Cool;

	public float MINUS_MANA;

	public void Clear()
	{
		Tribe = Tribe_Type.HUMAN;
		HP = 0;
		MP = 0;
		Size = 1f;
		ATK = 0;
		DEF = 0;
		INT = 0;
		DEX = 0;
		HP_Heal = 0f;
		MP_Heal = 0f;
		Lucky = 0f;
		Critical_DMG = 0;
		ATK_Speed = 0f;
		ATK_Range = 0f;
		Move_Speed = 0f;
		Misile_Speed = 0f;
		Charge_Delay = 0f;
		Knockback = 0f;
		KB_Regist = 0f;
		HP_Drain = 0f;
		MP_Drain = 0f;
		Death_HP_Drain = 0f;
		Death_MP_Drain = 0f;
		attack_type = Attack_Type.Melee;
		misile_type = Misile_Type.Normal;
		idle_type = Idle_Type.Idle;
		heal_type = Heal_Type.HP;
		Gold = 0;
		GETEXP = 0;
		Drop_Per = 0f;
		for (int i = 0; i < Item_Number.Length; i++)
		{
			Item_Number[i] = 0;
		}
		Q_Item = 0;
		ATK_Element = 0;
		DEF_Element = 0;
		MINUS_Cool = 0f;
		MINUS_MANA = 0f;
		for (int j = 0; j < Base_Class_Number.Length; j++)
		{
			Base_Class_Number[j] = 0;
		}
		for (int k = 0; k < NEXT_Class_Number.Length; k++)
		{
			NEXT_Class_Number[k] = 0;
		}
		for (int l = 0; l < Open_Skill.Length; l++)
		{
			Open_Skill[l] = 0;
		}
	}

	public void Copy_That(CharacterDATA Target_Data)
	{
		Name = Target_Data.Name;
		Tribe = Target_Data.Tribe;
		Size = Target_Data.Size;
		HP = Target_Data.HP;
		MP = Target_Data.MP;
		ATK = Target_Data.ATK;
		DEF = Target_Data.DEF;
		INT = Target_Data.INT;
		DEX = Target_Data.DEX;
		MP_Heal = Target_Data.MP_Heal;
		HP_Heal = Target_Data.HP_Heal;
		Lucky = Target_Data.Lucky;
		Critical_DMG = Target_Data.Critical_DMG;
		ATK_Speed = Target_Data.ATK_Speed;
		ATK_Range = Target_Data.ATK_Range;
		Move_Speed = Target_Data.Move_Speed;
		Misile_Speed = Target_Data.Misile_Speed;
		Charge_Delay = Target_Data.Charge_Delay;
		Knockback = Target_Data.Knockback;
		KB_Regist = Target_Data.KB_Regist;
		Death_HP_Drain = Target_Data.Death_HP_Drain;
		Death_MP_Drain = Target_Data.Death_MP_Drain;
		attack_type = Target_Data.attack_type;
		misile_type = Target_Data.misile_type;
		idle_type = Target_Data.idle_type;
		heal_type = Target_Data.heal_type;
		Gold = Target_Data.Gold;
		GETEXP = Target_Data.GETEXP;
		Drop_Per = Target_Data.Drop_Per;
		for (int i = 0; i < Target_Data.Item_Number.Length; i++)
		{
			Item_Number[i] = Target_Data.Item_Number[i];
		}
		Q_Item = Target_Data.Q_Item;
		ATK_Element = Target_Data.ATK_Element;
		DEF_Element = Target_Data.DEF_Element;
		MINUS_Cool = Target_Data.MINUS_Cool;
		MINUS_MANA = Target_Data.MINUS_MANA;
		Base_Class_LV = Target_Data.Base_Class_LV;
		for (int j = 0; j < Base_Class_Number.Length; j++)
		{
			Base_Class_Number[j] = Target_Data.Base_Class_Number[j];
		}
		for (int k = 0; k < Target_Data.NEXT_Class_Number.Length; k++)
		{
			NEXT_Class_Number[k] = Target_Data.NEXT_Class_Number[k];
		}
		for (int l = 0; l < Target_Data.Open_Skill.Length; l++)
		{
			Open_Skill[l] = Target_Data.Open_Skill[l];
		}
	}

	public void ADD_That(CharacterDATA Target_Data, bool Class_Change)
	{
		HP += Target_Data.HP;
		MP += Target_Data.MP;
		ATK += Target_Data.ATK;
		DEF += Target_Data.DEF;
		INT += Target_Data.INT;
		DEX += Target_Data.DEX;
		MP_Heal += Target_Data.MP_Heal;
		HP_Heal += Target_Data.HP_Heal;
		Lucky += Target_Data.Lucky;
		if (Lucky > 70f)
		{
			Lucky = 70f;
		}
		Critical_DMG += Target_Data.Critical_DMG;
		ATK_Speed += Target_Data.ATK_Speed;
		ATK_Range += Target_Data.ATK_Range;
		Move_Speed += Target_Data.Move_Speed;
		Misile_Speed += Target_Data.Misile_Speed;
		Charge_Delay += Target_Data.Charge_Delay;
		Knockback += Target_Data.Knockback;
		KB_Regist += Target_Data.KB_Regist;
		HP_Drain += Target_Data.HP_Drain;
		MP_Drain += Target_Data.MP_Drain;
		Death_HP_Drain += Target_Data.Death_HP_Drain;
		Death_MP_Drain += Target_Data.Death_MP_Drain;
		Gold += Target_Data.Gold;
		GETEXP += Target_Data.GETEXP;
		Drop_Per += Target_Data.Drop_Per;
		MINUS_Cool += Target_Data.MINUS_Cool;
		MINUS_MANA += Target_Data.MINUS_MANA;
	}

	public int Skill_Check(int Skill_ID)
	{
		for (int i = 0; i < Open_Skill.Length; i++)
		{
			if (Open_Skill[i].Equals(Skill_ID))
			{
				return i;
			}
		}
		return 999;
	}
}
