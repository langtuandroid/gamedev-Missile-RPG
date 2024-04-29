using UnityEngine;

public class Reward_Icon : MonoBehaviour
{
	public UI2DSprite Target_sprite;

	public UILabel Count_Label;

	public bool Empty;

	public void Set_Empty()
	{
		Empty = true;
		Target_sprite.gameObject.SetActive(false);
		Count_Label.gameObject.SetActive(false);
	}

	public void Setting_Misile(int ID, int value)
	{
		Empty = false;
		Target_sprite.sprite2D = Sprite_DB.me.Parts_Sprite[ID];
		Count_Label.text = Now_Data.INT_to_ABC(value);
		Target_sprite.gameObject.SetActive(true);
		Count_Label.gameObject.SetActive(true);
	}

	public void Setting_P_stone(int value)
	{
		Empty = false;
		Count_Label.text = Now_Data.INT_to_ABC(value);
		Target_sprite.width = 57;
		Target_sprite.height = 45;
		Target_sprite.sprite2D = Sprite_DB.me.P_stone;
		Target_sprite.gameObject.SetActive(true);
		Count_Label.gameObject.SetActive(true);
	}

	public void Setting_Box(int ID, int value)
	{
		Empty = false;
		Target_sprite.sprite2D = Sprite_DB.me.BOX_Icon[ID];
		Target_sprite.width = 220;
		Target_sprite.height = 220;
		if (value > 1)
		{
			Count_Label.text = string.Format("X {0}", value);
		}
		else
		{
			Count_Label.text = string.Empty;
		}
		Target_sprite.gameObject.SetActive(true);
		Count_Label.gameObject.SetActive(true);
	}
}
