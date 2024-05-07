using SimpleJSON;
using UnityEngine;

public class Box_DB : MonoBehaviour
{
	public static Box_DB me;

	public TextAsset DB_file;

	private string DB_file_string;

	public BoxData[] box_DB;

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
		box_DB = new BoxData[jSONNode.Count];
		for (int i = 0; i < jSONNode.Count; i++)
		{
			box_DB[i] = new BoxData();
			box_DB[i].Name = jSONNode[i]["name"];
			box_DB[i].Price = jSONNode[i]["price"].AsInt;
			box_DB[i].Stone_Min = jSONNode[i]["stonemin"].AsInt;
			box_DB[i].Stone_Max = jSONNode[i]["stonemax"].AsInt;
			box_DB[i].Rewadrd_A_ID_from = jSONNode[i]["afrom"].AsInt;
			box_DB[i].Rewadrd_A_ID_to = jSONNode[i]["ato"].AsInt;
			box_DB[i].Rewadrd_A_ID_value = jSONNode[i]["avalue"].AsInt;
			box_DB[i].Rewadrd_B_ID_from = jSONNode[i]["bfrom"].AsInt;
			box_DB[i].Rewadrd_B_ID_to = jSONNode[i]["bto"].AsInt;
			box_DB[i].Rewadrd_B_ID_value = jSONNode[i]["bvalue"].AsInt;
			box_DB[i].Rewadrd_C_ID_from = jSONNode[i]["cfrom"].AsInt;
			box_DB[i].Rewadrd_C_ID_to = jSONNode[i]["cto"].AsInt;
			box_DB[i].Rewadrd_C_ID_value = jSONNode[i]["cvalue"].AsInt;
			box_DB[i].Rewadrd_D_ID_from = jSONNode[i]["dfrom"].AsInt;
			box_DB[i].Rewadrd_D_ID_to = jSONNode[i]["dto"].AsInt;
			box_DB[i].Rewadrd_D_ID_value = jSONNode[i]["dvalue"].AsInt;
		}
		Debug.Log("박스DB적용.");
	}
}
