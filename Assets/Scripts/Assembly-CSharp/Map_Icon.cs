using UnityEngine;

public class Map_Icon : MonoBehaviour
{
	public int Dungeon_ID;

	public bool Go_Possible;

	public UI2DSprite Map_Icon_sprite;

	public UI2DSprite Monster_sprite;

	public GameObject Flag;

	public bool Hell_P_Golrem;

	public void Setting()
	{
		Map_Icon_sprite.sprite2D = Sprite_DB.me.Map_Icon[Dungeon_DB.me.dungeon_DB[Dungeon_ID].Building_TYPE];
		Monster_sprite.sprite2D = Sprite_DB.me.Monster_Icon[Dungeon_DB.me.dungeon_DB[Dungeon_ID].Dungeon_TYPE];
		if (Flag != null)
		{
			Flag.SetActive(false);
		}
		if (Dungeon_ID > Now_Data.me.NOW_DUNGEON_LV)
		{
			Map_Icon_sprite.color = Color.gray;
			Monster_sprite.color = Color.gray;
			Monster_sprite.gameObject.SetActive(true);
			if (Flag != null)
			{
				Flag.SetActive(false);
			}
			Go_Possible = false;
		}
		else if (Dungeon_ID < Now_Data.me.NOW_DUNGEON_LV)
		{
			Map_Icon_sprite.color = Color.white;
			Monster_sprite.gameObject.SetActive(false);
			if (Flag != null)
			{
				Flag.SetActive(true);
			}
			Go_Possible = true;
		}
		else
		{
			Map_Icon_sprite.color = Color.white;
			Monster_sprite.color = Color.white;
			Monster_sprite.gameObject.SetActive(true);
			if (Flag != null)
			{
				Flag.SetActive(false);
			}
			Go_Possible = true;
		}
	}

	public void OnClick()
	{
		SoundManager.me.Setting();
		if (Hell_P_Golrem)
		{
			UI_Master.me.dungeon_Popup.DUNGEON_Intro_P_Stone();
		}
		else if (Go_Possible)
		{
			UI_Master.me.dungeon_Popup.DUNGEON_Intro_Popup_Setting(Dungeon_ID);
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NOTYET_GO"));
		}
	}
}
