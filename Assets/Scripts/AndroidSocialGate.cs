using System;
using UnityEngine;

public class AndroidSocialGate : SA_Singleton<AndroidSocialGate>
{
	public static event Action<bool, string> OnShareIntentCallback;

	static AndroidSocialGate()
	{
		AndroidSocialGate.OnShareIntentCallback = delegate
		{
		};
	}

	public static void StartGooglePlusShare(string text, Texture2D texture = null)
	{
		AN_SocialSharingProxy.StartGooglePlusShareIntent(text, (!(texture == null)) ? Convert.ToBase64String(texture.EncodeToPNG()) : string.Empty);
	}

	public static void StartShareIntent(string caption, string message, string packageNamePattern = "")
	{
		StartShareIntentWithSubject(caption, message, string.Empty, packageNamePattern);
	}

	public static void StartShareIntent(string caption, string message, Texture2D texture, string packageNamePattern = "")
	{
		StartShareIntentWithSubject(caption, message, string.Empty, texture, packageNamePattern);
	}

	public static void StartShareIntentWithSubject(string caption, string message, string subject, string packageNamePattern = "")
	{
		AN_SocialSharingProxy.StartShareIntent(caption, message, subject, packageNamePattern);
	}

	public static void StartShareIntentWithSubject(string caption, string message, string subject, Texture2D texture, string packageNamePattern = "")
	{
		byte[] inArray = texture.EncodeToPNG();
		string media = Convert.ToBase64String(inArray);
		AN_SocialSharingProxy.StartShareIntent(caption, message, subject, media, packageNamePattern);
	}

	public static void SendMail(string caption, string message, string subject, string recipients, Texture2D texture = null)
	{
		if (texture != null)
		{
			byte[] inArray = texture.EncodeToPNG();
			string media = Convert.ToBase64String(inArray);
			AN_SocialSharingProxy.SendMailWithImage(caption, message, subject, recipients, media);
		}
		else
		{
			AN_SocialSharingProxy.SendMail(caption, message, subject, recipients);
		}
	}
}
