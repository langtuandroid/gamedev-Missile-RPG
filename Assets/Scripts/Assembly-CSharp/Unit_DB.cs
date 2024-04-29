using Keiwando.BigInteger;
using SimpleJSON;
using UnityEngine;

public class Unit_DB : MonoBehaviour
{
	public static Unit_DB me;

	public Misile_DB misile_DB;

	public TextAsset DB_file;

	private string DB_file_string;

	public Unit_Data[] unit_DB;

	public string[] fibo_table = new string[1000];

	public string[] smooth_ONE_table = new string[1000];

	public string[] smooth_Double_table = new string[1000];

	public string[] smooth_Triple_table = new string[1000];

	public string[] smooth_Fourth_table = new string[1000];

	public string[] Double_table = new string[100];

	public string[] JEGOP_table = new string[10000];

	public string[] JEGOP_TEN_table = new string[10000];

	public string[] JEGOP_SASIP_PER_table = new string[10000];

	public int[] LV_Point = new int[9] { 10, 25, 50, 100, 200, 400, 800, 1200, 2000 };

	public int[] TechUpgradeLV = new int[22]
	{
		0, 20, 60, 120, 200, 300, 420, 560, 720, 900,
		1100, 1320, 1560, 1820, 2100, 2400, 2700, 3000, 3600, 2400,
		2400, 2400
	};

	public static int[] TierPrice = new int[24]
	{
		100, 200, 300, 400, 500, 600, 800, 1000, 1200, 1400,
		1600, 1900, 2200, 2500, 2900, 3300, 3700, 4100, 4500, 4900,
		5400, 5900, 6400, 6500
	};

	public static int[] Tier_Buff = new int[25]
	{
		20, 40, 60, 80, 100, 200, 300, 400, 800, 1500,
		2500, 4000, 6000, 14000, 20000, 30000, 40000, 50000, 80000, 100000,
		250000, 500000, 900000, 900000, 900000
	};

	private string Imsi_load;

	private string[] Imsi_load_part;

	private float Multiply_A;

	private float Multiply_B;

	public void Awake()
	{
		if (me == null)
		{
			me = this;
		}
	}

	public void Setting()
	{
		DB_file_string = DB_file.text;
		JSONNode jSONNode = JSON.Parse(DB_file_string);
		unit_DB = new Unit_Data[jSONNode.Count];
		for (int i = 0; i < jSONNode.Count; i++)
		{
			unit_DB[i] = new Unit_Data();
			unit_DB[i].DMG = jSONNode[i]["name"];
			unit_DB[i].DMG = jSONNode[i]["dmg"];
			unit_DB[i].ATK_SPEED = jSONNode[i]["atkspeed"].AsFloat;
			unit_DB[i].ATK_TYPE = jSONNode[i]["missiletype"].AsInt;
			unit_DB[i].FIX_DPS = jSONNode[i]["dps"];
			switch ((string)jSONNode[i]["unittype"])
			{
			case "보병":
				unit_DB[i].unit_TYPE = Unit_Type.Human;
				break;
			case "장갑":
				unit_DB[i].unit_TYPE = Unit_Type.Mechanic;
				break;
			case "공중":
				unit_DB[i].unit_TYPE = Unit_Type.Air;
				break;
			}
			unit_DB[i].TIER_PLUS = jSONNode[i]["tierupplus"];
			unit_DB[i].TIERUP_Price = jSONNode[i]["tierupprice"];
			unit_DB[i].BUFF_ID = new int[9];
			unit_DB[i].BUFF_Value = new float[9];
			for (int j = 0; j < unit_DB[i].BUFF_ID.Length; j++)
			{
				unit_DB[i].BUFF_ID[j] = misile_DB.OPTION_TYPE_to_ID(jSONNode[i][string.Format("lv{0}type", LV_Point[j])]);
				unit_DB[i].BUFF_Value[j] = jSONNode[i][string.Format("lv{0}value", LV_Point[j])].AsFloat;
			}
		}
		Debug.Log("유닛 DB적용.");
	}

