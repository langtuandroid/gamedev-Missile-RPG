using System.Collections;
using DG.Tweening;
using Keiwando.BigInteger;
using UnityEngine;

public class Dungeon_Boss : MonoBehaviour
{
	public Enemy enemy;

	public Stage_Type boss_type;

	public float shot_time_check;

	public GameObject Tongue;

	public int Parts_LV;

	public GameObject[] Parts;

	public bool[] Parts_Broken_ING;

	private float[] Skill_Time = new float[3] { 10f, 12f, 8f };

	public float[] Skill_Time_Check;

	public UISlider Skill_slider;

	public float Kidnap_Time = 10f;

	public int Target_ID;

	public Vector3 Attack_direction;

	public float arrow_direction;

	public float Delay = 10f;

	public float Go_speed = 0.3f;

	public Ease Go_Ease = Ease.Linear;

	public float Back_speed = 0.5f;

	public Ease Back_Ease = Ease.InCubic;

	public GameObject Emp_EF;

	public Sprite_Animator S_animator;

	public void FixedUpdate()
	{
		switch (boss_type)
		{
		case Stage_Type.Dungeon_A:
			shot_time_check += 0.05f * Fight_Master.me.Game_Speed;
			Skill_slider.value = (Kidnap_Time - shot_time_check) / Kidnap_Time;
			if (shot_time_check >= Kidnap_Time)
			{
				shot_time_check = 0f;
				Kidnap_Shot();
			}
			break;
		case Stage_Type.Dungeon_B:
			break;
		case Stage_Type.Dungeon_D:
		{
			for (int i = 0; i < Parts_Broken_ING.Length; i++)
			{
				if (Parts_Broken_ING[i])
				{
					Skill_Time_Check[i] += 0.05f * Fight_Master.me.Game_Speed;
					if (Skill_Time_Check[i] >= Skill_Time[i])
					{
						Skill_Time_Check[i] = 0f;
						Diablor_Skill(i);
					}
				}
			}
			break;
		}
		case Stage_Type.Dungeon_C:
			break;
		}
	}

	public void Kidnap_Shot()
	{
		enemy.Chat(Localization.Get("DUNGEON_BOSS_A_SKILL"));
		Target_ID = Random.Range(0, Fight_Master.me.Units.Length);
		int num = 100;
		bool flag = false;
		while (!flag)
		{
			if (Fight_Master.me.Units[Target_ID].gameObject.activeSelf)
			{
				flag = true;
				continue;
			}
			Target_ID = Random.Range(0, Fight_Master.me.Units.Length);
			num--;
			if (num >= 0)
			{
				continue;
			}
			break;
		}
		if (flag)
		{
			Attack_direction = base.transform.InverseTransformPoint(new Vector3(Fight_Master.me.Units[Target_ID].sprite_Renderer.transform.position.x, Fight_Master.me.Units[Target_ID].sprite_Renderer.transform.position.y - 1f, 0f));
			arrow_direction = Mathf.Atan2(Attack_direction.x, Attack_direction.y) * 57.29578f;
			Tongue.transform.rotation = Quaternion.Euler(0f, 0f, 0f - arrow_direction - 90f);
			GO_Tongue();
		}
	}

