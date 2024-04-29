using UnityEngine;

public class UM_CameraAPIExample : BaseIOSFeaturePreview
{
	public Texture2D hello_texture;

	public Texture2D darawTexgture;

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Camera And Gallery", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth + 10, buttonHeight), "Save Screenshot To Camera Roll"))
		{
			SA_Singleton<UM_Camera>.instance.OnImageSaved += OnImageSaved;
			SA_Singleton<UM_Camera>.instance.SaveScreenshotToGallery();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Save Texture To Camera Roll"))
		{
			SA_Singleton<UM_Camera>.instance.OnImageSaved += OnImageSaved;
			SA_Singleton<UM_Camera>.instance.SaveImageToGalalry(hello_texture);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Image From Album"))
		{
			SA_Singleton<UM_Camera>.instance.OnImagePicked += OnImage;
			SA_Singleton<UM_Camera>.instance.GetImageFromGallery();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Image From Camera"))
		{
			SA_Singleton<UM_Camera>.instance.OnImagePicked += OnImage;
			SA_Singleton<UM_Camera>.instance.GetImageFromCamera();
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "PickedImage", style);
		StartY += YLableStep;
		if (darawTexgture != null)
		{
			GUI.DrawTexture(new Rect(StartX, StartY, buttonWidth, buttonWidth), darawTexgture);
		}
	}

	private void OnImageSaved(UM_ImageSaveResult result)
	{
		if (result.IsSucceeded)
		{
			new MobileNativeMessage("Image Saved", result.imagePath);
		}
		else
		{
			new MobileNativeMessage("Failed", "Image Save Failed");
		}
	}

	private void OnImage(UM_ImagePickResult result)
	{
		SA_Singleton<UM_Camera>.instance.OnImageSaved -= OnImageSaved;
		if (result.IsSucceeded)
		{
			darawTexgture = result.image;
		}
		SA_Singleton<UM_Camera>.instance.OnImagePicked -= OnImage;
	}
}
