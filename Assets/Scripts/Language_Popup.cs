using UnityEngine;

public class Language_Popup : MonoBehaviour
{
	public Transform Selected_BG;

	public Transform[] Laungage_BTN;

	public void Awake()
	{
		if (PlayerPrefs.GetString("Language", "None").Equals("None"))
		{
			switch (Application.systemLanguage)
			{
			default:
				PlayerPrefs.SetString("Language", "English");
				Localization.language = "English";
				break;
			case SystemLanguage.Korean:
				PlayerPrefs.SetString("Language", "Korean");
				Localization.language = "Korean";
				break;
			case SystemLanguage.Japanese:
				PlayerPrefs.SetString("Language", "Japanese");
				Localization.language = "Japanese";
				break;
			case SystemLanguage.Chinese:
			case SystemLanguage.ChineseTraditional:
				PlayerPrefs.SetString("Language", "ChineseTraditional");
				Localization.language = "ChineseTraditional";
				break;
			case SystemLanguage.ChineseSimplified:
				PlayerPrefs.SetString("Language", "ChineseSimplified");
				Localization.language = "ChineseSimplified";
				break;
			case SystemLanguage.Spanish:
				PlayerPrefs.SetString("Language", "Spanish");
				Localization.language = "Spanish";
				break;
			}
		}
	}

	public void Setting()
	{
		switch (PlayerPrefs.GetString("Language", "English"))
		{
		case "English":
			Selected_BG.localPosition = Laungage_BTN[0].localPosition;
			break;
		case "Korean":
			Selected_BG.localPosition = Laungage_BTN[1].localPosition;
			break;
		case "ChineseSimplified":
			Selected_BG.localPosition = Laungage_BTN[2].localPosition;
			break;
		case "Japanese":
			Selected_BG.localPosition = Laungage_BTN[3].localPosition;
			break;
		case "Spanish":
			Selected_BG.localPosition = Laungage_BTN[4].localPosition;
			break;
		case "ChineseTraditional":
			Selected_BG.localPosition = Laungage_BTN[5].localPosition;
			break;
		}
		UI_Master.me.Popup(base.gameObject);
		UI_Master.me.MainIcon_Set.SetActive(false);
		UI_Master.me.MainIcon_Set.SetActive(true);
		UI_Master.me.sub_quest_ui[0].LANG_CHANGE();
		UI_Master.me.sub_quest_ui[1].LANG_CHANGE();
	}

	public void Set_English()
	{
		UI_Master.me.Popup_Close_All();
		SoundManager.me.Congretu();
		PlayerPrefs.SetString("Language", "English");
		Localization.language = "English";
		Setting();
	}

	public void Set_Korean()
	{
		UI_Master.me.Popup_Close_All();
		SoundManager.me.Congretu();
		PlayerPrefs.SetString("Language", "Korean");
		Localization.language = "Korean";
		Setting();
	}

	public void Set_Japanese()
	{
		UI_Master.me.Popup_Close_All();
		SoundManager.me.Congretu();
		PlayerPrefs.SetString("Language", "Japanese");
		Localization.language = "Japanese";
		Setting();
	}

	public void Set_Chinese_Simple()
	{
		UI_Master.me.Popup_Close_All();
		SoundManager.me.Congretu();
		PlayerPrefs.SetString("Language", "ChineseSimplified");
		Localization.language = "ChineseSimplified";
		Setting();
	}

	public void Set_Chinese_Traditional()
	{
		UI_Master.me.Popup_Close_All();
		SoundManager.me.Congretu();
		PlayerPrefs.SetString("Language", "ChineseTraditional");
		Localization.language = "ChineseTraditional";
		Setting();
	}

	public void Set_Spanish()
	{
		UI_Master.me.Popup_Close_All();
		SoundManager.me.Congretu();
		PlayerPrefs.SetString("Language", "Spanish");
		Localization.language = "Spanish";
		Setting();
	}
}
