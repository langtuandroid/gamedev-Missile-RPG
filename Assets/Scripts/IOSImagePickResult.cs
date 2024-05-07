using System;
using UnityEngine;

public class IOSImagePickResult : ISN_Result
{
	private Texture2D _image;

	public Texture2D Image
	{
		get
		{
			return _image;
		}
	}

	[Obsolete("image is deprecated, please use Image instead.")]
	public Texture2D image
	{
		get
		{
			return Image;
		}
	}

	public IOSImagePickResult(string ImageData)
		: base(true)
	{
		if (ImageData.Length == 0)
		{
			_IsSucceeded = false;
			return;
		}
		byte[] data = Convert.FromBase64String(ImageData);
		_image = new Texture2D(1, 1);
		_image.LoadImage(data);
		_image.hideFlags = HideFlags.DontSave;
		if (!IOSNativeSettings.Instance.DisablePluginLogs)
		{
			Debug.Log("IOSImagePickResult: w" + _image.width + " h: " + _image.height);
		}
	}
}
