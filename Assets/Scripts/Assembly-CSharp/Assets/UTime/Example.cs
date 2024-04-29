using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UTime
{
	public class Example : MonoBehaviour
	{
		public Text Time;

		public Text Time_2;

		public void Start()
		{
		}

		private void OnTimeReceived(bool success, string error, DateTime time)
		{
			if (success)
			{
				Time.text = string.Format("Network time (UTC+0) = {0}\nTo local time = {1}", time, time.ToLocalTime());
				Time_2.text = string.Format("To local time = {0}", time.ToLocalTime());
			}
			else
			{
				Time.text = error;
			}
		}
	}
}
