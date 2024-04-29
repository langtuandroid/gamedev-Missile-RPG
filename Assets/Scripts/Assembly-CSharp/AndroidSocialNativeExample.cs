using System.Collections;
using UnityEngine;

public class AndroidSocialNativeExample : MonoBehaviour
{
	public Texture2D shareTexture;

	private void Awake()
	{
		SA_StatusBar.text = "Social Sharing scene is loaded";
	}

	public void ShareText()
	{
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", string.Empty);
	}

	public void ShareScreehshot()
	{
		StartCoroutine(PostScreenshot());
	}

	public void ShareImage()
	{
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "Sharing Hello wolrd image", shareTexture, string.Empty);
	}

	public void TwitterShare()
	{
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", shareTexture, "twi");
	}

	public void ShareMail()
	{
		AndroidSocialGate.SendMail("Hello Share Intent", "This is my text to share <br> <strong> html text </strong> ", "My E-mail Subject", "mail1@gmail.com, mail2@gmail.com", shareTexture);
	}

	public void InstaShare()
	{
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", shareTexture, "insta");
	}

	public void GoogleShare()
	{
		AndroidSocialGate.StartGooglePlusShare("This is my text to share", shareTexture);
	}

	public void ShareFB()
	{
		StartCoroutine(PostFBScreenshot());
	}

	private IEnumerator PostScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", tex, string.Empty);
		Object.Destroy(tex);
	}

	private IEnumerator PostFBScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", tex, "facebook.katana");
		Object.Destroy(tex);
	}
}
