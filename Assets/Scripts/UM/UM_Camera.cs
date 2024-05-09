using System;
using UnityEngine;
using Zenject;

public class UM_Camera : SA_Singleton<UM_Camera>
{
	[Inject] private AndroidCamera _androidCamera;

    public event Action<UM_ImagePickResult> OnImagePicked = delegate
	{
	};

	public event Action<UM_ImageSaveResult> OnImageSaved = delegate
	{
	};

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
        _androidCamera.OnImagePicked = (Action<AndroidImagePickResult>)Delegate.Combine(_androidCamera.OnImagePicked, new Action<AndroidImagePickResult>(OnAndroidImagePicked));
		IOSCamera.OnImagePicked += OnIOSImagePicked;
        _androidCamera.OnImageSaved = (Action<GallerySaveResult>)Delegate.Combine(_androidCamera.OnImageSaved, new Action<GallerySaveResult>(OnAndroidImageSaved));
		IOSCamera.OnImageSaved += OnIOSImageSaved;
	}

	public void SaveImageToGalalry(Texture2D image)
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
                _androidCamera.SaveImageToGallery(image);
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
                _androidCamera.SaveScreenshotToGallery();
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
			_androidCamera.GetImageFromGallery();
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
			_androidCamera.GetImageFromCamera();
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
