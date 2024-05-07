using UnityEngine;

public class TweenRotationEnabler : MonoBehaviour
{
	private TweenRotation _tr;

	private void Awake()
	{
		_tr = base.gameObject.GetComponent<TweenRotation>();
	}

	private void OnEnable()
	{
		_tr.Reset();
		_tr.enabled = true;
	}
}
