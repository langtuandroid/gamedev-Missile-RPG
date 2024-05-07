using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWWTextureLoader : MonoBehaviour
{
	public static Dictionary<string, Texture2D> LocalCache = new Dictionary<string, Texture2D>();

	private string _url;

	public event Action<Texture2D> OnLoad = delegate
	{
	};

	public static WWWTextureLoader Create()
	{
		return new GameObject("WWWTextureLoader").AddComponent<WWWTextureLoader>();
	}

	public void LoadTexture(string url)
	{
		_url = url;
		if (LocalCache.ContainsKey(_url))
		{
			this.OnLoad(LocalCache[_url]);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			StartCoroutine(LoadCoroutin());
		}
	}

	private IEnumerator LoadCoroutin()
	{
		WWW www = new WWW(_url);
		yield return www;
		if (www.error == null)
		{
			UpdateLocalCache(_url, www.texture);
			this.OnLoad(www.texture);
		}
		else
		{
			this.OnLoad(null);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private static void UpdateLocalCache(string url, Texture2D image)
	{
	}
}
