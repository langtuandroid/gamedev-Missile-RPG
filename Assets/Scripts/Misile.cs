using System;
using System.Collections;
using Keiwando.BigInteger;
using UnityEngine;

public class Misile : MonoBehaviour
{
	public int Missile_ID;

	public int Main_Ele;

	public float Speed;

	public BigInteger DMG = 0;

	public BigInteger T_DMG = 0;

	public float KnockBack = 0.5f;

	public Vector3 direction = default(Vector3);

	public bool Direct;

	public bool Oneshot;

	public bool Bomb = true;

	public bool After_Bomb;

	public bool Piercing;

	public Character owner;

	public Character EX_Player;

	public Transform ThisTransform;

	public GameObject Parent;

	public bool Critical;

	public float Life_time = 5f;

	public SpriteRenderer Main_Sprite;

	public ParticleSystem Main_particle;

	public bool WORK_Possible;

	public Transform target;

	private float tx;

	private float ty;

	private float tz;

	private float v;

	private float g = 9.8f;

	private float elapsed_time;

	public float max_height;

	private float t;

	private Vector3 start_pos;

	private Vector3 end_pos;

	private float dat;

	private float dh;

	private float mh;

	private float a;

	private float b;

	private float c;

	private Vector3 tpos;

	private float txx;

	private float tyy;

	private float tzz;

	public void Clear()
	{
		Critical = false;
		After_Bomb = false;
		Main_Ele = 0;
		Speed = 0f;
		DMG = 0;
		T_DMG = 0;
		KnockBack = 1f;
		owner = null;
		EX_Player = null;
		Piercing = false;
		Main_particle.gameObject.SetActive(false);
	}

	public void Shoot(Vector3 EndPos, float Gravity, float Max_height, Action onComplete)
	{
		start_pos = new Vector3(ThisTransform.position.x, ThisTransform.position.y, ThisTransform.position.z);
		g = Gravity;
		max_height = Max_height;
		if (max_height < 0.1f)
		{
			max_height = 0.1f;
		}
		dh = EndPos.y - ThisTransform.position.y;
		mh = max_height - ThisTransform.position.y;
		if (mh < 0f)
		{
			mh = 0f;
		}
		ty = Mathf.Sqrt(2f * g * mh);
		a = g;
		b = -2f * ty;
		c = 2f * dh;
		dat = (0f - b + Mathf.Sqrt(b * b - 4f * a * c)) / (2f * a);
		if (dat <= 0f || float.IsNaN(dat))
		{
			dat = 1f;
		}
		tx = (0f - (ThisTransform.position.x - EndPos.x)) / dat;
		tz = (0f - (ThisTransform.position.z - EndPos.z)) / dat;
		elapsed_time = 0f;
		StartCoroutine(ShootImpl(onComplete));
	}

	private IEnumerator ShootImpl(Action onComplete)
	{
		while (true)
		{
			if (Fight_Master.me.GS.Equals(GameState.Play))
			{
				elapsed_time += Time.deltaTime * 1.5f;
				txx = start_pos.x + tx * elapsed_time;
				tyy = start_pos.y + ty * elapsed_time - 0.5f * g * elapsed_time * elapsed_time;
				tzz = start_pos.z + tz * elapsed_time;
				tpos = new Vector3(txx, tyy, tzz);
				ThisTransform.LookAt(tpos);
				ThisTransform.position = tpos;
				if (elapsed_time >= dat)
				{
					break;
				}
			}
			yield return null;
		}
		onComplete();
	}

	private void OnEnable()
	{
		if (!After_Bomb && Main_Sprite != null)
		{
			Main_Sprite.sprite = Sprite_DB.me.Misile_Sprite[Missile_ID];
		}
		if (!Direct)
		{
			StopAllCoroutines();
			Shoot(new Vector3(target.position.x, target.position.y + UnityEngine.Random.Range(-0.5f, 1.5f), target.position.z), 9.8f, target.position.x / (20f + UnityEngine.Random.Range(0f, 10f)) + UnityEngine.Random.Range(0f, 0.5f), Exit);
		}
		Life_time = 5f;
		if (Missile_ID >= 60)
		{
			Main_particle.gameObject.SetActive(false);
		}
		else
		{
			Main_particle.gameObject.SetActive(true);
		}
		WORK_Possible = true;
	}

	private void Update()
	{
		if (!Fight_Master.me.GS.Equals(GameState.Play))
		{
			return;
		}
		if (Direct)
		{
			if (Speed > 0f)
			{
				if (!Oneshot)
				{
					base.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
				}
				else
				{
					base.transform.Translate(Vector3.forward * Speed * Now_Data.me.MISSILE_SPEED * Time.deltaTime);
				}
			}
			if (!Oneshot && ThisTransform.position.y < -2f)
			{
				if (UnityEngine.Random.Range(0, 100) < 5)
				{
					Boom();
					return;
				}
				if (ThisTransform.position.y < -3f)
				{
					Boom();
					return;
				}
			}
		}
		Life_time -= Time.deltaTime;
		if (Life_time <= 0f)
		{
			Boom();
		}
	}

	public void Boom()
	{
		if (UnityEngine.Random.Range(0, 100) < 20)
		{
			SoundManager.me.Boom();
		}
		Exit();
		OBJ_Pool.Make_OBJ(OBJ_Pool.me.effect_boom, ref OBJ_Pool.effect_boom_Number, ThisTransform.position);
	}

	public void Exit()
	{
		WORK_Possible = false;
		base.gameObject.SetActive(false);
	}
}
