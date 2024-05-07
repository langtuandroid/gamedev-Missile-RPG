using UnityEngine;

public class Portal : MonoBehaviour
{
	public int Portal_Color;

	public int Drop_Parts;

	public SpriteRenderer Main_Sprite_A;

	public SpriteRenderer Main_Sprite_B;

	public SpriteRenderer[] Portal_Parts;

	public UISlider Timer;

	public Enemy enemy;

	public float life_time;

	public void Setting(bool ToDefault)
	{
		for (int i = 0; i < Portal_Parts.Length; i++)
		{
			Portal_Parts[i].gameObject.SetActive(false);
		}
		if (Fight_Master.me.Rare_Portal_Possible && !ToDefault)
		{
			Fight_Master.me.Rare_Portal_Possible = false;
			Drop_Parts = Random.Range(0, 10);
			Portal_Parts[Drop_Parts].gameObject.SetActive(true);
			Portal_Color = Random.Range(1, 5);
			switch (Portal_Color)
			{
			case 1:
				Main_Sprite_A.sprite = Sprite_DB.me.Portal_Parts_Red[10];
				Main_Sprite_B.sprite = Sprite_DB.me.Portal_Parts_Red[11];
				Portal_Parts[Drop_Parts].sprite = Sprite_DB.me.Portal_Parts_Red[Drop_Parts];
				break;
			case 2:
				Main_Sprite_A.sprite = Sprite_DB.me.Portal_Parts_Green[10];
				Main_Sprite_B.sprite = Sprite_DB.me.Portal_Parts_Green[11];
				Portal_Parts[Drop_Parts].sprite = Sprite_DB.me.Portal_Parts_Green[Drop_Parts];
				break;
			case 3:
				Main_Sprite_A.sprite = Sprite_DB.me.Portal_Parts_Blue[10];
				Main_Sprite_B.sprite = Sprite_DB.me.Portal_Parts_Blue[11];
				Portal_Parts[Drop_Parts].sprite = Sprite_DB.me.Portal_Parts_Blue[Drop_Parts];
				break;
			case 4:
				Main_Sprite_A.sprite = Sprite_DB.me.Portal_Parts_Black[10];
				Main_Sprite_B.sprite = Sprite_DB.me.Portal_Parts_Black[11];
				Portal_Parts[Drop_Parts].sprite = Sprite_DB.me.Portal_Parts_Black[Drop_Parts];
				break;
			}
			UI_Master.me.Good_MSG(Localization.Get("RARE_PORTAL"));
			enemy.Chat(Localization.Get("RARE_PORTAL_CHAT"));
			Timer.gameObject.SetActive(true);
			life_time = 30f;
			Timer.value = 1f;
		}
		else
		{
			Default_Portal_Setting();
		}
	}

	public void Default_Portal_Setting()
	{
		life_time = 0f;
		Portal_Color = 0;
		Drop_Parts = 0;
		Timer.gameObject.SetActive(false);
		for (int i = 0; i < Portal_Parts.Length; i++)
		{
			Portal_Parts[i].gameObject.SetActive(false);
		}
		Main_Sprite_A.sprite = Sprite_DB.me.Portal_Parts_Empty[10];
		Main_Sprite_B.sprite = Sprite_DB.me.Portal_Parts_Empty[11];
		enemy.HP_bar.gameObject.SetActive(true);
		enemy.HP_text.gameObject.SetActive(true);
	}

	public void ReSetting()
	{
		UI_Master.me.Warning(Localization.Get("PORTAL_RUN"));
		enemy.HP = enemy.Basic_Data.HP;
		enemy.HP_bar.value = 1f;
		enemy.HP_text.text = Now_Data.INT_to_ABC(enemy.HP);
		enemy.PS = PlayerState.Idle;
		base.gameObject.layer = 11;
		enemy.ThisTransform.position = new Vector3(Fight_Master.me.Portal_Spawn.position.x, Fight_Master.me.Portal_Spawn.position.y + Random.Range(-1f, 1f), Fight_Master.me.Portal_Spawn.position.z);
		enemy.Z_Update();
		Default_Portal_Setting();
	}

	public void FixedUpdate()
	{
		if (Portal_Color != 0)
		{
			life_time -= 0.05f * Fight_Master.me.Game_Speed;
			Timer.value = life_time / 30f;
			if (life_time <= 0f)
			{
				ReSetting();
			}
		}
	}
}
