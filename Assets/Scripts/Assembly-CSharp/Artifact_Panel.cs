using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Keiwando.BigInteger;
using UnityEngine;

public class Artifact_Panel : MonoBehaviour
{
	public Artifact_Upgrde_BTN[] artifact_Upgrde_BTNs;

	public List<int> HIDE_things;

	public Artifact_Upgrde_BTN_Diablo[] artifact_Upgrde_BTNs_Diablo;

	public List<int> HIDE_things_diablo;

	public List<int> SHOW_things_diablo;

	public int Selected_Category;

	public GameObject Normal_Artifact_Scroll;

	public GameObject Diablo_Artifact_Scroll;

	public UISprite Artifact_Get_BTN_sprite;

	public GameObject NO_Artifact;

	public GameObject NO_Diablo_Artifact;

	public GameObject Flash;

	public UILabel Diablo_remain_Label;

	public BigInteger Get_Artifact_Price;

	public UILabel Get_Artifact_Price_label;

	public GameObject Get_Artifact_popup;

	public UI2DSprite Get_Artifact_sprite;

	public UILabel Get_Artifact_Name_label;

	public UILabel Get_Artifact_Word_label;

	public int Artifact_ID;

	public bool GET_ING;

	public GameObject Get_Artifact_BTN;

	public GameObject[] Gas_Icon;

	public GameObject Gas_station_Icon;

	public GameObject Gas_station_Open_Icon;

	public SpriteRenderer Final_sprite;

	public GameObject ING_BG;

	public void Setting()
	{
		Now_Data.me.Artifact_Possesion.Clear();
		for (int i = 0; i < Now_Data.me.Now_Artifact_LV.Length; i++)
		{
			if (Now_Data.me.Now_Artifact_LV[i] > 0)
			{
				Now_Data.me.Artifact_Possesion.Add(i);
			}
		}
		if (Now_Data.me.Artifact_Possesion.Count.Equals(Artifact_DB.me.artifact_DB.Length))
		{
			Get_Artifact_BTN.SetActive(false);
		}
		else
		{
			Get_Artifact_BTN.SetActive(true);
		}
		if (Now_Data.me.Artifact_Possesion.Count > 0)
		{
			NO_Artifact.SetActive(false);
		}
		else
		{
			NO_Artifact.SetActive(true);
		}
		SHOW_things_diablo.Clear();
		for (int j = 0; j < Now_Data.me.Diablo_Artifact_LV.Length; j++)
		{
			if (Now_Data.me.Diablo_Artifact_LV[j] > 0)
			{
				SHOW_things_diablo.Add(j);
			}
		}
		if (SHOW_things_diablo.Count > 0)
		{
			NO_Diablo_Artifact.SetActive(false);
		}
		else
		{
			NO_Diablo_Artifact.SetActive(true);
		}
		Set_BTNs(true);
		Get_Artifact_popup.SetActive(false);
		Price_Count();
		Gas_station_Icon.SetActive(true);
		Gas_station_Open_Icon.SetActive(false);
		Final_sprite.gameObject.SetActive(false);
		ING_BG.SetActive(false);
		if (Selected_Category.Equals(0))
		{
			Normal_Artifact_Scroll.SetActive(true);
			Diablo_Artifact_Scroll.SetActive(false);
		}
		else
		{
			Normal_Artifact_Scroll.SetActive(false);
			Diablo_Artifact_Scroll.SetActive(true);
		}
		int num = 0;
		for (int k = 0; k < Now_Data.me.Diablo_Artifact_Parts_count.Length; k++)
		{
			num += Now_Data.me.Diablo_Artifact_Parts_count[k];
		}
		Diablo_remain_Label.text = Localization.Get("DIABLO_EXPLAIN_A") + string.Format("{0}", num) + Localization.Get("DIABLO_EXPLAIN_B") + string.Format("{0}", 95 - num) + Localization.Get("DIABLO_EXPLAIN_C");
		Flash.SetActive(false);
		Flash.SetActive(true);
	}

	public void Price_Count()
	{
		Get_Artifact_Price = Artifact_DB.GET_ARTIFACT_PRICE[Now_Data.me.Artifact_Possesion.Count] * 10;
		Get_Artifact_Price_label.text = Now_Data.INT_to_ABC(Get_Artifact_Price);
		if (Now_Data.me.CRYSTAL_Possible(Get_Artifact_Price))
		{
			Artifact_Get_BTN_sprite.spriteName = "Btn_Artifact";
			UI_Master.me.Artifact_Alram.SetActive(true);
		}
		else
		{
			Artifact_Get_BTN_sprite.spriteName = "Btn_Disabled";
			UI_Master.me.Artifact_Alram.SetActive(false);
		}
	}

