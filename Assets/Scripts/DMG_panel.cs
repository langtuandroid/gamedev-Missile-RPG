using Keiwando.BigInteger;
using UnityEngine;

public class DMG_panel : MonoBehaviour
{
	public UILabel DMG_label;

	public void Setting(BigInteger value, bool critical)
	{
		if (critical)
		{
			DMG_label.color = new Color32(byte.MaxValue, 15, 15, byte.MaxValue);
			base.transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
		}
		else
		{
			DMG_label.color = Color.white;
			base.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
		}
		DMG_label.text = Now_Data.INT_to_ABC(value);
		base.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
	}
}
