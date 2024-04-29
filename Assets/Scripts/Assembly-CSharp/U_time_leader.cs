using System;
using System.Collections;
using Assets.UTime;
using UnityEngine;

public class U_time_leader : MonoBehaviour
{
	public static U_time_leader me;

	public DateTime world_date;

	public UTime utime;

	public bool Time_reading;

	public bool Chaat;

	public bool time_Load_ING;

	public void Awake()
	{
		if (me == null)
		{
			me = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			utime.HasConnection(delegate(bool connection)
			{
				Debug.Log("Connection: " + connection);
			});
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void Time_Load()
	{
		me.Time_reading = false;
		time_Load_ING = true;
		utime.GetUtcTimeAsync(OnTimeReceived);
		StopCoroutine(Time_Waiting());
		StartCoroutine(Time_Waiting());
	}

	public IEnumerator Time_Waiting()
	{
		yield return new WaitForSeconds(1f);
		if (time_Load_ING)
		{
			utime.GetUtcTimeAsync(OnTimeReceived);
		}
		yield return new WaitForSeconds(0.5f);
		if (time_Load_ING)
		{
			utime.GetUtcTimeAsync(OnTimeReceived);
		}
	}

	private void OnTimeReceived(bool success, string error, DateTime time)
	{
		if (success)
		{
			time_Load_ING = false;
			world_date = time.ToLocalTime();
			Time_reading = true;
		}
		else
		{
			time_Load_ING = false;
			Debug.Log("시간 읽어오기 불가능.");
			Time_reading = false;
		}
	}
}
