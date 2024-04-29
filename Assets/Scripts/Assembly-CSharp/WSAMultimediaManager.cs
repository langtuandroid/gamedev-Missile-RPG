using System;
using UnityEngine;

public class WSAMultimediaManager
{
	public static void PickImageFromGallery()
	{
	}

	public static void SaveScreenshot()
	{
		SA_ScreenShotMaker instance = SA_Singleton<SA_ScreenShotMaker>.instance;
		instance.OnScreenshotReady = (Action<Texture2D>)Delegate.Combine(instance.OnScreenshotReady, new Action<Texture2D>(ScreenshotReady));
		SA_Singleton<SA_ScreenShotMaker>.instance.GetScreenshot();
	}

	private static void ScreenshotReady(Texture2D screenshot)
	{
		Debug.Log("[ScreenshotReady]");
		SA_ScreenShotMaker instance = SA_Singleton<SA_ScreenShotMaker>.instance;
		instance.OnScreenshotReady = (Action<Texture2D>)Delegate.Remove(instance.OnScreenshotReady, new Action<Texture2D>(ScreenshotReady));
	}
}
