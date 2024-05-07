using System.Collections;
using DG.Tweening;
using Keiwando.BigInteger;
using UnityEngine;

public class Character : MonoBehaviour
{
	public Ch_Type CH_TYPE;

	public int CH_ID;

	public PlayerState PS;

	public MoveState moveState;

	public CH_Direction direction;

	public Animator animator;

	public Sprite_Animator S_animator;

	public GameObject S_Pivot;

	public Transform ThisTransform;

	public Rigidbody2D rigidbody2D;

	public bool DIRECT_SHOT;

	public Transform Direct_direction;

	public bool To_Portal;

	public bool Piercing_ATTACKER;

	public Transform[] Shot_Point;

	public SpriteRenderer sprite_Renderer;

	public GameObject GUN;

	public Character_Buff_Manager character_Buff_Manager;

	public UILabel Text;

	public UILabel GetDMG_Label;

	public UISlider HP_Slider;

	public UILabel HP_Slider_label;

	public GameObject Heal_EF;

	public BigInteger HP;

	public CharacterDATA Basic_Data;

	public Vector3 Origin_Position;

	public bool Origin_Setting;

	public Transform Center_Point;

	public float HP_time;

	public float HP_time_MAX;

	public bool ReadyToAttack;

	private int attack_count;

	public bool Shot_Possible;

	public Vector3 Attack_direction;

	public Misile misile;

	public float arrow_direction;

	public float TRUE_DMG;

	private float distance_checksum;

	private int Misile_array_Number;

	private Character attacked_BY;

	private BigInteger Real_DMG = new BigInteger(0L);

	public float knockback_value;

	public Vector3 directToTarget;

	public Enemy attack_target_CH;

	public float move_Speed;

	public float Changed_Speed;

	public float Changed_ATK_Speed;

	private float Imsi_Heal_DMG;

	public UIPanel uiPanel;

	private int Imsi_int;

	private int Imsi_int_R;

	private int Imsi_Limit;

	public int Gold_Limit;

	public int Item_Limit;

	public float atk_delay;

	public float shot_delay;

	public void Birth_Unit(int ID)
	{
		CH_ID = ID;
		PS = PlayerState.Idle;
		Status_Setting();
		Changed_ATK_Speed = Basic_Data.ATK_Speed * 0.0005f;
		Z_Update();
		base.gameObject.SetActive(true);
		Main_Player.me.player.Chat(Localization.Get(string.Format("ADD_PARTY_{0:000}", ID)));
	}

	public void Birth_Skill_Unit()
	{
		PS = PlayerState.Idle;
		Basic_Data.ATK = Main_Player.me.player.Basic_Data.ATK * (Misile_DB.me.skill_DB[2].Skill_LV[Now_Data.me.Active_Skill_LV[2]].Value * (100 + Now_Data.me.SKILL_Value_Plus[2]) / 100) / 100;
		DIRECT_SHOT = true;
		Changed_ATK_Speed = 0.0005f;
		Basic_Data.Misile_Speed = 15f;
		base.gameObject.SetActive(true);
	}

	public void Status_Setting()
	{
		Basic_Data.ATK = Unit_DB.me.DMG_by_LV(CH_ID, Now_Data.me.Now_Unit_LV[CH_ID]);
		Basic_Data.ATK_Range = 10f;
		Basic_Data.ATK_TYPE = Unit_DB.me.unit_DB[CH_ID].ATK_TYPE;
		Basic_Data.ATK_Speed = 1f / Unit_DB.me.unit_DB[CH_ID].ATK_SPEED;
		Basic_Data.Lucky = Now_Data.me.Unit_Ciritical[CH_ID];
		switch (Unit_DB.me.unit_DB[CH_ID].unit_TYPE)
		{
		case Unit_Type.Human:
			Basic_Data.Critical_DMG = (int)Now_Data.me.HUMAN_CRITICAL_DMG_PER;
			break;
		case Unit_Type.Mechanic:
			Basic_Data.Critical_DMG = (int)Now_Data.me.Mechanin_CRITICAL_DMG_PER;
			break;
		case Unit_Type.Air:
			Basic_Data.Critical_DMG = (int)Now_Data.me.HUMAN_CRITICAL_DMG_PER;
			break;
		}
	}

