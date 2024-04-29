using System;
using UnityEngine;
 

[Serializable]
public class JSCall : MonoBehaviour
{
	public virtual void Start()
	{
		SendMessage("InitGameCneter");
	}

	public virtual void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 150f, 40f), "Submit Score"))
		{
			SendMessage("SubmitScore", 100);
		}
		 
	}

	public virtual void Main()
	{
	}
}
