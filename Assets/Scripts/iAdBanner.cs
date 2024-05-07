using System;
using UnityEngine;

public class iAdBanner
{
	private bool _IsLoaded;

	private bool _IsOnScreen;

	private bool firstLoad = true;

	private bool _ShowOnLoad = true;

	private int _id;

	private TextAnchor _anchor;

	public int id
	{
		get
		{
			return _id;
		}
	}

	public bool IsLoaded
	{
		get
		{
			return _IsLoaded;
		}
	}

	public bool IsOnScreen
	{
		get
		{
			return _IsOnScreen;
		}
	}

	public bool ShowOnLoad
	{
		get
		{
			return _ShowOnLoad;
		}
		set
		{
			_ShowOnLoad = value;
		}
	}

	public TextAnchor anchor
	{
		get
		{
			return _anchor;
		}
	}

	public int gravity
	{
		get
		{
			switch (_anchor)
			{
			case TextAnchor.LowerCenter:
				return 81;
			case TextAnchor.LowerLeft:
				return 83;
			case TextAnchor.LowerRight:
				return 85;
			case TextAnchor.MiddleCenter:
				return 17;
			case TextAnchor.MiddleLeft:
				return 19;
			case TextAnchor.MiddleRight:
				return 21;
			case TextAnchor.UpperCenter:
				return 49;
			case TextAnchor.UpperLeft:
				return 51;
			case TextAnchor.UpperRight:
				return 53;
			default:
				return 48;
			}
		}
	}

	public event Action AdLoadedAction = delegate
	{
	};

	public event Action FailToReceiveAdAction = delegate
	{
	};

	public event Action AdWiewLoadedAction = delegate
	{
	};

	public event Action AdViewActionBeginAction = delegate
	{
	};

	public event Action AdViewFinishedAction = delegate
	{
	};

	public iAdBanner(TextAnchor anchor, int id)
	{
		_id = id;
		_anchor = anchor;
	}

	public iAdBanner(int x, int y, int id)
	{
		_id = id;
	}

	public void Hide()
	{
		if (_IsOnScreen)
		{
			_IsOnScreen = false;
		}
	}

	public void Show()
	{
		if (!_IsOnScreen)
		{
			_IsOnScreen = true;
		}
	}

	public void didFailToReceiveAdWithError()
	{
		this.FailToReceiveAdAction();
	}

	public void bannerViewDidLoadAd()
	{
		_IsLoaded = true;
		if (ShowOnLoad && firstLoad)
		{
			Show();
			firstLoad = false;
		}
		this.AdLoadedAction();
	}

	public void bannerViewWillLoadAd()
	{
		this.AdWiewLoadedAction();
	}

	public void bannerViewActionDidFinish()
	{
		this.AdViewFinishedAction();
	}

	public void bannerViewActionShouldBegin()
	{
		this.AdViewActionBeginAction();
	}
}
