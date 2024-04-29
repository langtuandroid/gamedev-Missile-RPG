using UnityEngine;

public class FXQ_TargetAnimatorEvent : MonoBehaviour
{
	public enum ParticleEvent
	{
		None = 0,
		Attack = 1,
		UI = 2
	}

	public Animator m_TargetAnimator;

	public ParticleEvent m_ParticleEvent;
}
