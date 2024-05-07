using System.Collections;
using UnityEngine;

public class UM_Social : MonoBehaviour
{
	public static UM_Social me;

	private GUIStyle style;

	private GUIStyle style2;

	public Texture2D textureForPost;

	private void Awake()
	{
		if (me == null)
		{
			me = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void ScreenshotToSocial()
	{
		StartCoroutine(PostFBScreenshot());
	}

	public void ScreenshotToFacebook()
	{
		StartCoroutine(PostFBScreenshot());
	}

	public void ScreenshotToTwitter()
	{
		StartCoroutine(PostTwitterScreenshot());
	}

	public void ShareLinkToFaceBook()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			UM_ShareUtility.FacebookShare("https://play.google.com/store/apps/details?id=com.moontm.cartoon999");
			break;
		case RuntimePlatform.IPhonePlayer:
			UM_ShareUtility.FacebookShare("https://itunes.apple.com/app/dungeon-999f/id969388298");
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void ShareLinkToTwitter()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			UM_ShareUtility.TwitterShare("https://play.google.com/store/apps/details?id=com.moontm.cartoon999");
			break;
		case RuntimePlatform.IPhonePlayer:
			UM_ShareUtility.TwitterShare("https://itunes.apple.com/app/dungeon-999f/id969388298");
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void ShareMedia()
	{
		UM_ShareUtility.ShareMedia(Localization.Get("Cartoon999"), string.Format("Share_{0:000}", Random.Range(0, 5)));
	}

	private IEnumerator PostScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		UM_ShareUtility.ShareMedia(Localization.Get("Cartoon999"), string.Format("Share_{0:000}", Random.Range(0, 5)), tex);
		Object.Destroy(tex);
	}

	private IEnumerator PostTwitterScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		UM_ShareUtility.TwitterShare(string.Format("Share_{0:000}", Random.Range(0, 5)), tex);
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
		UM_ShareUtility.FacebookShare(string.Format("Share_{0:000}", Random.Range(0, 5)), tex);
		Object.Destroy(tex);
	}
}
