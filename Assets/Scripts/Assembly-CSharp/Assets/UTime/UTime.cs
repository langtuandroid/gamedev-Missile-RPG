using System;
using System.Collections;
using UnityEngine;

namespace Assets.UTime
{
	public class UTime : MonoBehaviour
	{
		private const string TimeServer = "https://script.google.com/macros/s/AKfycbyal_mx91_jytjMzr_ykoP3NfZXBVMNRNXCX7qmt0QpTj6mAHg/exec";

		private static UTime _instance;

		public void Awake()
		{
			_instance = this;
		}

		public void GetUtcTimeAsync(Action<bool, string, DateTime> callback)
		{
			StartCoroutine(Download("https://script.google.com/macros/s/AKfycbyal_mx91_jytjMzr_ykoP3NfZXBVMNRNXCX7qmt0QpTj6mAHg/exec", delegate(WWW www)
			{
				if (www.error == null)
				{
					try
					{
						callback(true, null, DateTime.Parse(www.text).ToUniversalTime());
						return;
					}
					catch (Exception ex)
					{
						callback(false, ex.Message, DateTime.MinValue);
						return;
					}
				}
				callback(false, www.error, DateTime.MinValue);
			}));
		}

		public void HasConnection(Action<bool> callback)
		{
			StartCoroutine(Download("https://script.google.com/macros/s/AKfycbyal_mx91_jytjMzr_ykoP3NfZXBVMNRNXCX7qmt0QpTj6mAHg/exec", delegate(WWW www)
			{
				callback(www.error == null);
			}));
		}

		private static IEnumerator Download(string url, Action<WWW> callback)
		{
			WWW www = new WWW(url);
			yield return www;
			callback(www);
		}
	}
}
