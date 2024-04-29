using System;
using System.Collections;
using UnityEngine;

public class IOSCamera : ISN_Singleton<IOSCamera>
{
	private bool IsWaitngForResponce;

	public static event Action<IOSImagePickResult> OnImagePicked;

	public static event Action<ISN_Result> OnImageSaved;

	public static event Action<string> OnVideoPathPicked;

	static IOSCamera()
	{
		IOSCamera.OnImagePicked = delegate
		{
		};
		IOSCamera.OnImageSaved = delegate
		{
		};
		IOSCamera.OnVideoPathPicked = delegate
		{
		};
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void SaveTextureToCameraRoll(Texture2D texture)
	{
	}

	public void SaveScreenshotToCameraRoll()
	{
		StartCoroutine(SaveScreenshot());
	}

	public void GetVideoPathFromAlbum()
	{
	}

	[Obsolete("GetImageFromAlbum is deprecated, please use PickImage(ISN_ImageSource.Album) ")]
	public void GetImageFromAlbum()
	{
		PickImage(ISN_ImageSource.Album);
	}

	[Obsolete("GetImageFromCamera is deprecated, please use PickImage(ISN_ImageSource.Camera) ")]
	public void GetImageFromCamera()
	{
		PickImage(ISN_ImageSource.Camera);
	}

	public void PickImage(ISN_ImageSource source)
	{
		if (!IsWaitngForResponce)
		{
			IsWaitngForResponce = true;
		}
	}

	private void OnImagePickedEvent(string data)
	{
		IsWaitngForResponce = false;
		IOSImagePickResult obj = new IOSImagePickResult(data);
		IOSCamera.OnImagePicked(obj);
	}

	private void OnImageSaveFailed()
	{
		ISN_Result obj = new ISN_Result(false);
		IOSCamera.OnImageSaved(obj);
	}

	private void OnImageSaveSuccess()
	{
		ISN_Result obj = new ISN_Result(true);
		IOSCamera.OnImageSaved(obj);
	}

	private void OnVideoPickedEvent(string path)
	{
		IOSCamera.OnVideoPathPicked(path);
	}

	private IEnumerator SaveScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		tex.Apply();
		SaveTextureToCameraRoll(tex);
		UnityEngine.Object.Destroy(tex);
	}
}
