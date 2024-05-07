using UnityEngine;

public class FXQ_Adamantine : MonoBehaviour
{
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	private void Start()
	{
		anim.SetBool("UnderAttack", false);
	}

	private void Update()
	{
	}

	public void UnderAttack()
	{
		if (!(anim == null) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		{
			anim.Play("Hurt");
		}
	}
}
