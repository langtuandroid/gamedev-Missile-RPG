using UnityEngine;

public class Info_Popup : MonoBehaviour
{
	public Status_UI[] status_UIs;

	public void Setting()
	{
		int num = 1;
		for (int i = 0; i < status_UIs.Length; i++)
		{
			status_UIs[i].Status_ID = num;
			status_UIs[i].Setting();
			num++;
			if (num.Equals(12) || num.Equals(13))
			{
				num = 14;
			}
			if (num.Equals(55) || num.Equals(56))
			{
				num = 57;
			}
			if (num.Equals(72) || num.Equals(74))
			{
				num++;
			}
		}
		UI_Master.me.Popup(base.gameObject);
	}
}
