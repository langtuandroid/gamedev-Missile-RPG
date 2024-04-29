using UnityEngine;

public class Dropport_CTRL : MonoBehaviour
{
	public void Shake()
	{
		SoundManager.me.Boom();
		Fight_Master.me.CameraShake_Long();
		for (int i = 0; i < Fight_Master.me.Live_Enemies.Count; i++)
		{
			Fight_Master.me.Live_Enemies[i].Invincible = false;
			Fight_Master.me.Live_Enemies[i].Remove();
			OBJ_Pool.Make_OBJ(OBJ_Pool.me.DeadSpill, ref OBJ_Pool.DeadSpill_Number, Fight_Master.me.Live_Enemies[i].ThisTransform.position);
		}
		Fight_Master.me.Live_Enemies.Clear();
		for (int j = 0; j < Fight_Master.me.Units.Length; j++)
		{
			if (Fight_Master.me.Units[j].gameObject.activeSelf)
			{
				OBJ_Pool.Make_OBJ(OBJ_Pool.me.effect_boom, ref OBJ_Pool.effect_boom_Number, Fight_Master.me.Units[j].ThisTransform.position);
				if (Fight_Master.me.Units[j].ThisTransform.position.y < 0f)
				{
					OBJ_Pool.Make_OBJ(OBJ_Pool.me.Player_DeadSpill, ref OBJ_Pool.Player_DeadSpill_Number, Fight_Master.me.Units[j].ThisTransform.position);
				}
				Fight_Master.me.Units[j].gameObject.SetActive(false);
			}
		}
	}

	public void Dropport_End()
	{
		SoundManager.me.Congretu();
		Fight_Master.me.StartCoroutine(Fight_Master.me.NEW_START_by_Drop_Port());
	}
}
