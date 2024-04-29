using System.Collections;
using Keiwando.BigInteger;
using UnityEngine;

public class Main_Player : MonoBehaviour
{
	public Character player;

	public static Main_Player me;

	public Transform Bigshot_Point;

	public Transform Smallshot_Point;

	public Character Skill_Unit;

	public UILabel ATK_label;

	public UISlider SteamPACK_Slider;

	public UILabel SteamPACK_Count_label;

	public GameObject SteamPACK_ING_Icon;

	public int Now_Tap_Count;

	public int Now_Tap_Count_for_Doubleshot;

	public int Now_Tap_Count_for_Doubleshot_ADD_SHOT;

	public int Now_Tap_Count_for_AddSteampack;

	public int Now_Tap_Count_for_Booms;

	public int Now_Tap_Count_for_Bigshot;

	public bool SteamPACK;

	public GameObject SteamPACK_sprite;

	public float Now_SteamPACK_Time;

	public bool Double_Shot_ING;

	public GameObject Double_Shot_sprite;

	public float Now_Double_Shot_Time;

	public bool Cloudshot_ING;

	public GameObject Cloudshot_sprite;

	public float Now_Cloudshot_Time;

	public bool Add_Mineral_ING;

	public GameObject Add_Mineral_sprite;

	public float Now_Add_Mineral_Time;

	public bool Add_SteamPACK_ING;

	public GameObject Add_SteamPACK_sprite;

	public float Now_Add_SteamPACK_Time;

	public bool FAST_MARINE_ING;

	public GameObject FAST_MARINE_sprite;

	public float Now_FAST_MARINE_Time;

	public bool Autoshot_ING;

	public GameObject Autoshot_sprite;

	public UILabel Autoshot_time_label;

	public int Mineral_Bonus_ING = 100;

	public GameObject Mineral_Bonus_sprite;

	public UILabel Mineral_Bonus_time_label;

	public bool Cooltime_Buff_ING;

	public GameObject Cooltime_Buff_sprite;

	public UILabel Cooltime_Buff_time_label;

	public bool Mineral_CART_ING;

	public GameObject Mineral_CART_sprite;

	public UILabel Mineral_CART_time_label;

	public SpriteRenderer[] Player_Body;

	public bool MINERAL_GOLREM_ING;

	public int Now_MINERAL_GOLREM_Time;

	public int Bazuka_ID;

	public int CH_ID;

	public GameObject[] Main_Bazuka;

	public GameObject[] Second_Bazuka;

	public bool Shot_Possible;

	public float shot_Check;

	private float Ten_Sec_Checksum;

	public float AUTOShot_delay;

	public float ADD_CRITIC;

	public GameObject Cooltime_Popup;

	public int cooltime_reset_price;

	public UILabel cooltime_reset_label;

	public GameObject Mineral_Bonus_Popup;

	public GameObject Autoshot_Popup;

	public UILabel Mineral_Bonus_LV_label;

	public UILabel Mineral_Bonus_Wording_label;

	public UILabel Mineral_Bonus_count_label;

	public UISlider Mineral_Bonus_count_Slider;

	public UILabel Autoshot_LV_label;

	public UILabel Autoshot_Wording_label;

	public UILabel Autoshot_count_label;

	public UISlider Autoshot_count_Slider;

	private int AD_Skill_UPGRADE_NUMBER = 5;

	public GameObject Mineral_Booster_BTN;

	public UILabel Mineral_Booster_count_label;

	public GameObject Mineral_Booster_BTN_AD;

	public GameObject Autoshot_Booster_BTN;

	public UILabel Autoshot_Booster_count_label;

	public GameObject Autoshot_Booster_BTN_AD;

	public void Awake()
	{
		me = this;
	}

	public void Start()
	{
		Status_Setting();
		for (int i = 0; i < Now_Data.me.Active_Skill_LV.Length; i++)
		{
			UI_Master.me.skill_Use_BTNs[i].LOAD();
		}
		UI_Master.me.Rest_Mineral_Setting();
		UI_Master.me.Box_Checking();
		if (Now_Data.me.Mineral_Buff_Time > 0f)
		{
			Mineral_Bonus_ING = 100 + Mineral_Bonus_Value();
		}
		if (Now_Data.me.Missile_Buff_Time > 0f)
		{
			Autoshot_ING = true;
		}
		if (Now_Data.me.Cooltime_Buff_Time > 0f)
		{
			Cooltime_Buff_ING = true;
			Cooltime_Buff_sprite.SetActive(true);
		}
		if (Now_Data.me.Mineral_CART_Buff_Time > 0f)
		{
			Mineral_CART_ING = true;
			Mineral_CART_sprite.SetActive(true);
		}
	}

