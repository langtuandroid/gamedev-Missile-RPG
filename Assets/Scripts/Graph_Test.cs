using System.Collections.Generic;
using UnityEngine;

public class Graph_Test : MonoBehaviour
{
	public Monster_DB monster_DB;

	public GameObject Cube;

	public GameObject target_OBJ;

	public List<GameObject> List_Obj;

	public int Target_Count = 1000;

	public int Skip_count = 5;

	public void Setting()
	{
		Delete();
		for (int i = 10; i < Target_Count; i++)
		{
			float num = float.Parse((monster_DB.Monster_HP_by_LV(2, i * Skip_count, false, false) / 1000).ToString());
			target_OBJ = Object.Instantiate(Cube, new Vector3(i * 50, num * 0.001f, 0f), Quaternion.identity) as GameObject;
			target_OBJ.name = string.Format("{0}", i * Skip_count);
			List_Obj.Add(target_OBJ);
		}
	}

	public void Delete()
	{
		for (int i = 0; i < List_Obj.Count; i++)
		{
			Object.Destroy(List_Obj[i].gameObject);
		}
		List_Obj.Clear();
	}
}
