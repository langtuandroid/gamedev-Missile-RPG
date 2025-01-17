using UnityEngine;

public class GK_UserPhotoLoadResult : ISN_Result
{
	private Texture2D _Photo;

	private GK_PhotoSize _Size;

	public Texture2D Photo
	{
		get
		{
			return _Photo;
		}
	}

	public GK_PhotoSize Size
	{
		get
		{
			return _Size;
		}
	}

	public GK_UserPhotoLoadResult(GK_PhotoSize size, Texture2D photo)
		: base(true)
	{
		_Size = size;
		_Photo = photo;
	}

	public GK_UserPhotoLoadResult(GK_PhotoSize size, string errorData)
		: base(errorData)
	{
		_Size = size;
	}
}
