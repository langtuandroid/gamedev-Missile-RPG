using UnityEngine;

public class AppEventHandlerExample : MonoBehaviour
{
	private void Awake()
	{
		ISN_Singleton<IOSNativeAppEvents>.Instance.Subscribe();
		IOSNativeAppEvents.OnApplicationDidReceiveMemoryWarning += OnApplicationDidReceiveMemoryWarning;
		IOSNativeAppEvents.OnApplicationDidBecomeActive += HandleOnApplicationDidBecomeActive;
	}

	private void HandleOnApplicationDidBecomeActive()
	{
		Debug.Log("Caught OnApplicationDidBecomeActive event");
	}

	private void OnApplicationDidReceiveMemoryWarning()
	{
		Debug.Log("Caught OnApplicationDidReceiveMemoryWarning event");
	}
}
