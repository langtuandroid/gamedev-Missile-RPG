using UnityEngine;

public class Bazuka_SetIcon : MonoBehaviour
{
	public int ID;

	public UI2DSprite Bazuka_sprite;

	public UILabel Word_label;

	public UISprite Main_BG;

	public void Setting()
	{
		if (Now_Data.me.Bazuka_Possible[ID] > 0)
		{
			Bazuka_sprite.color = Color.white;
			Main_BG.color = Color.white;
		}
		else
		{
			Bazuka_sprite.color = Color.black;
			Main_BG.color = Color.gray;
		}
	}

	public void Equip()
	{
		if (Now_Data.me.Bazuka_Possible[ID] > 0)
		{
			Now_Data.me.Bazuka_ID = ID;
			Security.SetInt("Bazuka_ID", Now_Data.me.Bazuka_ID);
			Fight_Master.me.All_setting();
			UI_Master.me.Misile_Upgrade_Popup.Setting();
			UI_Master.me.Misile_Upgrade_Popup.Update_Bazuka_popup();
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_BAZUKA"));
		}
	}
}
