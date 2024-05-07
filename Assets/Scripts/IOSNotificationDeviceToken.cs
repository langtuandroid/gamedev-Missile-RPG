using System;

public class IOSNotificationDeviceToken
{
	private string _tokenString;

	private byte[] _tokenBytes;

	public string tokenString
	{
		get
		{
			return _tokenString;
		}
	}

	public byte[] tokenBytes
	{
		get
		{
			return _tokenBytes;
		}
	}

	public IOSNotificationDeviceToken(byte[] token)
	{
		_tokenBytes = token;
		_tokenString = BitConverter.ToString(token).Replace("-", string.Empty).ToLower();
	}
}
