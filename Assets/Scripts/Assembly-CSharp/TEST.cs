using UnityEngine;

public class TEST : MonoBehaviour
{
	public int LV;

	public float speed = 1f;

	public float time_check;

	public void Update()
	{
		time_check += Time.deltaTime * speed;
		if (time_check >= 1f)
		{
			time_check = 0f;
			LV++;
			Debug.Log(LV * LV);
		}
	}
}
public class TEst : MonoBehaviour
{
}
