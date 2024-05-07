using Keiwando.BigInteger;
using SimpleJSON;
using UnityEngine;

public class Misile_DB : MonoBehaviour
{
	public static Misile_DB me;

	public TextAsset DB_file;

	private string DB_file_string;

	public Misile_Data[] misile_DB;

	public TextAsset RARE_DB_file;

	private string RARE_DB_file_string;

	public Rare_Data[] RARE_misile_DB;

	public TextAsset Bazuka_Parts_DB_file;

	private string Bazuka_Parts_DB_file_string;

	public Misile_Data[] Bazuka_Parts_DB;

	public TextAsset Skill_DB_file;

	private string Skill_DB_file_string;

	public TextAsset Status_DB_file;

	private string Status_DB_file_string;

	public Skill_Data_Table[] skill_DB;

	public Skill_Data_BASIC[] skill_DB_Basic;

	public Status_Data[] status_data;

	public static int[] M_Value = new int[21]
	{
		0, 100, 150, 250, 400, 700, 1200, 2200, 3900, 7000,
		13000, 23200, 42800, 80000, 150000, 280000, 550000, 1000000, 2000000, 4000000,
		7000000
	};

	private string Imsi_load;

	private string[] Imsi_load_part;

	private string Missile_DMG_Plus = "1";

	private BigInteger Imsi;

	private BigInteger Multiply;

	private string Missile_Price_Basic = "45";

	public void Awake()
	{
		if (me == null)
		{
			me = this;
		}
	}

