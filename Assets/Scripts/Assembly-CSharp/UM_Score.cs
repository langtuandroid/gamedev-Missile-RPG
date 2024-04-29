using System;
using UnityEngine;

public class UM_Score
{
	private UM_Player player;

	private GK_Score _GK_Score;

	private GPScore _GP_Score;

	public bool IsValid
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return _GP_Score != null;
			case RuntimePlatform.IPhonePlayer:
				return _GK_Score != null;
			default:
				return true;
			}
		}
	}

	public int Rank
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					return _GP_Score.Rank;
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.Rank;
				}
			}
			return -1;
		}
	}

	public long LongScore
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					return _GP_Score.LongScore;
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.LongScore;
				}
			}
			return 0L;
		}
	}

	public float CurrencyScore
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					return _GP_Score.CurrencyScore;
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.CurrencyScore;
				}
			}
			return 0f;
		}
	}

	public TimeSpan TimeScore
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					return _GP_Score.TimeScore;
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.Milliseconds;
				}
			}
			return System.TimeSpan.FromMilliseconds(0.0);
		}
	}

	public string LeaderboardId
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					return _GP_Score.LeaderboardId;
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.LeaderboardId;
				}
			}
			return string.Empty;
		}
	}

	public UM_TimeSpan TimeSpan
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					return _GP_Score.TimeSpan.Get_UM_TimeSpan();
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.TimeSpan.Get_UM_TimeSpan();
				}
			}
			return UM_TimeSpan.ALL_TIME;
		}
	}

	public UM_CollectionType Collection
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					return _GP_Score.Collection.Get_UM_Collection();
				case RuntimePlatform.IPhonePlayer:
					return _GK_Score.Collection.Get_UM_Collection();
				}
			}
			return UM_CollectionType.GLOBAL;
		}
	}

	public UM_Player Player
	{
		get
		{
			return player;
		}
	}

	public GK_Score GameCenterScore
	{
		get
		{
			return _GK_Score;
		}
	}

	public GPScore GooglePlayScore
	{
		get
		{
			return _GP_Score;
		}
	}

	public UM_Score(GK_Score gkScore, GPScore gpScore)
	{
		_GK_Score = gkScore;
		_GP_Score = gpScore;
		if (IsValid)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
			{
				GooglePlayerTemplate playerById2 = SA_Singleton<GooglePlayManager>.Instance.GetPlayerById(_GP_Score.PlayerId);
				player = new UM_Player(null, playerById2);
				break;
			}
			case RuntimePlatform.IPhonePlayer:
			{
				GK_Player playerById = GameCenterManager.GetPlayerById(_GK_Score.PlayerId);
				player = new UM_Player(playerById, null);
				break;
			}
			case RuntimePlatform.PS3:
			case RuntimePlatform.XBOX360:
				break;
			}
		}
	}
}
