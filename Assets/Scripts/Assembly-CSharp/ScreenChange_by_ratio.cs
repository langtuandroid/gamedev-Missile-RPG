using UnityEngine;

public class ScreenChange_by_ratio : MonoBehaviour
{
	public float Ratio_A;

	public float Ratio_B;

	public Camera Main_Camera;

	private float Ratio_Value;

	public float multiply_value = 2f;

	public float Main_size = 6f;

	public GameObject[] Dungeon_BG;

	public void Start()
	{
		Setting();
	}

	public void Setting()
	{
		Position_by_size();
		Main_Camera.orthographicSize = Main_size;
		Ratio_Value = (float)Screen.width / (float)Screen.height;
		if ((float)Screen.width / (float)Screen.height < 1.77f)
		{
			Main_Camera.orthographicSize = Main_size + (0.77f / (Ratio_Value - 1f) - 1f) * multiply_value;
		}
		else
		{
			Main_Camera.orthographicSize = Main_size;
		}
	}

	public void Position_by_size()
	{
		if (Main_size.Equals(6f))
		{
			base.transform.localPosition = new Vector3(1.5f, -1f, -20f);
		}
		else if (Main_size.Equals(6.5f))
		{
			base.transform.localPosition = new Vector3(1f, -0.5f, -20f);
		}
		else if (Main_size.Equals(7f))
		{
			base.transform.localPosition = new Vector3(-0.5f, 0f, -20f);
		}
		else if (Main_size.Equals(7.5f))
		{
			base.transform.localPosition = new Vector3(-1f, 0.5f, -20f);
		}
	}
}