	public void Kill_CHECK()
	{
		if (!enemy.DUNGEON_BOSS_FIELD)
		{
			switch (boss_type)
			{
			case Stage_Type.Dungeon_A:
				if (Fight_Master.me.Live_Enemies.Count.Equals(0))
				{
					Debug.Log("던전보스잡음.");
					UI_Master.me.Dungeon_Clear_Popup.Setting(true);
				}
				break;
			case Stage_Type.Dungeon_B:
			{
				if (Fight_Master.me.Live_Enemies.Count.Equals(0))
				{
					Debug.Log("던전보스잡음.");
					UI_Master.me.Dungeon_Clear_Popup.Setting(true);
					break;
				}
				for (int i = 0; i < Fight_Master.me.Live_Enemies.Count; i++)
				{
					Fight_Master.me.Live_Enemies[i].boss_data.KnockBack();
				}
				break;
			}
			case Stage_Type.Dungeon_D:
				Debug.Log("던전보스잡음.");
				UI_Master.me.Dungeon_Clear_Popup.Setting(true);
				if (Now_Data.me.NOW_DUNGEON_CHAPTER < 3)
				{
					UM_GameService.me.ReportArchievement(string.Format("arch_0"));
				}
				break;
			case Stage_Type.Dungeon_C:
				break;
			}
			return;
		}
		bool flag = false;
		switch (boss_type)
		{
		case Stage_Type.Dungeon_A:
		case Stage_Type.Dungeon_D:
			flag = true;
			break;
		case Stage_Type.Dungeon_B:
		{
			if (Fight_Master.me.Live_Enemies.Count.Equals(0))
			{
				flag = true;
				break;
			}
			for (int j = 0; j < Fight_Master.me.Live_Enemies.Count; j++)
			{
				Fight_Master.me.Live_Enemies[j].boss_data.KnockBack();
			}
			break;
		}
		}
		if (flag)
		{
			SoundManager.me.BOSS_Die();
			OBJ_Pool.Make_OBJ(OBJ_Pool.me.BIG_DeadSpill, ref OBJ_Pool.BIG_DeadSpill_Number, enemy.ThisTransform.position);
			enemy.Drop_GOLD(Monster_DB.me.Monster_Gold_by_LV(26, Now_Data.me.LV, true) * (int)(Now_Data.me.GOLD_BONUS_PER_ALL + Now_Data.me.GOLD_BONUS_PER_BOSS + (float)Main_Player.me.Mineral_Bonus_ING) / 100, 20);
			if (Now_Data.me.Boss_Save_Gold > 0f)
			{
				UI_Master.me.Good_MSG(string.Format("{0}{1}{2}", Localization.Get("BOSS_DROP_GOLD_A"), Now_Data.me.Boss_Save_Gold, Localization.Get("BOSS_DROP_GOLD_B")));
				Now_Data.me.GoldChange(Now_Data.me.GOLD / 100 * (int)Now_Data.me.Boss_Save_Gold);
			}
			Now_Data.me.NOW_BOSSKILL += (BigInteger)1;
			Now_Data.me.ALL_BOSSKILL += (BigInteger)1;
			Security.SetString("NOW_BOSSKILL", Now_Data.me.NOW_BOSSKILL.ToString());
			Security.SetString("ALL_BOSSKILL", Now_Data.me.ALL_BOSSKILL.ToString());
			Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_BOSSKILL);
			if (Now_Data.me.ALL_BOSSKILL < 6)
			{
				UM_GameService.me.ReportArchievement(string.Format("arch_{0}", Now_Data.me.ALL_BOSSKILL));
			}
			for (int k = 0; k < Now_Data.me.sub_quest_count; k++)
			{
				UI_Master.me.sub_quest_ui[k].Txt_Update(Quest_Goal_Type.NOW_BOSSKILL);
			}
			Fight_Master.me.stage_type = Stage_Type.Normal;
			Fight_Master.me.GS = GameState.Play;
			Fight_Master.me.Givup_BTN.SetActive(false);
			Fight_Master.me.Next_LV_START();
			UI_Master.me.MainIcon_Set.transform.DOLocalMoveX(54f, 0.5f);
			SoundManager.me.BGM_Play(0);
			if (Now_Data.me.BEST_LV.Equals(310))
			{
				Tutorial_Manager.me.Set_TIME_ATK_Tuto();
			}
			else if (boss_type.Equals(Stage_Type.Dungeon_A))
			{
				Fight_Master.me.All_setting();
			}
			else if (boss_type.Equals(Stage_Type.Dungeon_D))
			{
				UI_Master.me.box_Open_Panel.Setting_DIABLO_REWARD(Now_Data.me.LV / 1000);
				UI_Master.me.Good_MSG(Localization.Get("DIABLO_CLEAR_LAP"));
				Now_Data.me.DIABLO_LAPTIME *= 0.5f;
				Security.SetFloat("DIABLO_LAPTIME", Now_Data.me.DIABLO_LAPTIME);
			}
		}
	}

	public void GO_Tongue()
	{
		SoundManager.me.Tongue_Skill();
		Tongue.SetActive(true);
		float endValue = Vector2.Distance(Fight_Master.me.Units[Target_ID].sprite_Renderer.transform.position, Tongue.transform.position) * 0.05f;
		Tongue.transform.DOScaleX(endValue, Go_speed).SetEase(Go_Ease).OnComplete(Comeback_Tongue);
	}

	public void Comeback_Tongue()
	{
		Fight_Master.me.Units[Target_ID].ThisTransform.DOMove(Tongue.transform.position, Back_speed).SetEase(Back_Ease).OnComplete(END);
		Tongue.transform.DOScaleX(0.01f, Back_speed).SetEase(Back_Ease).OnComplete(Hide_Tongue);
	}

	public void Hide_Tongue()
	{
		Tongue.SetActive(false);
	}

	public void END()
	{
		Fight_Master.me.Units[Target_ID].gameObject.SetActive(false);
	}

	public void KnockBack()
	{
		UI_Master.me.Stage_label.text = string.Format("{0}/5", Fight_Master.me.Live_Enemies.Count);
		enemy.Changed_Speed += 50f;
	}

	public IEnumerator Stop()
	{
		yield return new WaitForSeconds(1f);
		enemy.rigidbody2D.velocity = Vector2.zero;
	}

	public void Parts_LV_Check()
	{
		if (Parts_LV > 0 && enemy.HP * 4 < enemy.Basic_Data.HP * Parts_LV)
		{
			Broken_Parts();
		}
	}

	public void Broken_Parts()
	{
		Parts_LV--;
		bool flag = false;
		int num = Random.Range(0, 3);
		while (!flag)
		{
			if (!Parts_Broken_ING[num])
			{
				flag = true;
				break;
			}
			num = Random.Range(0, 3);
		}
		Parts_Broken_ING[num] = true;
		Parts[num].gameObject.SetActive(false);
		if (Parts_LV.Equals(0))
		{
			Debug.Log("모든 파츠 박살.");
		}
		else
		{
			Debug.Log(num + "번 파츠 박살.");
		}
	}

	public void Diablor_Skill(int ID)
	{
		for (int i = 0; i < Skill_Time_Check.Length; i++)
		{
			if (Parts_Broken_ING[i] && i != ID)
			{
				Skill_Time_Check[i] -= 1f;
			}
		}
		switch (ID)
		{
		case 0:
			MAKE_WAVE();
			break;
		case 1:
			MAKE_EMP();
			break;
		case 2:
			MAKE_WALL();
			break;
		}
	}

	public void MAKE_WAVE()
	{
		Fight_Master.me.CameraShake();
		int num = Monster_DB.me.Monster_Groups[Now_Data.me.NOW_Theme].Monster_ID[Random.Range(0, Monster_DB.me.Monster_Groups[Now_Data.me.NOW_Theme].Monster_ID.Length)];
		int group_Avarage = Monster_DB.me.monster_DB[num].Group_Avarage;
		float num2 = 0f;
		num2 = ((!Monster_DB.me.monster_DB[num].Random_Position) ? (2.8f / (float)group_Avarage) : 0f);
		for (int i = 0; i < group_Avarage; i++)
		{
			if (num2.Equals(0f))
			{
				OBJ_Pool.Make_Enemy(num, new Vector3(Fight_Master.me.Normal_Enemy_Spawn.position.x + Random.Range(-1f, 1f), Random.Range(-5f, -4f), 0f), false, enemy.CH_LV);
			}
			else
			{
				OBJ_Pool.Make_Enemy(num, new Vector3(Fight_Master.me.Normal_Enemy_Spawn.position.x, 0f - (float)i * num2 - 5f, 0f), false, enemy.CH_LV);
			}
		}
	}

	public void MAKE_EMP()
	{
		Emp_EF.SetActive(false);
		Emp_EF.SetActive(true);
		Fight_Master.me.CameraShake();
		Fight_Master.me.Flash();
		Fight_Master.me.Misile_Clear();
	}

	public void MAKE_WALL()
	{
		Fight_Master.me.CameraShake();
		S_animator.Attack();
		OBJ_Pool.Make_Enemy(23, new Vector3(Fight_Master.me.Wall_Spawn.position.x + (float)Random.Range(0, 1), Fight_Master.me.Wall_Spawn.position.y, 0f), false, enemy.CH_LV);
	}
}
