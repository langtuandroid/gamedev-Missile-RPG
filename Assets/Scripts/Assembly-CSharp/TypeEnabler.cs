using UnityEngine;

public class TypeEnabler : MonoBehaviour
{
	private TypewriterEffect _ta;

	private void Awake()
	{
		_ta = base.gameObject.GetComponent<TypewriterEffect>();
	}

	private void OnEnable()
	{
		_ta.ResetToBeginning();
		_ta.enabled = true;
	}
}
