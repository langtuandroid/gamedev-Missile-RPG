using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Box_Open_Panel : MonoBehaviour
{
	public UI2DSprite box_Sprite;

	public Transform ThisTransform;

	public GameObject Reward_Panel;

	public Reward_Icon[] reward_Icon;

	public GameObject First_Boxshot;

	public bool FIN;

	private int Target_Box_ID;

	public int Target_Diablo_ID;

	private bool DARKBOX;

	private float BEFORE_DIABLOR_RECORD;

	private float NOW_DIABLOR_RECORD;

	public bool[] Possible_REWARD;

	public bool SKIP;

	private int Get_Value;

	private int Get_ID;

	public List<int> Get_IDs = new List<int>();

	public GameObject Quit_BTN;

	public void Setting(int ID, bool Direct)
	{
		UI_Master.me.new_misile.GET_Misile_IDs.Clear();
		FIN = false;
		UI_Master.me.PAUSE();
		Target_Box_ID = 0;
		if (Direct)
		{
			Target_Box_ID = ID;
		}
		else
		{
			for (int i = 0; i < Now_Data.me.BOX_Count.Length; i++)
			{
				if (Now_Data.me.BOX_Count[i] > 0)
				{
					Target_Box_ID = i;
				}
			}
		}
		box_Sprite.sprite2D = Sprite_DB.me.BOX_Icon[Target_Box_ID];
		Now_Data.me.BOX_Count[Target_Box_ID]--;
		Security.SetInt(string.Format("BOX_Count_{0:000}", Target_Box_ID), Now_Data.me.BOX_Count[Target_Box_ID]);
		UI_Master.me.Box_Checking();
		if (Target_Box_ID.Equals(6))
		{
			DARKBOX = true;
			Target_Diablo_ID = Random.Range(0, 10);
			int num = 10000;
			while (Now_Data.me.Diablo_Artifact_Parts_count[Target_Diablo_ID] >= Artifact_DB.me.diablo_artifact_DB[Target_Diablo_ID].MAX_LV)
			{
				Target_Diablo_ID = Random.Range(0, 10);
				num--;
				if (num < 0)
				{
					DARKBOX = false;
					Debug.Log("디아유물수집완료인듯?");
					break;
				}
			}
			if (DARKBOX)
			{
				Now_Data.me.Diablo_Artifact_Parts_count[Target_Diablo_ID]++;
				Security.SetInt(string.Format("Diablo_Artifact_Parts_count_{0:000}", Target_Diablo_ID), Now_Data.me.Diablo_Artifact_Parts_count[Target_Diablo_ID]);
				if (Now_Data.me.Diablo_Artifact_LV[Target_Diablo_ID] < 1)
				{
					Now_Data.me.Diablo_Artifact_LV[Target_Diablo_ID] = 1;
					Security.SetInt(string.Format("Diablo_Artifact_LV_{0:000}", Target_Diablo_ID), Now_Data.me.Diablo_Artifact_LV[Target_Diablo_ID]);
				}
			}
		}
		else
		{
			DARKBOX = false;
		}
		if (!DARKBOX)
		{
			REWARD_GETTING(ID, Direct);
		}
		base.gameObject.SetActive(true);
		Reward_Panel.SetActive(false);
		First_Boxshot.SetActive(true);
		ThisTransform.DOLocalMoveY(500f, 0.5f).From(true).SetEase(Ease.InOutCirc)
			.OnComplete(Move_End);
	}

	public void REWARD_GETTING(int ID, bool Direct)
	{
		Get_IDs.Clear();
		Get_Value = Random.Range(Box_DB.me.box_DB[Target_Box_ID].Stone_Min, Box_DB.me.box_DB[Target_Box_ID].Stone_Max) * (int)(100f + Now_Data.me.Hellsteon_Bonus) / 100;
		reward_Icon[0].Setting_P_stone(Get_Value);
		Now_Data.me.P_STONE_Change(Get_Value);
		if (Box_DB.me.box_DB[Target_Box_ID].Rewadrd_A_ID_value != 0)
		{
			Get_ID = Selceted_ID(Box_DB.me.box_DB[Target_Box_ID].Rewadrd_A_ID_from, Box_DB.me.box_DB[Target_Box_ID].Rewadrd_A_ID_to);
			Get_Value = Box_DB.me.box_DB[Target_Box_ID].Rewadrd_A_ID_value;
			reward_Icon[1].Setting_Misile(Get_ID, Get_Value);
			Now_Data.me.Misile_Parts[Get_ID] += Get_Value;
			Security.SetInt(string.Format("Misile_Parts_{0:000}", Get_ID), Now_Data.me.Misile_Parts[Get_ID]);
			Get_IDs.Add(Get_ID);
		}
		else
		{
			reward_Icon[1].Set_Empty();
		}
		if (Box_DB.me.box_DB[Target_Box_ID].Rewadrd_B_ID_value != 0)
		{
			Get_ID = Selceted_ID(Box_DB.me.box_DB[Target_Box_ID].Rewadrd_B_ID_from, Box_DB.me.box_DB[Target_Box_ID].Rewadrd_B_ID_to);
			Get_Value = Box_DB.me.box_DB[Target_Box_ID].Rewadrd_B_ID_value;
			reward_Icon[2].Setting_Misile(Get_ID, Get_Value);
			Now_Data.me.Misile_Parts[Get_ID] += Get_Value;
			Security.SetInt(string.Format("Misile_Parts_{0:000}", Get_ID), Now_Data.me.Misile_Parts[Get_ID]);
			Get_IDs.Add(Get_ID);
		}
		else
		{
			reward_Icon[2].Set_Empty();
		}
		if (Box_DB.me.box_DB[Target_Box_ID].Rewadrd_C_ID_value != 0)
		{
			Get_ID = Selceted_ID(Box_DB.me.box_DB[Target_Box_ID].Rewadrd_C_ID_from, Box_DB.me.box_DB[Target_Box_ID].Rewadrd_C_ID_to);
			Get_Value = Box_DB.me.box_DB[Target_Box_ID].Rewadrd_C_ID_value;
			reward_Icon[3].Setting_Misile(Get_ID, Get_Value);
			Now_Data.me.Misile_Parts[Get_ID] += Get_Value;
			Security.SetInt(string.Format("Misile_Parts_{0:000}", Get_ID), Now_Data.me.Misile_Parts[Get_ID]);
			Get_IDs.Add(Get_ID);
		}
		else
		{
			reward_Icon[3].Set_Empty();
		}
		if (Box_DB.me.box_DB[Target_Box_ID].Rewadrd_D_ID_value != 0)
		{
			Get_ID = Selceted_ID(Box_DB.me.box_DB[Target_Box_ID].Rewadrd_D_ID_from, Box_DB.me.box_DB[Target_Box_ID].Rewadrd_D_ID_to);
			Get_Value = Box_DB.me.box_DB[Target_Box_ID].Rewadrd_D_ID_value;
			reward_Icon[4].Setting_Misile(Get_ID, Get_Value);
			Now_Data.me.Misile_Parts[Get_ID] += Get_Value;
			Security.SetInt(string.Format("Misile_Parts_{0:000}", Get_ID), Now_Data.me.Misile_Parts[Get_ID]);
			Get_IDs.Add(Get_ID);
		}
		else
		{
			reward_Icon[4].Set_Empty();
		}
		UI_Master.me.new_misile.GET_Misile_IDs.Clear();
		for (int i = 0; i < Get_IDs.Count; i++)
		{
			if (Now_Data.me.Misile_TIER[Get_IDs[i]] < 1)
			{
				UI_Master.me.new_misile.GET_Misile_IDs.Add(Get_IDs[i]);
				Now_Data.me.Misile_TIER[Get_IDs[i]] = 1;
				Security.SetInt(string.Format("Misile_TIER_{0:000}", Get_IDs[i]), Now_Data.me.Misile_TIER[Get_IDs[i]]);
				Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_MISSILE_COUNT);
			}
		}
		UI_Master.me.misile_Parts_Manager.AnyMisile_PossibleCheck();
	}

	public void Setting_DIABLO_REWARD(int Diablo_LV)
	{
		DARKBOX = false;
		BEFORE_DIABLOR_RECORD = Security.GetFloat(string.Format("DIABLO_{0:000}_RECORD", Diablo_LV), -1f);
		NOW_DIABLOR_RECORD = Now_Data.me.DIABLO_LAPTIME;
		Security.SetFloat(string.Format("DIABLO_{0:000}_RECORD", Diablo_LV), NOW_DIABLOR_RECORD);
		Get_IDs.Clear();
		SKIP = true;
		for (int i = 0; i < 5; i++)
		{
			Possible_REWARD[i] = false;
		}
		if (NOW_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[0])
		{
			Possible_REWARD[0] = true;
			Possible_REWARD[1] = true;
			Possible_REWARD[2] = true;
			Possible_REWARD[3] = true;
			Possible_REWARD[4] = true;
		}
		else if (NOW_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[1])
		{
			Possible_REWARD[1] = true;
			Possible_REWARD[2] = true;
			Possible_REWARD[3] = true;
			Possible_REWARD[4] = true;
		}
		else if (NOW_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[2])
		{
			Possible_REWARD[2] = true;
			Possible_REWARD[3] = true;
			Possible_REWARD[4] = true;
		}
		else if (NOW_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[3])
		{
			Possible_REWARD[3] = true;
			Possible_REWARD[4] = true;
		}
		else
		{
			Possible_REWARD[4] = true;
		}
		if (BEFORE_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[0] && BEFORE_DIABLOR_RECORD > 0f)
		{
			Possible_REWARD[0] = false;
			Possible_REWARD[1] = false;
			Possible_REWARD[2] = false;
			Possible_REWARD[3] = false;
			Possible_REWARD[4] = false;
			SKIP = true;
		}
		else if (BEFORE_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[1] && BEFORE_DIABLOR_RECORD > 0f)
		{
			Possible_REWARD[1] = false;
			Possible_REWARD[2] = false;
			Possible_REWARD[3] = false;
			Possible_REWARD[4] = false;
		}
		else if (BEFORE_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[2] && BEFORE_DIABLOR_RECORD > 0f)
		{
			Possible_REWARD[2] = false;
			Possible_REWARD[3] = false;
			Possible_REWARD[4] = false;
		}
		else if (BEFORE_DIABLOR_RECORD < Now_Data.me.DIABLO_LAPTIME_LV[3] && BEFORE_DIABLOR_RECORD > 0f)
		{
			Possible_REWARD[3] = false;
			Possible_REWARD[4] = false;
		}
		else if (BEFORE_DIABLOR_RECORD > 0f)
		{
			Possible_REWARD[4] = false;
		}
		for (int j = 0; j < 5; j++)
		{
			if (Possible_REWARD[j].Equals(true))
			{
				SKIP = false;
				break;
			}
		}
		if (!SKIP)
		{
			int num = 0;
			if (Possible_REWARD[0].Equals(true))
			{
				Get_ID = Dungeon_DB.me.dungeon_DB_for_REWARD[Diablo_LV].REWARD_A_ID;
				Get_Value = Dungeon_DB.me.dungeon_DB_for_REWARD[Diablo_LV].REWARD_A_Value;
				reward_Icon[num].Setting_Box(Get_ID, Get_Value);
				Now_Data.me.BOX_Count[Get_ID] += Get_Value;
				Security.SetInt(string.Format("BOX_Count_{0:000}", Get_ID), Now_Data.me.BOX_Count[Get_ID]);
				num++;
			}
			if (Possible_REWARD[1].Equals(true))
			{
				Get_ID = Dungeon_DB.me.dungeon_DB_for_REWARD[Diablo_LV].REWARD_B_ID;
				Get_Value = Dungeon_DB.me.dungeon_DB_for_REWARD[Diablo_LV].REWARD_B_Value;
				reward_Icon[num].Setting_Box(Get_ID, Get_Value);
				Now_Data.me.BOX_Count[Get_ID] += Get_Value;
				Security.SetInt(string.Format("BOX_Count_{0:000}", Get_ID), Now_Data.me.BOX_Count[Get_ID]);
				num++;
			}
			if (Possible_REWARD[2].Equals(true))
			{
				Get_ID = Dungeon_DB.me.dungeon_DB_for_REWARD[Diablo_LV].REWARD_C_ID;
				Get_Value = Dungeon_DB.me.dungeon_DB_for_REWARD[Diablo_LV].REWARD_C_Value;
				reward_Icon[num].Setting_Box(Get_ID, Get_Value);
				Now_Data.me.BOX_Count[Get_ID] += Get_Value;
				Security.SetInt(string.Format("BOX_Count_{0:000}", Get_ID), Now_Data.me.BOX_Count[Get_ID]);
				num++;
			}
			if (Possible_REWARD[3].Equals(true))
			{
				Get_ID = Dungeon_DB.me.dungeon_DB_for_REWARD[Diablo_LV].REWARD_D_ID;
				Get_Value = Dungeon_DB.me.dungeon_DB_for_REWARD[Diablo_LV].REWARD_D_Value;
				reward_Icon[num].Setting_Box(Get_ID, Get_Value);
				Now_Data.me.BOX_Count[Get_ID] += Get_Value;
				Security.SetInt(string.Format("BOX_Count_{0:000}", Get_ID), Now_Data.me.BOX_Count[Get_ID]);
				num++;
			}
			if (Possible_REWARD[4].Equals(true))
			{
				Get_ID = Dungeon_DB.me.dungeon_DB_for_REWARD[Diablo_LV].REWARD_E_ID;
				Get_Value = Dungeon_DB.me.dungeon_DB_for_REWARD[Diablo_LV].REWARD_E_Value;
				reward_Icon[num].Setting_Box(Get_ID, Get_Value);
				Now_Data.me.BOX_Count[Get_ID] += Get_Value;
				Security.SetInt(string.Format("BOX_Count_{0:000}", Get_ID), Now_Data.me.BOX_Count[Get_ID]);
				num++;
			}
			for (int k = num; k < 5; k++)
			{
				reward_Icon[k].Set_Empty();
			}
			FIN = false;
			UI_Master.me.PAUSE();
			box_Sprite.sprite2D = Sprite_DB.me.Monster_Icon[4];
			base.gameObject.SetActive(true);
			Reward_Panel.SetActive(false);
			First_Boxshot.SetActive(true);
			ThisTransform.DOLocalMoveY(500f, 0.5f).From(true).SetEase(Ease.InOutCirc)
				.OnComplete(Move_End);
			SoundManager.me.Congretu();
			UI_Master.me.Good_MSG("DIABLO_CLEAR_FIELD");
		}
	}

	public void Move_End()
	{
		SoundManager.me.Booster_B();
		ThisTransform.DOShakePosition(1f, 5f, 30, 180f).OnComplete(Shake_End);
	}

	public void Shake_End()
	{
		SoundManager.me.Congretu();
		First_Boxshot.SetActive(false);
		if (DARKBOX)
		{
			UI_Master.me.artifact_Panel.Show_Diablo_Artifact(Target_Diablo_ID);
			return;
		}
		Reward_Panel.SetActive(true);
		UI_Master.me.Popup(base.gameObject);
		StartCoroutine(Set_Flow());
	}

	public IEnumerator Set_Flow()
	{
		for (int j = 0; j < reward_Icon.Length; j++)
		{
			reward_Icon[j].gameObject.SetActive(false);
		}
		Quit_BTN.SetActive(false);
		yield return new WaitForSeconds(0.2f);
		for (int i = 0; i < reward_Icon.Length; i++)
		{
			if (!reward_Icon[i].Empty)
			{
				SoundManager.me.Boom();
				reward_Icon[i].gameObject.SetActive(true);
				yield return new WaitForSeconds(0.2f * (float)(reward_Icon.Length - i) / 2f);
			}
		}
		yield return new WaitForSeconds(0.2f);
		Quit_BTN.SetActive(true);
		FIN = true;
		UI_Master.me.Box_Checking();
	}

	public void Update()
	{
		if (FIN && !UI_Master.me.new_misile.gameObject.activeSelf && UI_Master.me.new_misile.GET_Misile_IDs.Count > 0)
		{
			UI_Master.me.new_misile.Setting(true);
		}
	}

	public void OnDisable()
	{
		UI_Master.me.Popup_Close_All();
		if (Tutorial_Manager.me.Missile_LOCK.activeSelf)
		{
			Tutorial_Manager.me.Set_Missile_Tuto();
		}
	}

	public int Selceted_ID(int from, int to)
	{
		int num = 0;
		int num2 = 0;
		if (to - from == 10)
		{
			return Random.Range(from, to);
		}
		switch ((to - from) / 10)
		{
		default:
			return Random.Range(from, to);
		case 2:
			if (Random.Range(0, 6).Equals(0))
			{
				return Random.Range(to - 10, to);
			}
			return Random.Range(from, from + 10);
		case 3:
			num2 = Random.Range(0, 31);
			if (num2.Equals(0))
			{
				return Random.Range(from + 20, from + 10);
			}
			if (num2 < 6)
			{
				return Random.Range(from + 10, from + 20);
			}
			return Random.Range(from, from + 10);
		case 4:
			num2 = Random.Range(0, 31);
			if (num2.Equals(0))
			{
				return Random.Range(from + 30, from + 40);
			}
			if (num2 < 6)
			{
				return Random.Range(from + 20, from + 30);
			}
			if (num2 < 26)
			{
				return Random.Range(from + 10, from + 20);
			}
			return Random.Range(from, from + 10);
		case 5:
			num2 = Random.Range(0, 31);
			if (num2.Equals(0))
			{
				return Random.Range(from + 40, from + 50);
			}
			if (num2 < 6)
			{
				return Random.Range(from + 30, from + 40);
			}
			if (num2 < 26)
			{
				return Random.Range(from + 20, from + 30);
			}
			if (num2 < 126)
			{
				return Random.Range(from + 10, from + 20);
			}
			return Random.Range(from, from + 10);
		}
	}
}