	public void Status_Setting()
	{
		player.Basic_Data.ATK = Misile_DB.me.DMG_by_LV(0, Now_Data.me.LV_Misile);
		player.Basic_Data.ATK_Range = 12f;
		player.Basic_Data.Lucky = Now_Data.me.MISSILE_CRITICAL_PER;
		player.Basic_Data.Critical_DMG = (int)Now_Data.me.MISSILE_CRITICAL_DMG_PER;
		if (Bazuka_ID != Now_Data.me.Bazuka_ID)
		{
			Bazuka_ID = Now_Data.me.Bazuka_ID;
			for (int i = 0; i < Main_Bazuka.Length; i++)
			{
				Main_Bazuka[i].SetActive(false);
				Second_Bazuka[i].SetActive(false);
				player.Shot_Point[i].gameObject.SetActive(false);
			}
			Main_Bazuka[Bazuka_ID].SetActive(true);
			player.Shot_Point[Bazuka_ID].gameObject.SetActive(true);
			Second_Bazuka[Bazuka_ID].SetActive(true);
		}
		if (Now_Data.me.Bazuka_ID > 0)
		{
			Now_Data.me.AUTOShot_delay = 2f;
			Now_Data.me.AUTOShot_Count = 1 + Now_Data.me.Bazuka_ID * 2;
		}
		else
		{
			Now_Data.me.AUTOShot_delay = 0f;
		}
		if (Now_Data.me.Bazuka_Possible[1] > 0)
		{
			Now_Data.me.Active_Skill_LV[6] = 1;
			UI_Master.me.skill_Use_BTNs[6].LOAD();
		}
		if (Now_Data.me.Bazuka_Possible[2] > 0)
		{
			Now_Data.me.sub_quest_count = 2;
		}
		if (Now_Data.me.Bazuka_Possible[3] > 0)
		{
			for (int j = 0; j < Player_Body.Length; j++)
			{
				Player_Body[j].sprite = Sprite_DB.me.Player_Suite_A[j];
			}
			for (int k = 0; k < player.S_animator.Move_Sprite.Length; k++)
			{
				player.S_animator.Move_Sprite[k] = Sprite_DB.me.Player_Suite_A_RUN[k];
			}
		}
		for (int l = 0; l < 4; l++)
		{
			player.Shot_Point[l].gameObject.SetActive(false);
		}
		player.Shot_Point[Now_Data.me.Bazuka_ID].gameObject.SetActive(true);
	}

