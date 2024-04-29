using UnityEngine;

public class Event_Shop_Icon : MonoBehaviour
{
	public int Item_ID;

	public UILabel name_Label;

	public UILabel word_Label;

	public UILabel Price_Label;

	public UILabel rest_time_Label;

	public int Price;

	public GameObject Btn_BUY;

	public GameObject Not_Today;

	public void Setting()
	{
		name_Label.text = Localization.Get(string.Format("EVENT_ITEM_NAME_{0:000}", Item_ID));
		word_Label.text = Localization.Get(string.Format("EVENT_ITEM_WORD_{0:000}", Item_ID));
		Price_Label.text = string.Format("{0}", Price);
		if (!Now_Data.me.EventShopItem_Used[Item_ID].Equals(0))
		{
			Not_Today.SetActive(true);
		}
		else
		{
			Not_Today.SetActive(false);
		}
	}

	public void Update()
	{
		if (Item_ID.Equals(0))
		{
			if (Now_Data.me.Mineral_CART_Buff_Time > 1f)
			{
				rest_time_Label.text = Time_Checker.ShowTime_Label_noT(Now_Data.me.Mineral_CART_Buff_Time);
			}
			else
			{
				rest_time_Label.text = string.Empty;
			}
		}
		else if (Item_ID.Equals(1))
		{
			if (Now_Data.me.Cooltime_Buff_Time > 1f)
			{
				rest_time_Label.text = Time_Checker.ShowTime_Label_noT(Now_Data.me.Cooltime_Buff_Time);
			}
			else
			{
				rest_time_Label.text = string.Empty;
			}
		}
	}

	public void BUY()
	{
		if (!Now_Data.me.EventShopItem_Used[Item_ID].Equals(0))
		{
			UI_Master.me.Warning(Localization.Get("NOT_TODAY"));
			return;
		}
		if (Item_ID.Equals(0))
		{
			if (Main_Player.me.Mineral_CART_ING)
			{
				UI_Master.me.Warning(Localization.Get("ALREADY_USE"));
				return;
			}
		}
		else if (Item_ID.Equals(1) && Main_Player.me.Cooltime_Buff_ING)
		{
			UI_Master.me.Warning(Localization.Get("ALREADY_USE"));
			return;
		}
		if (Now_Data.me.EVENT_Candy_Possible(Price))
		{
			Now_Data.me.EVENT_Candy_Change(-Price);
			SoundManager.me.Congretu();
			SoundManager.me.NEW_MISSILE();
			switch (Item_ID)
			{
			case 0:
				Main_Player.me.Mineral_CART_sprite.SetActive(true);
				Main_Player.me.Mineral_CART_ING = true;
				Now_Data.me.Mineral_CART_Buff_Time = 7200f;
				break;
			case 1:
				Now_Data.me.Cooltime_Buff_Time = 3600f;
				Main_Player.me.Cooltime_Buff_ING = true;
				Main_Player.me.Cooltime_Buff_sprite.SetActive(true);
				break;
			case 2:
				Now_Data.me.P_STONE_Change(Random.Range(99, 112));
				Now_Data.me.EventShopItem_Used[Item_ID] = 1;
				Security.SetInt(string.Format("EventShopItem_Used_{0}", Item_ID), 1);
				break;
			case 3:
				Now_Data.me.BOX_Count[0] += 7;
				Security.SetInt(string.Format("BOX_Count_{0:000}", 0), Now_Data.me.BOX_Count[0]);
				UI_Master.me.Popup_Close_All();
				UI_Master.me.box_Open_Panel.Setting(0, true);
				UI_Master.me.Box_Checking();
				break;
			case 4:
				Now_Data.me.BOX_Count[3]++;
				Security.SetInt(string.Format("BOX_Count_{0:000}", 3), Now_Data.me.BOX_Count[3]);
				UI_Master.me.Popup_Close_All();
				UI_Master.me.box_Open_Panel.Setting(3, true);
				Now_Data.me.EventShopItem_Used[Item_ID] = 1;
				Security.SetInt(string.Format("EventShopItem_Used_{0}", Item_ID), 1);
				break;
			case 5:
				Now_Data.me.BOX_Count[7]++;
				Security.SetInt(string.Format("BOX_Count_{0:000}", 7), Now_Data.me.BOX_Count[7]);
				UI_Master.me.Popup_Close_All();
				UI_Master.me.box_Open_Panel.Setting(7, true);
				Now_Data.me.EventShopItem_Used[Item_ID] = 1;
				Security.SetInt(string.Format("EventShopItem_Used_{0}", Item_ID), 1);
				break;
			case 6:
			{
				for (int i = 0; i < 3; i++)
				{
					int num = Random.Range(1, 5);
					int num2 = Random.Range(0, 10);
					Now_Data.me.portal_Parts[num].PORTAL_Parts_count[num2]++;
					if ((float)Random.Range(0, 100) < Now_Data.me.Portal_Parts_Drop_Per)
					{
						Now_Data.me.portal_Parts[num].PORTAL_Parts_count[num2]++;
					}
					Security.SetInt(string.Format("Portal_{0:000}_Parts_{1:000}_Count", num, num2), Now_Data.me.portal_Parts[num].PORTAL_Parts_count[num2]);
				}
				break;
			}
			}
			UI_Master.me.Popup_Close_All();
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("NEED_EVENT_CANDY_A"));
		}
	}
}
