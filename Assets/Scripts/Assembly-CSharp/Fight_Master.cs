using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Keiwando.BigInteger;
using UnityEngine;

public class Fight_Master : MonoBehaviour
{
	public UILabel TIME_LABEL;

	public float PLAY_TIME;

	public static Fight_Master me;

	public GameState GS;

	public Stage_Type stage_type;

	public GameObject[] stage_Background;

	public GameObject[] Normal_theme;

	public float Game_Speed = 1f;

	public Character Player;

	public ActionMan action_man;

	public ActionMan[] Mini_mans;

	public int Target_Mop_count;

	public bool BOSS_FIGHT;

	public bool SPECIAL_BOSS_A;

	public bool SPECIAL_BOSS_B;

	public bool SPECIAL_BOSS_D;

	public float Mop_delay = 1f;

	public float Mop_delay_ADD;

	public float Mop_delay_Check;

	public Transform Normal_Enemy_Spawn;

	public Transform Wall_Spawn;

	public Transform Portal_Spawn;

	public Transform OutSide_Enemy_Spawn;

	public Main_Player Main_player;

	public Enemy NOW_TARGET;

	public List<Enemy> Live_Enemies;

	public Character[] Units;

	private float BoxFairy_BOY_time_Origin = 120f;

	private float BoxFairy_BOY_time = 120f;

	public float BoxFairy_BOY_time_NOW = -180f;

	private bool BoxFairy_BOY_D;

	private float BoxFairy_MAN_time_Origin = 600f;

	private float BoxFairy_MAN_time = 600f;

	public float BoxFairy_MAN_time_NOW = -180f;

	private bool BoxFairy_MAN_D;

	private float Rae_Portal_time = 600f;

	public float Rae_Portal_time_NOW;

	public bool Rare_Portal_Possible;

	private float Dungeon_Key_time = 600f;

	public float Dungeon_Key_time_NOW;

	public bool Dungeon_Key_Possible;

	public Transform Fairy_Point_A;

	public Transform Fairy_Point_B;

	public BoxFairy BoxFairy_BOY;

	public BoxFairy BoxFairy_BOY_Second;

	public BoxFairy BoxFairy_MAN;

	public BoxFairy BoxFairy_MAN_Second;

	public float Playetime_for_10sec;

	public int Group_count_check;

	public int Group_count = 6;

	public int Group_ID = 1;

	public int WaveCount;

	public float Group_Arrange_value;

	public bool Back_Delay;

	public float AUTO_P_STONE_MAKING_time;

	public float AUTO_Mineral_MAKING_time;

	public float SAVE_delay;

	public float time_save_Check;

	public int Monuet_Check;

	public int Ten_Minuet_Check;

	public int EVENT_CANDY_Possible;

	public GameObject BOSS_BTN;

	private int theme_number;

	private int SNOW_NUMBER;

	private int BG_Number;

	public GameObject Speed_FadeIN;

	public GameObject Drop_Port_anim;

	public int SAVE_POPUP_ID;

	public bool ONE_or_DDEA;

	public Camera WorldMap_Camera;

	public GameObject Flash_obj;

	public GameObject Skill_Icons;

	public GameObject Givup_BTN;

	public int Dungeon_C_Monster_LV;

	public int Dungeon_C_monster_Kill_Count;

	public Transform Dungeon_C_BG;

	public float Dungeon_C_Speed;

	public float GameOver_time_check;

	public float Dungeon_E_Time;

	public int Dungeon_E_Hell_stone_Count;

	public int Dungeon_E_Hell_stone_BOX_Count;

	public void Awake()
	{
		me = this;
		if (Scene_Change.me != null)
		{
			Scene_Change.me.Exit();
		}
	}

