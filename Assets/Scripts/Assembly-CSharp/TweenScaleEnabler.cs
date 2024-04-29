using UnityEngine;

public class TweenScaleEnabler : MonoBehaviour
{
	private TweenScale _ts;

	private void Awake()
	{
		_ts = base.gameObject.GetComponent<TweenScale>();
	}

	private void OnEnable()
	{
		_ts.Reset();
		_ts.enabled = true;
	}
}
