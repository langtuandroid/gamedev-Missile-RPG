using System.Collections;
using System.Collections.Generic;
using Keiwando.BigInteger;
using UnityEngine;

public class Character_Buff_Manager : MonoBehaviour
{
	public Character character;

	public List<BUFF> Now_Buffs;

	public bool Invinsible;

	public bool Invisible;

	public bool Detect;

	public bool Frozen;

	public bool Confused;

	public bool Poisoned;

	public bool Stun;

	public bool Blind;

	public bool Slow;

	public bool Sleeping;

	public int SAFE_Frozen;

	public int SAFE_Confused;

	public int SAFE_Poisoned;

	public int SAFE_Stun;

	public int SAFE_Blind;

	public int SAFE_Slow;

	public int SAFE_Sleeping;

	public int All_Safe;

	public bool NO_HURT;

	public float Poison_DMG;

	public bool Beam_Buff;

	public bool Boom_Buff;

	public bool Poison_Beam;

	public bool Silence;

	public bool Mirror;

	public Character attack_target;

	public float DEF_Broken;

	public float ATK_Broken;

	public float MAX_HP_Broken;

	private float _sec;

	private int Effect_ID;

	private bool Working_Possible;

	private bool Imun;

	public void DIE()
	{
		StartCoroutine(FadeOut(0.5f));
	}

	public IEnumerator FadeOut(float Delay)
	{
		yield return new WaitForSeconds(Delay);
		float a = 1f;
		for (int i = 0; i < 10; i++)
		{
			a -= 0.1f;
			character.sprite_Renderer.color = new Color(1f, 1f, 1f, a);
			yield return new WaitForSeconds(0.1f);
		}
		base.gameObject.SetActive(false);
		character.Remove();
	}

	public void All_Clear()
	{
		StopAllCoroutines();
		Now_Buffs.Clear();
		Invinsible = false;
		Invisible = false;
		Detect = false;
		Frozen = false;
		Confused = false;
		Poisoned = false;
		Stun = false;
		Blind = false;
		Slow = false;
		Sleeping = false;
		SAFE_Frozen = 0;
		SAFE_Confused = 0;
		SAFE_Poisoned = 0;
		SAFE_Stun = 0;
		SAFE_Blind = 0;
		SAFE_Slow = 0;
		SAFE_Sleeping = 0;
		All_Safe = 0;
		Poison_DMG = 0f;
		Beam_Buff = false;
		Boom_Buff = false;
		Poison_Beam = false;
		Silence = false;
		Mirror = false;
		attack_target = null;
		DEF_Broken = 0f;
		ATK_Broken = 0f;
		MAX_HP_Broken = 0f;
		NO_HURT = false;
	}

	public IEnumerator Update_Co()
	{
		while (Now_Buffs.Count > 0)
		{
			if (Fight_Master.me.GS.Equals(GameState.Play))
			{
				_sec += 0.1f;
				if (_sec > 1f)
				{
					_sec = 0f;
					if (Frozen)
					{
						character.animator.SetTrigger("ICE");
					}
					if (Poisoned)
					{
						character.HP -= new BigInteger(string.Format("{0}", Poison_DMG));
						character.GetDMG_Label.text = string.Format("-{0:N0}", Poison_DMG);
						character.GetDMG_Label.gameObject.SetActive(false);
						character.GetDMG_Label.gameObject.SetActive(true);
						character.HPUpdate();
					}
				}
				for (int i = 0; i < Now_Buffs.Count; i++)
				{
					if (!Now_Buffs[i].Buff_Time.Equals(999999f))
					{
						Now_Buffs[i].Buff_Time -= 0.1f + Random.Range(-0.05f, 0.05f);
						if (Now_Buffs[i].Buff_Time <= 0f)
						{
							Buff_CTRL(false, Now_Buffs[i].Buff_ID, 0, 0f, 0f, 0f, 0f);
						}
					}
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void Buff_EF(bool Positive_Negative)
	{
		if (!Positive_Negative)
		{
		}
	}

	public void Buff_FINISH_Target(int Target_ID)
	{
		Buff_CTRL(false, Target_ID, 0, 0f, 0f, 0f, 0f);
	}

	public void Buff_CTRL(bool START_or_END, int ID, int buff_LV, float buff_time, float buff_value, float buff_value_B, float buff_value_C)
	{
	}

	public int Find_Buff(int Buff_ID)
	{
		for (int i = 0; i < Now_Buffs.Count; i++)
		{
			if (Now_Buffs[i].Buff_ID.Equals(Buff_ID))
			{
				return i;
			}
		}
		return 999;
	}

	public void Buff_CLEAR()
	{
		for (int i = 0; i < Now_Buffs.Count; i++)
		{
			Buff_CTRL(false, Now_Buffs[i].Buff_ID, 0, 0f, 0f, 0f, 0f);
		}
	}

	public void DeBuff_CLEAR()
	{
		for (int i = 0; i < Now_Buffs.Count; i++)
		{
			if (!Now_Buffs[i].Positive_Negative)
			{
				Buff_CTRL(false, Now_Buffs[i].Buff_ID, 0, 0f, 0f, 0f, 0f);
			}
		}
	}

	public void Speed_Arrange()
	{
		if (Stun || Frozen || Sleeping)
		{
			character.Changed_Speed = 0f;
			character.move_Speed = 0f;
			character.Changed_ATK_Speed = 0f;
			character.animator.SetBool("Death", true);
		}
		else
		{
			if (!character.PS.Equals(PlayerState.Death) && !character.PS.Equals(PlayerState.Pool))
			{
				character.animator.SetBool("Death", false);
				character.animator.Play("Idle");
				character.GO_IDLE();
			}
			character.Changed_Speed = character.Basic_Data.Move_Speed;
			character.Changed_ATK_Speed = character.Basic_Data.ATK_Speed;
			for (int i = 0; i < Now_Buffs.Count; i++)
			{
				if (!Now_Buffs[i].Speed_Buff.Equals(0))
				{
					switch (Now_Buffs[i].Speed_Buff)
					{
					case 1:
						character.Changed_Speed += Now_Buffs[i].Buff_Value_A;
						character.Changed_ATK_Speed += Now_Buffs[i].Buff_Value_B;
						break;
					case 2:
						character.Changed_Speed += Now_Buffs[i].Buff_Value_B;
						character.Changed_ATK_Speed += Now_Buffs[i].Buff_Value_C;
						break;
					case 3:
						character.Changed_ATK_Speed += Now_Buffs[i].Buff_Value_B;
						break;
					}
				}
			}
		}
		character.move_Speed = character.Changed_Speed;
		if (Slow)
		{
			character.move_Speed = 0f;
			character.Changed_Speed = 0f;
			character.GO_IDLE();
		}
		if (character.Changed_Speed > character.Basic_Data.Move_Speed || character.Changed_ATK_Speed > character.Basic_Data.ATK_Speed)
		{
			character.animator.SetFloat("SPEED", 2f);
		}
	}
}
