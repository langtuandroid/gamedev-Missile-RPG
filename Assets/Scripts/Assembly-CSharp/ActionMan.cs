using DG.Tweening;
using UnityEngine;

public class ActionMan : MonoBehaviour
{
	public Transform ThisTransform;

	public Sprite_Animator sprite_animator;

	public GameObject Shadow;

	public float Attack_Dealy = 0.1f;

	public float Attack_Dealy_Check;

	public int Go_Stage_Count;

	public bool Action_ING;

	public bool BIG;

	public float Delay;

	public float COME_SPEED = 1.5f;

	public Ease ease = Ease.InCubic;

	public void Update()
	{
		if (!Action_ING)
		{
			return;
		}
		if (BIG)
		{
			Attack_Dealy_Check += Time.deltaTime * Fight_Master.me.Game_Speed;
			if (Attack_Dealy_Check >= Attack_Dealy)
			{
				Attack_Dealy_Check = 0f;
				Attack();
			}
		}
		if (Go_Stage_Count <= 0)
		{
			End();
		}
	}

	public void Attack()
	{
		sprite_animator.Attack_One_Action();
		SoundManager.me.Punch();
		Fight_Master.me.CameraShake_Long();
		if (BIG)
		{
			ThisTransform.DOShakePosition(0.2f, 0.5f);
			while (Fight_Master.me.Live_Enemies.Count > 0)
			{
				Fight_Master.me.Live_Enemies[0].Invincible = false;
				Fight_Master.me.Live_Enemies[0].TRUE_DEATH();
			}
		}
		else
		{
			for (int i = 0; i < Fight_Master.me.Live_Enemies.Count; i++)
			{
				Fight_Master.me.Live_Enemies[i].Invincible = false;
				Fight_Master.me.Live_Enemies[i].Hurt(0, Main_Player.me.player.Basic_Data.ATK * 100, 0f, ThisTransform.position, Main_Player.me.player, false);
			}
		}
		Go_Stage_Count--;
	}

	public void Show()
	{
		Action_ING = false;
		Shadow.SetActive(false);
		sprite_animator.renderer.sprite = sprite_animator.Idle_Sprite[0];
	}

	public void Landing()
	{
		Fight_Master.me.CameraShake_Long();
		Shadow.SetActive(true);
		Action_ING = true;
		Attack_Dealy_Check = Attack_Dealy - 0.1f;
		if (!BIG)
		{
			OBJ_Pool.Make_OBJ(OBJ_Pool.me.effect_boom, ref OBJ_Pool.effect_boom_Number, ThisTransform.position);
			Attack();
		}
	}

	public void End()
	{
		Action_ING = false;
		sprite_animator.renderer.sprite = sprite_animator.Idle_Sprite[0];
		ThisTransform.DOMove(new Vector3(20f, 3f, 0f), 0.5f).SetEase(Ease.OutBounce).OnComplete(Complete);
	}

	public void Complete()
	{
		base.gameObject.SetActive(false);
	}

	public void OnEnable()
	{
		ThisTransform.position = new Vector3(-18f, 4f, 0f);
		Show();
		if (BIG)
		{
			ThisTransform.DOMove(new Vector3(7f, -5.2f, 0f), 0.5f).SetDelay(Delay).SetEase(Ease.InCubic)
				.OnComplete(Landing);
		}
		else
		{
			ThisTransform.DOMove(new Vector3(-4.83f, 0.14f, 0f), 0.5f).SetDelay(Delay).SetEase(ease)
				.OnComplete(Stop_A);
		}
	}

	public void Stop_A()
	{
		ThisTransform.DOMove(new Vector3(-2.35f, -1f, 0f), COME_SPEED).SetEase(ease).OnComplete(Stop_B);
		SoundManager.me.Fairy_Touch();
	}

	public void Stop_B()
	{
		ThisTransform.DOMove(new Vector3(7f, -5.2f, 0f), 0.2f).SetEase(ease).OnComplete(Landing);
	}
}
