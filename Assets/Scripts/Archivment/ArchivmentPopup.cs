using Keiwando.BigInteger;
using UnityEngine;

public class ArchivmentPopup : MonoBehaviour
{
	public UILabel Star_count;

	public ArchivmentButtonBehaviour[] ArchivmentButtonBehaviours;

	public BigInteger Need_Star;

	private bool All_Clear = true;

	public void Setting()
	{
		Arch_LV_Setting();
		for (int i = 0; i < ArchivmentButtonBehaviours.Length; i++)
		{
			ArchivmentButtonBehaviours[i].Arch_Type_ID = i + 1;
			ArchivmentButtonBehaviours[i].Setting();
		}
		UI_Master.me.Popup(base.gameObject);
	}

	public void Arch_LV_Setting()
	{
		Star_count.text = string.Format("{0}", Now_Data.me.Arch_Star);
		Need_Star = 5 + Now_Data.me.Archievemnet_Upgrade_LV * 5;
	}

	public void Upgrade()
	{
		if (Now_Data.me.Arch_Star >= Need_Star)
		{
			Now_Data.me.Arch_Star -= Need_Star;
			Security.SetString("Arch_Star", Now_Data.me.Arch_Star.ToString());
			Now_Data.me.Archievemnet_Upgrade_LV++;
			Security.SetInt("Archievemnet_Upgrade_LV", Now_Data.me.Archievemnet_Upgrade_LV);
			Arch_LV_Setting();
			Fight_Master.me.All_setting();
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_STAR"));
		}
	}

	public void OnDisable()
	{
		All_Clear = true;
		for (int i = 0; i < UI_Master.me.ArchivmentPopup.ArchivmentButtonBehaviours.Length; i++)
		{
			if (UI_Master.me.ArchivmentPopup.ArchivmentButtonBehaviours[i].Get_Reward_Possible)
			{
				All_Clear = false;
				break;
			}
		}
		if (All_Clear)
		{
			UI_Master.me.Arch_Alram.SetActive(false);
			if (!UI_Master.me.PORTAL_PARTS_Alram.activeSelf)
			{
				UI_Master.me.PAUSE_Alram.SetActive(false);
			}
		}
		else
		{
			UI_Master.me.Arch_Alram.SetActive(true);
			UI_Master.me.PAUSE_Alram.SetActive(true);
		}
	}
}
