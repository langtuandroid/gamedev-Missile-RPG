using UnityEngine;

public class Misile_Cleaner : MonoBehaviour
{
	public Misile misile;

	public void OnTriggerEnter2D(Collider2D Target)
	{
		if (Target.CompareTag("Misile"))
		{
			misile = Target.gameObject.GetComponent<Misile>();
			if (misile != null)
			{
				misile.Boom();
			}
		}
	}

	public void Exit()
	{
		base.gameObject.SetActive(false);
	}
}
