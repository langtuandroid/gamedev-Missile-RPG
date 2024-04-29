using UnityEngine;

public class TweenPositionEnabler : MonoBehaviour
{
	private TweenPosition _tp;

	private void Awake()
	{
		_tp = base.gameObject.GetComponent<TweenPosition>();
	}

	private void OnEnable()
	{
		_tp.Reset();
		_tp.enabled = true;
	}
}
