using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
	public static Scene_Change me;

	public GameObject Origin;

	public GameObject Fade;

	public void Awake()
	{
		me = this;
	}

	public void OnEnable()
	{
		StartCoroutine(First_Wait());
	}

	public IEnumerator First_Wait()
	{
		yield return new WaitForSeconds(0.2f);
		SceneManager.LoadSceneAsync("MAIN_NEW0419", LoadSceneMode.Additive);
	}

	public void Exit()
	{
		StartCoroutine(REAL_EXIT());
	}

	public IEnumerator REAL_EXIT()
	{
		yield return new WaitForSeconds(1f);
		Origin.SetActive(false);
		Fade.SetActive(true);
		yield return new WaitForSeconds(1f);
		base.gameObject.SetActive(false);
		yield return new WaitForSeconds(1f);
		SceneManager.UnloadScene("LOGO");
	}
}
