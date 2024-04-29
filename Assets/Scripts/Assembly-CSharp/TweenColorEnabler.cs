using UnityEngine;

public class TweenColorEnabler : MonoBehaviour
{
	private TweenColor _ta;

	private void Awake()
	{
		_ta = base.gameObject.GetComponent<TweenColor>();
	}

	private void OnEnable()
	{
		_ta.Reset();
		_ta.enabled = true;
	}
}
