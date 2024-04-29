using System;
using UnityEngine;

public class UM_Player
{
	private GK_Player _GK_Player;

	private GooglePlayerTemplate _GP_Player;

	public string PlayerId
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return _GP_Player.playerId;
			case RuntimePlatform.IPhonePlayer:
				return _GK_Player.Id;
			default:
				return string.Empty;
			}
		}
	}

	public string Name
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return _GP_Player.name;
			case RuntimePlatform.IPhonePlayer:
				return _GK_Player.Alias;
			default:
				return string.Empty;
			}
		}
	}

	[Obsolete("Avatar is deprectaed, plase use SmallPhoto instead")]
	public Texture2D Avatar
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return _GP_Player.image;
			case RuntimePlatform.IPhonePlayer:
				return _GK_Player.SmallPhoto;
			default:
				return null;
			}
		}
	}

	public Texture2D SmallPhoto
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return _GP_Player.icon;
			case RuntimePlatform.IPhonePlayer:
				return _GK_Player.SmallPhoto;
			default:
				return null;
			}
		}
	}

	public Texture2D BigPhoto
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return _GP_Player.image;
			case RuntimePlatform.IPhonePlayer:
				return _GK_Player.BigPhoto;
			default:
				return null;
			}
		}
	}

	public GK_Player GameCenterPlayer
	{
		get
		{
			return _GK_Player;
		}
	}

	public GooglePlayerTemplate GooglePlayPlayer
	{
		get
		{
			return _GP_Player;
		}
	}

	public event Action<Texture2D> BigPhotoLoaded = delegate
	{
	};

	public event Action<Texture2D> SmallPhotoLoaded = delegate
	{
	};

	public UM_Player(GK_Player gk, GooglePlayerTemplate gp)
	{
		_GK_Player = gk;
		_GP_Player = gp;
		if (_GK_Player != null)
		{
			_GK_Player.OnPlayerPhotoLoaded += HandleOnPlayerPhotoLoaded;
		}
		if (_GP_Player != null)
		{
			_GP_Player.BigPhotoLoaded += HandleBigPhotoLoaded;
			_GP_Player.SmallPhotoLoaded += HandleSmallPhotoLoaded;
		}
	}

	public void LoadBigPhoto()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			_GP_Player.LoadImage();
			break;
		case RuntimePlatform.IPhonePlayer:
			_GK_Player.LoadPhoto(GK_PhotoSize.GKPhotoSizeNormal);
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	public void LoadSmallPhoto()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			_GP_Player.LoadIcon();
			break;
		case RuntimePlatform.IPhonePlayer:
			_GK_Player.LoadPhoto(GK_PhotoSize.GKPhotoSizeSmall);
			break;
		case RuntimePlatform.PS3:
		case RuntimePlatform.XBOX360:
			break;
		}
	}

	private void HandleSmallPhotoLoaded(Texture2D tex)
	{
		this.SmallPhotoLoaded(tex);
	}

	private void HandleBigPhotoLoaded(Texture2D tex)
	{
		this.BigPhotoLoaded(tex);
	}

	private void HandleOnPlayerPhotoLoaded(GK_UserPhotoLoadResult res)
	{
		if (res.IsSucceeded)
		{
			if (res.Size == GK_PhotoSize.GKPhotoSizeSmall)
			{
				this.SmallPhotoLoaded(res.Photo);
			}
			else
			{
				this.BigPhotoLoaded(res.Photo);
			}
		}
	}
}
