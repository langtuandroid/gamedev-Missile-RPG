using DG.Tweening;
using UnityEngine;

public class BoxFairy : MonoBehaviour
{
	public int Fairy_ID;

	public Transform ThisTransform;

	public float Speed = 10f;

	public GameObject FairyBOX_BOY;

	public GameObject FairyBOX_MAN;

	public int ROLL_COUNT;

	public void Setting(int ID)
	{
		Fairy_ID = ID;
		if (Fairy_ID < 4)
		{
			ThisTransform.position = Fight_Master.me.Fairy_Point_A.position;
			FairyBOX_BOY.SetActive(true);
			FairyBOX_MAN.SetActive(false);
		}
		else
		{
			ThisTransform.position = Fight_Master.me.Fairy_Point_B.position;
			FairyBOX_BOY.SetActive(false);
			FairyBOX_MAN.SetActive(true);
		}
		ROLL_COUNT = 4;
		base.gameObject.SetActive(true);
		ThisTransform.localScale = new Vector3(1f, 1f, 1f);
		Speed = 1f;
		ThisTransform.DOMoveY(2.5f, 1f);
	}

	public void FixedUpdate()
	{
		ThisTransform.Translate(Vector3.left * Speed * 0.05f * Fight_Master.me.Game_Speed);
		if (ROLL_COUNT.Equals(4))
		{
			if (ThisTransform.position.x < -2f)
			{
				ROLL_COUNT--;
				Speed *= -1f;
				ThisTransform.localScale = new Vector3(-1f, 1f, 1f);
			}
		}
		else if (ROLL_COUNT.Equals(3) && ThisTransform.position.x > 4f)
		{
			ROLL_COUNT--;
			Speed *= -1f;
			ThisTransform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (ROLL_COUNT.Equals(2) && ThisTransform.position.x < -3f)
		{
			ROLL_COUNT--;
			Speed *= -1f;
			ThisTransform.localScale = new Vector3(-1f, 1f, 1f);
		}
		if (ROLL_COUNT.Equals(1))
		{
			if (ThisTransform.position.x > 3f)
			{
				ROLL_COUNT--;
				Speed *= -1f;
				ThisTransform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		else if (ThisTransform.position.x < -12f)
		{
			base.gameObject.SetActive(false);
		}
	}

	public void OnMouseDown()
	{
		if (UI_Master.me.popups[0] == null)
		{
			if (Fight_Master.me.SAVE_POPUP_ID.Equals(0) || Now_Data.me.ALL_DROPPORT > 2)
			{
				UI_Master.me.fairybox_POPUP.target = this;
				UI_Master.me.fairybox_POPUP.Setting(Fairy_ID);
			}
			else
			{
				UI_Master.me.shop_Panel.box_buy_window.Setting(UI_Master.me.shop_Panel.Shop_Item_BTNs[Fight_Master.me.SAVE_POPUP_ID]);
				Fight_Master.me.SAVE_POPUP_ID = 0;
			}
		}
	}

	public void EXIT()
	{
		OBJ_Pool.Make_OBJ(OBJ_Pool.me.effect_boom, ref OBJ_Pool.effect_boom_Number, FairyBOX_BOY.transform.position);
		OBJ_Pool.Make_OBJ(OBJ_Pool.me.effect_die, ref OBJ_Pool.effect_die_Number, FairyBOX_BOY.transform.position);
		SoundManager.me.Artifact_Upgrade();
		Now_Data.me.EVENT_Candy_Change(1);
		base.gameObject.SetActive(false);
	}
}
