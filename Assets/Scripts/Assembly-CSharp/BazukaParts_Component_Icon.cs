using UnityEngine;

public class BazukaParts_Component_Icon : MonoBehaviour
{
	public int ID;

	public UILabel Parts_count_Label;

	public UI2DSprite Parts_IMG;

	public UILabel Option_Label;

	public void Setting()
	{
		if (Now_Data.me.Bazuka_Parts[ID] > 0)
		{
			Parts_count_Label.transform.parent.gameObject.SetActive(true);
			Parts_count_Label.text = string.Format("{0}", Now_Data.me.Bazuka_Parts[ID]);
			Parts_IMG.color = Color.white;
			Option_Label.text = string.Format("{0}\n+{1}%", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Misile_DB.me.Bazuka_Parts_DB[ID].EQUIP_Option_A)), Misile_DB.me.Bazuka_Parts_DB[ID].EQUIP_Option_A_Plus * (float)Now_Data.me.Bazuka_Parts[ID]);
		}
		else
		{
			Parts_count_Label.transform.parent.gameObject.SetActive(false);
			Parts_IMG.color = Color.black;
			Option_Label.text = string.Empty;
		}
	}
}
