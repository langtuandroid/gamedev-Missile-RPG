using UnityEngine;

public class Download_BTN_Flow : MonoBehaviour
{
	public int delay = 280;

	public Transform Download_AA;

	public Transform Download_BB;

	public Transform[] BTNs;

	public bool PACK_SHOP;

	public GameObject Speed_double;

	public GameObject Speed_triple;

	public void OnEnable()
	{
		if (PACK_SHOP)
		{
			if (Now_Data.me.BEST_LV < 210)
			{
				Speed_double.SetActive(false);
				Speed_triple.SetActive(false);
				for (int i = 0; i < BTNs.Length; i++)
				{
					BTNs[i].localPosition = new Vector3(i * 280, 0f, 0f);
				}
			}
			else if (Security.GetInt("IAP_GameSpeed", 0).Equals(0))
			{
				Speed_double.SetActive(true);
				Speed_triple.SetActive(false);
				for (int j = 0; j < BTNs.Length; j++)
				{
					BTNs[j].localPosition = new Vector3(280 + j * 280, 0f, 0f);
				}
			}
			else if (Security.GetInt("IAP_GameSpeed", 0).Equals(1))
			{
				Speed_double.SetActive(false);
				Speed_triple.SetActive(true);
				for (int k = 0; k < BTNs.Length; k++)
				{
					BTNs[k].localPosition = new Vector3(280 + k * 280, 0f, 0f);
				}
			}
			else
			{
				Speed_double.SetActive(false);
				Speed_triple.SetActive(false);
				for (int l = 0; l < BTNs.Length; l++)
				{
					BTNs[l].localPosition = new Vector3(l * 280, 0f, 0f);
				}
			}
		}
		else if (Application.platform.Equals(RuntimePlatform.IPhonePlayer))
		{
			Download_AA.gameObject.SetActive(false);
			Download_BB.gameObject.SetActive(false);
			for (int m = 0; m < BTNs.Length; m++)
			{
				BTNs[m].localPosition = new Vector3(m * 280, 0f, 0f);
			}
		}
		else if (Security.GetInt("DOWNLOAD_AA", 0).Equals(0))
		{
			Download_AA.localPosition = new Vector3(0f, 0f, 0f);
			if (Security.GetInt("DOWNLOAD_B", 0).Equals(0))
			{
				Download_BB.localPosition = new Vector3(280f, 0f, 0f);
				for (int n = 0; n < BTNs.Length; n++)
				{
					BTNs[n].localPosition = new Vector3(560 + n * 280, 0f, 0f);
				}
			}
			else
			{
				for (int num = 0; num < BTNs.Length; num++)
				{
					BTNs[num].localPosition = new Vector3(280 + num * 280, 0f, 0f);
				}
				Download_BB.localPosition = new Vector3(280 + BTNs.Length * 280, 0f, 0f);
			}
		}
		else if (Security.GetInt("DOWNLOAD_B", 0).Equals(0))
		{
			Download_BB.localPosition = new Vector3(0f, 0f, 0f);
			for (int num2 = 0; num2 < BTNs.Length; num2++)
			{
				BTNs[num2].localPosition = new Vector3(280 + num2 * 280, 0f, 0f);
			}
			Download_AA.localPosition = new Vector3(280 + BTNs.Length * 280, 0f, 0f);
		}
		else
		{
			for (int num3 = 0; num3 < BTNs.Length; num3++)
			{
				BTNs[num3].localPosition = new Vector3(num3 * 280, 0f, 0f);
			}
			Download_AA.localPosition = new Vector3(BTNs.Length * 280, 0f, 0f);
			Download_BB.localPosition = new Vector3(280 + BTNs.Length * 280, 0f, 0f);
		}
	}
}