	public void Update()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			Main_player.SHOT();
		}
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			UI_Master.me.AUTO_MISSILE_UPGRADE();
		}
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			UI_Master.me.AUTO_UPGRADE();
		}
		if (!GS.Equals(GameState.Ready) && Input.GetKeyDown(KeyCode.Escape) && !Back_Delay)
		{
			if (UI_Master.me.popups[0] == null)
			{
				UI_Master.me.Popup(UI_Master.me.QUIT_Panel);
			}
			else
			{
				UI_Master.me.Close_Popup();
			}
		}
		if (GS.Equals(GameState.Play) && stage_type.Equals(Stage_Type.Dungeon_C))
		{
			Mop_delay_Check += Time.deltaTime * (float)Random.Range(1, 5);
			Dungeon_C_BG.Translate(Vector3.left * Time.deltaTime * me.Game_Speed * Dungeon_C_Speed);
			if (Mop_delay_Check >= 1f)
			{
				Mop_delay_Check = 0f;
				Mop_delay_ADD = 0f;
				OBJ_Pool.Make_Stop_Monster_try = 7;
				OBJ_Pool.Make_Stop_Monster(24, OutSide_Enemy_Spawn.position, Dungeon_C_Monster_LV);
			}
		}
	}

	public void FixedUpdate()
	{
		if (GS.Equals(GameState.Play))
		{
			if (Now_Data.me.BEST_LV > 310)
			{
				Now_Data.me.DIABLO_LAPTIME += 0.05f * Game_Speed;
				TIME_LABEL.text = Time_Checker.ShowTime_Label_noT(Now_Data.me.DIABLO_LAPTIME);
			}
			if (stage_type.Equals(Stage_Type.Normal))
			{
				if (!BOSS_FIGHT || Target_Mop_count > 0)
				{
					Mop_delay_Check += 0.05f * (float)Random.Range(1, 3);
					if (Mop_delay_Check >= Mop_delay + Mop_delay_ADD)
					{
						Mop_delay_Check = 0f;
						if (Live_Enemies.Count < 100)
						{
							if (Group_count_check.Equals(0))
							{
								Mop_delay_ADD = 0f;
								Group_ID = Monster_DB.me.Monster_Groups[Now_Data.me.NOW_Theme].Monster_ID[Random.Range(0, Monster_DB.me.Monster_Groups[Now_Data.me.NOW_Theme].Monster_ID.Length)];
								if ((float)Random.Range(0, 100) < Now_Data.me.Golrem_Per || Main_Player.me.MINERAL_GOLREM_ING)
								{
									if (Main_Player.me.MINERAL_GOLREM_ING)
									{
										if (Main_Player.me.Now_MINERAL_GOLREM_Time.Equals(1))
										{
											Group_ID = 21;
											Group_count = 1;
										}
										else
										{
											Group_ID = 25;
											Group_count = 1;
										}
										Main_Player.me.MINERAL_GOLREM_ING = false;
									}
									else
									{
										Group_ID = 21;
										Group_count = 1;
									}
								}
								Group_count = Monster_DB.me.monster_DB[Group_ID].Group_Avarage;
								if (!Group_count.Equals(1))
								{
									Group_count += Random.Range(-1, 2);
									if (Group_count > 15)
									{
										Group_count = 15;
									}
								}
								if (Monster_DB.me.monster_DB[Group_ID].Random_Position)
								{
									Group_Arrange_value = 0f;
								}
								else
								{
									Group_Arrange_value = 2.75f / (float)Group_count;
								}
								if (Group_Arrange_value.Equals(0f))
								{
									OBJ_Pool.Make_Enemy(Group_ID, new Vector3(Normal_Enemy_Spawn.position.x + Random.Range(-1.5f, 2f), Normal_Enemy_Spawn.position.y + Random.Range(-1.5f, 1.5f), 0f), BOSS_FIGHT, Now_Data.me.LV);
								}
								else
								{
									OBJ_Pool.Make_Enemy(Group_ID, new Vector3(Normal_Enemy_Spawn.position.x, -2.25f, 0f), BOSS_FIGHT, Now_Data.me.LV);
								}
								Group_count_check++;
							}
							else if (Group_count_check >= Group_count)
							{
								WaveCount++;
								Group_count_check = 0;
								if (WaveCount.Equals(1))
								{
									Mop_delay_ADD += 8f;
								}
								else
								{
									Mop_delay_ADD += 8 + WaveCount * 3;
								}
								Group_count = Random.Range(6, 12);
							}
							else
							{
								if (Group_Arrange_value.Equals(0f))
								{
									OBJ_Pool.Make_Enemy(Group_ID, new Vector3(Normal_Enemy_Spawn.position.x + Random.Range(-1f, 1f), Normal_Enemy_Spawn.position.y + Random.Range(-1.5f, 1f), 0f), BOSS_FIGHT, Now_Data.me.LV);
								}
								else
								{
									OBJ_Pool.Make_Enemy(Group_ID, new Vector3(Normal_Enemy_Spawn.position.x, 0f - (float)Group_count_check * Group_Arrange_value - 2.25f, 0f), BOSS_FIGHT, Now_Data.me.LV);
								}
								Group_count_check++;
							}
						}
					}
				}
				BoxFairy_BOY_time_NOW += 0.05f;
				if (BoxFairy_BOY_time_NOW >= BoxFairy_BOY_time)
				{
					if (!BoxFairy_BOY.gameObject.activeSelf)
					{
						BoxFairy_BOY.Setting(Random.Range(0, 4));
					}
					else
					{
						BoxFairy_BOY_Second.Setting(Random.Range(0, 4));
					}
					if (Now_Data.me.BoxFairy_PER_DOUBLE >= Random.Range(0f, 100f))
					{
						BoxFairy_BOY_time += 2f;
					}
					else
					{
						BoxFairy_BOY_time = BoxFairy_BOY_time_Origin;
						BoxFairy_BOY_time_NOW = Random.Range(1, 30);
					}
				}
				BoxFairy_MAN_time_NOW += 0.05f;
				if (BoxFairy_MAN_time_NOW >= BoxFairy_MAN_time)
				{
					if (!BoxFairy_MAN.gameObject.activeSelf)
					{
						BoxFairy_MAN.Setting(Random.Range(4, 8));
					}
					else
					{
						BoxFairy_MAN_Second.Setting(Random.Range(4, 8));
					}
					if (Now_Data.me.BoxFairy_PER_DOUBLE >= Random.Range(0f, 100f))
					{
						BoxFairy_MAN_time += 2f;
					}
					else
					{
						BoxFairy_MAN_time = BoxFairy_MAN_time_Origin;
						BoxFairy_MAN_time_NOW = Random.Range(1, 60);
					}
				}
			}
			else if (stage_type.Equals(Stage_Type.Dungeon_A))
			{
				GameOver_time_check -= 0.05f * Game_Speed;
				UI_Master.me.Stage_label.text = Time_Checker.ShowTime_Label_noT(GameOver_time_check);
				if (GameOver_time_check <= 0f)
				{
					if (SPECIAL_BOSS_A)
					{
						Stage_Fail();
					}
					else
					{
						UI_Master.me.Stage_label.text = Localization.Get("GAMEOVER");
						UI_Master.me.Dungeon_Clear_Popup.Setting(false);
					}
				}
			}
			else if (stage_type.Equals(Stage_Type.Dungeon_D))
			{
				GameOver_time_check -= 0.05f * Game_Speed;
				UI_Master.me.Stage_label.text = Time_Checker.ShowTime_Label_noT(GameOver_time_check);
				if (GameOver_time_check <= 0f)
				{
					if (SPECIAL_BOSS_D)
					{
						Stage_Fail();
					}
					else
					{
						UI_Master.me.Stage_label.text = Localization.Get("GAMEOVER");
						UI_Master.me.Dungeon_Clear_Popup.Setting(false);
					}
				}
			}
			else if (stage_type.Equals(Stage_Type.Dungeon_E))
			{
				Dungeon_E_Time -= 0.05f * Game_Speed;
				UI_Master.me.Stage_label.text = Time_Checker.ShowTime_Label_noT(Dungeon_E_Time);
				if (Dungeon_E_Time <= 0f)
				{
					UI_Master.me.Stage_label.text = Localization.Get("GAMEOVER");
					UI_Master.me.Dungeon_Clear_Popup.AGAIN_Setting();
				}
			}
			Playetime_for_10sec += 0.05f;
			if (Playetime_for_10sec >= 1f)
			{
				Playetime_for_10sec = 0f;
				Now_Data.me.ALL_PLAYTIME += (BigInteger)1;
				Now_Data.me.NOW_PLAYTIME += (BigInteger)1;
				if (Now_Data.me.NOW_PLAYTIME % 60 == 0)
				{
					Security.SetString("ALL_PLAYTIME", Now_Data.me.ALL_PLAYTIME.ToString());
					Security.SetString("NOW_PLAYTIME", Now_Data.me.NOW_PLAYTIME.ToString());
					Security.SetFloat("DIABLO_LAPTIME", Now_Data.me.DIABLO_LAPTIME);
					Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_PLAYTIME);
				}
				for (int i = 0; i < Now_Data.me.sub_quest_count; i++)
				{
					UI_Master.me.sub_quest_ui[i].Txt_Update(Quest_Goal_Type.NOW_PLAYTIME);
				}
			}
		}
		if (!Rare_Portal_Possible)
		{
			Rae_Portal_time_NOW += 0.05f * me.Game_Speed;
			if (Rae_Portal_time_NOW >= Rae_Portal_time)
			{
				Rae_Portal_time_NOW = Random.Range(0, 120);
				Rare_Portal_Possible = true;
			}
		}
		for (int j = 0; j < Now_Data.me.Building_LV.Length; j++)
		{
			if (Now_Data.me.Building_LV[j] > 0 && !Now_Data.me.Buidling_Woriking_Possible[j])
			{
				Now_Data.me.Buidling_Working_time_Check[j] -= 0.05f;
				if (Now_Data.me.Buidling_Working_time_Check[j] <= 0f)
				{
					Now_Data.me.Buidling_Woriking_Possible[j] = true;
					Security.SetFloat(string.Format("Buidling_Working_time_Check_{0:000}", j), 0f);
					UI_Master.me.Dungeon_Alram.SetActive(true);
				}
			}
		}
		if (time_save_Check < 10f)
		{
			time_save_Check += 0.05f * me.Game_Speed;
			return;
		}
		time_save_Check = 0f;
		if (U_time_leader.me.Time_reading)
		{
			Time_Checker.Time_Save("LAST_PLAY_TIME");
			Now_Data.me.Buff_Time_Save(0f);
		}
		for (int k = 0; k < Now_Data.me.Building_LV.Length; k++)
		{
			if (Now_Data.me.Building_LV[k] > 0)
			{
				Security.SetFloat(string.Format("Buidling_Working_time_Check_{0:000}", k), Now_Data.me.Buidling_Working_time_Check[k]);
			}
		}
		Monuet_Check++;
		if (Monuet_Check >= 6)
		{
			Monuet_Check = 0;
			EVENT_CANDY_Possible += Random.Range(1, 4);
			Ten_Minuet_Check++;
			if (Ten_Minuet_Check >= 5)
			{
				Ten_Minuet_Check = 0;
				EVENT_CANDY_Possible = Random.Range(12, 32);
			}
		}
	}

	public void Wave_Start(bool Theme_check)
	{
		int num = Now_Data.me.LV / 10;
		if (num < 100)
		{
			Now_Data.me.NOW_Theme = Monster_DB.me.stage_DB[num].Theme;
		}
		else
		{
			Now_Data.me.NOW_Theme = Random.Range(1, 5);
		}
		if (!Now_Data.me.NOW_STAGE.Equals(num))
		{
			Now_Data.me.NOW_STAGE = num;
			if (Theme_check)
			{
				StartCoroutine(Themechange(true));
				if (Now_Data.me.Stage_Skip > 0)
				{
					UI_Master.me.Good_MSG(Localization.Get("SKIP_STAGE"));
					Now_Data.me.LV += Now_Data.me.Stage_Skip;
				}
			}
			else
			{
				StartCoroutine(Themechange(false));
			}
		}
		else
		{
			Now_Data.me.NOW_STAGE = num;
			StartCoroutine(Themechange(false));
		}
	}

	public void REAL_START(bool Change)
	{
		stage_type = Stage_Type.Normal;
		theme_number = Now_Data.me.LV % 1000;
		if (theme_number >= 300 && theme_number < 310)
		{
			SPECIAL_BOSS_A = true;
			stage_Background[0].SetActive(false);
			stage_Background[1].SetActive(true);
		}
		else if (theme_number >= 600 && theme_number < 610)
		{
			SPECIAL_BOSS_B = true;
			stage_Background[0].SetActive(false);
			stage_Background[1].SetActive(true);
		}
		else if (theme_number >= 0 && theme_number < 10)
		{
			SPECIAL_BOSS_D = true;
			stage_Background[0].SetActive(false);
			stage_Background[1].SetActive(true);
		}
		else
		{
			stage_Background[0].SetActive(true);
			stage_Background[1].SetActive(false);
			for (int i = 0; i < Normal_theme.Length; i++)
			{
				Normal_theme[i].SetActive(false);
			}
			if (Change)
			{
				if (Random.Range(0, 10) < 5)
				{
					BG_Number = 4;
				}
				else
				{
					BG_Number = (Now_Data.me.NOW_STAGE - 1) % 4;
				}
			}
			Normal_theme[BG_Number].SetActive(true);
		}
		SoundManager.me.Congretu();
		Mop_delay = 0.05f;
		Mop_delay_Check = 0f;
		BOSS_FIGHT = false;
		SPECIAL_BOSS_A = false;
		SPECIAL_BOSS_B = false;
		SPECIAL_BOSS_D = false;
		BOSS_BTN.SetActive(false);
		WaveCount = 0;
		Mop_delay_ADD = 0f;
		OBJ_Pool.Make_Portal(22, Portal_Spawn.position, true, Now_Data.me.LV);
		if ((Now_Data.me.LV % 10).Equals(9))
		{
			if (Now_Data.me.BOSS_Failed)
			{
				BOSS_BTN.SetActive(true);
			}
			else
			{
				BOSS_FIGHT = true;
				if ((Now_Data.me.LV % 1000).Equals(309))
				{
					SPECIAL_BOSS_A = true;
				}
				else if ((Now_Data.me.LV % 1000).Equals(609))
				{
					SPECIAL_BOSS_B = true;
				}
				else if ((Now_Data.me.LV % 1000).Equals(9))
				{
					SPECIAL_BOSS_D = true;
				}
			}
		}
		if (BOSS_FIGHT)
		{
			for (int j = 0; j < me.Live_Enemies.Count; j++)
			{
				me.Live_Enemies[j].Remove();
			}
			me.Live_Enemies.Clear();
			if (SPECIAL_BOSS_A)
			{
				Givup_BTN.SetActive(true);
				BOSS_BTN.SetActive(false);
				for (int k = 0; k < stage_Background.Length; k++)
				{
					stage_Background[k].SetActive(false);
				}
				stage_Background[1].SetActive(true);
				stage_type = Stage_Type.Dungeon_A;
				GameOver_time_check = 30f;
				OBJ_Pool.me.Dungeon_Boss_A[0].Birth_Dungeon_A_Boss(Now_Data.me.LV);
				float num = 1f;
				for (int l = 0; l < Now_Data.me.Now_Unit_LV.Length; l++)
				{
					if (Now_Data.me.Now_Unit_LV[l] > 0)
					{
						num += 1f;
					}
				}
				OBJ_Pool.me.Dungeon_Boss_A[0].boss_data.shot_time_check = 0f;
				OBJ_Pool.me.Dungeon_Boss_A[0].boss_data.Kidnap_Time = 30f / num;
				OBJ_Pool.me.Dungeon_Boss_A[0].gameObject.SetActive(true);
				OBJ_Pool.me.Dungeon_Boss_A[0].DUNGEON_BOSS_FIELD = true;
				GS = GameState.Play;
			}
			else if (SPECIAL_BOSS_B)
			{
				Givup_BTN.SetActive(true);
				BOSS_BTN.SetActive(false);
				for (int m = 0; m < stage_Background.Length; m++)
				{
					stage_Background[m].SetActive(false);
				}
				stage_Background[1].SetActive(true);
				stage_type = Stage_Type.Dungeon_B;
				for (int n = 0; n < OBJ_Pool.me.Dungeon_Boss_B.Length; n++)
				{
					OBJ_Pool.me.Dungeon_Boss_B[n].ThisTransform.position = new Vector3(10f + (float)n * 3.5f, -3.7f, 0f);
					OBJ_Pool.me.Dungeon_Boss_B[n].Birth_Dungeon_A_Boss(Now_Data.me.LV);
					OBJ_Pool.me.Dungeon_Boss_B[n].Basic_Data.HP /= (BigInteger)4;
					OBJ_Pool.me.Dungeon_Boss_B[n].HP = OBJ_Pool.me.Dungeon_Boss_B[n].Basic_Data.HP;
					OBJ_Pool.me.Dungeon_Boss_B[n].Changed_Speed = 50f;
					OBJ_Pool.me.Dungeon_Boss_B[n].HP_bar.gameObject.SetActive(false);
					OBJ_Pool.me.Dungeon_Boss_B[n].HP_text.gameObject.SetActive(false);
					OBJ_Pool.me.Dungeon_Boss_B[n].DUNGEON_BOSS_FIELD = true;
					OBJ_Pool.me.Dungeon_Boss_B[n].gameObject.SetActive(true);
				}
				UI_Master.me.Stage_label.text = string.Format("{0}/5", Live_Enemies.Count);
				GS = GameState.Play;
			}
			else if (SPECIAL_BOSS_D)
			{
				for (int num2 = 0; num2 < stage_Background.Length; num2++)
				{
					stage_Background[num2].SetActive(false);
				}
				stage_Background[1].SetActive(true);
				Givup_BTN.SetActive(true);
				BOSS_BTN.SetActive(false);
				SoundManager.me.BGM_Play(2);
				stage_type = Stage_Type.Dungeon_D;
				GameOver_time_check = 180f;
				OBJ_Pool.me.Dungeon_Boss_D.Birth_Dungeon_A_Boss(Now_Data.me.LV);
				OBJ_Pool.me.Dungeon_Boss_D.Basic_Data.HP *= (BigInteger)10;
				OBJ_Pool.me.Dungeon_Boss_D.HP *= (BigInteger)10;
				for (int num3 = 0; num3 < 3; num3++)
				{
					OBJ_Pool.me.Dungeon_Boss_D.boss_data.Parts_Broken_ING[num3] = false;
					OBJ_Pool.me.Dungeon_Boss_D.boss_data.Parts[num3].SetActive(true);
					OBJ_Pool.me.Dungeon_Boss_D.boss_data.Skill_Time_Check[num3] = 100f;
				}
				OBJ_Pool.me.Dungeon_Boss_D.boss_data.Parts_LV = 3;
				OBJ_Pool.me.Dungeon_Boss_D.DUNGEON_BOSS_FIELD = true;
				OBJ_Pool.me.Dungeon_Boss_D.gameObject.SetActive(true);
				if ((float)Random.Range(0, 100) < Now_Data.me.Diablo_Hurt_PER)
				{
					OBJ_Pool.me.Dungeon_Boss_D.HP = OBJ_Pool.me.Dungeon_Boss_D.Basic_Data.HP / 4 * 3;
				}
				GS = GameState.Play;
			}
			else
			{
				Target_Mop_count = 1;
				UI_Master.me.Stage_label.text = string.Format("STAGE {0} - BOSS", Now_Data.me.LV - 9);
				OBJ_Pool.Make_Enemy(Monster_DB.me.Monster_Groups[Now_Data.me.NOW_Theme].Boss_ID, Portal_Spawn.position, true, Now_Data.me.LV);
			}
		}
		else
		{
			UI_Master.me.Stage_label.text = string.Format("STAGE {0}", Now_Data.me.LV - 9);
		}
		UI_Master.me.Stage_label.gameObject.SetActive(false);
		UI_Master.me.Stage_label.gameObject.SetActive(true);
		if (Tutorial_Manager.me != null && Tutorial_Manager.me.gameObject.activeSelf && Now_Data.me.LV >= 110 && Tutorial_Manager.me.Dungeon_LOCK.activeSelf)
		{
			Tutorial_Manager.me.Set_Dungeon_Tuto();
		}
	}

	public IEnumerator Themechange(bool Change)
	{
		if (Change)
		{
			Speed_FadeIN.SetActive(false);
			Speed_FadeIN.SetActive(true);
			yield return new WaitForSeconds(0.5f);
		}
		else
		{
			yield return new WaitForSeconds(0.1f);
		}
		REAL_START(Change);
		yield return new WaitForSeconds(5.5f);
	}

	public void Boss_Again()
	{
		Now_Data.me.BOSS_Failed = false;
		for (int i = 0; i < Live_Enemies.Count; i++)
		{
			OBJ_Pool.Make_OBJ(OBJ_Pool.me.DeadSpill, ref OBJ_Pool.DeadSpill_Number, Live_Enemies[i].ThisTransform.position);
			Live_Enemies[i].Remove();
		}
		Live_Enemies.Clear();
		Misile_Clear();
		Wave_Start(false);
	}

	public void All_KILL()
	{
		CameraShake_Long();
		SoundManager.me.GameOver();
		Player.gameObject.SetActive(false);
		OBJ_Pool.Make_OBJ(OBJ_Pool.me.Player_DeadSpill, ref OBJ_Pool.Player_DeadSpill_Number, Player.ThisTransform.position);
		for (int i = 0; i < Units.Length; i++)
		{
			if (Units[i].gameObject.activeSelf)
			{
				OBJ_Pool.Make_OBJ(OBJ_Pool.me.effect_boom, ref OBJ_Pool.effect_boom_Number, Units[i].ThisTransform.position);
				if (Units[i].ThisTransform.position.y < 0f)
				{
					OBJ_Pool.Make_OBJ(OBJ_Pool.me.Player_DeadSpill, ref OBJ_Pool.Player_DeadSpill_Number, Units[i].ThisTransform.position);
				}
				Units[i].gameObject.SetActive(false);
			}
		}
	}

	public void Stage_Fail()
	{
		if (!GS.Equals(GameState.End))
		{
			GS = GameState.End;
			Misile_Clear();
			All_KILL();
			UI_Master.me.MainIcon_Set.transform.DOLocalMoveX(-330f, 0.5f);
			if (BOSS_FIGHT)
			{
				BOSS_FIGHT = false;
				Now_Data.me.BOSS_Failed = true;
			}
			Time.timeScale = 3f;
			StartCoroutine(NEW_START());
		}
	}

	public void Dropport_Animation_DO()
	{
		if (!GS.Equals(GameState.End))
		{
			GS = GameState.End;
			Misile_Clear();
			Player.gameObject.SetActive(false);
			Drop_Port_anim.SetActive(true);
			UI_Master.me.MainIcon_Set.transform.DOLocalMoveX(-330f, 0.5f);
			if (BOSS_FIGHT)
			{
				BOSS_FIGHT = false;
				Now_Data.me.BOSS_Failed = true;
			}
			Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		}
	}

	public void NEW_Start_by_d()
	{
		StartCoroutine(NEW_START_by_Drop_Port());
	}

	public IEnumerator NEW_START_by_Drop_Port()
	{
		UI_Master.me.Dropport_FADE.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		me.Drop_Port_anim.SetActive(false);
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		if (Now_Data.me.BOSS_Failed)
		{
			BOSS_BTN.SetActive(true);
		}
		Stage_Close();
		yield return new WaitForSeconds(1.5f);
		UI_Master.me.MainIcon_Set.transform.DOLocalMoveX(54f, 0.5f);
		for (int i = 0; i < OBJ_Pool.me.Player_DeadSpill.Length; i++)
		{
			OBJ_Pool.me.Player_DeadSpill[i].SetActive(false);
		}
		All_setting();
		UI_Master.me.Dropport_FADE.SetActive(false);
		UI_Master.me.FadeIN.SetActive(false);
		me.stage_Background[0].SetActive(true);
		Wave_Start(false);
		yield return new WaitForSeconds(0.1f);
		GS = GameState.Play;
		UI_Master.me.Good_MSG(Localization.Get("GET_GAS_BY_DROPPORT"));
	}

	public IEnumerator NEW_START()
	{
		yield return new WaitForSeconds(2.5f);
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		UI_Master.me.MainIcon_Set.transform.DOLocalMoveX(54f, 0.5f);
		if (Now_Data.me.BOSS_Failed)
		{
			BOSS_BTN.SetActive(true);
		}
		Stage_Close();
		UI_Master.me.GameOver_FADE.SetActive(true);
		me.stage_Background[0].SetActive(true);
		UI_Master.me.GameOver_txt.transform.DOShakePosition(0.3f, 50f, 30, 180f).OnComplete(Shake_END);
		yield return new WaitForSeconds(1.5f);
		for (int i = 0; i < OBJ_Pool.me.Player_DeadSpill.Length; i++)
		{
			OBJ_Pool.me.Player_DeadSpill[i].SetActive(false);
		}
		All_setting();
		UI_Master.me.GameOver_FADE.SetActive(false);
		UI_Master.me.FadeIN.SetActive(false);
		Wave_Start(false);
		yield return new WaitForSeconds(0.1f);
		GS = GameState.Play;
	}

	public void Shake_END()
	{
		UI_Master.me.GameOver_txt.transform.localPosition = new Vector3(0f, -222f, 0f);
	}

	public void Next_LV_START()
	{
		if (!Now_Data.me.BOSS_Failed)
		{
			Now_Data.me.LV++;
			Security.SetInt("LV", Now_Data.me.LV);
			Security.SetFloat("DIABLO_LAPTIME", Now_Data.me.DIABLO_LAPTIME);
			if (Now_Data.me.LV > Now_Data.me.BEST_LV)
			{
				Now_Data.me.BEST_LV = Now_Data.me.LV;
				if (Random.Range(0, 10) < 2)
				{
					UM_GameService.me.ReportScore(Now_Data.me.BEST_LV);
				}
				Security.SetInt("BEST_LV", Now_Data.me.BEST_LV);
				Now_Data.me.Check_Possible(Quest_Goal_Type.BEST_LV);
				if (Now_Data.me.LV.Equals(151))
				{
					SAVE_POPUP_ID = 18;
				}
				else if (Now_Data.me.LV.Equals(351))
				{
					SAVE_POPUP_ID = 19;
				}
				else if (Now_Data.me.LV.Equals(501))
				{
					SAVE_POPUP_ID = 20;
				}
				if (Now_Data.me.BEST_LV > 210)
				{
					UI_Master.me.Speed_BTN.SetActive(true);
				}
			}
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("MUSTBOSS"));
			BOSS_BTN.SetActive(false);
			BOSS_BTN.SetActive(true);
		}
		Wave_Start(true);
	}

	public void Change_Hold()
	{
		ONE_or_DDEA = true;
	}

	public void Change_to_Yunsok()
	{
		ONE_or_DDEA = false;
		Mop_delay = 1f;
	}

	public void CameraShake()
	{
		BeforeShake();
		WorldMap_Camera.DOShakePosition(0.1f, 0.2f, 30, 180f, false).OnComplete(BeforeShake);
	}

	public void CameraShake_Long()
	{
		BeforeShake();
		WorldMap_Camera.DOShakePosition(0.2f, 0.3f, 30, 180f, false).OnComplete(BeforeShake);
	}

	public void CameraShake_LongLong()
	{
		BeforeShake();
		SoundManager.me.Warning();
		WorldMap_Camera.DOShakePosition(1f, 0.6f, 30, 180f, false).OnComplete(BeforeShake);
	}

	public void BeforeShake()
	{
		UI_Master.me.screenChange_by_ratio.Setting();
	}

	public void Again()
	{
		Application.LoadLevel("#Basic_Scene");
	}

	public void All_setting()
	{
		Now_Data.me.Status_Chogihwa();
		Now_Data.me.All_Buff_Counting();
		Time.timeScale = Now_Data.me.Changed_GameSpeed + Now_Data.me.IAP_GameSpeed;
		Main_player.Status_Setting();
		Main_player.gameObject.SetActive(true);
		StartCoroutine(Unit_Open());
	}

	public IEnumerator Unit_Open()
	{
		if (Now_Data.me.Now_Unit_LV[16] > 0)
		{
			UI_Master.me.screenChange_by_ratio.Main_size = 7.5f;
		}
		else if (Now_Data.me.Now_Unit_LV[7] > 0)
		{
			UI_Master.me.screenChange_by_ratio.Main_size = 7f;
		}
		else if (Now_Data.me.Now_Unit_LV[4] > 0)
		{
			UI_Master.me.screenChange_by_ratio.Main_size = 6.5f;
		}
		UI_Master.me.screenChange_by_ratio.Setting();
		for (int i = 0; i < Units.Length; i++)
		{
			Unit_Setting(i);
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void Unit_Setting(int ID)
	{
		if (Now_Data.me.Now_Unit_LV[ID] > 0 && !stage_type.Equals(Stage_Type.Dungeon_C))
		{
			if (!Units[ID].gameObject.activeSelf)
			{
				Units[ID].Birth_Unit(ID);
			}
			Units[ID].Status_Setting();
		}
		Security.SetInt(string.Format("Now_Unit_LV_{0:000}", ID), Now_Data.me.Now_Unit_LV[ID]);
	}

	public void Stage_Close()
	{
		GS = GameState.End;
		Player.S_animator.Stop();
		UI_Master.me.FadeOut.SetActive(true);
		for (int i = 0; i < Live_Enemies.Count; i++)
		{
			Live_Enemies[i].Remove();
		}
		Live_Enemies.Clear();
		for (int j = 0; j < stage_Background.Length; j++)
		{
			stage_Background[j].SetActive(false);
		}
		for (int k = 0; k < OBJ_Pool.me.DeadSpill.Length; k++)
		{
			OBJ_Pool.me.DeadSpill[k].SetActive(false);
		}
		for (int l = 0; l < OBJ_Pool.me.BIG_DeadSpill.Length; l++)
		{
			OBJ_Pool.me.BIG_DeadSpill[l].SetActive(false);
		}
		for (int m = 0; m < OBJ_Pool.me.Player_DeadSpill.Length; m++)
		{
			OBJ_Pool.me.Player_DeadSpill[m].SetActive(false);
		}
		Misile_Clear();
	}

	public void Misile_Clear()
	{
		for (int i = 0; i < OBJ_Pool.me.Fireball_misile.Length; i++)
		{
			OBJ_Pool.me.Fireball_misile[i].Exit();
		}
		for (int j = 0; j < OBJ_Pool.me.Small_misile_misile.Length; j++)
		{
			OBJ_Pool.me.Small_misile_misile[j].Exit();
		}
		for (int k = 0; k < OBJ_Pool.me.Big_misile_misile.Length; k++)
		{
			OBJ_Pool.me.Big_misile_misile[k].Exit();
		}
	}

	public void Flash()
	{
		Flash_obj.SetActive(false);
		Flash_obj.SetActive(true);
	}

	public void PLAY_TO_NORMAL_STAGE_Setting()
	{
		Stage_Close();
		stage_Background[0].SetActive(true);
		Skill_Icons.SetActive(true);
		All_setting();
		if (Now_Data.me.LV > 20)
		{
			for (int i = 0; i < Now_Data.me.sub_quest_count; i++)
			{
				UI_Master.me.sub_quest_ui[i].gameObject.SetActive(true);
			}
		}
		stage_type = Stage_Type.Normal;
		GS = GameState.Play;
		Wave_Start(false);
		UI_Master.me.MainIcon_Set.transform.DOLocalMoveX(54f, 0.5f);
		Givup_BTN.SetActive(false);
		SoundManager.me.BGM_Play(0);
		if (Now_Data.me.LV.Equals(10))
		{
			StartCoroutine(F_Tuto());
		}
	}

	public void REVIEW_CHECK()
	{
		StartCoroutine(REVIEW_OPEN());
	}

	public IEnumerator REVIEW_OPEN()
	{
		yield return new WaitForSeconds(0.5f);
		SoundManager.me.Portal_Die();
		UI_Master.me.Popup(UI_Master.me.REVIEW_ASK_Popup);
	}

	public IEnumerator F_Tuto()
	{
		yield return new WaitForSeconds(0.5f);
		if (Tutorial_Manager.me != null && Tutorial_Manager.me.CHECK && Now_Data.me.ALL_DROPPORT < 1)
		{
			Tutorial_Manager.me.Tutorial_IMGs[7].SetActive(true);
		}
	}

	public void PLAY_TO_Dungeon_Setting()
	{
		UI_Master.me.MainIcon_Set.transform.DOLocalMoveX(-330f, 0.5f);
		Givup_BTN.SetActive(true);
		BOSS_BTN.SetActive(false);
		for (int i = 0; i < UI_Master.me.sub_quest_ui.Length; i++)
		{
			UI_Master.me.sub_quest_ui[i].gameObject.SetActive(false);
		}
		BoxFairy_BOY.gameObject.SetActive(false);
		BoxFairy_BOY_Second.gameObject.SetActive(false);
		BoxFairy_MAN.gameObject.SetActive(false);
		BoxFairy_MAN_Second.gameObject.SetActive(false);
		SoundManager.me.BGM_Play(1);
		if (Now_Data.me.NOW_Selected_Dungeon.Equals(-1))
		{
			Givup_BTN.SetActive(false);
			PLAY_TO_Dungen_E_Setting();
			return;
		}
		switch (Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].Dungeon_TYPE)
		{
		case 1:
			PLAY_TO_Dungen_A_Setting();
			break;
		case 2:
			PLAY_TO_Dungen_B_Setting();
			break;
		case 3:
			PLAY_TO_Dungen_C_Setting();
			break;
		case 4:
			PLAY_TO_Dungen_D_Setting();
			break;
		case 5:
			PLAY_TO_Dungen_E_Setting();
			break;
		}
	}

	public void PLAY_TO_Dungen_A_Setting()
	{
		Stage_Close();
		stage_Background[1].SetActive(true);
		stage_type = Stage_Type.Dungeon_A;
		All_setting();
		GameOver_time_check = 30f;
		if (Now_Data.me.NOW_DUNGEON_CHAPTER.Equals(1))
		{
			OBJ_Pool.me.Dungeon_Boss_A[0].Birth_Dungeon_A_Boss(Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].StageNumber_for_HP);
		}
		else
		{
			OBJ_Pool.me.Dungeon_Boss_A[0].Birth_Dungeon_A_Boss(Dungeon_DB.me.dungeon_DB[Dungeon_DB.me.dungeon_DB.Length - 1].StageNumber_for_HP * (Now_Data.me.NOW_DUNGEON_CHAPTER - 1) + Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].StageNumber_for_HP);
		}
		float num = 1f;
		for (int i = 0; i < Now_Data.me.Now_Unit_LV.Length; i++)
		{
			if (Now_Data.me.Now_Unit_LV[i] > 0)
			{
				num += 1f;
			}
		}
		OBJ_Pool.me.Dungeon_Boss_A[0].boss_data.shot_time_check = 0f;
		OBJ_Pool.me.Dungeon_Boss_A[0].boss_data.Kidnap_Time = 30f / num;
		OBJ_Pool.me.Dungeon_Boss_A[0].DUNGEON_BOSS_FIELD = false;
		OBJ_Pool.me.Dungeon_Boss_A[0].gameObject.SetActive(true);
		GS = GameState.Play;
	}

	public void PLAY_TO_Dungen_B_Setting()
	{
		Stage_Close();
		stage_Background[1].SetActive(true);
		stage_type = Stage_Type.Dungeon_B;
		All_setting();
		for (int i = 0; i < OBJ_Pool.me.Dungeon_Boss_B.Length; i++)
		{
			OBJ_Pool.me.Dungeon_Boss_B[i].ThisTransform.position = new Vector3(10f + (float)i * 3.5f, -3.7f, 0f);
			if (Now_Data.me.NOW_DUNGEON_CHAPTER.Equals(1))
			{
				OBJ_Pool.me.Dungeon_Boss_B[i].Birth_Dungeon_A_Boss(Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].StageNumber_for_HP);
			}
			else
			{
				OBJ_Pool.me.Dungeon_Boss_B[i].Birth_Dungeon_A_Boss(Dungeon_DB.me.dungeon_DB[Dungeon_DB.me.dungeon_DB.Length - 1].StageNumber_for_HP * (Now_Data.me.NOW_DUNGEON_CHAPTER - 1) + Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].StageNumber_for_HP);
			}
			OBJ_Pool.me.Dungeon_Boss_B[i].Basic_Data.HP /= (BigInteger)4;
			OBJ_Pool.me.Dungeon_Boss_B[i].HP = OBJ_Pool.me.Dungeon_Boss_B[i].Basic_Data.HP;
			OBJ_Pool.me.Dungeon_Boss_B[i].Changed_Speed = 50f;
			OBJ_Pool.me.Dungeon_Boss_B[i].HP_bar.gameObject.SetActive(false);
			OBJ_Pool.me.Dungeon_Boss_B[i].HP_text.gameObject.SetActive(false);
			OBJ_Pool.me.Dungeon_Boss_B[i].DUNGEON_BOSS_FIELD = false;
			OBJ_Pool.me.Dungeon_Boss_B[i].gameObject.SetActive(true);
		}
		UI_Master.me.Stage_label.text = string.Format("{0}/5", Live_Enemies.Count);
		GS = GameState.Play;
	}

	public void PLAY_TO_Dungen_C_Setting()
	{
		Stage_Close();
		stage_Background[3].SetActive(true);
		Dungeon_C_BG.localPosition = new Vector3(0f, 0.6f, 0f);
		Dungeon_C_monster_Kill_Count = 0;
		if (Now_Data.me.NOW_DUNGEON_CHAPTER.Equals(1))
		{
			Dungeon_C_Monster_LV = Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].StageNumber_for_HP;
		}
		else
		{
			Dungeon_C_Monster_LV = Dungeon_DB.me.dungeon_DB[Dungeon_DB.me.dungeon_DB.Length - 1].StageNumber_for_HP * (Now_Data.me.NOW_DUNGEON_CHAPTER - 1) + Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].StageNumber_for_HP;
		}
		stage_type = Stage_Type.Dungeon_C;
		for (int i = 0; i < Units.Length; i++)
		{
			Units[i].gameObject.SetActive(false);
		}
		Dungeon_C_monster_Kill_Count = 100;
		UI_Master.me.Stage_label.text = string.Format("{0}/100", Dungeon_C_monster_Kill_Count);
		GS = GameState.Play;
		Player.S_animator.Move();
	}

	public void PLAY_TO_Dungen_D_Setting()
	{
		SoundManager.me.BGM_Play(2);
		Stage_Close();
		stage_Background[4].SetActive(true);
		stage_type = Stage_Type.Dungeon_D;
		All_setting();
		GameOver_time_check = 180f;
		if (Now_Data.me.NOW_DUNGEON_CHAPTER.Equals(1))
		{
			OBJ_Pool.me.Dungeon_Boss_D.Birth_Dungeon_A_Boss(Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].StageNumber_for_HP + 100);
		}
		else
		{
			OBJ_Pool.me.Dungeon_Boss_D.Birth_Dungeon_A_Boss((Dungeon_DB.me.dungeon_DB[Dungeon_DB.me.dungeon_DB.Length - 1].StageNumber_for_HP + 100) * (Now_Data.me.NOW_DUNGEON_CHAPTER - 1) + Dungeon_DB.me.dungeon_DB[Now_Data.me.NOW_Selected_Dungeon].StageNumber_for_HP);
		}
		OBJ_Pool.me.Dungeon_Boss_D.Basic_Data.HP *= (BigInteger)2;
		OBJ_Pool.me.Dungeon_Boss_D.HP *= (BigInteger)2;
		for (int i = 0; i < 3; i++)
		{
			OBJ_Pool.me.Dungeon_Boss_D.boss_data.Parts_Broken_ING[i] = false;
			OBJ_Pool.me.Dungeon_Boss_D.boss_data.Parts[i].SetActive(true);
			OBJ_Pool.me.Dungeon_Boss_D.boss_data.Skill_Time_Check[i] = 100f;
		}
		OBJ_Pool.me.Dungeon_Boss_D.boss_data.Parts_LV = 3;
		OBJ_Pool.me.Dungeon_Boss_D.DUNGEON_BOSS_FIELD = false;
		OBJ_Pool.me.Dungeon_Boss_D.gameObject.SetActive(true);
		if ((float)Random.Range(0, 100) < Now_Data.me.Diablo_Hurt_PER)
		{
			OBJ_Pool.me.Dungeon_Boss_D.HP = OBJ_Pool.me.Dungeon_Boss_D.Basic_Data.HP / 4 * 3;
		}
		GS = GameState.Play;
	}

	public void PLAY_TO_Dungen_E_Setting()
	{
		Dungeon_E_Time = 15f;
		if (Now_Data.me.Dungeon_E_Play_time > 0f)
		{
			Dungeon_E_Time = Dungeon_E_Time * (100f + Now_Data.me.Dungeon_E_Play_time) / 100f;
		}
		Dungeon_E_Hell_stone_Count = 0;
		Dungeon_E_Hell_stone_BOX_Count = 0;
		Stage_Close();
		stage_Background[3].SetActive(true);
		Dungeon_C_BG.localPosition = new Vector3(0f, 0f, 0f);
		for (int i = 0; i < OBJ_Pool.me.Dungeon_Boss_E.Length; i++)
		{
			OBJ_Pool.me.Dungeon_Boss_E[i].ThisTransform.localPosition = new Vector3(Random.Range(2, 30), -2.2f, 0f);
			OBJ_Pool.me.Dungeon_Boss_E[i].HP_bar.gameObject.SetActive(false);
			OBJ_Pool.me.Dungeon_Boss_E[i].HP_text.gameObject.SetActive(false);
			OBJ_Pool.me.Dungeon_Boss_E[i].gameObject.SetActive(true);
			OBJ_Pool.me.Dungeon_Boss_E[i].Changed_Speed = 150f;
			OBJ_Pool.me.Dungeon_Boss_E[i].Birth_Dungeon_A_Boss(1);
			OBJ_Pool.me.Dungeon_Boss_E[i].Basic_Data.HP = Random.Range(80, 100);
			OBJ_Pool.me.Dungeon_Boss_E[i].HP = OBJ_Pool.me.Dungeon_Boss_E[i].Basic_Data.HP;
		}
		stage_type = Stage_Type.Dungeon_E;
		for (int j = 0; j < Units.Length; j++)
		{
			Units[j].gameObject.SetActive(false);
		}
		UI_Master.me.Stage_label.text = string.Format("{0}", Time_Checker.ShowTime_Label_noT(Dungeon_E_Time));
		UI_Master.me.Dungeon_Clear_Popup.START_SETTING();
	}

	public void Giveup()
	{
		if (SPECIAL_BOSS_A || SPECIAL_BOSS_B || SPECIAL_BOSS_D)
		{
			Stage_Fail();
		}
		else
		{
			UI_Master.me.Dungeon_Clear_Popup.Setting(false);
		}
	}
}
