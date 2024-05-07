using Keiwando.BigInteger;
using SimpleJSON;
using UnityEngine;

public class Artifact_DB : MonoBehaviour
{
	public static Artifact_DB me;

	public Misile_DB misile_db;

	public TextAsset DB_file;

	private string DB_file_string;

	public Artifact_Data[] artifact_DB;

	public TextAsset diablo_artifact_DB_file;

	private string diablo_artifact_DB_file_string;

	public Artifact_Data[] diablo_artifact_DB;

	public static int[] GET_ARTIFACT_PRICE = new int[48]
	{
		1, 3, 7, 14, 25, 41, 63, 92, 129, 175,
		231, 298, 377, 469, 575, 696, 849, 1036, 1259, 1520,
		1821, 2164, 2551, 2984, 3465, 3996, 4579, 5216, 5909, 6660,
		7501, 8435, 9465, 10594, 11825, 13161, 14605, 16160, 17829, 19615,
		21521, 23591, 25829, 28239, 30825, 33591, 36541, 40000
	};

	private string Imsi_load;

	private string[] Imsi_load_part;

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
		artifact_DB = new Artifact_Data[jSONNode.Count];
		for (int i = 0; i < jSONNode.Count; i++)
		{
			artifact_DB[i] = new Artifact_Data();
			artifact_DB[i].ID = 0;
			artifact_DB[i].LVUP_Price = jSONNode[i]["upgradeprice"];
			artifact_DB[i].LVUP_Price_Plus = jSONNode[i]["upgradepriceplus"];
			artifact_DB[i].BUFF_A_Type = misile_db.OPTION_TYPE_to_ID(jSONNode[i]["buffatype"]);
			artifact_DB[i].BUFF_A_Value = jSONNode[i]["buffavalue"].AsFloat;
			artifact_DB[i].BUFF_A_Value_PLUS = jSONNode[i]["buffavalueplus"].AsFloat;
			artifact_DB[i].BUFF_B_Type = misile_db.OPTION_TYPE_to_ID(jSONNode[i]["buffbtype"]);
			artifact_DB[i].BUFF_B_Value = jSONNode[i]["buffbvalue"].AsFloat;
			artifact_DB[i].BUFF_B_Value_PLUS = jSONNode[i]["buffbvalueplus"].AsFloat;
			artifact_DB[i].MAX_LV = jSONNode[i]["maxlv"].AsInt;
		}
		Debug.Log("유물 DB적용.");
		diablo_artifact_DB_file_string = diablo_artifact_DB_file.text;
		JSONNode jSONNode2 = JSON.Parse(diablo_artifact_DB_file_string);
		diablo_artifact_DB = new Artifact_Data[jSONNode2.Count];
		for (int j = 0; j < jSONNode2.Count; j++)
		{
			diablo_artifact_DB[j] = new Artifact_Data();
			diablo_artifact_DB[j].ID = 0;
			diablo_artifact_DB[j].LVUP_Price = jSONNode2[j]["upgradeprice"];
			diablo_artifact_DB[j].LVUP_Price_Plus = jSONNode2[j]["upgradepriceplus"];
			diablo_artifact_DB[j].BUFF_A_Type = misile_db.OPTION_TYPE_to_ID(jSONNode2[j]["buffatype"]);
			diablo_artifact_DB[j].BUFF_A_Value = jSONNode2[j]["buffavalue"].AsFloat;
			diablo_artifact_DB[j].BUFF_A_Value_PLUS = jSONNode2[j]["buffavalueplus"].AsFloat;
			diablo_artifact_DB[j].BUFF_B_Type = misile_db.OPTION_TYPE_to_ID(jSONNode2[j]["buffbtype"]);
			diablo_artifact_DB[j].BUFF_B_Value = jSONNode2[j]["buffbvalue"].AsFloat;
			diablo_artifact_DB[j].BUFF_B_Value_PLUS = jSONNode2[j]["buffbvalueplus"].AsFloat;
			diablo_artifact_DB[j].MAX_LV = jSONNode2[j]["maxlv"].AsInt;
		}
		Debug.Log("디아블로 유물 DB적용.");
	}

	public BigInteger Price_by_LV(int ID, int LV)
	{
		BigInteger bigInteger = 0;
		bigInteger = ((LV >= GET_ARTIFACT_PRICE.Length) ? ((BigInteger)(GET_ARTIFACT_PRICE[47] + (LV - 47) * 1000)) : ((BigInteger)GET_ARTIFACT_PRICE[LV]));
		return bigInteger + ID % 3;
	}
}
