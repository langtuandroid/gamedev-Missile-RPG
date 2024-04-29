using UnityEngine;

public class Skill_Popup : MonoBehaviour
{
	public Skill_Upgrade_BTN[] Skill_Upgrade_BTNs;

	public static Skill_Popup me;

	public void Awake()
	{
		me = this;
	}

	public void Setting(bool OPEN)
	{
		for (int i = 0; i < Skill_Upgrade_BTNs.Length; i++)
		{
			Skill_Upgrade_BTNs[i].Skill_ID = i;
			Skill_Upgrade_BTNs[i].Setting(OPEN);
			if (i > 0)
			{
				if (Now_Data.me.Active_Skill_LV[i - 1] > 0)
				{
					Skill_Upgrade_BTNs[i].gameObject.SetActive(true);
				}
				else
				{
					Skill_Upgrade_BTNs[i].gameObject.SetActive(false);
				}
			}
		}
	}
}
