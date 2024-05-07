using UnityEngine;

public class AndroidNotificationBuilder
{
	public class NotificationColor
	{
		private Color _value;

		public Color Value
		{
			get
			{
				return _value;
			}
		}

		public NotificationColor(Color value)
		{
			_value = value;
		}
	}

	private const string SOUND_SILENT = "SOUND_SILENT";

	private int _id = 1;

	private string _title = string.Empty;

	private string _message = string.Empty;

	private int _time = 1;

	private string _sound = string.Empty;

	private string _smallIcon = string.Empty;

	private bool _vibration;

	private bool _showIfAppForeground = true;

	private string _largeIcon = string.Empty;

	private Texture2D _bigPicture;

	private NotificationColor _color;

	public int Id
	{
		get
		{
			return _id;
		}
	}

	public string Title
	{
		get
		{
			return _title;
		}
	}

	public string Message
	{
		get
		{
			return _message;
		}
	}

	public int Time
	{
		get
		{
			return _time;
		}
	}

	public NotificationColor Color
	{
		get
		{
			return _color;
		}
	}

	public string Sound
	{
		get
		{
			return _sound;
		}
	}

	public string Icon
	{
		get
		{
			return _smallIcon;
		}
	}

	public bool Vibration
	{
		get
		{
			return _vibration;
		}
	}

	public bool ShowIfAppForeground
	{
		get
		{
			return _showIfAppForeground;
		}
	}

	public string LargeIcon
	{
		get
		{
			return _largeIcon;
		}
	}

	public Texture2D BigPicture
	{
		get
		{
			return _bigPicture;
		}
	}

	public AndroidNotificationBuilder(int id, string title, string message, int time)
	{
		_id = id;
		_title = title;
		_message = message;
		_time = time;
		_largeIcon = ((!(AndroidNativeSettings.Instance.LocalNotificationLargeIcon == null)) ? AndroidNativeSettings.Instance.LocalNotificationLargeIcon.name.ToLower() : string.Empty);
		_smallIcon = ((!(AndroidNativeSettings.Instance.LocalNotificationSmallIcon == null)) ? AndroidNativeSettings.Instance.LocalNotificationSmallIcon.name.ToLower() : string.Empty);
		_sound = ((!(AndroidNativeSettings.Instance.LocalNotificationSound == null)) ? AndroidNativeSettings.Instance.LocalNotificationSound.name : string.Empty);
		_vibration = AndroidNativeSettings.Instance.EnableVibrationLocal;
		_showIfAppForeground = AndroidNativeSettings.Instance.ShowWhenAppIsForeground;
	}

	public void SetColor(NotificationColor color)
	{
		_color = color;
	}

	public void SetSoundName(string sound)
	{
		_sound = sound;
	}

	public void SetIconName(string icon)
	{
		_smallIcon = icon;
	}

	public void SetVibration(bool vibration)
	{
		_vibration = vibration;
	}

	public void SetSilentNotification()
	{
		_sound = "SOUND_SILENT";
	}

	public void ShowIfAppIsForeground(bool show)
	{
		_showIfAppForeground = show;
	}

	public void SetLargeIcon(string icon)
	{
		_largeIcon = icon;
	}

	public void SetBigPicture(Texture2D picture)
	{
		_bigPicture = picture;
	}
}
