using Keiwando.BigInteger;
using SimpleJSON;
using UnityEngine;

public class Monster_DB : MonoBehaviour
{
	public static Monster_DB me;

	public TextAsset DB_file;

	private string DB_file_string;

	public TextAsset Monster_DB_file;

	private string Monster_DB_file_string;

	public Stage_Data[] stage_DB;

	public Enemy_Group_Data[] Monster_Groups;

	public Monster_Data[] monster_DB;

	public int[] monster_kill_count;

	private string Imsi_load;

	private string[] Imsi_load_part;

	private BigInteger Answer;

	private BigInteger Bonus;

	private BigInteger Basic_HP = 2;

	private int Graph_Change_Stage = 70;

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
		stage_DB = new Stage_Data[jSONNode.Count];
		for (int i = 0; i < jSONNode.Count; i++)
		{
			stage_DB[i] = new Stage_Data();
			stage_DB[i].Theme = jSONNode[i]["theme"].AsInt;
			stage_DB[i].Enemy_Delay = jSONNode[i]["enemyrate"].AsFloat;
			stage_DB[i].Basic_HP = jSONNode[i]["basichp"];
			stage_DB[i].HP_per_LV = jSONNode[i]["hpperlvimsi"];
			stage_DB[i].Drop_Gold = jSONNode[i]["dropgold"];
		}
		Debug.Log("스테이지DB 적용.");
		Monster_DB_file_string = Monster_DB_file.text;
		JSONNode jSONNode2 = JSON.Parse(Monster_DB_file_string);
		monster_DB = new Monster_Data[jSONNode2.Count];
		monster_kill_count = new int[jSONNode2.Count];
		for (int j = 0; j < jSONNode2.Count; j++)
		{
			monster_DB[j] = new Monster_Data();
			monster_DB[j].Name = jSONNode2[j]["name"];
			monster_DB[j].Basic_HP = jSONNode2[j]["hp"];
			monster_DB[j].ATK_Speed = jSONNode2[j]["movespeed"];
			monster_DB[j].Move_Speed = jSONNode2[j]["movespeed"].AsFloat;
			monster_DB[j].Drop_Gold = jSONNode2[j]["gold"];
			monster_DB[j].Group_Avarage = jSONNode2[j]["groupaverage"].AsInt;
			Debug.Log(jSONNode2[j]["arangetype"].AsInt);
			if (jSONNode2[j]["arangetype"].AsInt == 1)
			{
				monster_DB[j].Random_Position = true;
			}
			else
			{
				monster_DB[j].Random_Position = false;
			}
			Debug.Log(monster_DB[j].Random_Position);
		}
		Debug.Log("몬스터DB적용.");
	}

	public BigInteger Monster_HP_by_LV(int ID, int LV, bool Boss, bool Portal)
	{
		LV -= 10;
		int num = LV / 10;
		int num2 = LV / 10 % 5;
		if (num < Graph_Change_Stage)
		{
			Answer = new BigInteger(Unit_DB.me.JEGOP_SASIP_PER_table[num]) / 100 * Basic_HP;
		}
		else
		{
			Answer = new BigInteger(Unit_DB.me.JEGOP_SASIP_PER_table[Graph_Change_Stage]) * new BigInteger(Unit_DB.me.JEGOP_TEN_table[num - Graph_Change_Stage]) / 10000 * Basic_HP;
		}
		Answer += Basic_HP * num;
		Answer = Answer * new BigInteger(monster_DB[ID].Basic_HP) / 100;
		if (Boss)
		{
			Answer += Monster_HP_by_LV(ID, LV + 10, false, false);
			Answer /= (BigInteger)2;
			Answer = Answer * (100 + num2 * 20) / 100;
		}
		return Answer;
	}

	public BigInteger Monster_Gold_by_LV(int ID, int LV, bool Boss)
	{
		Answer = Monster_HP_by_LV(ID, LV, Boss, false);
		Answer *= (BigInteger)60;
		Answer /= (BigInteger)100;
		return Answer + 5;
	}
}
