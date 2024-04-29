using UnityEngine;

public class Misile_Classup_panel : MonoBehaviour
{
	public bool SHOW;

	public UI2DSprite Misile_sprite;

	public UILabel label_Name;

	public UILabel label_Word;

	public int Misile_ID;

	public void Setting(int ID)
	{
		Misile_ID = ID;
		Misile_sprite.color = Color.white;
		Misile_sprite.sprite2D = Sprite_DB.me.Bazukaparts_Sprite[ID];
		label_Name.text = Localization.Get(string.Format("BAZUKA_PARTS_{0:000}", ID));
		if (Misile_DB.me.Bazuka_Parts_DB[ID].EQUIP_Option_A != 999)
		{
			if (Misile_DB.me.Bazuka_Parts_DB[ID].EQUIP_Option_A_Plus > 0f)
			{
				label_Word.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.Bazuka_Parts_DB[ID].EQUIP_Option_A)), Misile_DB.me.Bazuka_Parts_DB[ID].EQUIP_Option_A_Plus, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.Bazuka_Parts_DB[ID].EQUIP_Option_A)));
			}
			else
			{
				label_Word.text = string.Format("{0} {1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.Bazuka_Parts_DB[ID].EQUIP_Option_A)), Misile_DB.me.Bazuka_Parts_DB[ID].EQUIP_Option_A_Plus, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Misile_DB.me.Bazuka_Parts_DB[ID].EQUIP_Option_A)));
			}
		}
	}

	public void OnClick()
	{
		UI_Master.me.Close_Popup();
		UI_Master.me.Misile_Upgrade_Popup.Get_Parts(Misile_ID);
	}
}
