using UnityEngine;

public class PortalParts_Info : MonoBehaviour
{
	public UILabel Set_Name_label;

	public UILabel DMG_Bonus_label;

	public int Portal_Color_ID;

	public UI2DSprite[] portal_part_sprites;

	public UILabel[] portal_part_counts;

	public UILabel Total_DMG_Bonus_label;

	public float PER;

	private float PORTAL_EF_VALUE = 2f;

	public void Setting()
	{
		Set_Name_label.text = string.Format("[FFEF64FF]{0}[-] : {1}", Localization.Get(string.Format("SET_NAME_{0:000}", Portal_Color_ID)), Now_Data.me.portal_Parts[Portal_Color_ID].BEST_LEVEL);
		DMG_Bonus_label.text = string.Format("{0} : [FF9D34FF]X{1}[-] ( {2} : [FF9D34FF]X{3}[-])", Localization.Get(string.Format("SET_NAME_{0:000}", Portal_Color_ID)), Now_Data.me.portal_Parts[Portal_Color_ID].BEST_LEVEL * 4, Localization.Get(string.Format("NEXT_LV")), (Now_Data.me.portal_Parts[Portal_Color_ID].BEST_LEVEL + 1) * 4);
		for (int i = 0; i < portal_part_sprites.Length; i++)
		{
			if (Now_Data.me.portal_Parts[Portal_Color_ID].PORTAL_Parts_count[i] > 0)
			{
				switch (Portal_Color_ID)
				{
				case 1:
					portal_part_sprites[i].sprite2D = Sprite_DB.me.Portal_Parts_Red[i];
					break;
				case 2:
					portal_part_sprites[i].sprite2D = Sprite_DB.me.Portal_Parts_Green[i];
					break;
				case 3:
					portal_part_sprites[i].sprite2D = Sprite_DB.me.Portal_Parts_Blue[i];
					break;
				case 4:
					portal_part_sprites[i].sprite2D = Sprite_DB.me.Portal_Parts_Black[i];
					break;
				}
				portal_part_counts[i].text = string.Format("{0}", Now_Data.me.portal_Parts[Portal_Color_ID].PORTAL_Parts_count[i]);
			}
			else
			{
				portal_part_sprites[i].sprite2D = Sprite_DB.me.Portal_Parts_Empty[i];
				portal_part_counts[i].text = string.Format(string.Empty);
			}
		}
		PER = 0f;
		int num = 0;
		for (int j = 0; j < Now_Data.me.portal_Parts[Portal_Color_ID].PORTAL_Parts_count.Length; j++)
		{
			num += Now_Data.me.portal_Parts[Portal_Color_ID].PORTAL_Parts_count[j];
		}
		if (Now_Data.me.portal_Parts[Portal_Color_ID].BEST_LEVEL > 0)
		{
			if (Now_Data.me.Total_PotalSet_LV > 0)
			{
				PER = (float)num * PORTAL_EF_VALUE * (float)(Now_Data.me.portal_Parts[Portal_Color_ID].BEST_LEVEL * 4) * (float)Now_Data.me.Total_PotalSet_LV * 4f;
			}
			else
			{
				PER = (float)num * PORTAL_EF_VALUE * (float)(Now_Data.me.portal_Parts[Portal_Color_ID].BEST_LEVEL * 4);
			}
		}
		else
		{
			PER = (float)num * PORTAL_EF_VALUE;
		}
		switch (Portal_Color_ID)
		{
		case 1:
			Total_DMG_Bonus_label.text = string.Format("[FFEF64FF]{0}[-] [FFEF64FF]+ {1}%[-] ", Localization.Get("STATUS_014_NAME_A"), PER);
			break;
		case 2:
			Total_DMG_Bonus_label.text = string.Format("[FFEF64FF]{0}[-] [FFEF64FF]+ {1}% ", Localization.Get("STATUS_015_NAME_A"), PER);
			break;
		case 3:
			Total_DMG_Bonus_label.text = string.Format("[FFEF64FF]{0}[-] [FFEF64FF]+ {1}% ", Localization.Get("STATUS_016_NAME_A"), PER);
			break;
		case 4:
			Total_DMG_Bonus_label.text = string.Format("[FFEF64FF]{0}[-] [FFEF64FF]+ {1}% ", Localization.Get("STATUS_008_NAME_A"), PER);
			break;
		}
	}
}
