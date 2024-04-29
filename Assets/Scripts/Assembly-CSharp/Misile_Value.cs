using Keiwando.BigInteger;
using UnityEngine;

public class Misile_Value
{
	public int Main_Ele;

	public float Speed;

	public BigInteger DMG = 0;

	public BigInteger T_DMG = 0;

	public float KnockBack = 0.5f;

	public Vector3 direction = default(Vector3);

	public bool Oneshot;

	public bool Bomb = true;

	public bool After_Bomb;

	public Character owner;

	public Character EX_Player;

	public Transform ThisTransform;

	public bool Critical;

	public float Life_time = 0.5f;
}
