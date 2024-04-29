using UnityEngine;

[ExecuteInEditMode]
public class DB_MANAGER : MonoBehaviour
{
	public Monster_DB monster_DB;

	public Misile_DB misile_DB;

	public Arch_DB arch_DB;

	public Unit_DB unit_DB;

	public Artifact_DB artifact_DB;

	public Dungeon_DB dungeon_DB;

	public Box_DB box_DB;

	public static DB_MANAGER me;

	public void Awake()
	{
		me = this;
	}

	public void All_DB_UPDATE()
	{
		monster_DB.Setting();
		misile_DB.Setting();
		arch_DB.Setting();
		unit_DB.Setting();
		artifact_DB.Setting();
		dungeon_DB.Setting();
		box_DB.Setting();
	}
}
