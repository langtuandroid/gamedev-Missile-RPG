using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	public bool OnlyDeactivate;

	public ParticleSystem particleSystem;

	private void OnEnable()
	{
		StartCoroutine(CheckIfAlive());
	}

	private IEnumerator CheckIfAlive()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);
			if (!particleSystem.IsAlive(true))
			{
				base.gameObject.SetActive(false);
			}
		}
	}
}