	public BigInteger DMG_by_LV(int ID, int LV)
	{
		int num = ID;
		int num2 = LV;
		if (LV > LV_Point[8])
		{
			ID += 27;
			LV -= LV_Point[8];
		}
		BigInteger bigInteger = 0;
		int num3 = num2 / 1000;
		int num4 = num2 % 1000;
		BigInteger bigInteger2 = new BigInteger(unit_DB[ID].DMG);
		if (bigInteger2 < 1)
		{
			bigInteger2 = 1;
		}
		bigInteger += new BigInteger(unit_DB[ID].DMG) * LV + (bigInteger2 * (new BigInteger(smooth_ONE_table[999]) * num3) + bigInteger2 * new BigInteger(smooth_ONE_table[num4])) / 100;
		Multiply_A = 100f;
		for (int i = 0; i < Now_Data.me.Now_Unit_Tier[num]; i++)
		{
			Multiply_A += Tier_Buff[i];
		}
		Multiply_B = 100f;
		switch (unit_DB[ID].unit_TYPE)
		{
		case Unit_Type.Human:
			Multiply_B += Now_Data.me.HUMAN_ATTACK_PER;
			break;
		case Unit_Type.Mechanic:
			Multiply_B += Now_Data.me.Mechanin_ATTACK_PER;
			break;
		case Unit_Type.Air:
			Multiply_B += Now_Data.me.Air_ATTACK_PER;
			break;
		}
		Multiply_B += Now_Data.me.All_UNIT_ATTACK_PER + Now_Data.me.DMG_PLUS_PER_ALL_TARGET;
		Multiply_B += (float)Now_Data.me.Archive_COUNT * Now_Data.me.ATTACK_PER_Star_Count;
		Multiply_B += Now_Data.me.ATTACK_PER_Artifact_Count * Now_Data.me.ATTACK_PER_Artifact_Count;
		Multiply_B += Now_Data.me.Bazuka_Compo_Bonus * (float)Now_Data.me.Bazuka_Compo_count;
		bigInteger *= (BigInteger)(int)Multiply_A;
		bigInteger /= (BigInteger)100;
		bigInteger *= (BigInteger)(int)Multiply_B;
		bigInteger /= (BigInteger)100;
		bigInteger *= (BigInteger)(int)(Now_Data.me.Unit_DMG_PER[num] + 100f);
		bigInteger /= (BigInteger)100;
		bigInteger *= (BigInteger)(int)(Now_Data.me.DMG_PLUS_PER_ALL + Now_Data.me.DMG_PER_ARTIFACT_FINAL + 100f);
		return bigInteger / 100;
	}

	public BigInteger Price_by_LV(int ID, int LV)
	{
		int num = ID;
		int num2 = LV;
		if (LV > LV_Point[8])
		{
			ID += 27;
			LV -= LV_Point[8];
		}
		BigInteger bigInteger = new BigInteger("500");
		for (int i = 0; i < ID; i++)
		{
			bigInteger *= (BigInteger)((i + 1) * 3 * (10 + (i + 1)) / 5 + 4);
		}
		bigInteger *= new BigInteger(me.JEGOP_table[LV]) * (10 + LV) / 10;
		bigInteger /= (BigInteger)100;
		for (int j = 0; j < LV_Point.Length; j++)
		{
			if ((LV + 1).Equals(LV_Point[j]))
			{
				bigInteger *= (BigInteger)5;
				break;
			}
		}
		switch (unit_DB[ID].unit_TYPE)
		{
		case Unit_Type.Human:
			bigInteger *= (BigInteger)(int)(100f - (Now_Data.me.HUMAN_UPGRADE_DISCOUNT + Now_Data.me.Price_Discount_Unit));
			break;
		case Unit_Type.Mechanic:
			bigInteger *= (BigInteger)(int)(100f - (Now_Data.me.Mechanin_UPGRADE_DISCOUNT + Now_Data.me.Price_Discount_Unit));
			break;
		case Unit_Type.Air:
			bigInteger *= (BigInteger)(int)(100f - (Now_Data.me.Air_UPGRADE_DISCOUNT + Now_Data.me.Price_Discount_Unit));
			break;
		}
		return bigInteger / 100;
	}

	public void NUMBER_Setting()
	{
		Fibo_count();
		Smooth_One_count();
		Smooth_Double_count();
		Smooth_Triple_count();
		Smooth_Fourth_count();
		Double_count();
		TechUpgradeLV_count();
		JEGOP_count();
	}

	public void Fibo_count()
	{
		fibo_table[0] = "1";
		fibo_table[1] = "2";
		for (int i = 2; i < fibo_table.Length; i++)
		{
			fibo_table[i] = (new BigInteger(fibo_table[i - 2]) + new BigInteger(fibo_table[i - 1])).ToString();
		}
		Debug.Log("피보나치 - LV_50 : " + fibo_table[50] + "피보나치 - LV_100 : " + fibo_table[100] + "피보나치 - LV_999 : " + fibo_table[999]);
	}

