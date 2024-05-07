using UnityEngine;

[ExecuteInEditMode]
public class Z_Update_Live : MonoBehaviour
{
	private float Z_order = 0.5f;

	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.y * 0.5f - base.transform.position.x * 0.001f);
	}
}
