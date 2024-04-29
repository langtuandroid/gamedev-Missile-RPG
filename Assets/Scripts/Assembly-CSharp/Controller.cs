using UnityEngine;

public class Controller : MonoBehaviour
{
	public Character character;

	public bool Mute;

	public void Attack()
	{
		if (!(character != null))
		{
		}
	}

	public void Attack_Fin()
	{
		if (character != null)
		{
			character.Fight_End();
		}
	}

	public void Fight_End()
	{
		if (character != null)
		{
			character.Fight_End();
		}
	}

	public void Charge()
	{
	}

	public void Skill()
	{
	}

	public void EXIT()
	{
		base.gameObject.SetActive(false);
	}
}