	public void Setting()
	{
		Status_DB_file_string = Status_DB_file.text;
		JSONNode jSONNode = JSON.Parse(Status_DB_file_string);
		status_data = new Status_Data[jSONNode.Count];
		for (int i = 0; i < jSONNode.Count; i++)
		{
			status_data[i] = new Status_Data();
			status_data[i].ID = jSONNode[i]["id"].AsInt;
			status_data[i].Name = jSONNode[i]["name"];
		}
		Debug.Log("스탯 DB적용.");
		DB_file_string = DB_file.text;
		JSONNode jSONNode2 = JSON.Parse(DB_file_string);
		misile_DB = new Misile_Data[jSONNode2.Count];
		for (int j = 0; j < jSONNode2.Count; j++)
		{
			misile_DB[j] = new Misile_Data();
			switch ((string)jSONNode2[j]["type"])
			{
			case "Direct":
				misile_DB[j].Type = 0;
				break;
			case "Curve":
				misile_DB[j].Type = 1;
				break;
			}
			misile_DB[j].Missile_Speed = jSONNode2[j]["speed"].AsFloat;
			misile_DB[j].Rare = jSONNode2[j]["tier"].AsInt;
			misile_DB[j].EQUIP_Option_A = OPTION_TYPE_to_ID(jSONNode2[j]["setoptiona"]);
			misile_DB[j].EQUIP_Option_A_Value = jSONNode2[j]["setavalue"].AsFloat;
			misile_DB[j].EQUIP_Option_A_Plus = jSONNode2[j]["setaplus"].AsFloat;
			misile_DB[j].EQUIP_Option_B = OPTION_TYPE_to_ID(jSONNode2[j]["setoptionb"]);
			misile_DB[j].EQUIP_Option_B_Value = jSONNode2[j]["setbvalue"].AsFloat;
			misile_DB[j].EQUIP_Option_B_Plus = jSONNode2[j]["setbplus"].AsFloat;
			misile_DB[j].EQUIP_Option_C = OPTION_TYPE_to_ID(jSONNode2[j]["setoptionc"]);
			misile_DB[j].EQUIP_Option_C_Value = jSONNode2[j]["setcvalue"].AsFloat;
			misile_DB[j].EQUIP_Option_C_Plus = jSONNode2[j]["setcplus"].AsFloat;
			misile_DB[j].BOX_Option_A = OPTION_TYPE_to_ID(jSONNode2[j]["boxoptiona"]);
			misile_DB[j].BOX_Option_A_Value = jSONNode2[j]["boxavalue"].AsFloat;
			misile_DB[j].BOX_Option_A_Plus = jSONNode2[j]["boxaplus"].AsFloat;
			misile_DB[j].BOX_Option_B = OPTION_TYPE_to_ID(jSONNode2[j]["boxoptionb"]);
			misile_DB[j].BOX_Option_B_Value = jSONNode2[j]["boxbvalue"].AsFloat;
			misile_DB[j].BOX_Option_B_Plus = jSONNode2[j]["boxbplus"].AsFloat;
			misile_DB[j].BOX_Option_C = OPTION_TYPE_to_ID(jSONNode2[j]["boxoptionc"]);
			misile_DB[j].BOX_Option_C_Value = jSONNode2[j]["boxcvalue"].AsFloat;
			misile_DB[j].BOX_Option_C_Plus = jSONNode2[j]["boxcplus"].AsFloat;
		}
		Debug.Log("미사일DB적용.");
		RARE_DB_file_string = RARE_DB_file.text;
		JSONNode jSONNode3 = JSON.Parse(RARE_DB_file_string);
		RARE_misile_DB = new Rare_Data[7];
		for (int k = 0; k < 7; k++)
		{
			RARE_misile_DB[k] = new Rare_Data();
			RARE_misile_DB[k].Need_Parts = new int[20];
		}
		for (int l = 0; l < jSONNode3.Count; l++)
		{
			for (int m = 1; m < 7; m++)
			{
				RARE_misile_DB[m].Need_Parts[l] = jSONNode3[l][string.Format("rare{0}", m)].AsInt;
			}
		}
		Debug.Log("미사일레어 DB적용.");
		Bazuka_Parts_DB_file_string = Bazuka_Parts_DB_file.text;
		JSONNode jSONNode4 = JSON.Parse(Bazuka_Parts_DB_file_string);
		Bazuka_Parts_DB = new Misile_Data[jSONNode4.Count];
		for (int n = 0; n < jSONNode4.Count; n++)
		{
			Bazuka_Parts_DB[n] = new Misile_Data();
			Bazuka_Parts_DB[n].EQUIP_Option_A = OPTION_TYPE_to_ID(jSONNode4[n]["buffatype"]);
			Bazuka_Parts_DB[n].EQUIP_Option_A_Value = jSONNode4[n]["buffavalue"].AsFloat;
			Bazuka_Parts_DB[n].EQUIP_Option_A_Plus = jSONNode4[n]["buffavalue"].AsFloat;
		}
		Debug.Log("바주카 DB적용.");
		Skill_DB_file_string = Skill_DB_file.text;
		JSONNode jSONNode5 = JSON.Parse(Skill_DB_file_string);
		skill_DB_Basic = new Skill_Data_BASIC[jSONNode5.Count];
		for (int num = 0; num < jSONNode5.Count; num++)
		{
			skill_DB_Basic[num] = new Skill_Data_BASIC();
			skill_DB_Basic[num].COOL = jSONNode5[num]["cool"].AsFloat;
			skill_DB_Basic[num].Remain_Time = jSONNode5[num]["time"].AsFloat;
			skill_DB_Basic[num].Value = jSONNode5[num]["value"].AsInt;
			skill_DB_Basic[num].Value_Plus = jSONNode5[num]["valueplus"].AsInt;
			skill_DB_Basic[num].Price = jSONNode5[num]["price"];
			skill_DB_Basic[num].Price_Plus = jSONNode5[num]["priceplus"];
			skill_DB_Basic[num].MAX_LV = jSONNode5[num]["maxlv"].AsInt;
		}
		skill_DB = new Skill_Data_Table[jSONNode5.Count];
		for (int num2 = 0; num2 < jSONNode5.Count; num2++)
		{
			skill_DB[num2] = new Skill_Data_Table();
			skill_DB[num2].Skill_LV = new Skill_Data[skill_DB_Basic[num2].MAX_LV];
			skill_DB[num2].Skill_LV[0] = new Skill_Data();
			skill_DB[num2].Skill_LV[0].Value = skill_DB_Basic[num2].Value;
			skill_DB[num2].Skill_LV[0].Price = skill_DB_Basic[num2].Price;
			for (int num3 = 1; num3 < skill_DB_Basic[num2].MAX_LV; num3++)
			{
				skill_DB[num2].Skill_LV[num3] = new Skill_Data();
				skill_DB[num2].Skill_LV[num3].Value = skill_DB_Basic[num2].Value + skill_DB_Basic[num2].Value_Plus * num3;
				skill_DB[num2].Skill_LV[num3].Price = (new BigInteger(skill_DB[num2].Skill_LV[num3 - 1].Price) * 800).ToString();
			}
		}
		Debug.Log("스킬 DB적용.");
	}

