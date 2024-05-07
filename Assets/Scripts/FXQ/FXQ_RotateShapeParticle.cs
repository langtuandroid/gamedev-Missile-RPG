using UnityEngine;

public class FXQ_RotateShapeParticle : MonoBehaviour
{
	public Vector3 m_StartRotation;

	public Vector3 m_RotationOvertime;

	private void Start()
	{
		base.transform.localEulerAngles = m_StartRotation;
	}

	private void Update()
	{
		base.transform.localEulerAngles = base.transform.localEulerAngles + m_RotationOvertime * Time.deltaTime;
	}
}
