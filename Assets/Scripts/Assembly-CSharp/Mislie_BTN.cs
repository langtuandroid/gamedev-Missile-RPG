using UnityEngine;

public class Mislie_BTN : MonoBehaviour
{
	public UI2DSprite Misile_sprite;

	public UISprite BG_sprite;

	public int Misile_ID;

	public UILabel Parts_count_label;

	public UISlider Parts_count_slider;

	public UILabel LV_label;

	public int Need_Parts;

	public GameObject Equiped_ICON;

	public bool for_EQUIP_CHANGE;

	public void Setting(int ID)
	{
		Misile_ID = ID;
		Misile_sprite.color = Color.white;
		Misile_sprite.sprite2D = Sprite_DB.me.Misile_Sprite[ID];
		Misile_sprite.transform.parent.localPosition = new Vector3((360f - Misile_sprite.sprite2D.textureRect.width) / 3.5f, 18f, 0f);
		BG_sprite.spriteName = string.Format("UI_Missile_Box{0}", Misile_DB.me.misile_DB[ID].Rare - 1);
		if (Now_Data.me.Misile_TIER[ID] < 20)
		{
			Need_Parts = Misile_DB.me.RARE_misile_DB[Misile_DB.me.misile_DB[ID].Rare].Need_Parts[Now_Data.me.Misile_TIER[ID]];
		}
		Parts_count_label.text = string.Format("{0}/{1}", Now_Data.me.Misile_Parts[ID], Need_Parts);
		Parts_count_slider.value = (float)Now_Data.me.Misile_Parts[ID] / (float)Need_Parts;
		LV_label.text = string.Format("{0}", Now_Data.me.Misile_TIER[ID]);
		Equiped_ICON.SetActive(false);
		for (int i = 0; i < Now_Data.me.EQUIP_MISILEs.Length; i++)
		{
			if (Misile_ID.Equals(Now_Data.me.EQUIP_MISILEs[i]))
			{
				Equiped_ICON.SetActive(true);
				break;
			}
		}
	}

	public void OnClick()
	{
		if (for_EQUIP_CHANGE)
		{
			UI_Master.me.misile_Parts_Manager.Equip_Change_Target(Misile_ID);
		}
		else
		{
			UI_Master.me.misile_Parts_Manager.Setting(Misile_ID, false);
		}
	}

	public void Tierup()
	{
		if (Now_Data.me.P_STONE_Possible(Misile_DB.me.TIER_Price_by_LV(Misile_ID, Now_Data.me.Misile_TIER[Misile_ID])))
		{
			Now_Data.me.P_STONE_Change(-Misile_DB.me.TIER_Price_by_LV(Misile_ID, Now_Data.me.Misile_TIER[Misile_ID]));
			Now_Data.me.Misile_TIER[Misile_ID]++;
			Now_Data.me.Misile_Parts[Misile_ID] -= Need_Parts;
			Security.SetInt(string.Format("Misile_Parts_{0:000}", Misile_ID), Now_Data.me.Misile_Parts[Misile_ID]);
			Security.SetInt(string.Format("Misile_TIER_{0:000}", Misile_ID), Now_Data.me.Misile_TIER[Misile_ID]);
			Setting(Misile_ID);
			Fight_Master.me.All_setting();
			Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_MISSILE_COUNT);
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_HELLSTONE"));
		}
	}
}