	public BigInteger DMG_by_LV(int ID, int LV)
	{
		BigInteger result = LV;
		int num = LV / 1000;
		if ((LV % 1000).Equals(0))
		{
			int num2 = 1;
		}
		result *= 900 + new BigInteger(Unit_DB.me.JEGOP_table[LV]);
		result /= (BigInteger)1000;
		int num3 = Now_Data.me.LV_Misile / 100;
		for (int i = 0; i < num3; i++)
		{
			result *= (BigInteger)2;
		}
		Multiply = 100;
		Multiply += (BigInteger)(int)Now_Data.me.MISSILE_DMG_PER;
		Multiply += (BigInteger)(int)(Now_Data.me.ATTACK_PER_Star_Count * (float)Now_Data.me.Archive_COUNT);
		Multiply += (BigInteger)(int)(Now_Data.me.ATTACK_PER_Artifact_Count * Now_Data.me.ATTACK_PER_Artifact_Count);
		Multiply += (BigInteger)(int)(Now_Data.me.ATTACK_PER_Missile_Count * (float)Now_Data.me.Missile_COUNT);
		Multiply += (BigInteger)((int)Now_Data.me.Bazuka_Compo_Bonus * Now_Data.me.Bazuka_Compo_count);
		result *= Multiply;
		result *= (BigInteger)(int)(Now_Data.me.MISSILE_DMG_PER_FINAL + 100f);
		result *= (BigInteger)(int)(Now_Data.me.DMG_PLUS_PER_ALL_TARGET + 100f);
		result *= (BigInteger)(int)(Now_Data.me.DMG_PLUS_PER_ALL + Now_Data.me.DMG_PER_ARTIFACT_FINAL + 100f);
		result /= (BigInteger)100000000;
		if (Now_Data.me.MISSILE_DMG_PER_BY_UNIT > 0f)
		{
			Imsi = 0;
			for (int j = 0; j < Fight_Master.me.Units.Length; j++)
			{
				if (Fight_Master.me.Units[j].gameObject.activeSelf)
				{
					if (Unit_DB.me.unit_DB[j].ATK_SPEED >= 1f)
					{
						Imsi += Unit_DB.me.DMG_by_LV(j, Now_Data.me.Now_Unit_LV[j]) / new BigInteger(((int)Unit_DB.me.unit_DB[j].ATK_SPEED).ToString());
					}
					else
					{
						Imsi += Unit_DB.me.DMG_by_LV(j, Now_Data.me.Now_Unit_LV[j]) * new BigInteger(((int)(1f / Unit_DB.me.unit_DB[j].ATK_SPEED)).ToString());
					}
				}
			}
			Imsi *= (BigInteger)(int)Now_Data.me.MISSILE_DMG_PER_BY_UNIT;
			Imsi /= (BigInteger)100;
			result += Imsi;
		}
		return result;
	}

	public BigInteger Price_by_LV(int ID, int LV)
	{
		BigInteger bigInteger = new BigInteger(Missile_Price_Basic);
		int num = LV / 1000;
		int num2 = LV % 1000;
		bigInteger *= new BigInteger(Unit_DB.me.JEGOP_TEN_table[LV]) * (100 + LV) / 100;
		bigInteger /= (BigInteger)100;
		if ((Now_Data.me.LV_Misile + 1) % 100 == 0)
		{
			bigInteger *= (BigInteger)5;
		}
		else if ((Now_Data.me.LV_Misile + 1) % 20 == 0)
		{
			bigInteger *= (BigInteger)3;
		}
		bigInteger *= (BigInteger)(int)(100f - Now_Data.me.Price_Discount_Misile);
		return bigInteger / 100;
	}

	public BigInteger TIER_Price_by_LV(int ID, int Tier_LV)
	{
		BigInteger result = new BigInteger(ID / 10 * 100);
		int num = Tier_LV / 1000;
		int num2 = Tier_LV % 1000;
		result += new BigInteger(ID / 10 * 100) * new BigInteger(Unit_DB.me.smooth_ONE_table[999]) * num + new BigInteger(ID / 10 * 100) * new BigInteger(Unit_DB.me.smooth_ONE_table[num2]);
		int num3 = Tier_LV / 3;
		for (int i = 0; i < num3; i++)
		{
			result *= (BigInteger)2;
		}
		return result;
	}

	public int OPTION_TYPE_to_ID(string value)
	{
		Debug.Log(value);
		if (string.IsNullOrEmpty(value))
		{
			return 999;
		}
		for (int i = 0; i < status_data.Length; i++)
		{
			if (value.Equals(status_data[i].Name))
			{
				return status_data[i].ID;
			}
		}
		return 999;
	}
}
