using UnityEngine;

public class Event_Shop_Panel : MonoBehaviour
{
	public Event_Shop_Icon[] event_Shop_Icons;

	public UILabel Event_Candy_label;

	public void Setting()
	{
		UI_Master.me.Popup(base.gameObject);
		Event_Candy_label.text = string.Format("{0}{1}", Now_Data.me.Event_Candy - Now_Data.me.PASWORD_Event_Candy, Localization.Get("COUNT"));
		for (int i = 0; i < event_Shop_Icons.Length; i++)
		{
			event_Shop_Icons[i].Setting();
		}
	}
}
