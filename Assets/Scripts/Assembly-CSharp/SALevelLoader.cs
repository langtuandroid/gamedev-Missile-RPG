using UnityEngine;
using UnityEngine.SceneManagement;

public class SALevelLoader : SA_Singleton<SALevelLoader>
{
	private Texture2D bg;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public void LoadLevel(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
