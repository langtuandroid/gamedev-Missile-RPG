using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UM_Leaderboard
{
	[SerializeField]
	public string id = "new leaderboard";

	public bool IsOpen = true;

	[SerializeField]
	public string IOSId = string.Empty;

	[SerializeField]
	public string AndroidId = string.Empty;

	[SerializeField]
	private string _Description = string.Empty;

	[SerializeField]
	private Texture2D _Texture;

	private GK_Leaderboard gk_Leaderboard;

	private GPLeaderBoard gp_Leaderboard;

	public bool IsValid
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				return gp_Leaderboard != null;
			case RuntimePlatform.IPhonePlayer:
				return gk_Leaderboard != null;
			default:
				return true;
			}
		}
	}

	public string Id
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					return gp_Leaderboard.Id;
				case RuntimePlatform.IPhonePlayer:
					return gk_Leaderboard.Id;
				}
			}
			return string.Empty;
		}
	}

	public string Name
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					return gp_Leaderboard.Name;
				case RuntimePlatform.IPhonePlayer:
					return gk_Leaderboard.Info.Title;
				}
			}
			return string.Empty;
		}
	}

	public bool CurrentPlayerScoreLoaded
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					return gp_Leaderboard.CurrentPlayerScoreLoaded;
				case RuntimePlatform.IPhonePlayer:
					return gk_Leaderboard.CurrentPlayerScoreLoaded;
				}
			}
			return false;
		}
	}

	public GK_Leaderboard GameCenterLeaderboard
	{
		get
		{
			return gk_Leaderboard;
		}
	}

	public GPLeaderBoard GooglePlayLeaderboard
	{
		get
		{
			return gp_Leaderboard;
		}
	}

	public string Description
	{
		get
		{
			return _Description;
		}
		set
		{
			_Description = value;
		}
	}

	public Texture2D Texture
	{
		get
		{
			return _Texture;
		}
		set
		{
			_Texture = value;
		}
	}

	public void Setup(GPLeaderBoard gpLeaderboard)
	{
		gp_Leaderboard = gpLeaderboard;
	}

	public void Setup(GK_Leaderboard gkLeaderboard)
	{
		gk_Leaderboard = gkLeaderboard;
	}

	public UM_Score GetScore(int rank, UM_TimeSpan scope, UM_CollectionType collection)
	{
		UM_Score result = null;
		if (IsValid)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
			{
				GPScore score2 = gp_Leaderboard.GetScore(rank, scope.Get_GP_TimeSpan(), collection.Get_GP_CollectionType());
				if (score2 != null)
				{
					result = new UM_Score(null, score2);
				}
				break;
			}
			case RuntimePlatform.IPhonePlayer:
			{
				GK_Score score = gk_Leaderboard.GetScore(rank, scope.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
				if (score != null)
				{
					result = new UM_Score(score, null);
				}
				break;
			}
			}
		}
		return result;
	}

	public List<UM_Score> GetScoresList(UM_TimeSpan span, UM_CollectionType collection)
	{
		List<UM_Score> list = new List<UM_Score>();
		if (IsValid)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
			{
				List<GPScore> scoresList2 = gp_Leaderboard.GetScoresList(span.Get_GP_TimeSpan(), collection.Get_GP_CollectionType());
				{
					foreach (GPScore item in scoresList2)
					{
						list.Add(new UM_Score(null, item));
					}
					return list;
				}
			}
			case RuntimePlatform.IPhonePlayer:
			{
				List<GK_Score> scoresList = gk_Leaderboard.GetScoresList(span.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
				{
					foreach (GK_Score item2 in scoresList)
					{
						list.Add(new UM_Score(item2, null));
					}
					return list;
				}
			}
			}
		}
		return list;
	}

	public UM_Score GetScoreByPlayerId(string playerId, UM_TimeSpan span, UM_CollectionType collection)
	{
		UM_Score result = null;
		if (IsValid)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
			{
				GPScore scoreByPlayerId2 = gp_Leaderboard.GetScoreByPlayerId(playerId, span.Get_GP_TimeSpan(), collection.Get_GP_CollectionType());
				if (scoreByPlayerId2 != null)
				{
					result = new UM_Score(null, scoreByPlayerId2);
				}
				break;
			}
			case RuntimePlatform.IPhonePlayer:
			{
				GK_Score scoreByPlayerId = gk_Leaderboard.GetScoreByPlayerId(playerId, span.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
				if (scoreByPlayerId != null)
				{
					result = new UM_Score(scoreByPlayerId, null);
				}
				break;
			}
			}
		}
		return result;
	}

	public UM_Score GetCurrentPlayerScore(UM_TimeSpan span, UM_CollectionType collection)
	{
		UM_Score result = null;
		if (IsValid)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
			{
				GPScore currentPlayerScore2 = gp_Leaderboard.GetCurrentPlayerScore(span.Get_GP_TimeSpan(), collection.Get_GP_CollectionType());
				if (currentPlayerScore2 != null)
				{
					result = new UM_Score(null, currentPlayerScore2);
				}
				break;
			}
			case RuntimePlatform.IPhonePlayer:
			{
				GK_Score currentPlayerScore = gk_Leaderboard.GetCurrentPlayerScore(span.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
				if (currentPlayerScore != null)
				{
					result = new UM_Score(currentPlayerScore, null);
				}
				break;
			}
			}
		}
		return result;
	}
}