	public void Set_BTNs(bool OPEN)
	{
		for (int i = 0; i < artifact_Upgrde_BTNs.Length; i++)
		{
			if (i < Now_Data.me.Artifact_Possesion.Count)
			{
				artifact_Upgrde_BTNs[i].Artifact_ID = Now_Data.me.Artifact_Possesion[i];
				artifact_Upgrde_BTNs[i].Setting(OPEN);
				artifact_Upgrde_BTNs[i].gameObject.SetActive(true);
			}
			else
			{
				artifact_Upgrde_BTNs[i].gameObject.SetActive(false);
			}
		}
		for (int j = 0; j < artifact_Upgrde_BTNs_Diablo.Length; j++)
		{
			if (j < SHOW_things_diablo.Count)
			{
				artifact_Upgrde_BTNs_Diablo[j].Artifact_ID = SHOW_things_diablo[j];
				artifact_Upgrde_BTNs_Diablo[j].Setting(OPEN);
				artifact_Upgrde_BTNs_Diablo[j].gameObject.SetActive(true);
			}
			else
			{
				artifact_Upgrde_BTNs_Diablo[j].gameObject.SetActive(false);
			}
		}
	}

	public void Get_Artifact()
	{
		Gas_station_Icon.SetActive(true);
		Gas_station_Open_Icon.SetActive(false);
		Final_sprite.gameObject.SetActive(false);
		if (!GET_ING)
		{
			if (Now_Data.me.CRYSTAL_Possible(Get_Artifact_Price))
			{
				GET_ING = true;
				ING_BG.SetActive(true);
				Now_Data.me.CRYSTAL_Change(-Get_Artifact_Price);
				bool flag = false;
				int num = 200;
				while (!flag)
				{
					Artifact_ID = Random.Range(0, Now_Data.me.Now_Artifact_LV.Length);
					if (Now_Data.me.Now_Artifact_LV[Artifact_ID].Equals(0))
					{
						flag = false;
						break;
					}
					num--;
					if (num > 0)
					{
						continue;
					}
					for (int i = 0; i < Now_Data.me.Now_Artifact_LV.Length; i++)
					{
						if (Now_Data.me.Now_Artifact_LV[Artifact_ID].Equals(0))
						{
							Artifact_ID = i;
						}
					}
					break;
				}
				Now_Data.me.Now_Artifact_LV[Artifact_ID]++;
				Security.SetInt(string.Format("Now_Artifact_LV_{0:000}", Artifact_ID), Now_Data.me.Now_Artifact_LV[Artifact_ID]);
				Now_Data.me.Artifact_COUNT = 0;
				for (int j = 0; j < Now_Data.me.Now_Artifact_LV.Length; j++)
				{
					if (Now_Data.me.Now_Artifact_LV[j] > 0)
					{
						Now_Data.me.Artifact_COUNT++;
					}
				}
				Now_Data.me.Check_Possible(Quest_Goal_Type.ARTIFACT_COUNT);
				Get_Artifact_sprite.sprite2D = Sprite_DB.me.Artifact_Sprite[Artifact_ID];
				Get_Artifact_Name_label.text = Localization.Get(string.Format("ARTIFACT_{0:000}_NAME", Artifact_ID));
				if (!Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type.Equals(999))
				{
					switch (Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)
					{
					default:
						Get_Artifact_Word_label.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)));
						break;
					case 77:
					{
						float num2 = Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value;
						for (int k = 0; k < Now_Data.me.Diablo_Artifact_LV[Artifact_ID]; k++)
						{
							num2 *= 2f;
						}
						Get_Artifact_Word_label.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)));
						break;
					}
					case 51:
					case 54:
					case 65:
						Get_Artifact_Word_label.text = string.Format("{0} {1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)));
						break;
					case 36:
					case 37:
					case 38:
					case 39:
					case 40:
					case 41:
					case 42:
					case 46:
					case 48:
					case 50:
					case 81:
					case 82:
					case 83:
					case 84:
					case 85:
					case 86:
					case 87:
					case 88:
					case 89:
					case 90:
					case 105:
						Get_Artifact_Word_label.text = string.Format("{0} -{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_A_Type)));
						break;
					}
				}
				else
				{
					Get_Artifact_Word_label.text = string.Empty;
				}
				if (!Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type.Equals(999))
				{
					switch (Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)
					{
					default:
						Get_Artifact_Word_label.text += string.Format("\n{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)));
						break;
					case 77:
					{
						float num3 = Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value;
						for (int l = 0; l < Now_Data.me.Diablo_Artifact_LV[Artifact_ID]; l++)
						{
							num3 *= 2f;
						}
						Get_Artifact_Word_label.text += string.Format("\n{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)));
						break;
					}
					case 51:
					case 54:
					case 65:
						Get_Artifact_Word_label.text += string.Format("\n{0} {1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)));
						break;
					case 36:
					case 37:
					case 38:
					case 39:
					case 40:
					case 41:
					case 42:
					case 46:
					case 48:
					case 50:
					case 81:
					case 82:
					case 83:
					case 84:
					case 85:
					case 86:
					case 87:
					case 88:
					case 89:
					case 90:
					case 105:
						Get_Artifact_Word_label.text += string.Format("\n{0} -{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)Now_Data.me.Now_Artifact_LV[Artifact_ID], Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.artifact_DB[Artifact_ID].BUFF_B_Type)));
						break;
					}
				}
				StopAllCoroutines();
				StartCoroutine(Getting_Action());
				Fight_Master.me.All_setting();
			}
			else
			{
				UI_Master.me.Warning(Localization.Get("NEED_GAS"));
			}
		}
		else
		{
			UI_Master.me.Warning(Localization.Get("WAIT_PLS"));
		}
	}

	public IEnumerator Getting_Action()
	{
		for (int i = 0; i < Gas_Icon.Length; i++)
		{
			SoundManager.me.Click();
			Gas_Icon[i].SetActive(false);
			Gas_Icon[i].SetActive(true);
			yield return new WaitForSeconds(0.6f - 0.1f * (float)i);
		}
		yield return new WaitForSeconds(0.05f);
		Gas_station_Icon.transform.DOShakePosition(0.5f, 30f, 30, 180f);
		yield return new WaitForSeconds(0.5f);
		Gas_station_Icon.transform.localPosition = new Vector3(13f, -36f, 0f);
		SoundManager.me.Hit();
		Gas_station_Icon.SetActive(false);
		Gas_station_Open_Icon.SetActive(true);
		Final_sprite.sprite = Sprite_DB.me.Artifact_Sprite[Artifact_ID];
		Final_sprite.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		Final_sprite.gameObject.SetActive(false);
		SoundManager.me.NEW_MISSILE();
		UI_Master.me.Popup(Get_Artifact_popup);
		GET_ING = false;
		ING_BG.SetActive(false);
	}

	public void Close_Popup()
	{
		UI_Master.me.Close_Popup();
		Gas_station_Icon.SetActive(true);
		Gas_station_Open_Icon.SetActive(false);
		Setting();
	}

	public void Tap_Artifact()
	{
		SoundManager.me.Click();
		Selected_Category = 0;
		Setting();
	}

	public void Tap_Diablor_Artifact()
	{
		SoundManager.me.Click();
		Selected_Category = 1;
		Setting();
	}

	public void Show_Diablo_Artifact(int ID)
	{
		Artifact_ID = ID;
		Get_Artifact_sprite.sprite2D = Sprite_DB.me.Diablo_Artifact_Sprite[Artifact_ID];
		Get_Artifact_Name_label.text = Localization.Get(string.Format("ARTIFACT_NAME_DIABLO_{0:000}", Artifact_ID));
		int num = Now_Data.me.Diablo_Artifact_LV[Artifact_ID];
		if (num < 1)
		{
			num = 1;
		}
		if (Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type != 999)
		{
			switch (Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)
			{
			default:
				Get_Artifact_Word_label.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)num, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)));
				break;
			case 77:
			{
				float num2 = Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value;
				for (int i = 0; i < Now_Data.me.Diablo_Artifact_LV[Artifact_ID]; i++)
				{
					num2 *= 2f;
				}
				Get_Artifact_Word_label.text = string.Format("{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)), num2, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)));
				break;
			}
			case 51:
			case 54:
			case 65:
				Get_Artifact_Word_label.text = string.Format("{0} {1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)num, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)));
				break;
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 41:
			case 42:
			case 46:
			case 48:
			case 50:
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 105:
				Get_Artifact_Word_label.text = string.Format("{0} -{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Value_PLUS * (float)num, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_A_Type)));
				break;
			}
		}
		else
		{
			Get_Artifact_Word_label.text = string.Empty;
		}
		if (Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type != 999)
		{
			switch (Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)
			{
			default:
				Get_Artifact_Word_label.text += string.Format("\n{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)num, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)));
				break;
			case 77:
			{
				float num3 = Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value;
				for (int j = 0; j < Now_Data.me.Diablo_Artifact_LV[Artifact_ID]; j++)
				{
					num3 *= 2f;
				}
				Get_Artifact_Word_label.text += string.Format("\n{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)), num3, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)));
				break;
			}
			case 51:
			case 54:
			case 65:
				Get_Artifact_Word_label.text += string.Format("\n{0} {1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)num, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)));
				break;
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 41:
			case 42:
			case 46:
			case 48:
			case 50:
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 105:
				Get_Artifact_Word_label.text += string.Format("\n{0} +{1}{2}", Localization.Get(string.Format("STATUS_{0:000}_NAME_A", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)), Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value + Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Value_PLUS * (float)num, Localization.Get(string.Format("STATUS_{0:000}_NAME_B", Artifact_DB.me.diablo_artifact_DB[Artifact_ID].BUFF_B_Type)));
				break;
			}
		}
		SoundManager.me.NEW_MISSILE();
		UI_Master.me.Popup(Get_Artifact_popup);
		Debug.Log("새창열기");
	}
}
