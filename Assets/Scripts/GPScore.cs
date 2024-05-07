using System;

[Serializable]
public class GPScore
{
	private int _rank;

	private long _score;

	private string _playerId;

	private string _leaderboardId;

	private GPCollectionType _collection;

	private GPBoardTimeSpan _timeSpan;

	[Obsolete("rank is deprectaed, plase use Rank instead")]
	public int rank
	{
		get
		{
			return _rank;
		}
	}

	public int Rank
	{
		get
		{
			return _rank;
		}
	}

	[Obsolete("score is deprectaed, plase use LongScore instead")]
	public long score
	{
		get
		{
			return _score;
		}
	}

	public long LongScore
	{
		get
		{
			return _score;
		}
	}

	public float CurrencyScore
	{
		get
		{
			return (float)_score / 100f;
		}
	}

	public TimeSpan TimeScore
	{
		get
		{
			return System.TimeSpan.FromMilliseconds(_score);
		}
	}

	[Obsolete("playerId is deprectaed, plase use PlayerId instead")]
	public string playerId
	{
		get
		{
			return _playerId;
		}
	}

	public string PlayerId
	{
		get
		{
			return _playerId;
		}
	}

	public GooglePlayerTemplate Player
	{
		get
		{
			return SA_Singleton<GooglePlayManager>.Instance.GetPlayerById(PlayerId);
		}
	}

	[Obsolete("leaderboardId is deprectaed, plase use LeaderboardId instead")]
	public string leaderboardId
	{
		get
		{
			return _leaderboardId;
		}
	}

	public string LeaderboardId
	{
		get
		{
			return _leaderboardId;
		}
	}

	[Obsolete("collection is deprectaed, plase use Collection instead")]
	public GPCollectionType collection
	{
		get
		{
			return _collection;
		}
	}

	public GPCollectionType Collection
	{
		get
		{
			return _collection;
		}
	}

	[Obsolete("timeSpan is deprectaed, plase use TimeSpan instead")]
	public GPBoardTimeSpan timeSpan
	{
		get
		{
			return _timeSpan;
		}
	}

	public GPBoardTimeSpan TimeSpan
	{
		get
		{
			return _timeSpan;
		}
	}

	public GPScore(long vScore, int vRank, GPBoardTimeSpan vTimeSpan, GPCollectionType sCollection, string lid, string pid)
	{
		_score = vScore;
		_rank = vRank;
		_playerId = pid;
		_leaderboardId = lid;
		_timeSpan = vTimeSpan;
		_collection = sCollection;
	}

	public void UpdateScore(long vScore)
	{
		_score = vScore;
	}
}
