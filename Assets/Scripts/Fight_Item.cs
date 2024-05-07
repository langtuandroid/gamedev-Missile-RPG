using System.Collections;
using UnityEngine;

public class Fight_Item : MonoBehaviour
{
	public Transform ThisTransform;

	public Rigidbody2D rigidbody2D;

	public Item_Type item_type;

	public SpriteRenderer sprite_img;

	private float y_position;

	public Transform Target;

	public int Item_Number;

	private bool follow;

	private float speed;

	public int bounce_count;

	public bool Hold;

	public bool GET_ing;

	public bool GET_Possible;

	public float X_power;

	public float Y_power;

	public float Randing_Position;

	public bool Randing_Possible;

	public bool GGOK_Front;

	private void OnEnable()
	{
		StopAllCoroutines();
		Hold = false;
		GET_ing = false;
		y_position = ThisTransform.position.y;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.gravityScale = 1f;
		sprite_img.color = Color.white;
		Randing_Possible = false;
		if (GGOK_Front)
		{
			X_power = -180f;
		}
		else
		{
			X_power = Random.Range(-150, 150);
		}
		Y_power = Random.Range(400, 500);
		rigidbody2D.AddForce(new Vector2(X_power, Y_power));
		Randing_Position = Random.Range(0f, -1.5f);
	}

	private IEnumerator Follow_Player()
	{
		yield return new WaitForSeconds(0.1f + Random.Range(0f, 0.2f));
		follow = true;
		yield return new WaitForSeconds(0.5f);
		base.gameObject.SetActive(false);
	}

	public void ItemSetting(int Number)
	{
	}

	public void Get()
	{
		GET_ing = true;
		GET_Possible = false;
		Hold = true;
		Delete();
	}

	public void Delete()
	{
		if (base.gameObject.activeSelf)
		{
			bounce_count = 0;
			rigidbody2D.gravityScale = 0f;
			rigidbody2D.velocity = new Vector2(0f, 0f);
			base.gameObject.layer = 9;
			base.gameObject.SetActive(false);
		}
	}

	public IEnumerator FadeOut()
	{
		yield return new WaitForSeconds(0.6f + Random.Range(0f, 0.4f));
		if (Item_Number != 0)
		{
			rigidbody2D.gravityScale = -0.1f;
			if (Item_Number.Equals(2))
			{
				UI_Master.me.Good_MSG(Localization.Get("GET_PORTAL"));
			}
			else if (Item_Number.Equals(3))
			{
				UI_Master.me.Good_MSG(Localization.Get("GET_HELLSTONE_BOX"));
				OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.localScale = Vector3.zero;
			}
			OBJ_Pool.me.Get_Rare_EF.transform.parent = ThisTransform;
			OBJ_Pool.me.Get_Rare_EF.transform.localPosition = new Vector3(0f, 0f, 0f);
			OBJ_Pool.me.Get_Rare_EF.gameObject.SetActive(false);
			OBJ_Pool.me.Get_Rare_EF.gameObject.SetActive(true);
			if (GGOK_Front)
			{
				float a = 1f;
				for (int i = 0; i < 50; i++)
				{
					a -= 0.02f;
					sprite_img.color = new Color(1f, 1f, 1f, a);
					yield return new WaitForSeconds(0.05f);
				}
			}
			OBJ_Pool.me.Get_Rare_EF.transform.parent = null;
		}
		else
		{
			float a2 = 1f;
			for (int j = 0; j < 10; j++)
			{
				a2 -= 0.1f;
				sprite_img.color = new Color(1f, 1f, 1f, a2);
				yield return new WaitForSeconds(0.025f);
			}
		}
		base.gameObject.SetActive(false);
	}

	public void Update()
	{
		if (Randing_Possible)
		{
			if (ThisTransform.localPosition.y <= Randing_Position)
			{
				rigidbody2D.velocity = new Vector2(0f, 0f);
				rigidbody2D.gravityScale = 0f;
				StopAllCoroutines();
				StartCoroutine(FadeOut());
				Randing_Possible = false;
			}
		}
		else if (ThisTransform.localPosition.y > 1f)
		{
			Randing_Possible = true;
		}
	}
}
