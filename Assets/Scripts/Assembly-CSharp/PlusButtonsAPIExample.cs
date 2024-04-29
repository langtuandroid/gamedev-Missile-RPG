using System;
using System.Collections.Generic;
using UnityEngine;

public class PlusButtonsAPIExample : MonoBehaviour
{
	private List<AN_PlusButton> Abuttons = new List<AN_PlusButton>();

	private AN_PlusButton PlusButton;

	private string PlusUrl = "https://unionassets.com/";

	public void CreatePlusButtons()
	{
		if (Abuttons.Count != 0)
		{
			return;
		}
		AN_PlusButton aN_PlusButton = new AN_PlusButton(PlusUrl, AN_PlusBtnSize.SIZE_TALL, AN_PlusBtnAnnotation.ANNOTATION_BUBBLE);
		aN_PlusButton.SetGravity(TextAnchor.UpperLeft);
		Abuttons.Add(aN_PlusButton);
		aN_PlusButton = new AN_PlusButton(PlusUrl, AN_PlusBtnSize.SIZE_SMALL, AN_PlusBtnAnnotation.ANNOTATION_INLINE);
		aN_PlusButton.SetGravity(TextAnchor.UpperRight);
		Abuttons.Add(aN_PlusButton);
		aN_PlusButton = new AN_PlusButton(PlusUrl, AN_PlusBtnSize.SIZE_MEDIUM, AN_PlusBtnAnnotation.ANNOTATION_INLINE);
		aN_PlusButton.SetGravity(TextAnchor.UpperCenter);
		Abuttons.Add(aN_PlusButton);
		aN_PlusButton = new AN_PlusButton(PlusUrl, AN_PlusBtnSize.SIZE_STANDARD, AN_PlusBtnAnnotation.ANNOTATION_INLINE);
		aN_PlusButton.SetGravity(TextAnchor.MiddleLeft);
		Abuttons.Add(aN_PlusButton);
		foreach (AN_PlusButton abutton in Abuttons)
		{
			abutton.ButtonClicked = (Action)Delegate.Combine(abutton.ButtonClicked, new Action(ButtonClicked));
		}
	}

	public void HideButtons()
	{
		foreach (AN_PlusButton abutton in Abuttons)
		{
			abutton.Hide();
		}
	}

	public void ShoweButtons()
	{
		foreach (AN_PlusButton abutton in Abuttons)
		{
			abutton.Show();
		}
	}

	public void CreateRandomPostButton()
	{
		if (PlusButton == null)
		{
			PlusButton = new AN_PlusButton(PlusUrl, AN_PlusBtnSize.SIZE_STANDARD, AN_PlusBtnAnnotation.ANNOTATION_BUBBLE);
			PlusButton.SetPosition(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height));
			AN_PlusButton plusButton = PlusButton;
			plusButton.ButtonClicked = (Action)Delegate.Combine(plusButton.ButtonClicked, new Action(ButtonClicked));
		}
	}

	public void ChangePosPostButton()
	{
		if (PlusButton != null)
		{
			PlusButton.SetPosition(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height));
		}
	}

	private void ButtonClicked()
	{
		AndroidMessage.Create("Click Detected", "Plus Button Click Detected");
	}

	private void OnDestroy()
	{
		HideButtons();
		if (PlusButton != null)
		{
			PlusButton.Hide();
		}
	}
}
