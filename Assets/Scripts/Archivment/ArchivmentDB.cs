using SimpleJSON;
using UnityEngine;

public class ArchivmentDB : MonoBehaviour
{
	public static ArchivmentDB me;

	public TextAsset DB_file;

	private string DB_file_string;

	public ArchivmentDatass[] arch_DB;

	public TextAsset Sub_DB_file;

	private string Sub_DB_file_string;

	public ArchivmentData[] SUB_arch_DB;

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
		int num = 1;
		int num2 = 0;
		arch_DB = new ArchivmentDatass[17];
		for (int i = 0; i < jSONNode.Count; i++)
		{
			if (jSONNode[i]["archtype"].AsInt.Equals(num2))
			{
				num++;
				if (i.Equals(jSONNode.Count - 1))
				{
					Debug.Log(num2 + "번 업적테이블 생성.");
					Debug.Log(num);
					if (num > 0)
					{
						arch_DB[num2] = new ArchivmentDatass();
						arch_DB[num2].Arch = new ArchivmentData[num];
						num = 0;
					}
					num2 = jSONNode[i]["archtype"].AsInt;
				}
			}
			else
			{
				Debug.Log(num2 + "번 업적테이블 생성.");
				Debug.Log(num);
				if (num > 0)
				{
					arch_DB[num2] = new ArchivmentDatass();
					arch_DB[num2].Arch = new ArchivmentData[num];
					num = 1;
				}
				num2 = jSONNode[i]["archtype"].AsInt;
			}
		}
		for (int j = 0; j < jSONNode.Count; j++)
		{
			arch_DB[jSONNode[j]["archtype"].AsInt].Arch[jSONNode[j]["lv"].AsInt] = new ArchivmentData();
			arch_DB[jSONNode[j]["archtype"].AsInt].Arch[jSONNode[j]["lv"].AsInt].GOAL_TYPE = Get_Goal_Type(jSONNode[j]["goaltype"]);
			arch_DB[jSONNode[j]["archtype"].AsInt].Arch[jSONNode[j]["lv"].AsInt].TARGET_VALUE = jSONNode[j]["targetnumber"];
			arch_DB[jSONNode[j]["archtype"].AsInt].Arch[jSONNode[j]["lv"].AsInt].REWARD_TYPE = jSONNode[j]["rewardtype"];
			arch_DB[jSONNode[j]["archtype"].AsInt].Arch[jSONNode[j]["lv"].AsInt].REWARD_VALUE = jSONNode[j]["rewardvalue"];
		}
		Debug.Log("업적DB적용.");
		Sub_DB_file_string = Sub_DB_file.text;
		JSONNode jSONNode2 = JSON.Parse(Sub_DB_file_string);
		SUB_arch_DB = new ArchivmentData[jSONNode2.Count];
		for (int k = 0; k < jSONNode2.Count; k++)
		{
			SUB_arch_DB[k] = new ArchivmentData();
			SUB_arch_DB[k].GOAL_TYPE = Get_Goal_Type(jSONNode2[k]["goaltype"]);
			SUB_arch_DB[k].TARGET_VALUE = jSONNode2[k]["targetnumber"];
			SUB_arch_DB[k].REWARD_TYPE = jSONNode2[k]["rewardtype"];
			SUB_arch_DB[k].REWARD_VALUE = jSONNode2[k]["rewardvalue"];
		}
	}

	public Quest_Goal_Type Get_Goal_Type(string target)
	{
		switch (target)
		{
		default:
			return Quest_Goal_Type.NONE;
		case "NOW_TAP":
			return Quest_Goal_Type.NOW_TAP;
		case "NOW_KILL":
			return Quest_Goal_Type.NOW_KILL;
		case "NOW_BOSSKILL":
			return Quest_Goal_Type.NOW_BOSSKILL;
		case "NOW_PLAYTIME":
			return Quest_Goal_Type.NOW_PLAYTIME;
		case "NOW_SKILL_USE":
			return Quest_Goal_Type.NOW_SKILL_USE;
		case "NOW_FARIY":
			return Quest_Goal_Type.NOW_FARIY;
		case "NOW_DROPPORT":
			return Quest_Goal_Type.NOW_DROPPORT;
		case "ALL_TAP":
			return Quest_Goal_Type.ALL_TAP;
		case "ALL_MINERAL":
			return Quest_Goal_Type.ALL_MINERAL;
		case "ALL_GAS":
			return Quest_Goal_Type.ALL_GAS;
		case "ALL_P_STONE":
			return Quest_Goal_Type.ALL_P_STONE;
		case "ARTIFACT_COUNT":
			return Quest_Goal_Type.ARTIFACT_COUNT;
		case "BEST_LV":
			return Quest_Goal_Type.BEST_LV;
		case "ALL_PLAYTIME":
			return Quest_Goal_Type.ALL_PLAYTIME;
		case "ALL_MISSILE_COUNT":
			return Quest_Goal_Type.ALL_MISSILE_COUNT;
		case "ALL_KILL":
			return Quest_Goal_Type.ALL_KILL;
		case "ALL_BOSSKILL":
			return Quest_Goal_Type.ALL_BOSSKILL;
		case "ALL_DUNGEON_CLEAR":
			return Quest_Goal_Type.ALL_DUNGEON_CLEAR;
		case "ALL_SKILL_USE":
			return Quest_Goal_Type.ALL_SKILL_USE;
		case "ALL_FAIRY":
			return Quest_Goal_Type.ALL_FAIRY;
		case "ALL_DROPPORT":
			return Quest_Goal_Type.ALL_DROPPORT;
		case "ALL_PROMOTE":
			return Quest_Goal_Type.ALL_PROMOTE;
		case "ALL_HIDDEN":
			return Quest_Goal_Type.ALL_HIDDEN;
		}
	}
}
