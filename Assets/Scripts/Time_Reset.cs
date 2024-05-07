using UnityEngine;
using UnityEngine.SceneManagement;

public class Time_Reset : MonoBehaviour
{
	public static Time_Reset me;

	public float wait_time;

	public float wait_time_limit = 60f;

	public bool Change_Scene;

	public UILabel Loading_Tip;

	public Animator Loading_animator;

	public RuntimeAnimatorController[] anis;

	public GameObject Loading;

	public bool FIRST_LOADING;

	public bool ON;

	public void Awake()
	{
		if (FIRST_LOADING)
		{
			ON = false;
			Loading_animator.runtimeAnimatorController = anis[Random.Range(0, anis.Length)];
			Loading_Tip.text = Localization.Get(string.Format("LOADING_TIP_{0:00}", Random.Range(0, 50)));
		}
	}

	private void Update()
	{
		if (!FIRST_LOADING)
		{
			wait_time += Time.deltaTime;
			if (!(wait_time > wait_time_limit))
			{
				return;
			}
			wait_time = 0f;
			if (Change_Scene)
			{
				SceneManager.LoadScene("#0_ALL_UI", LoadSceneMode.Single);
				base.gameObject.SetActive(false);
				if (Loading != null)
				{
					Loading.SetActive(true);
				}
			}
		}
		else if (!ON)
		{
			wait_time += Time.deltaTime;
			if (wait_time > wait_time_limit)
			{
				wait_time = 0f;
				OPEN_SCENE();
			}
		}
	}

	public void OPEN_SCENE()
	{
		if (FIRST_LOADING)
		{
			ON = true;
			Loading_animator.runtimeAnimatorController = anis[Random.Range(0, anis.Length)];
			Loading_Tip.text = Localization.Get(string.Format("LOADING_TIP_{0:00}", Random.Range(0, 50)));
			SceneManager.LoadScene("#0_ALL_UI", LoadSceneMode.Additive);
		}
	}
}
