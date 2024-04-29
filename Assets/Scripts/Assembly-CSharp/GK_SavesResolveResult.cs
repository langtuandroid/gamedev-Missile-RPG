using System.Collections.Generic;

public class GK_SavesResolveResult : ISN_Result
{
	private List<GK_SavedGame> _ResolvedSaves = new List<GK_SavedGame>();

	public List<GK_SavedGame> SavedGames
	{
		get
		{
			return _ResolvedSaves;
		}
	}

	public GK_SavesResolveResult(List<GK_SavedGame> saves)
		: base(true)
	{
		_ResolvedSaves = saves;
	}

	public GK_SavesResolveResult(string errorData)
		: base(errorData)
	{
	}
}
