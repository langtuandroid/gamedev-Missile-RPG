using System;
using UnityEngine;

public static class Time_Checker
{
	public static string dateTime = string.Empty;

	public static DateTime date;

	public static TimeSpan delta;

	private static DateTime SCENE_Final_Time_Load(int scene_number)
	{
		dateTime = Security.GetString(string.Format("SCENE_{0:000000}_VISIT", scene_number), null);
		if (string.IsNullOrEmpty(dateTime))
		{
			SCENE_Final_Time_Save(scene_number);
			U_time_leader.me.Time_Load();
			if (U_time_leader.me.Time_reading)
			{
				dateTime = string.Format("{0}", U_time_leader.me.world_date);
			}
			else
			{
				dateTime = string.Format("{0}", DateTime.Now);
			}
		}
		return Convert.ToDateTime(dateTime);
	}

	public static void SCENE_Final_Time_Save(int scene_number)
	{
		U_time_leader.me.Time_Load();
		if (U_time_leader.me.Time_reading)
		{
			dateTime = string.Format("{0}", U_time_leader.me.world_date);
		}
		else
		{
			dateTime = string.Format("{0}", DateTime.Now);
		}
		Security.SetString(string.Format("SCENE_{0:000000}_VISIT", scene_number), dateTime);
		Debug.Log(scene_number + "번 씬 저장한 시간 " + dateTime);
	}

	public static void AD_Check(int scene_number)
	{
		date = SCENE_Final_Time_Load(scene_number);
		U_time_leader.me.Time_Load();
		if (U_time_leader.me.Time_reading)
		{
			delta = U_time_leader.me.world_date - date;
		}
		else
		{
			delta = DateTime.Now - date;
		}
		Debug.Log("저장된 시간에서 지금까지 지난 시간 : " + (int)delta.TotalSeconds);
		if ((int)delta.TotalSeconds > 3600)
		{
			Debug.Log("시간초과 성공.");
		}
	}

	public static int Get_Scene_Visit_Time(int scene_number)
	{
		date = SCENE_Final_Time_Load(scene_number);
		U_time_leader.me.Time_Load();
		if (U_time_leader.me.Time_reading)
		{
			delta = U_time_leader.me.world_date - date;
		}
		else
		{
			delta = DateTime.Now - date;
		}
		Debug.Log("저장된 시간에서 지금까지 지난 시간 : " + (int)delta.TotalSeconds);
		if ((int)delta.TotalSeconds > 0)
		{
			return (int)delta.TotalSeconds;
		}
		return -1;
	}

	public static void Time_Save(string KEY)
	{
		string empty = string.Empty;
		if (U_time_leader.me.Time_reading)
		{
			empty = string.Format("{0}", U_time_leader.me.world_date);
			Security.SetString(KEY, empty);
		}
		else
		{
			empty = string.Format("{0}", new DateTime(DateTime.Today.Day - 1));
		}
	}

	public static int Time_LOAD(string KEY, bool TIME_Change_Ignore)
	{
		string @string = Security.GetString(KEY, string.Empty);
		if (string.IsNullOrEmpty(@string))
		{
			Debug.Log("<" + KEY + "> : 저장된 시간이 없습니다! 처음임.");
			return -999999999;
		}
		date = Convert.ToDateTime(@string);
		if (U_time_leader.me.Time_reading)
		{
			delta = U_time_leader.me.world_date - date;
			Debug.Log(string.Concat("시간불러오기 성공 : ", delta, " / ", delta.TotalSeconds));
		}
		else
		{
			Debug.Log("시간 불러오기 실패");
		}
		return (int)delta.TotalSeconds;
	}

	public static int DAY_LOAD(string KEY)
	{
		string @string = Security.GetString(KEY, string.Empty);
		if (string.IsNullOrEmpty(@string))
		{
			return -999999999;
		}
		date = Convert.ToDateTime(@string);
		Debug.Log(U_time_leader.me.world_date.DayOfYear);
		Debug.Log("《 " + KEY + " 》 값을 저장했던 시간에서 지금까지 지난 일수 : " + (U_time_leader.me.world_date.DayOfYear - date.DayOfYear));
		return U_time_leader.me.world_date.DayOfYear - date.DayOfYear;
	}

	public static void Time_Remove(string KEY)
	{
		Security.SetString(KEY, string.Empty);
	}

	public static string ShowTime_Label(float Left_time)
	{
		if (Left_time > 3600f)
		{
			return string.Format("{0:00}:{1:00}:{2:00}", (int)Left_time / 3600, (int)(Left_time / 60f) % 60, (int)Left_time % 60);
		}
		if (Left_time > 60f)
		{
			return string.Format("{0:00}:{1:00}", (int)(Left_time / 60f), (int)Left_time % 60);
		}
		return string.Format("{0:N0}초", (int)Left_time);
	}

	public static string ShowTime_Label_noT(float Left_time)
	{
		if (Left_time > 3600f)
		{
			return string.Format("{0:00}:{1:00}:{2:00}", (int)Left_time / 3600, (int)(Left_time / 60f) % 60, (int)Left_time % 60);
		}
		if (Left_time > 60f)
		{
			return string.Format("{0:0.#}:{1:00}", (int)(Left_time / 60f), (int)Left_time % 60);
		}
		return string.Format("{0:N1}", Left_time);
	}

	public static int Show_INT(float Left_time)
	{
		return 1 + (int)(Left_time / 300f);
	}
}
