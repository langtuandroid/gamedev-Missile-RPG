using UnityEngine;

public class Box_Mini_Explain : MonoBehaviour
{
	public int ID;

	public UILabel Text;

	public UILabel Hellstone_Label;

	public void OnEnable()
	{
		switch (ID)
		{
		case 1:
			Text.text = string.Format("{0}\nX {1}", Localization.Get("MISSILE"), "25");
			Hellstone_Label.text = string.Format("{0}\n{1}", Localization.Get("HELLSTONE"), "50~75");
			break;
		case 2:
			Text.text = string.Format("{0}\nX {1}", Localization.Get("MISSILE"), "50");
			Hellstone_Label.text = string.Format("{0}\n{1}", Localization.Get("HELLSTONE"), "150~200");
			break;
		case 3:
			Text.text = string.Format("{0}\nX {1}", Localization.Get("MISSILE"), "250");
			Hellstone_Label.text = string.Format("{0}\n{1}", Localization.Get("HELLSTONE"), "300~400");
			break;
		case 4:
			Hellstone_Label.text = string.Format("{0}\n{1}", Localization.Get("HELLSTONE"), "600~700");
			Text.text = string.Format("{0}\nX {1}", Localization.Get("MISSILE"), "75");
			break;
		case 5:
			Hellstone_Label.text = string.Format("{0}\n{1}", Localization.Get("HELLSTONE"), "1750~2000");
			break;
		}
	}
}
