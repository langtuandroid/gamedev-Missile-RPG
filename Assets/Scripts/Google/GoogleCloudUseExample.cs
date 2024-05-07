using System.Text;
using UnityEngine;

public class GoogleCloudUseExample : MonoBehaviour
{
	public GameObject[] hideOnConnect;

	public GameObject[] showOnConnect;

	private void Awake()
	{
		GoogleCloudManager.ActionAllStatesLoaded += OnAllLoaded;
		GoogleCloudManager.ActionStateLoaded += OnStateUpdated;
		GoogleCloudManager.ActionStateResolved += OnStateUpdated;
		GoogleCloudManager.ActionStateUpdated += OnStateUpdated;
		GoogleCloudManager.ActionStateConflict += OnStateConflict;
		SA_Singleton<GooglePlayConnection>.instance.Connect();
	}

	private void FixedUpdate()
	{
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			GameObject[] array = hideOnConnect;
			foreach (GameObject gameObject in array)
			{
				gameObject.SetActive(false);
			}
			GameObject[] array2 = showOnConnect;
			foreach (GameObject gameObject2 in array2)
			{
				gameObject2.SetActive(true);
			}
		}
		else
		{
			GameObject[] array3 = hideOnConnect;
			foreach (GameObject gameObject3 in array3)
			{
				gameObject3.SetActive(true);
			}
			GameObject[] array4 = showOnConnect;
			foreach (GameObject gameObject4 in array4)
			{
				gameObject4.SetActive(false);
			}
		}
	}

	private void LoadAllStates()
	{
		SA_Singleton<GoogleCloudManager>.instance.loadAllStates();
	}

	private void LoadState()
	{
		SA_Singleton<GoogleCloudManager>.instance.loadState(0);
	}

	private void UpdateState()
	{
		string s = "Hello bytes data";
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(s);
		SA_Singleton<GoogleCloudManager>.instance.updateState(0, bytes);
	}

	private void DeleteState()
	{
		SA_Singleton<GoogleCloudManager>.instance.deleteState(0);
		GoogleCloudManager.ActionStateDeleted += OnStateDeleted;
	}

	private void OnStateConflict(GoogleCloudResult result)
	{
		AN_PoupsProxy.showMessage("OnStateUpdated", string.Concat(result.message, "\n State ID: ", result.stateKey, "\n State Data: ", result.stateData, "\n State Conflict: ", result.serverConflictData, "\n State resolve: ", result.resolvedVersion));
		SA_Singleton<GoogleCloudManager>.instance.resolveState(result.stateKey, result.stateData, result.resolvedVersion);
	}

	private void OnStateUpdated(GoogleCloudResult result)
	{
		AN_PoupsProxy.showMessage("State Updated", result.message + "\n State ID: " + result.stateKey + "\n State Data: " + result.stateDataString);
	}

	private void OnAllLoaded(GoogleCloudResult result)
	{
		AN_PoupsProxy.showMessage("All States Loaded", result.message + "\nTotal states: " + SA_Singleton<GoogleCloudManager>.instance.states.Count);
	}

	private void OnStateDeleted(GoogleCloudResult result)
	{
		AN_PoupsProxy.showMessage("KeyDeleted", result.message + "\n for state key: " + result.stateKey);
	}
}