	public void FixedUpdate()
	{
		shot_Check += 0.05f;
		if (shot_Check >= 0.1f)
		{
			shot_Check = 0f;
			Shot_Possible = true;
		}
		if (SteamPACK)
		{
			Now_SteamPACK_Time -= 0.05f * Fight_Master.me.Game_Speed;
			if (Now_SteamPACK_Time <= 0f)
			{
				OFF_SteamPACK();
			}
			else
			{
				SteamPACK_Slider.value = Now_SteamPACK_Time / Now_Data.me.SteamPACK_Limit_Time;
				SteamPACK_Count_label.text = string.Format("{0:0}Sec", Now_SteamPACK_Time);
			}
		}
		if (Add_Mineral_ING)
		{
			Now_Add_Mineral_Time -= 0.05f * Fight_Master.me.Game_Speed;
			if (Now_Add_Mineral_Time <= 0f)
			{
				OFF_Add_Mineral();
			}
			else
			{
				UI_Master.me.skill_Use_BTNs[1].Sec_label.text = string.Format("[FF5900]{0:N1}[-]", Now_Add_Mineral_Time);
			}
		}
		if (Cloudshot_ING)
		{
			Now_Cloudshot_Time -= 0.05f * Fight_Master.me.Game_Speed;
			if (Now_Cloudshot_Time <= 0f)
			{
				OFF_Cloudshot();
			}
			else
			{
				UI_Master.me.skill_Use_BTNs[2].Sec_label.text = string.Format("[FF5900]{0:N1}[-]", Now_Cloudshot_Time);
			}
		}
		if (Double_Shot_ING)
		{
			Now_Double_Shot_Time -= 0.05f * Fight_Master.me.Game_Speed;
			if (Now_Double_Shot_Time <= 0f)
			{
				OFF_Double_Shot_ING();
			}
			else
			{
				UI_Master.me.skill_Use_BTNs[3].Sec_label.text = string.Format("[FF5900]{0:N1}[-]", Now_Double_Shot_Time);
			}
		}
		if (Add_SteamPACK_ING)
		{
			Now_Add_SteamPACK_Time -= 0.05f * Fight_Master.me.Game_Speed;
			if (Now_Add_SteamPACK_Time <= 0f)
			{
				OFF_Add_SteamPACK();
			}
			else
			{
				SteamPACK_Slider.value = Now_SteamPACK_Time / Now_Add_SteamPACK_Time;
				UI_Master.me.skill_Use_BTNs[4].Sec_label.text = string.Format("[FF5900]{0:N1}[-]", Now_Add_SteamPACK_Time);
			}
		}
		if (FAST_MARINE_ING)
		{
			Now_FAST_MARINE_Time -= 0.05f * Fight_Master.me.Game_Speed;
			if (Now_FAST_MARINE_Time <= 0f)
			{
				OFF_FAST_MARINE();
			}
			else
			{
				UI_Master.me.skill_Use_BTNs[5].Sec_label.text = string.Format("[FF5900]{0:N1}[-]", Now_FAST_MARINE_Time);
			}
		}
		if (Autoshot_ING)
		{
			SHOT();
			Now_Data.me.Missile_Buff_Time -= 0.05f * Fight_Master.me.Game_Speed;
			if (Now_Data.me.Missile_Buff_Time <= 0f)
			{
				Quit_Auto_Shot();
			}
			else
			{
				Autoshot_time_label.text = Time_Checker.ShowTime_Label_noT(Now_Data.me.Missile_Buff_Time);
			}
		}
		if (Mineral_Bonus_ING > 100)
		{
			Now_Data.me.Mineral_Buff_Time -= 0.05f * Fight_Master.me.Game_Speed;
			if (Now_Data.me.Mineral_Buff_Time <= 0f)
			{
				Quit_Mineral_Bonus();
			}
			else
			{
				Mineral_Bonus_time_label.text = Time_Checker.ShowTime_Label_noT(Now_Data.me.Mineral_Buff_Time);
			}
		}
		if (Cooltime_Buff_ING)
		{
			Now_Data.me.Cooltime_Buff_Time -= 0.05f * Fight_Master.me.Game_Speed;
			if (Now_Data.me.Cooltime_Buff_Time <= 0f)
			{
				Cooltime_Buff_ING = false;
				Cooltime_Buff_sprite.SetActive(false);
			}
		}
		if (Mineral_CART_ING)
		{
			Now_Data.me.Mineral_CART_Buff_Time -= 0.05f * Fight_Master.me.Game_Speed;
			if (Now_Data.me.Mineral_CART_Buff_Time <= 0f)
			{
				Mineral_CART_ING = false;
				Mineral_CART_sprite.SetActive(false);
			}
		}
		if (Now_Data.me.AUTOShot_delay > 0f)
		{
			AUTOShot_delay += 0.05f * Fight_Master.me.Game_Speed;
			if (AUTOShot_delay >= Now_Data.me.AUTOShot_delay)
			{
				AUTOShot_delay = 0f;
				StartCoroutine(REAL_FiveShot());
			}
		}
	}

	public IEnumerator REAL_FiveShot()
	{
		for (int i = 0; (float)i < Now_Data.me.AUTOShot_Count; i++)
		{
			Shot_Possible = true;
			SHOT();
			yield return new WaitForSeconds(0.03f);
		}
	}

	public void Gameove()
	{
		OFF_SteamPACK();
		OFF_Add_Mineral();
		OFF_Cloudshot();
		OFF_Double_Shot_ING();
		OFF_Add_SteamPACK();
		OFF_FAST_MARINE();
	}

