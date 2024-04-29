using UnityEngine;

public static class SA_EditorTesting
{
	public static bool IsInsideEditor
	{
		get
		{
			bool result = false;
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
			{
				result = true;
			}
			return result;
		}
	}
}
