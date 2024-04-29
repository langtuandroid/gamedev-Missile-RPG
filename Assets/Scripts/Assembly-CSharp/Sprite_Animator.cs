using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Sprite_Animator : MonoBehaviour
{
	public SpriteRenderer renderer;

	public Sprite[] Attack_Sprite;

	public Sprite[] Idle_Sprite;

	public Sprite[] Move_Sprite;

	public Sprite[] AUTO_Sprite;

	public bool Idle;

	public int sprite_ID;

	public bool AUTO_PLAY;

	public bool Loop;

	public int s_ID;

	public float Animation_speed = 0.1f;

	public bool Move_ING;

	private int Move_sprite_Number;

	public void OnEnable()
	{
		if (Idle)
		{
			sprite_ID = 0;
			StartCoroutine(Idle_animation());
		}
		if (Move_ING)
		{
			StartCoroutine(Move_co());
		}
		if (AUTO_PLAY)
		{
			StartCoroutine(Auto_co());
		}
	}

	public void Attack()
	{
		if (base.gameObject.activeSelf)
		{
			StopCoroutine(Idle_animation());
			StopCoroutine(Attack_co());
			StartCoroutine(Attack_co());
		}
	}

	public void Attack_One_Action()
	{
		renderer.sprite = Attack_Sprite[s_ID];
		s_ID++;
		if (s_ID >= Attack_Sprite.Length)
		{
			s_ID = 0;
		}
	}

	public IEnumerator Attack_co()
	{
		for (int i = 0; i < Attack_Sprite.Length; i++)
		{
			if (renderer != null)
			{
				renderer.sprite = Attack_Sprite[i];
			}
			yield return new WaitForSeconds(Animation_speed);
		}
	}

	public IEnumerator Idle_animation()
	{
		if (Idle_Sprite.Length > 1)
		{
			while (Idle)
			{
				renderer.sprite = Idle_Sprite[sprite_ID];
				sprite_ID++;
				if (sprite_ID >= Idle_Sprite.Length)
				{
					sprite_ID = 0;
				}
				yield return new WaitForSeconds(0.05f);
			}
		}
		else
		{
			renderer.sprite = Idle_Sprite[sprite_ID];
			yield return new WaitForSeconds(0.05f);
		}
	}

	public void Move()
	{
		Move_ING = true;
		base.transform.DORotate(new Vector3(0f, 0f, 3f), 0.15f).SetLoops(-1, LoopType.Yoyo);
		if (base.gameObject.activeSelf)
		{
			StopAllCoroutines();
			StartCoroutine(Move_co());
		}
	}

	public void Stop()
	{
		Move_ING = false;
		StopCoroutine(Move_co());
		renderer.sprite = Idle_Sprite[0];
		base.transform.DOKill();
	}

	public IEnumerator Move_co()
	{
		while (Move_ING)
		{
			if (renderer != null)
			{
				Move_sprite_Number++;
				if (Move_sprite_Number >= Move_Sprite.Length)
				{
					Move_sprite_Number = 0;
				}
			}
			renderer.sprite = Move_Sprite[Move_sprite_Number];
			yield return new WaitForSeconds(0.05f);
		}
	}

	public IEnumerator Auto_co()
	{
		for (int i = 0; i < AUTO_Sprite.Length; i++)
		{
			if (renderer != null)
			{
				renderer.sprite = AUTO_Sprite[i];
			}
			yield return new WaitForSeconds(Animation_speed);
		}
		if (Loop)
		{
			Again();
		}
	}

	public void Again()
	{
		if (Loop)
		{
			StopAllCoroutines();
			StartCoroutine(Auto_co());
		}
	}
}