	public void Smooth_One_count()
	{
		smooth_ONE_table[0] = "1";
		float num = 0f;
		for (int i = 1; i < smooth_ONE_table.Length; i++)
		{
			num += 1f;
			smooth_ONE_table[i] = (new BigInteger(smooth_ONE_table[i - 1]) + new BigInteger(num.ToString())).ToString();
		}
		Debug.Log("데이터1 - LV_50 : " + smooth_ONE_table[50] + " / LV_100 : " + smooth_ONE_table[100] + " / LV_999 : " + smooth_ONE_table[999]);
	}

	public void Smooth_Double_count()
	{
		smooth_Double_table[0] = "1";
		float num = 0f;
		for (int i = 1; i < smooth_Double_table.Length; i++)
		{
			num += 1f;
			smooth_Double_table[i] = (new BigInteger(smooth_Double_table[i - 1]) + new BigInteger(num.ToString()) * new BigInteger(num.ToString())).ToString();
		}
		Debug.Log("데이터2제곱 - LV_50 : " + smooth_Double_table[50] + " / LV_100 : " + smooth_Double_table[100] + " / LV_999 : " + smooth_Double_table[999]);
	}

	public void Smooth_Triple_count()
	{
		smooth_Triple_table[0] = "1";
		float num = 0f;
		for (int i = 1; i < smooth_Triple_table.Length; i++)
		{
			num += 1f;
			smooth_Triple_table[i] = (new BigInteger(smooth_Triple_table[i - 1]) + new BigInteger(num.ToString()) * new BigInteger(num.ToString()) * new BigInteger(num.ToString())).ToString();
		}
		Debug.Log("데이터3제곱 - LV_50 : " + smooth_Triple_table[50] + " / LV_100 : " + smooth_Triple_table[100] + " / LV_999 : " + smooth_Triple_table[999]);
	}

	public void Smooth_Fourth_count()
	{
		smooth_Fourth_table[0] = "1";
		float num = 0f;
		for (int i = 1; i < smooth_Fourth_table.Length; i++)
		{
			num += 1f;
			smooth_Fourth_table[i] = (new BigInteger(smooth_Fourth_table[i - 1]) + new BigInteger(num.ToString()) * new BigInteger(num.ToString()) * new BigInteger(num.ToString()) * new BigInteger(num.ToString())).ToString();
		}
		Debug.Log("데이터4제곱 - LV_50 : " + smooth_Fourth_table[50] + " / LV_100 : " + smooth_Fourth_table[100] + " / LV_999 : " + smooth_Fourth_table[999]);
	}

	public void Double_count()
	{
		Double_table[0] = "1";
		BigInteger bigInteger = 1;
		for (int i = 1; i < Double_table.Length; i++)
		{
			bigInteger *= (BigInteger)2;
			Double_table[i] = bigInteger.ToString();
		}
	}

	public void JEGOP_count()
	{
		JEGOP_table[0] = "100";
		BigInteger bigInteger = 100;
		for (int i = 1; i < JEGOP_table.Length; i++)
		{
			bigInteger *= (BigInteger)105;
			bigInteger /= (BigInteger)100;
			JEGOP_table[i] = bigInteger.ToString();
		}
		JEGOP_TEN_table[0] = "100";
		BigInteger bigInteger2 = 100;
		for (int j = 1; j < JEGOP_TEN_table.Length; j++)
		{
			bigInteger2 *= (BigInteger)110;
			bigInteger2 /= (BigInteger)100;
			JEGOP_TEN_table[j] = bigInteger2.ToString();
		}
		JEGOP_SASIP_PER_table[0] = "100";
		BigInteger bigInteger3 = 100;
		for (int k = 1; k < JEGOP_SASIP_PER_table.Length; k++)
		{
			bigInteger3 *= (BigInteger)139;
			bigInteger3 /= (BigInteger)100;
			JEGOP_SASIP_PER_table[k] = bigInteger3.ToString();
		}
	}

	public void TechUpgradeLV_count()
	{
		TechUpgradeLV[0] = 0;
		TechUpgradeLV[1] = 20;
		BigInteger bigInteger = 1;
		for (int i = 1; i < TechUpgradeLV.Length; i++)
		{
			TechUpgradeLV[i] = TechUpgradeLV[i - 1] + 20 * i;
		}
	}
}
