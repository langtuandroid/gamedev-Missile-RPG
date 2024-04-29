using UnityEngine;

public class Shop_Panel : MonoBehaviour
{
	public GameObject[] Tap_BTNs;

	public GameObject[] Drag_Menu;

	public GameObject Guide_Icon;

	public Box_buy_window box_buy_window;

	public Shop_Item_BTN[] Shop_Item_BTNs;

	public int Selected_tap;

	public void Clean()
	{
		for (int i = 0; i < Drag_Menu.Length; i++)
		{
			Drag_Menu[i].SetActive(false);
		}
	}

	public void Setting()
	{
		for (int i = 0; i < Shop_Item_BTNs.Length; i++)
		{
			Shop_Item_BTNs[i].Setting();
		}
		UI_Master.me.Popup(base.gameObject);
		Tap_D();
	}

	public void Open_Tap()
	{
		SoundManager.me.Click();
		Guide_Icon.transform.position = Tap_BTNs[Selected_tap].transform.position;
		Drag_Menu[Selected_tap].SetActive(true);
	}

	public void Tap_A()
	{
		Clean();
		Selected_tap = 0;
		Open_Tap();
	}

	public void Tap_B()
	{
		Clean();
		Selected_tap = 1;
		Open_Tap();
		UI_Master.me.uranium_Gift_Popup.Setting(8);
	}

	public void Tap_C()
	{
		Clean();
		Selected_tap = 2;
		Open_Tap();
	}

	public void Tap_D()
	{
		Clean();
		Selected_tap = 3;
		Open_Tap();
		UI_Master.me.uranium_Gift_Popup.Setting(9);
	}

	public void OnDisable()
	{
		if (UI_Master.me.Dungeon_Clear_Popup.gameObject.activeSelf)
		{
			UI_Master.me.Dungeon_Clear_Popup.Clear_info.SetActive(true);
		}
	}
}
