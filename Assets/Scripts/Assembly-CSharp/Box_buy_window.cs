using UnityEngine;

public class Box_buy_window : MonoBehaviour
{
	public Shop_Item_BTN target_BTN;

	public UILabel Box_Name;

	public UI2DSprite target_box_sprite;

	public GameObject[] Bazuka_Pack;

	public GameObject Hellstone_obj;

	public UILabel Hellstone_Label;

	public GameObject GET_Uranium_obj;

	public UILabel GET_Uranium_Label;

	public GameObject GET_Box_obj;

	public UI2DSprite GET_Box_Sprite;

	public UILabel GET_Box_Label;

	public GameObject Option_A;

	public UISprite Option_A_Sprite;

	public UILabel Option_A_Label;

	public GameObject Option_B;

	public UISprite Option_B_Sprite;

	public UILabel Option_B_Label;

	public UILabel Total_Label;

	public UILabel Bazuka_Label;

	public UILabel Uranium_Label;

	public int Box_ID;

	public GameObject URANIUM_BTN;

	public GameObject IAP_BTN;

	public GameObject Booster_OBJ;

	public UILabel IAP_Price;

	public void Setting(Shop_Item_BTN target)
	{
		target_BTN = target;
		GET_Uranium_obj.SetActive(false);
		Hellstone_obj.SetActive(false);
		Booster_OBJ.SetActive(false);
		IAP_BTN.SetActive(false);
		URANIUM_BTN.SetActive(false);
		Option_A.SetActive(false);
		Option_B.SetActive(false);
		GET_Box_obj.SetActive(false);
		target_box_sprite.gameObject.SetActive(false);
		Total_Label.text = string.Empty;
		Bazuka_Label.text = string.Empty;
		for (int i = 0; i < Bazuka_Pack.Length; i++)
		{
			Bazuka_Pack[i].SetActive(false);
		}
		Box_Name.text = Localization.Get(string.Format("SHOP_ITEM_NAME_{0:000}", target.Item_ID));
		if (target.Item_ID < 11)
		{
			target_box_sprite.gameObject.SetActive(true);
			URANIUM_BTN.SetActive(true);
			Box_ID = target.Item_ID - 5;
			target_box_sprite.sprite2D = Sprite_DB.me.BOX_Icon[Box_ID];
			Uranium_Label.text = string.Format("{0}", target.Price);
			if (!Box_ID.Equals(5))
			{
				Option_A.SetActive(true);
				Option_B.SetActive(true);
			}
			Hellstone_obj.SetActive(true);
			switch (Box_ID)
			{
			case 1:
				Hellstone_Label.text = string.Format("{0}\n{1}", Localization.Get("HELLSTONE"), "50~75");
				Option_A_Sprite.spriteName = "Prop_MissileComponent_3";
				Option_A_Label.color = new Color32(0, 174, byte.MaxValue, byte.MaxValue);
				Option_A_Label.text = string.Format("{0}\n{1}", Localization.Get("MISSILE_TIRE_003"), "50%");
				Option_B_Sprite.spriteName = "Prop_MissileComponent_4";
				Option_B_Label.color = new Color32(220, 0, byte.MaxValue, byte.MaxValue);
				Option_B_Label.text = string.Format("{0}\n{1}", Localization.Get("MISSILE_TIRE_004"), "5%");
				Total_Label.text = string.Format("{0}\nX {1}", Localization.Get("MISSILE"), "25");
				break;
			case 2:
				Hellstone_Label.text = string.Format("{0}\n{1}", Localization.Get("HELLSTONE"), "150~200");
				Option_A_Sprite.spriteName = "Prop_MissileComponent_4";
				Option_A_Label.color = new Color32(220, 0, byte.MaxValue, byte.MaxValue);
				Option_A_Label.text = string.Format("{0}\n{1}", Localization.Get("MISSILE_TIRE_004"), "50%");
				Option_B_Sprite.spriteName = "Prop_MissileComponent_5";
				Option_B_Label.color = new Color32(byte.MaxValue, 220, 0, byte.MaxValue);
				Option_B_Label.text = string.Format("{0}\n{1}", Localization.Get("MISSILE_TIRE_005"), "5%");
				Total_Label.text = string.Format("{0}\nX {1}", Localization.Get("MISSILE"), "50");
				break;
			case 3:
				Hellstone_Label.text = string.Format("{0}\n{1}", Localization.Get("HELLSTONE"), "300~400");
				Option_A_Sprite.spriteName = "Prop_MissileComponent_5";
				Option_A_Label.color = new Color32(byte.MaxValue, 220, 0, byte.MaxValue);
				Option_A_Label.text = string.Format("{0}\n{1}{2}", Localization.Get("MISSILE_TIRE_005"), "2", Localization.Get("COUNT_FIX"));
				Option_B.SetActive(false);
				Total_Label.text = string.Format("{0}\nX {1}!", Localization.Get("MISSILE"), "250");
				break;
			case 4:
				Hellstone_Label.text = string.Format("{0}\n{1}", Localization.Get("HELLSTONE"), "600~700");
				Option_A_Sprite.spriteName = "Prop_MissileComponent_5";
				Option_A_Label.color = new Color32(byte.MaxValue, 220, 0, byte.MaxValue);
				Option_A_Label.text = string.Format("{0}\n{1}{2}", Localization.Get("MISSILE_TIRE_005"), "8", Localization.Get("COUNT_FIX"));
				Option_B_Sprite.spriteName = "Prop_MissileComponent_6";
				Option_B_Label.color = new Color32(byte.MaxValue, 150, 0, byte.MaxValue);
				Option_B_Label.text = string.Format("{0}\n{1}{2}", Localization.Get("MISSILE_TIRE_006"), "7", Localization.Get("COUNT_FIX"));
				Total_Label.text = string.Format("{0}\nX {1}!", Localization.Get("MISSILE"), "75");
				break;
			case 5:
				Hellstone_Label.text = string.Format("{0}\n{1}", Localization.Get("HELLSTONE"), "1750~2000");
				Total_Label.text = string.Empty;
				break;
			}
		}
		else if (target.Item_ID.Equals(17))
		{
			IAP_BTN.SetActive(true);
			GET_Uranium_obj.SetActive(true);
			Booster_OBJ.SetActive(true);
			GET_Uranium_Label.text = string.Format("{0}\n{1}", Localization.Get("URANIUM"), "500");
			IAP_Price.text = Localization.Get(string.Format("IAP_PRICE_{0}", 17));
		}
		else
		{
			GET_Box_obj.SetActive(true);
			IAP_BTN.SetActive(true);
			IAP_Price.text = Localization.Get(string.Format("IAP_PRICE_{0}", target.Item_ID));
			Bazuka_Pack[target.Item_ID - 18].SetActive(true);
			GET_Uranium_obj.SetActive(true);
			switch (target.Item_ID)
			{
			case 18:
				GET_Uranium_Label.text = string.Format("{0}\n{1}{2}", Localization.Get("URANIUM"), "500", Localization.Get("COUNT"));
				GET_Box_Sprite.sprite2D = Sprite_DB.me.BOX_Icon[2];
				GET_Box_Label.text = string.Format("{0}\n{1}{2}", Localization.Get("SHOP_ITEM_NAME_007"), "3", Localization.Get("COUNT"));
				Bazuka_Label.text = Localization.Get("BAZUKA_WORD_001");
				break;
			case 19:
				GET_Uranium_Label.text = string.Format("{0}\n{1}{2}", Localization.Get("URANIUM"), "3000", Localization.Get("COUNT"));
				GET_Box_Sprite.sprite2D = Sprite_DB.me.BOX_Icon[3];
				GET_Box_Label.text = string.Format("{0}\n{1}{2}", Localization.Get("SHOP_ITEM_NAME_008"), "3", Localization.Get("COUNT"));
				Bazuka_Label.text = Localization.Get("BAZUKA_WORD_002");
				break;
			case 20:
				GET_Uranium_Label.text = string.Format("{0}\n{1}{2}", Localization.Get("URANIUM"), "6000", Localization.Get("COUNT"));
				GET_Box_Sprite.sprite2D = Sprite_DB.me.BOX_Icon[4];
				GET_Box_Label.text = string.Format("{0}\n{1}{2}", Localization.Get("SHOP_ITEM_NAME_009"), "3", Localization.Get("COUNT"));
				Bazuka_Label.text = Localization.Get("BAZUKA_WORD_003");
				break;
			}
		}
		UI_Master.me.Popup(base.gameObject);
	}

	public void Buy()
	{
		target_BTN.REAL_BUY();
	}
}
