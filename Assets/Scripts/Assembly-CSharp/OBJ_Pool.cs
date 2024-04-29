using UnityEngine;

public class OBJ_Pool : MonoBehaviour
{
	public static OBJ_Pool me;

	public DMG_panel[] DMG_panel;

	public static int DMG_panel_Number;

	public GameObject[] effect_Hitted;

	public static int effect_Hitted_Number;

	public GameObject[] effect_die;

	public static int effect_die_Number;

	public GameObject[] effect_boom;

	public static int effect_boom_Number;

	public static int Fireball_misile_Number;

	public Misile[] Fireball_misile;

	public static int Big_misile_Number;

	public Misile[] Big_misile_misile;

	public static int Small_misile_Number;

	public Misile[] Small_misile_misile;

	public static int Fire_misile_Number;

	public Misile[] Fire_misile_misile;

	public static int DeadSpill_Number;

	public GameObject[] DeadSpill;

	public static int BIG_DeadSpill_Number;

	public GameObject[] BIG_DeadSpill;

	public static int Player_DeadSpill_Number;

	public GameObject[] Player_DeadSpill;

	public static int DUNGEON_DeadSpill_Number;

	public GameObject[] DUNGEON_DeadSpill;

	public static int Beams_Number;

	public Misile[] Beams;

	public static int Boom_Number;

	public Misile[] Booms;

	public Enemy[] stop_enemy;

	public int stop_enemy_Number;

	public Enemy_group[] enemy_group;

	public Enemy[] enemy;

	public static int enemy_Number;

	public Enemy[] Dungeon_Boss_A;

	public static int Dungeon_Boss_A_Number;

	public Enemy[] Dungeon_Boss_B;

	public static int Dungeon_Boss_B_Number;

	public Enemy[] Dungeon_Boss_C;

	public static int Dungeon_Boss_C_Number;

	public Enemy Dungeon_Boss_D;

	public Enemy[] Dungeon_Boss_E;

	public static int Dungeon_Boss_E_Number;

	public static int Coin_Number;

	public Fight_Item[] Coin;

	public static int Items_Number;

	public Fight_Item[] Items;

	public static int Crystal_Number;

	public Fight_Item[] Crystal;

	public GameObject Get_Rare_EF;

	public static int Opening_C_Number;

	public GameObject[] Opening_C;

	private static bool BIG;

	public static int Make_Stop_Monster_try;

	public void Awake()
	{
		me = this;
	}

	public static void Make_OBJ(GameObject[] A, ref int B, Vector3 C)
	{
		if (A.Length > 0)
		{
			if (B >= A.Length)
			{
				B = 0;
			}
			A[B].SetActive(false);
			A[B].transform.position = C;
			A[B].SetActive(true);
			B++;
			if (B >= A.Length)
			{
				B = 0;
			}
		}
		else
		{
			Debug.Log("풀에 아무것도 없음.");
		}
	}

	public static void Make_ANY_Enemy(Vector3 C, int LV)
	{
		if (me.enemy[enemy_Number].PS.Equals(PlayerState.Pool) || me.enemy[enemy_Number].PS.Equals(PlayerState.Death))
		{
			Fight_Master.me.Target_Mop_count--;
			me.enemy[enemy_Number].PS = PlayerState.Idle;
			me.enemy[enemy_Number].gameObject.SetActive(false);
			me.enemy[enemy_Number].ThisTransform.position = new Vector3(C.x + Random.Range(-1f, 5f), C.y + Random.Range(-3f, 3f), C.z);
			me.enemy[enemy_Number].gameObject.SetActive(true);
			me.enemy[enemy_Number].Birth(LV);
			enemy_Number++;
			if (enemy_Number >= me.enemy.Length)
			{
				enemy_Number = 0;
			}
		}
		else
		{
			enemy_Number++;
			if (enemy_Number >= me.enemy.Length)
			{
				enemy_Number = 0;
			}
			else
			{
				Make_ANY_Enemy(C, LV);
			}
		}
	}

