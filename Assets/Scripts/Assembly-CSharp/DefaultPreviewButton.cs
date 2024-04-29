using System;
using UnityEngine;

public class DefaultPreviewButton : MonoBehaviour
{
	public Texture normalTexture;

	public Texture pressedTexture;

	public Texture disabledTexture;

	public Texture selectedTexture;

	private Texture normalTex;

	public AudioClip sound;

	public AudioClip disabledsound;

	private bool IsDisabled;

	public string text
	{
		get
		{
			TextMesh componentInChildren = base.gameObject.GetComponentInChildren<TextMesh>();
			return componentInChildren.text;
		}
		set
		{
			TextMesh[] componentsInChildren = base.gameObject.GetComponentsInChildren<TextMesh>();
			TextMesh[] array = componentsInChildren;
			foreach (TextMesh textMesh in array)
			{
				textMesh.text = value;
			}
		}
	}

	public event Action ActionClick = delegate
	{
	};

	private void Awake()
	{
		if (GetComponent<AudioSource>() == null)
		{
			base.gameObject.AddComponent<AudioSource>();
			GetComponent<AudioSource>().clip = sound;
			GetComponent<AudioSource>().Stop();
		}
		GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
		normalTex = normalTexture;
	}

	public void Select()
	{
		normalTexture = selectedTexture;
		OnTimeoutPress();
	}

	public void Unselect()
	{
		normalTexture = normalTex;
		OnTimeoutPress();
	}

	public void DisabledButton()
	{
		if (disabledTexture != null)
		{
			GetComponent<Renderer>().material.mainTexture = disabledTexture;
		}
		IsDisabled = true;
	}

	public void EnabledButton()
	{
		if (disabledTexture != null)
		{
			GetComponent<Renderer>().material.mainTexture = normalTexture;
		}
		IsDisabled = false;
	}

	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo, float.PositiveInfinity) && hitInfo.transform.gameObject == base.gameObject)
		{
			OnClick();
		}
	}

	protected virtual void OnClick()
	{
		if (IsDisabled)
		{
			GetComponent<AudioSource>().PlayOneShot(disabledsound);
			return;
		}
		GetComponent<AudioSource>().PlayOneShot(sound);
		GetComponent<Renderer>().material.mainTexture = pressedTexture;
		this.ActionClick();
		CancelInvoke("OnTimeoutPress");
		Invoke("OnTimeoutPress", 0.1f);
	}

	private void OnTimeoutPress()
	{
		GetComponent<Renderer>().material.mainTexture = normalTexture;
	}
}
