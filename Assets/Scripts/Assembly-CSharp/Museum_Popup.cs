using UnityEngine;

public class Museum_Popup : MonoBehaviour
{
	public PortalParts_Info[] portalParts_Info;

	public UILabel Total_set_label;

	public UILabel Total_set_bonus_label;

	public void Setting()
	{
		Now_Data.me.Portal_BEST_Count();
		for (int i = 0; i < portalParts_Info.Length; i++)
		{
			portalParts_Info[i].Portal_Color_ID = i + 1;
			portalParts_Info[i].Setting();
		}
		UI_Master.me.Popup(base.gameObject);
		Total_set_label.text = string.Format("{0} : {1}", Localization.Get(string.Format("SET_NAME_{0:000}", 0)), Now_Data.me.Total_PotalSet_LV);
		Total_set_bonus_label.text = string.Format("{0} : [FF9D34FF]X {1}[-] ( {2} : [FF9D34FF]X {3}[-])", Localization.Get(string.Format("SET_NAME_{0:000}", 0)), Now_Data.me.Total_PotalSet_LV * 4, Localization.Get(string.Format("NEXT_LV")), (Now_Data.me.Total_PotalSet_LV + 1) * 4);
		UI_Master.me.PORTAL_PARTS_Alram.SetActive(false);
		if (!UI_Master.me.Arch_Alram.activeSelf)
		{
			UI_Master.me.PAUSE_Alram.SetActive(false);
		}
	}
}