	public void REAL_TAP()
	{
		Now_Data.me.NOW_TAP += (BigInteger)1;
		Now_Data.me.ALL_TAP += (BigInteger)1;
		SHOT();
		if (Now_Data.me.ALL_TAP % 10 == 0)
		{
			Security.SetString("ALL_TAP", Now_Data.me.ALL_TAP.ToString());
			Security.SetString("NOW_TAP", Now_Data.me.NOW_TAP.ToString());
			Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_TAP);
		}
		for (int i = 0; i < Now_Data.me.sub_quest_count; i++)
		{
			UI_Master.me.sub_quest_ui[i].Txt_Update(Quest_Goal_Type.NOW_TAP);
		}
	}

	public void SHOT()
	{
		if (!Fight_Master.me.GS.Equals(GameState.Play) || !Shot_Possible)
		{
			return;
		}
		Shot_Possible = false;
		player.Attack_Motion(true);
		player.Attack();
		SoundManager.me.Player_Shot();
		if (!SteamPACK)
		{
			Now_Tap_Count++;
			if (Now_Tap_Count >= Now_Data.me.SteamPACK_Need_tap)
			{
				ON_SteamPACK();
			}
			else
			{
				SteamPACK_Slider.value = (float)Now_Tap_Count / (float)Now_Data.me.SteamPACK_Need_tap;
				SteamPACK_Count_label.text = string.Format("{0}/{1}", Now_Tap_Count, Now_Data.me.SteamPACK_Need_tap);
			}
		}
		if (Double_Shot_ING && Now_Data.me.ADD_Doubleshot_missile > 0)
		{
			Now_Tap_Count_for_Doubleshot_ADD_SHOT++;
			if (Now_Tap_Count_for_Doubleshot_ADD_SHOT >= 10)
			{
				Now_Tap_Count_for_Doubleshot_ADD_SHOT = 0;
				StartCoroutine(MINI_Shot(Now_Data.me.ADD_Doubleshot_missile));
			}
		}
		if (Now_Data.me.COUNT_for_Doubleshot != 0)
		{
			Now_Tap_Count_for_Doubleshot++;
			if (Now_Tap_Count_for_Doubleshot / Now_Data.me.COUNT_for_Doubleshot >= 1)
			{
				Now_Tap_Count_for_Doubleshot = 0;
				player.Attack_Motion(true);
				player.Attack();
			}
		}
		if (Now_Data.me.COUNT_for_SteamPack != 0)
		{
			Now_Tap_Count_for_AddSteampack++;
			if (Now_Tap_Count_for_AddSteampack / Now_Data.me.COUNT_for_SteamPack >= 1)
			{
				Now_Tap_Count_for_AddSteampack = 0;
				Now_Tap_Count += Now_Data.me.SteamPACK_Need_tap / 100;
			}
		}
		if (Now_Data.me.COUNT_Boom_five != 0)
		{
			Now_Tap_Count_for_Booms++;
			if (Now_Tap_Count_for_Booms / Now_Data.me.COUNT_Boom_five >= 1)
			{
				Now_Tap_Count_for_Booms = 0;
				Fire_Work(5);
			}
		}
		if (Now_Data.me.COUNT_BigShot != 0)
		{
			Now_Tap_Count_for_Bigshot++;
			if (Now_Tap_Count_for_Bigshot / Now_Data.me.COUNT_BigShot >= 1)
			{
				Now_Tap_Count_for_Bigshot = 0;
				Skill_A();
			}
		}
		Now_Data.me.NOW_TAP += (BigInteger)1;
		Now_Data.me.ALL_TAP += (BigInteger)1;
		if (Now_Data.me.ALL_TAP % 10 == 0)
		{
			Security.SetString("ALL_TAP", Now_Data.me.ALL_TAP.ToString());
			Security.SetString("NOW_TAP", Now_Data.me.NOW_TAP.ToString());
		}
		for (int i = 0; i < Now_Data.me.sub_quest_count; i++)
		{
			UI_Master.me.sub_quest_ui[i].Txt_Update(Quest_Goal_Type.NOW_TAP);
		}
	}

	public void ON_SteamPACK()
	{
		Now_Tap_Count = 0;
		SteamPACK = true;
		Now_SteamPACK_Time = Now_Data.me.SteamPACK_Limit_Time;
		SteamPACK_sprite.SetActive(true);
		player.Chat(Localization.Get("STEAMPACK_START_CHAT"));
		Now_Data.me.MISSILE_SPEED = 3f;
		Fight_Master.me.CameraShake();
		Fight_Master.me.CameraShake_Long();
		SoundManager.me.SteamPACK();
		SteamPACK_Slider.gameObject.SetActive(true);
		SteamPACK_ING_Icon.gameObject.SetActive(true);
	}

	public void OFF_SteamPACK()
	{
		SteamPACK = false;
		SteamPACK_sprite.SetActive(false);
		Now_Data.me.MISSILE_SPEED = 1.5f;
		if (Now_Data.me.SteamPACK_Fin_Boom > 0)
		{
			Fire_Work(Now_Data.me.SteamPACK_Fin_Boom);
		}
		SteamPACK_ING_Icon.gameObject.SetActive(false);
	}

	public void SmallShot_BOOM(int Count)
	{
		StartCoroutine(SmallShot(Count));
	}

	public IEnumerator SmallShot(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Skill_Booms();
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(0.1f);
	}

	public void Skill_Booms()
	{
		if (Fight_Master.me.Live_Enemies.Count <= 0)
		{
			return;
		}
		while (OBJ_Pool.me.Small_misile_misile[OBJ_Pool.Small_misile_Number].gameObject.activeSelf)
		{
			OBJ_Pool.Small_misile_Number++;
			if (OBJ_Pool.Small_misile_Number >= OBJ_Pool.me.Small_misile_misile.Length)
			{
				OBJ_Pool.Small_misile_Number = 0;
				return;
			}
		}
		OBJ_Pool.me.Small_misile_misile[OBJ_Pool.Small_misile_Number].target = Fight_Master.me.Live_Enemies[Random.Range(0, Fight_Master.me.Live_Enemies.Count)].Hit_Point;
		OBJ_Pool.me.Small_misile_misile[OBJ_Pool.Small_misile_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Small_misile_misile[OBJ_Pool.Small_misile_Number].ThisTransform.position = Smallshot_Point.position;
		OBJ_Pool.me.Small_misile_misile[OBJ_Pool.Small_misile_Number].DMG = player.Basic_Data.ATK * 3;
		OBJ_Pool.me.Small_misile_misile[OBJ_Pool.Small_misile_Number].gameObject.SetActive(true);
		OBJ_Pool.Small_misile_Number++;
		if (OBJ_Pool.Small_misile_Number >= OBJ_Pool.me.Small_misile_misile.Length)
		{
			OBJ_Pool.Small_misile_Number = 0;
		}
	}

	public void Fire_Work(int Count)
	{
		StartCoroutine(Fire_Work_Co(Count));
	}

	public IEnumerator Fire_Work_Co(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Fire_Work_oneshot();
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(0.1f);
	}

	public void Fire_Work_oneshot()
	{
		if (Fight_Master.me.Live_Enemies.Count <= 0)
		{
			return;
		}
		int num = 100;
		while (OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].gameObject.activeSelf)
		{
			OBJ_Pool.Fire_misile_Number++;
			if (OBJ_Pool.Fire_misile_Number >= OBJ_Pool.me.Fire_misile_misile.Length)
			{
				OBJ_Pool.Fire_misile_Number = 0;
				return;
			}
			if (num <= 0)
			{
				break;
			}
			num--;
		}
		SoundManager.me.FIREShot();
		OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].target = Fight_Master.me.Live_Enemies[Random.Range(0, Fight_Master.me.Live_Enemies.Count)].Hit_Point;
		OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].ThisTransform.position = Smallshot_Point.position;
		OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].DMG = player.Basic_Data.ATK * 3;
		OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].gameObject.SetActive(true);
	}

	public void SmallShot_FROM_FARIY(int Count)
	{
		Debug.Log("폭죽 " + Count);
		StartCoroutine(SmallShot_FROM_FARIY_Corountin(Count));
	}

	public IEnumerator SmallShot_FROM_FARIY_Corountin(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Skill_Booms_FROM_FARIY();
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(0.1f);
	}

	public void Skill_Booms_FROM_FARIY()
	{
		if (Fight_Master.me.Live_Enemies.Count <= 0)
		{
			return;
		}
		int num = 100;
		while (OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].gameObject.activeSelf)
		{
			OBJ_Pool.Fire_misile_Number++;
			if (OBJ_Pool.Fire_misile_Number >= OBJ_Pool.me.Fire_misile_misile.Length)
			{
				OBJ_Pool.Fire_misile_Number = 0;
				return;
			}
			if (num <= 0)
			{
				break;
			}
			num--;
		}
		SoundManager.me.FIREShot();
		OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].target = Fight_Master.me.Live_Enemies[Random.Range(0, Fight_Master.me.Live_Enemies.Count)].Hit_Point;
		OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].ThisTransform.position = UI_Master.me.fairybox_POPUP.target.ThisTransform.position;
		OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].DMG = player.Basic_Data.ATK * 3;
		OBJ_Pool.me.Fire_misile_misile[OBJ_Pool.Fire_misile_Number].gameObject.SetActive(true);
	}

	public IEnumerator MINI_Shot(int count)
	{
		for (int i = 0; i < count; i++)
		{
			MINI_SHOT();
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(0.1f);
	}

	public void MINI_SHOT()
	{
		player.Arrow_Attack(false, 0, true);
	}

	public void Skill_BB()
	{
		Skill_Unit.Birth_Skill_Unit();
	}

	public void Skill_A()
	{
		while (OBJ_Pool.me.Big_misile_misile[OBJ_Pool.Big_misile_Number].gameObject.activeSelf)
		{
			OBJ_Pool.Big_misile_Number++;
			if (OBJ_Pool.Big_misile_Number >= OBJ_Pool.me.Big_misile_misile.Length)
			{
				OBJ_Pool.Big_misile_Number = 0;
				return;
			}
		}
		OBJ_Pool.me.Big_misile_misile[OBJ_Pool.Big_misile_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Big_misile_misile[OBJ_Pool.Big_misile_Number].ThisTransform.position = Bigshot_Point.position;
		OBJ_Pool.me.Big_misile_misile[OBJ_Pool.Big_misile_Number].DMG = player.Basic_Data.ATK * Misile_DB.me.skill_DB[0].Skill_LV[Now_Data.me.Active_Skill_LV[0]].Value * (100 + Now_Data.me.SKILL_Value_Plus[0]) / 100;
		OBJ_Pool.me.Big_misile_misile[OBJ_Pool.Big_misile_Number].gameObject.SetActive(true);
		OBJ_Pool.Big_misile_Number++;
		if (OBJ_Pool.Big_misile_Number >= OBJ_Pool.me.Big_misile_misile.Length)
		{
			OBJ_Pool.Big_misile_Number = 0;
		}
		if (Now_Data.me.ADD_small_Missile > 0)
		{
			SmallShot_BOOM(Now_Data.me.ADD_small_Missile);
		}
	}

	public void Skill_B()
	{
		Add_Mineral_ING = true;
		Now_Add_Mineral_Time = 30f * (100f + Now_Data.me.SKILL_Effectivetime[1] + Now_Data.me.ALL_SKILL_Effectivetime) / 100f;
		Add_Mineral_sprite.SetActive(true);
		player.Chat(Localization.Get("SKILL_B_CHAT"));
	}

	public void OFF_Add_Mineral()
	{
		Add_Mineral_ING = false;
		Add_Mineral_sprite.SetActive(false);
		UI_Master.me.skill_Use_BTNs[1].USE_ING = false;
		UI_Master.me.skill_Use_BTNs[1].ING_EF.SetActive(false);
	}

	public void Skill_C()
	{
		Cloudshot_ING = true;
		Now_Cloudshot_Time = 30f * (100f + Now_Data.me.SKILL_Effectivetime[2] + Now_Data.me.ALL_SKILL_Effectivetime) / 100f;
		Skill_Unit.Birth_Skill_Unit();
		player.Chat(Localization.Get("SKILL_C_CHAT"));
	}

	public void OFF_Cloudshot()
	{
		Cloudshot_ING = false;
		Skill_Unit.Remove();
		UI_Master.me.skill_Use_BTNs[2].USE_ING = false;
		UI_Master.me.skill_Use_BTNs[2].ING_EF.SetActive(false);
	}

	public void Skill_D()
	{
		Double_Shot_ING = true;
		Now_Double_Shot_Time = (float)((27 + Misile_DB.me.skill_DB[3].Skill_LV[Now_Data.me.Active_Skill_LV[3]].Value) * (100 + Now_Data.me.SKILL_Value_Plus[3]) / 100) * (100f + Now_Data.me.SKILL_Effectivetime[3] + Now_Data.me.ALL_SKILL_Effectivetime) / 100f;
		Double_Shot_sprite.SetActive(true);
		Second_Bazuka[Now_Data.me.Bazuka_ID].gameObject.SetActive(true);
		player.Shot_Point[Now_Data.me.Bazuka_ID + 4].gameObject.SetActive(true);
		player.Chat(Localization.Get("SKILL_D_CHAT"));
	}

	public void OFF_Double_Shot_ING()
	{
		Double_Shot_ING = false;
		Double_Shot_sprite.SetActive(false);
		for (int i = 0; i < Second_Bazuka.Length; i++)
		{
			Second_Bazuka[i].gameObject.SetActive(false);
		}
		player.Shot_Point[4].gameObject.SetActive(false);
		player.Shot_Point[5].gameObject.SetActive(false);
		player.Shot_Point[6].gameObject.SetActive(false);
		player.Shot_Point[7].gameObject.SetActive(false);
		UI_Master.me.skill_Use_BTNs[3].USE_ING = false;
		UI_Master.me.skill_Use_BTNs[3].ING_EF.SetActive(false);
	}

	public void Skill_E()
	{
		Add_SteamPACK_ING = true;
		Now_Add_SteamPACK_Time = 30f * (100f + Now_Data.me.SKILL_Effectivetime[4] + Now_Data.me.ALL_SKILL_Effectivetime) / 100f;
		Now_SteamPACK_Time = Now_Add_SteamPACK_Time;
		ADD_CRITIC = Misile_DB.me.skill_DB[4].Skill_LV[Now_Data.me.Active_Skill_LV[4]].Value * (100 + Now_Data.me.SKILL_Value_Plus[4]) / 100;
		player.Basic_Data.Critical_DMG += (BigInteger)(int)ADD_CRITIC;
		Add_SteamPACK_sprite.SetActive(true);
		player.Chat(Localization.Get("SKILL_E_CHAT"));
		SteamPACK = true;
		SteamPACK_sprite.SetActive(true);
		Now_Data.me.MISSILE_SPEED = 3f;
	}

	public void OFF_Add_SteamPACK()
	{
		SteamPACK = false;
		Add_SteamPACK_ING = false;
		Add_SteamPACK_sprite.SetActive(false);
		SteamPACK_sprite.SetActive(false);
		Now_Data.me.MISSILE_SPEED = 1.5f;
		player.Basic_Data.Critical_DMG -= (BigInteger)(int)ADD_CRITIC;
		UI_Master.me.skill_Use_BTNs[4].USE_ING = false;
		UI_Master.me.skill_Use_BTNs[4].ING_EF.SetActive(false);
	}

	public void Skill_F()
	{
		FAST_MARINE_ING = true;
		Now_FAST_MARINE_Time = 30f * (100f + Now_Data.me.SKILL_Effectivetime[5] + Now_Data.me.ALL_SKILL_Effectivetime) / 100f;
		FAST_MARINE_sprite.SetActive(true);
		player.Chat(Localization.Get("SKILL_F_CHAT"));
	}

	public void OFF_FAST_MARINE()
	{
		FAST_MARINE_ING = false;
		FAST_MARINE_sprite.SetActive(false);
		UI_Master.me.skill_Use_BTNs[5].USE_ING = false;
		UI_Master.me.skill_Use_BTNs[5].ING_EF.SetActive(false);
	}

	public void OPEN_All_Cooltiem()
	{
		if (Now_Data.me.Active_Skill_LV[0] >= 1)
		{
			cooltime_reset_price = 100 - (int)Now_Data.me.CoolReset_Discount;
			cooltime_reset_label.text = string.Format("-{0}", cooltime_reset_price);
			UI_Master.me.Popup(Cooltime_Popup);
		}
		else
		{
			UI_Master.me.Good_MSG("NOSKILL");
		}
	}

	public void All_Cooltiem_Clear()
	{
		if (Now_Data.me.MEDAL_Possible(cooltime_reset_price))
		{
			Now_Data.me.MEDAL_Change(-cooltime_reset_price);
			for (int i = 0; i < UI_Master.me.skill_Use_BTNs.Length; i++)
			{
				UI_Master.me.skill_Use_BTNs[i].Skill_Cooltime_now = 0f;
			}
			UI_Master.me.Popup_Close_All();
		}
		else
		{
			UI_Master.me.Open_Uranium_Popup();
		}
	}

	public void Open_Mineral_Bonus_Popup()
	{
		if (Mineral_Bonus_ING.Equals(100))
		{
			Mineral_Bonus_LV_label.text = string.Format("LV.{0}", Now_Data.me.MINERALBOOM_LV + 1);
			Mineral_Bonus_Wording_label.text = string.Format("{0}{1}{2}", Localization.Get("MINERAL_MINE_WORDING_A"), Mineral_Bonus_Value(), Localization.Get("MINERAL_MINE_WORDING_B"));
			Mineral_Bonus_count_label.text = string.Format("{0}/{1}", Now_Data.me.MINERALBOOM_EXP, 10);
			Mineral_Bonus_count_Slider.value = (float)Now_Data.me.MINERALBOOM_EXP / 10f;
			if (Now_Data.me.Booster_MINERAL_MINE > 0)
			{
				Mineral_Booster_count_label.text = string.Format("{0}", Now_Data.me.Booster_MINERAL_MINE);
				Mineral_Booster_BTN.SetActive(true);
				Mineral_Booster_BTN_AD.SetActive(false);
			}
			else
			{
				Autoshot_Booster_BTN.SetActive(false);
				Autoshot_Booster_BTN_AD.SetActive(true);
			}
			UI_Master.me.Popup(Mineral_Bonus_Popup);
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("ALREADY_USE"));
		}
	}

	public void Open_Autoshot_Popup()
	{
		if (!Autoshot_ING)
		{
			Autoshot_LV_label.text = string.Format("LV.{0}", Now_Data.me.Autoshot_LV + 1);
			Autoshot_Wording_label.text = string.Format("{0}{1}{2}", Localization.Get("Autoshot_WORDING_A"), Autoshot_TIME(), Localization.Get("Autoshot_WORDING_B"));
			Autoshot_count_label.text = string.Format("{0}/{1}", Now_Data.me.Autoshot_EXP, 10);
			Autoshot_count_Slider.value = (float)Now_Data.me.Autoshot_EXP / 10f;
			if (Now_Data.me.Booster_LOCKON > 0)
			{
				Autoshot_Booster_count_label.text = string.Format("{0}", Now_Data.me.Booster_LOCKON);
				Autoshot_Booster_BTN.SetActive(true);
				Autoshot_Booster_BTN_AD.SetActive(false);
			}
			else
			{
				Autoshot_Booster_BTN.SetActive(false);
				Autoshot_Booster_BTN_AD.SetActive(true);
			}
			UI_Master.me.Popup(Autoshot_Popup);
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("ALREADY_USE"));
		}
	}

	public float Autoshot_TIME()
	{
		return 180 + Now_Data.me.Autoshot_LV * 30;
	}

	public int Mineral_Bonus_Value()
	{
		return 50 + Now_Data.me.MINERALBOOM_LV * 10;
	}

	public void Set_AutoShot_by_Booster()
	{
		Now_Data.me.Booster_LOCKON--;
		Security.SetInt("Booster_LOCKON", Now_Data.me.Booster_LOCKON);
		Set_AutoShot(false);
	}

	public void Set_AutoShot_by_AD()
	{
		//if (VIDEO_ADS.me.isPossible(true))
		//{
		//	VIDEO_ADS.me.ShowRewardedVideo(2);
		//}
		//else
		//{
		//	UI_Master.me.Warning(Localization.Get("AD_WARNING"));
		//}
	}

	public void Set_Mineral_Bonus_by_Booster()
	{
		Now_Data.me.Booster_MINERAL_MINE--;
		Security.SetInt("Booster_MINERAL_MINE", Now_Data.me.Booster_MINERAL_MINE);
		Set_Mineral_Bonus(false);
	}

	public void Set_Mineral_by_AD()
	{
		//if (VIDEO_ADS.me.isPossible(true))
		//{
		//	VIDEO_ADS.me.ShowRewardedVideo(3);
		//}
		//else
		//{
		//	UI_Master.me.Warning(Localization.Get("AD_WARNING"));
		//}
	}

	public void Set_AutoShot(bool by_AD)
	{
		SoundManager.me.Booster_A();
		Autoshot_ING = true;
		Now_Data.me.Missile_Buff_Time = Autoshot_TIME();
		if ((float)Random.Range(0, 100) < Now_Data.me.Booster_DOUBLE_PER && !by_AD)
		{
			Now_Data.me.Missile_Buff_Time *= 2f;
			UI_Master.me.Good_MSG(Localization.Get("DOUBLE_BOSSTER"));
		}
		if (Now_Data.me.Booster_ADD_Time > 0f)
		{
			Now_Data.me.Missile_Buff_Time = Now_Data.me.Missile_Buff_Time * (100f + Now_Data.me.Booster_ADD_Time) / 100f;
		}
		Security.SetFloat("Missile_Buff_Time", Now_Data.me.Missile_Buff_Time);
		UI_Master.me.Popup_Close_All();
		player.Chat(Localization.Get("SKILL_AUTO_SHOT"));
		Autoshot_sprite.SetActive(true);
		Now_Data.me.Autoshot_EXP++;
		if (Now_Data.me.Autoshot_EXP >= 10)
		{
			Now_Data.me.Autoshot_EXP = 0;
			Now_Data.me.Autoshot_LV++;
		}
		Security.SetInt("Autoshot_EXP", Now_Data.me.Autoshot_EXP);
		Security.SetInt("Autoshot_LV", Now_Data.me.Autoshot_LV);
	}

	public void Set_Mineral_Bonus(bool by_AD)
	{
		SoundManager.me.Booster_B();
		Mineral_Bonus_ING = 100 + Mineral_Bonus_Value();
		Now_Data.me.Mineral_Buff_Time = 600f;
		Security.SetFloat("Mineral_Buff_Time", Now_Data.me.Mineral_Buff_Time);
		if ((float)Random.Range(0, 100) < Now_Data.me.Booster_DOUBLE_PER && !by_AD)
		{
			Now_Data.me.Mineral_Buff_Time *= 2f;
			UI_Master.me.Good_MSG(Localization.Get("DOUBLE_BOSSTER"));
		}
		if (Now_Data.me.Booster_ADD_Time > 0f)
		{
			Now_Data.me.Mineral_Buff_Time = Now_Data.me.Mineral_Buff_Time * (100f + Now_Data.me.Booster_ADD_Time) / 100f;
		}
		UI_Master.me.Popup_Close_All();
		player.Chat(Localization.Get("SKILL_MINERAL_MINE"));
		Mineral_Bonus_sprite.SetActive(true);
		Now_Data.me.MINERALBOOM_EXP++;
		if (Now_Data.me.MINERALBOOM_EXP >= 10)
		{
			Now_Data.me.MINERALBOOM_EXP = 0;
			Now_Data.me.MINERALBOOM_LV++;
		}
		Security.SetInt("MINERALBOOM_EXP", Now_Data.me.MINERALBOOM_EXP);
		Security.SetInt("MINERALBOOM_LV", Now_Data.me.MINERALBOOM_LV);
	}

	public void Quit_Auto_Shot()
	{
		UI_Master.me.Warning(Localization.Get("BOOSTER_END_A"));
		Autoshot_ING = false;
		Autoshot_sprite.SetActive(false);
		Autoshot_time_label.text = string.Format("LV.{0}", Now_Data.me.Autoshot_LV + 1);
		Now_Data.me.Buff_Time_Save(0f);
	}

	public void Quit_Mineral_Bonus()
	{
		UI_Master.me.Warning(Localization.Get("BOOSTER_END_B"));
		Mineral_Bonus_sprite.SetActive(false);
		Mineral_Bonus_ING = 100;
		Mineral_Bonus_time_label.text = string.Format("LV.{0}", Now_Data.me.MINERALBOOM_LV + 1);
		Now_Data.me.Buff_Time_Save(0f);
	}

	public void OnEnable()
	{
		player.Text.transform.parent.gameObject.SetActive(false);
	}
}