	public static void Make_Enemy(int ID, Vector3 C, bool Boss, int LV)
	{
		if (me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].PS.Equals(PlayerState.Pool) || me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].PS.Equals(PlayerState.Death))
		{
			Fight_Master.me.Target_Mop_count--;
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].PS = PlayerState.Idle;
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].CH_ID = ID;
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].BOSS = Boss;
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].gameObject.SetActive(false);
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].ThisTransform.position = new Vector3(C.x, C.y, C.z);
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].gameObject.SetActive(true);
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].Birth(LV);
			me.enemy_group[ID].enemy_Number++;
			if (me.enemy_group[ID].enemy_Number >= me.enemy_group[ID].enemy.Length)
			{
				me.enemy_group[ID].enemy_Number = 0;
			}
		}
		else
		{
			me.enemy_group[ID].enemy_Number++;
			if (me.enemy_group[ID].enemy_Number >= me.enemy_group[ID].enemy.Length)
			{
				me.enemy_group[ID].enemy_Number = 0;
			}
			else
			{
				Make_Enemy(ID, C, Boss, LV);
			}
		}
	}

	public static void Make_Portal(int ID, Vector3 C, bool Invincible, int LV)
	{
		if (me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].PS.Equals(PlayerState.Pool) || me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].PS.Equals(PlayerState.Death))
		{
			Fight_Master.me.Target_Mop_count--;
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].PS = PlayerState.Idle;
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].BOSS = false;
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].Portal = true;
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].gameObject.SetActive(false);
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].ThisTransform.position = new Vector3(C.x, C.y + Random.Range(-0.3f, 0.3f), C.z);
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].gameObject.SetActive(true);
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].Birth(LV);
			me.enemy_group[ID].enemy[me.enemy_group[ID].enemy_Number].Invincible = Invincible;
			me.enemy_group[ID].enemy_Number++;
			if (me.enemy_group[ID].enemy_Number >= me.enemy_group[ID].enemy.Length)
			{
				me.enemy_group[ID].enemy_Number = 0;
			}
		}
		else
		{
			me.enemy_group[ID].enemy_Number++;
			if (me.enemy_group[ID].enemy_Number >= me.enemy_group[ID].enemy.Length)
			{
				me.enemy_group[ID].enemy_Number = 0;
			}
			else
			{
				Make_Portal(ID, C, Invincible, LV);
			}
		}
	}

	public static void Make_Stop_Monster(int ID, Vector3 C, int LV)
	{
		if (me.stop_enemy[me.stop_enemy_Number].PS.Equals(PlayerState.Pool) || me.stop_enemy[me.stop_enemy_Number].PS.Equals(PlayerState.Death))
		{
			if (Random.Range(0, 10) < 5)
			{
				BIG = true;
				LV += Random.Range(10, 25);
			}
			else
			{
				BIG = false;
			}
			if (BIG)
			{
				me.stop_enemy[me.stop_enemy_Number].ThisTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
			}
			else
			{
				me.stop_enemy[me.stop_enemy_Number].ThisTransform.localScale = new Vector3(0.8f, 0.8f, 1f);
			}
			me.stop_enemy[me.stop_enemy_Number].PS = PlayerState.Idle;
			me.stop_enemy[me.stop_enemy_Number].CH_ID = ID;
			me.stop_enemy[me.stop_enemy_Number].gameObject.SetActive(false);
			me.stop_enemy[me.stop_enemy_Number].ThisTransform.position = new Vector3(C.x, C.y, C.z);
			me.stop_enemy[me.stop_enemy_Number].gameObject.SetActive(true);
			me.stop_enemy[me.stop_enemy_Number].Birth_Dungeon_C_MONSTER(LV);
			me.stop_enemy_Number++;
			if (me.stop_enemy_Number >= me.stop_enemy.Length)
			{
				me.stop_enemy_Number = 0;
			}
			return;
		}
		Make_Stop_Monster_try--;
		if (Make_Stop_Monster_try > 0)
		{
			me.stop_enemy_Number++;
			if (me.stop_enemy_Number >= me.stop_enemy.Length)
			{
				me.stop_enemy_Number = 0;
			}
			Make_Stop_Monster(ID, C, LV);
		}
	}
}
