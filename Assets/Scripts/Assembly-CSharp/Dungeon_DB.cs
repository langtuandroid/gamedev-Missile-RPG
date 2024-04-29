using SimpleJSON;
using UnityEngine;

public class Dungeon_DB : MonoBehaviour
{
	public static Dungeon_DB me;

	public TextAsset DB_file;

	private string DB_file_string;

	public Dungeon_Data[] dungeon_DB;

	public TextAsset DB_file_for_REWARD;

	private string DB_file_for_REWARD_string;

	public Dungeon_REWARD_Data[] dungeon_DB_for_REWARD;

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
		dungeon_DB = new Dungeon_Data[jSONNode.Count];
		for (int i = 0; i < jSONNode.Count; i++)
		{
			dungeon_DB[i] = new Dungeon_Data();
			dungeon_DB[i].Building_TYPE = jSONNode[i]["buildingtype"].AsInt;
			dungeon_DB[i].Dungeon_TYPE = jSONNode[i]["dungeontype"].AsInt;
			dungeon_DB[i].StageNumber_for_HP = jSONNode[i]["stagenumberforhp"].AsInt;
			dungeon_DB[i].REWARD_A_TYPE = jSONNode[i]["rewardatype"];
			dungeon_DB[i].REWARD_A_Value = jSONNode[i]["rewardavalue"];
			dungeon_DB[i].REWARD_A_Value_Plus = jSONNode[i]["rewardavalueplus"];
			dungeon_DB[i].REWARD_B_TYPE = jSONNode[i]["rewardbtype"];
			dungeon_DB[i].REWARD_B_Value = jSONNode[i]["rewardbvalue"];
			dungeon_DB[i].REWARD_B_Value_Plus = jSONNode[i]["rewardbvalueplus"];
		}
		Debug.Log("던전 DB 적용.");
		DB_file_for_REWARD_string = DB_file_for_REWARD.text;
		JSONNode jSONNode2 = JSON.Parse(DB_file_for_REWARD_string);
		dungeon_DB_for_REWARD = new Dungeon_REWARD_Data[jSONNode2.Count];
		for (int j = 0; j < jSONNode2.Count; j++)
		{
			dungeon_DB_for_REWARD[j] = new Dungeon_REWARD_Data();
			dungeon_DB_for_REWARD[j].REWARD_A_ID = jSONNode2[j]["rewardid0"].AsInt;
			dungeon_DB_for_REWARD[j].REWARD_A_Value = jSONNode2[j]["rewardvalue0"].AsInt;
			dungeon_DB_for_REWARD[j].REWARD_B_ID = jSONNode2[j]["rewardid1"].AsInt;
			dungeon_DB_for_REWARD[j].REWARD_B_Value = jSONNode2[j]["rewardvalue1"].AsInt;
			dungeon_DB_for_REWARD[j].REWARD_C_ID = jSONNode2[j]["rewardid2"].AsInt;
			dungeon_DB_for_REWARD[j].REWARD_C_Value = jSONNode2[j]["rewardvalue2"].AsInt;
			dungeon_DB_for_REWARD[j].REWARD_D_ID = jSONNode2[j]["rewardid3"].AsInt;
			dungeon_DB_for_REWARD[j].REWARD_D_Value = jSONNode2[j]["rewardvalue3"].AsInt;
			dungeon_DB_for_REWARD[j].REWARD_E_ID = jSONNode2[j]["rewardid4"].AsInt;
			dungeon_DB_for_REWARD[j].REWARD_E_Value = jSONNode2[j]["rewardvalue4"].AsInt;
		}
		Debug.Log("던전 DB 적용.");
	}
}
