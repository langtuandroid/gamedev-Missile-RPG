public class GK_SaveDataLoaded : ISN_Result
{
	private GK_SavedGame _SavedGame;

	public GK_SavedGame SavedGame
	{
		get
		{
			return _SavedGame;
		}
	}

	public GK_SaveDataLoaded(GK_SavedGame save)
		: base(true)
	{
		_SavedGame = save;
	}

	public GK_SaveDataLoaded(string errorData)
		: base(errorData)
	{
	}
}
