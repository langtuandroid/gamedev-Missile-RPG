using System;
using UnityEngine;

public class UM_Camera : SA_Singleton<UM_Camera>
{
	public event Action<UM_ImagePickResult> OnImagePicked = delegate
	{
	};

	public event Action<UM_ImageSaveResult> OnImageSaved = delegate
	{
	};

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		AndroidCamera androidCamera = SA_Singleton<AndroidCamera>.instance;
		androidCamera.OnImagePicked = (Action<AndroidImagePickResult>)Delegate.Combine(androidCamera.OnImagePicked, new Action<AndroidImagePickResult>(OnAndroidImagePicked));
		IOSCamera.OnImagePicked += OnIOSImagePicked;
		AndroidCamera androidCamera2 = SA_Singleton<AndroidCamera>.instance;
		androidCamera2.OnImageSaved = (Action<GallerySaveResult>)Delegate.Combine(androidCamera2.OnImageSaved, new Action<GallerySaveResult>(OnAndroidImageSaved));
		IOSCamera.OnImageSaved += OnIOSImageSaved;
	}

	public void SaveImageToGalalry(Texture2D image)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			SA_Singleton<AndroidCamera>.instance.SaveImageToGallery(image);
			break;
		case RuntimePlatform.IPhonePlayer:
			ISN_Singleton<IOSCamera>.instance.SaveTextureToCameraRoll(image);
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void SaveScreenshotToGallery()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			SA_Singleton<AndroidCamera>.instance.SaveScreenshotToGallery();
			break;
		case RuntimePlatform.IPhonePlayer:
			ISN_Singleton<IOSCamera>.instance.SaveScreenshotToCameraRoll();
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void GetImageFromGallery()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			SA_Singleton<AndroidCamera>.instance.GetImageFromGallery();
			break;
		case RuntimePlatform.IPhonePlayer:
			ISN_Singleton<IOSCamera>.instance.PickImage(ISN_ImageSource.Library);
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void GetImageFromCamera()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			SA_Singleton<AndroidCamera>.instance.GetImageFromCamera();
			break;
		case RuntimePlatform.IPhonePlayer:
			ISN_Singleton<IOSCamera>.instance.PickImage(ISN_ImageSource.Camera);
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	private void OnAndroidImagePicked(AndroidImagePickResult obj)
	{
		UM_ImagePickResult obj2 = new UM_ImagePickResult(obj.Image);
		this.OnImagePicked(obj2);
	}

	private void OnIOSImagePicked(IOSImagePickResult obj)
	{
		UM_ImagePickResult obj2 = new UM_ImagePickResult(obj.Image);
		this.OnImagePicked(obj2);
	}

	private void OnAndroidImageSaved(GallerySaveResult res)
	{
		UM_ImageSaveResult obj = new UM_ImageSaveResult(res.imagePath, res.IsSucceeded);
		this.OnImageSaved(obj);
	}

	private void OnIOSImageSaved(ISN_Result res)
	{
		UM_ImageSaveResult obj = new UM_ImageSaveResult(string.Empty, res.IsSucceeded);
		this.OnImageSaved(obj);
	}
}
