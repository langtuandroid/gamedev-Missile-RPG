using UnityEngine;
using UnityEngine.UI;
public class CFX_Demo_GTToggle : MonoBehaviour
{
	public Sprite Normal;

	public Sprite Hover;

	public Color NormalColor = new Color32(128, 128, 128, 128);

	public Color DisabledColor = new Color32(128, 128, 128, 48);

	public bool State = true;

	public string Callback;

	public GameObject Receiver;

	private Rect CollisionRect;

	private bool Over;

	 private Text Label;

	private void Awake()
	{
		CollisionRect = Camera.main.rect;
	 Label = GetComponentInChildren<Text>();
		UpdateTexture();
	}

	private void Update()
	{
		if (CollisionRect.Contains(Input.mousePosition))
		{
			Over = true;
			if (Input.GetMouseButtonDown(0))
			{
				OnClick();
			}
		}
		else
		{
			Over = false;
		 GetComponent<Image>().color = NormalColor;
		}
		UpdateTexture();
	}

	private void OnClick()
	{
		State = !State;
		Receiver.SendMessage(Callback);
	}

	private void UpdateTexture()
	{
		Color color = ((!State) ? DisabledColor : NormalColor);
		if (Over)
		{
			 GetComponent<Image>().sprite = Hover;
		}
		else
		{
		 GetComponent<Image>().sprite = Normal;
		}
		GetComponent<Image>().color = color;
		if (Label != null)
		{
			Label.color = color * 1.75f;
		}
	}
}
