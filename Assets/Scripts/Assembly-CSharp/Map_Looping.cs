using UnityEngine;

public class Map_Looping : MonoBehaviour
{
	public GameObject[] Maps;

	public bool Change_Ready;

	public Transform ThisTransform;

	public int Count = 1;

	public int change_Target;

	public void OnEnable()
	{
		Count = 1;
		change_Target = 0;
		Maps[0].transform.localPosition = new Vector3(0f, 0f, 0f);
		Maps[1].transform.localPosition = new Vector3(57.6f, 0f, 0f);
	}

	public void Update()
	{
		if (ThisTransform.position.x <= -57.6f * (float)Count)
		{
			Maps[change_Target].transform.localPosition = new Vector3(57.6f * (float)(Count + 1), 0f, 0f);
			Count++;
			if (change_Target.Equals(0))
			{
				change_Target = 1;
			}
			else
			{
				change_Target = 0;
			}
		}
	}
}
