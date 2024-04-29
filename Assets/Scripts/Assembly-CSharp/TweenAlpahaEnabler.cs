using UnityEngine;

public class TweenAlpahaEnabler : MonoBehaviour
{
	private TweenAlpha _ta;

	private void Awake()
	{
		_ta = base.gameObject.GetComponent<TweenAlpha>();
	}

	private void OnEnable()
	{
		_ta.Reset();
		_ta.enabled = true;
	}
}