	public void GO_IDLE()
	{
		if (base.gameObject.activeSelf && PS != PlayerState.Death && PS != PlayerState.Pool && !PS.Equals(PlayerState.Idle))
		{
			move_Speed = Changed_Speed;
			rigidbody2D.velocity = new Vector2(0f, 0f);
			animator.SetTrigger("Idle");
			PS = PlayerState.Idle;
			moveState = MoveState.Stop;
			animator.SetBool("Move", false);
			Z_Update();
		}
	}

	public void Z_Update()
	{
		ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, ThisTransform.position.y * ThisTransform.position.x * 0.001f);
	}

	public void Attack_Motion(bool stop)
	{
		if (PS.Equals(PlayerState.Death) || PS.Equals(PlayerState.Pool))
		{
			return;
		}
		if (DIRECT_SHOT)
		{
			Fight_Master.me.CameraShake();
		}
		ReadyToAttack = false;
		if (S_Pivot != null)
		{
			S_Pivot.SetActive(false);
			S_Pivot.SetActive(true);
		}
		if (S_animator != null)
		{
			S_animator.Attack();
			return;
		}
		if (stop)
		{
			move_Speed = 0f;
			moveState = MoveState.Stop;
			animator.SetBool("Move", false);
		}
		switch (attack_count)
		{
		case 0:
			animator.SetTrigger("Attack_A");
			attack_count++;
			break;
		case 1:
			animator.SetTrigger("Attack_B");
			attack_count++;
			break;
		case 2:
			animator.SetTrigger("Attack_C");
			attack_count = 0;
			break;
		}
	}

	public void Attack()
	{
		atk_delay = 0f;
		Shot_Possible = false;
		if (!PS.Equals(PlayerState.Death) && !PS.Equals(PlayerState.Pool))
		{
			StopCoroutine(Attack_co());
			StartCoroutine(Attack_co());
		}
	}

	public IEnumerator Attack_co()
	{
		if (GUN != null)
		{
			GUN.SetActive(false);
			GUN.SetActive(true);
		}
		Find_Target();
		if (attack_target_CH != null)
		{
			if (attack_target_CH.ThisTransform.position.x <= 4f)
			{
				for (int i = 0; i < Shot_Point.Length; i++)
				{
					if (Shot_Point[i].gameObject.activeSelf)
					{
						Arrow_Attack(true, i, false);
						yield return new WaitForSeconds(0.05f);
					}
				}
			}
			else
			{
				for (int j = 0; j < Shot_Point.Length; j++)
				{
					if (Shot_Point[j].gameObject.activeSelf)
					{
						Arrow_Attack(false, j, false);
						yield return new WaitForSeconds(0.05f);
					}
				}
			}
		}
		yield return new WaitForSeconds(0.05f);
	}

	public void Find_Target()
	{
		attack_target_CH = null;
		distance_checksum = 999f;
		for (int i = 0; i < Fight_Master.me.Live_Enemies.Count; i++)
		{
			if (Fight_Master.me.Live_Enemies[i].ThisTransform.position.x < distance_checksum && Fight_Master.me.Live_Enemies[i].ThisTransform.position.x > ThisTransform.position.x)
			{
				distance_checksum = Fight_Master.me.Live_Enemies[i].ThisTransform.position.x;
				attack_target_CH = Fight_Master.me.Live_Enemies[i];
			}
		}
		if (!To_Portal)
		{
			return;
		}
		for (int j = 0; j < Fight_Master.me.Live_Enemies.Count; j++)
		{
			if (Fight_Master.me.Live_Enemies[j].Portal)
			{
				attack_target_CH = Fight_Master.me.Live_Enemies[j];
			}
		}
	}

	public void Arrow_Attack(bool UnconditionalDirect, int POINT, bool Minimun)
	{
		if (!DIRECT_SHOT)
		{
			SoundManager.me.Attack_CH(CH_ID);
		}
		misile = OBJ_Pool.me.Fireball_misile[OBJ_Pool.Fireball_misile_Number];
		OBJ_Pool.Fireball_misile_Number++;
		if (OBJ_Pool.Fireball_misile_Number >= OBJ_Pool.me.Fireball_misile.Length)
		{
			OBJ_Pool.Fireball_misile_Number = 0;
		}
		misile.Clear();
		if (!(attack_target_CH != null))
		{
			return;
		}
		misile.target = attack_target_CH.Hit_Point;
		if (DIRECT_SHOT)
		{
			misile.Missile_ID = Now_Data.me.EQUIP_MISILEs[Random.Range(0, Now_Data.me.EQUIP_POSSILBEs_Count)];
			if (misile.Missile_ID.Equals(-1))
			{
				misile.Missile_ID = Now_Data.me.EQUIP_MISILEs[0];
			}
		}
		else if (CH_TYPE.Equals(Ch_Type.Player))
		{
			misile.Missile_ID = Now_Data.me.EQUIP_MISILEs[Misile_array_Number];
			Misile_array_Number++;
			if (Misile_array_Number >= Now_Data.me.EQUIP_MISILEs.Length)
			{
				Misile_array_Number = 0;
			}
			if (misile.Missile_ID.Equals(-1))
			{
				Misile_array_Number = 0;
				misile.Missile_ID = Now_Data.me.EQUIP_MISILEs[Misile_array_Number];
			}
		}
		else
		{
			misile.Missile_ID = Basic_Data.ATK_TYPE;
		}
		if (Minimun)
		{
			misile.DMG = Basic_Data.ATK / 2;
			misile.ThisTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		}
		else
		{
			if ((float)Random.Range(0, 10000) < Basic_Data.Lucky * 100f)
			{
				misile.Critical = true;
			}
			else
			{
				misile.Critical = false;
			}
			if (Piercing_ATTACKER)
			{
				misile.Piercing = true;
			}
			if (CH_TYPE.Equals(Ch_Type.Player))
			{
				if (Main_Player.me.SteamPACK)
				{
					misile.Critical = true;
				}
				if (Now_Data.me.PIERCING_ATTACK > 0f && (float)Random.Range(0, 100) < Now_Data.me.PIERCING_ATTACK)
				{
					misile.Piercing = true;
				}
			}
			if (DIRECT_SHOT)
			{
				misile.Direct = true;
			}
			else if (UnconditionalDirect)
			{
				misile.Direct = true;
			}
			else if (Misile_DB.me.misile_DB[misile.Missile_ID].Type.Equals(0))
			{
				misile.Direct = true;
			}
			else
			{
				misile.Direct = false;
			}
			if (misile.Critical)
			{
				misile.Direct = true;
				misile.ThisTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
				misile.DMG = Basic_Data.ATK * Basic_Data.Critical_DMG / 100;
			}
			else
			{
				misile.ThisTransform.localScale = new Vector3(1f, 1f, 1f);
				misile.DMG = Basic_Data.ATK;
			}
		}
		if (CH_TYPE.Equals(Ch_Type.Friend) && !DIRECT_SHOT)
		{
			misile.DMG /= (BigInteger)Shot_Point.Length;
		}
		misile.ThisTransform.position = Shot_Point[POINT].position;
		misile.owner = this;
		misile.Oneshot = true;
		misile.KnockBack = Basic_Data.Knockback;
		misile.Speed = Basic_Data.Misile_Speed;
		if (DIRECT_SHOT)
		{
			misile.Oneshot = false;
			misile.ThisTransform.localRotation = Direct_direction.localRotation;
		}
		else if (misile.Direct)
		{
			misile.ThisTransform.LookAt(new Vector3(attack_target_CH.Hit_Point.position.x + Random.Range(-0.5f, 0.5f), attack_target_CH.Hit_Point.position.y + Random.Range(-0.5f, 0.5f), attack_target_CH.Hit_Point.position.z));
		}
		misile.gameObject.SetActive(true);
	}

	public void Hurt(int MAIN_Ele, BigInteger DMG, float K_Value, Vector3 From, Character FromCH, bool critical)
	{
		attacked_BY = FromCH;
		if (PS != PlayerState.Death && PS != PlayerState.Pool)
		{
			SoundManager.me.Hit();
			OBJ_Pool.Make_OBJ(OBJ_Pool.me.effect_Hitted, ref OBJ_Pool.effect_Hitted_Number, animator.transform.position);
			Real_DMG = DMG;
			if (Real_DMG <= 1)
			{
				Real_DMG = 1;
			}
			else if (base.gameObject.activeSelf)
			{
				StopAllCoroutines();
				StartCoroutine(Hurt_State());
			}
		}
		HP_MINUS(Real_DMG, critical);
	}

	public void KNOCK_BACK(float K_Value, Vector3 From)
	{
		knockback_value = (K_Value - Basic_Data.KB_Regist * 0.8f) * 5f;
		if (knockback_value < 0f)
		{
			knockback_value = 1f;
		}
		if (knockback_value > 1500f)
		{
			knockback_value = 1500f;
		}
		rigidbody2D.velocity = new Vector2(0f, 0f);
		rigidbody2D.AddForce(new Vector2(knockback_value, 0f));
	}

	public IEnumerator Hurt_State()
	{
		moveState = MoveState.Stop;
		animator.SetBool("Move", false);
		animator.SetTrigger("Hurt");
		PS = PlayerState.Hurt;
		yield return new WaitForSeconds(0.3f);
		if (PS.Equals(PlayerState.Hurt))
		{
			Fight_End();
		}
	}

	public void Fight_End()
	{
		attack_target_CH = null;
		GO_IDLE();
		if (PS != PlayerState.Death && PS != PlayerState.Pool)
		{
			move_Speed = Changed_Speed;
			rigidbody2D.velocity = new Vector2(0f, 0f);
		}
	}

	public void HP_MINUS(BigInteger value, bool critical)
	{
		if (value > 0)
		{
			HP -= value;
			OBJ_Pool.me.DMG_panel[OBJ_Pool.DMG_panel_Number].Setting(value, critical);
			OBJ_Pool.me.DMG_panel[OBJ_Pool.DMG_panel_Number].transform.position = new Vector3(ThisTransform.position.x, HP_Slider.transform.position.y + 0.3f, 0f);
			OBJ_Pool.DMG_panel_Number++;
			if (OBJ_Pool.DMG_panel_Number >= OBJ_Pool.me.DMG_panel.Length)
			{
				OBJ_Pool.DMG_panel_Number = 0;
			}
			HPUpdate();
		}
	}

	public void Healing(BigInteger DMG)
	{
		if (PS != PlayerState.Death)
		{
			if (HP + DMG > Basic_Data.HP)
			{
				DMG = Basic_Data.HP - HP;
			}
			Heal_EF.SetActive(false);
			Heal_EF.SetActive(true);
			GetDMG_Label.gameObject.SetActive(false);
			GetDMG_Label.text = string.Format("+{0:N0}", DMG);
			GetDMG_Label.gameObject.SetActive(true);
			HP += DMG;
			HPUpdate();
		}
	}

	public void HPUpdate()
	{
		HP_Slider.value = Now_Data.Divide_to_Float(HP, Basic_Data.HP);
		HP_Slider.gameObject.SetActive(true);
		if (HP > Basic_Data.HP)
		{
			HP = Basic_Data.HP;
			if (!(HP <= Basic_Data.HP))
			{
			}
		}
		else if (HP <= 0)
		{
			HP = 0;
			Die();
		}
	}

	public void Die()
	{
		if (base.gameObject.activeSelf && !PS.Equals(PlayerState.Death) && !PS.Equals(PlayerState.Pool))
		{
			StopAllCoroutines();
			StartCoroutine(FadeOut(0.2f));
			TRUE_DEATH();
		}
	}

	public void TRUE_DEATH()
	{
		if (uiPanel != null)
		{
			uiPanel.sortingOrder = 0;
		}
		HP = 0;
		moveState = MoveState.Stop;
		PS = PlayerState.Death;
		move_Speed = 0f;
		if (animator != null)
		{
			animator.speed = 1f;
			animator.SetBool("Move", false);
			animator.SetBool("Death", true);
		}
		base.gameObject.layer = 9;
		if (HP_Slider != null)
		{
			HP_Slider_label.gameObject.SetActive(false);
			HP_Slider.gameObject.SetActive(false);
		}
	}

	public IEnumerator FadeOut(float Delay)
	{
		yield return new WaitForSeconds(Delay);
		float a = 1f;
		for (int i = 0; i < 10; i++)
		{
			a -= 0.1f;
			sprite_Renderer.color = new Color(1f, 1f, 1f, a);
			yield return new WaitForSeconds(0.1f);
		}
		base.gameObject.SetActive(false);
		Remove();
	}

	public void Remove()
	{
		PS = PlayerState.Pool;
		sprite_Renderer.color = Color.white;
		Text.transform.parent.gameObject.SetActive(false);
		if (character_Buff_Manager != null)
		{
			character_Buff_Manager.All_Clear();
		}
		if (HP_Slider != null)
		{
			HP_Slider.value = 1f;
			HP_Slider.gameObject.SetActive(false);
		}
		base.gameObject.SetActive(false);
	}

	public void MakeCoin(BigInteger value)
	{
		if (OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GET_Possible)
		{
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Get();
		}
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.position = base.gameObject.transform.position;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(true);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.localScale = Vector3.zero;
		OBJ_Pool.Coin_Number++;
		if (OBJ_Pool.Coin_Number >= OBJ_Pool.me.Coin.Length)
		{
			OBJ_Pool.Coin_Number = 0;
		}
	}

	public void Chat(string script)
	{
		if (Text != null)
		{
			Text.transform.parent.gameObject.SetActive(false);
			Text.text = script;
			Text.transform.parent.gameObject.SetActive(true);
		}
	}

	public void FixedUpdate()
	{
		if (!Fight_Master.me.GS.Equals(GameState.Play))
		{
			return;
		}
		if (!Shot_Possible)
		{
			shot_delay += 0.05f;
			if (shot_delay >= 0.1f)
			{
				shot_delay = 0f;
				Shot_Possible = true;
			}
		}
		if (!CH_TYPE.Equals(Ch_Type.Friend))
		{
			return;
		}
		if (Main_Player.me.FAST_MARINE_ING)
		{
			atk_delay += Changed_ATK_Speed * (100f + Now_Data.me.Unit_ATTACK_SPEED_PER[CH_ID]) * (100f + (float)Now_Data.me.SKILL_Value_Plus[5] + (float)Misile_DB.me.skill_DB[5].Skill_LV[Now_Data.me.Active_Skill_LV[5]].Value) * 0.01f;
		}
		else
		{
			atk_delay += Changed_ATK_Speed * (100f + Now_Data.me.Unit_ATTACK_SPEED_PER[CH_ID]);
		}
		if (atk_delay >= 1f)
		{
			Attack_Motion(true);
			Attack();
		}
		if (HP_time > 0f)
		{
			HP_time -= 0.05f;
			if (HP_time <= 0f)
			{
				Die();
				return;
			}
			HP_Slider.value = HP_time / HP_time_MAX;
			HP_Slider_label.text = string.Format("{0:0.#}Sec", HP_time);
		}
	}

	public void OnEnable()
	{
		if (!Origin_Setting)
		{
			Origin_Setting = true;
			Z_Update();
			Origin_Position = ThisTransform.position;
		}
		else
		{
			ThisTransform.position = Origin_Position;
		}
		Opening();
	}

	public void Opening()
	{
		ThisTransform.localPosition = new Vector3(ThisTransform.localPosition.x, ThisTransform.localPosition.y + 10f, Origin_Position.z);
		if (Unit_DB.me.unit_DB[CH_ID].unit_TYPE.Equals(Unit_Type.Air))
		{
			ThisTransform.DOMoveY(Origin_Position.y, 2f).SetEase(Ease.InCubic);
		}
		else
		{
			ThisTransform.DOMoveY(Origin_Position.y, 0.5f).SetEase(Ease.InCubic).OnComplete(Fin);
		}
	}

	public void Fin()
	{
		SoundManager.me.Boom();
		Fight_Master.me.CameraShake();
		OBJ_Pool.Make_OBJ(OBJ_Pool.me.Opening_C, ref OBJ_Pool.Opening_C_Number, ThisTransform.position);
	}
}
