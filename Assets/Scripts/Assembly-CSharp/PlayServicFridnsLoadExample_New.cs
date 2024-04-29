using UnityEngine;

public class PlayServicFridnsLoadExample_New : MonoBehaviour
{
	public GameObject avatar;

	public SA_Label playerLabel;

	public DefaultPreviewButton connectButton;

	private Texture defaulttexture;

	public DefaultPreviewButton[] ConnectionDependedntButtons;

	public CustomPlayerUIRow[] rows;

	private void Awake()
	{
		playerLabel.text = "Player Disconnected";
		defaulttexture = avatar.GetComponent<Renderer>().material.mainTexture;
		GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
		GooglePlayManager.ActionFriendsListLoaded += OnFriendListLoaded;
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			OnPlayerConnected();
		}
	}

	private void ConncetButtonPress()
	{
		Debug.Log("GooglePlayManager State  -> " + GooglePlayConnection.State);
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			SA_StatusBar.text = "Disconnecting from Play Service...";
			SA_Singleton<GooglePlayConnection>.Instance.Disconnect();
		}
		else
		{
			SA_StatusBar.text = "Connecting to Play Service...";
			SA_Singleton<GooglePlayConnection>.Instance.Connect();
		}
	}

	private void Update()
	{
		CustomPlayerUIRow[] array = rows;
		foreach (CustomPlayerUIRow customPlayerUIRow in array)
		{
			customPlayerUIRow.Disable();
		}
		if (GooglePlayConnection.State != GPConnectionState.STATE_CONNECTED)
		{
			return;
		}
		int num = 0;
		foreach (string friends in SA_Singleton<GooglePlayManager>.instance.friendsList)
		{
			GooglePlayerTemplate playerById = SA_Singleton<GooglePlayManager>.instance.GetPlayerById(friends);
			if (playerById != null)
			{
				rows[num].playerId.text = playerById.playerId;
				rows[num].playerName.text = playerById.name;
				if (playerById.hasIconImage && playerById.icon != null)
				{
					rows[num].hasIcon.text = "Yes";
				}
				else
				{
					rows[num].hasIcon.text = "No";
				}
				if (playerById.hasHiResImage && playerById.image != null)
				{
					rows[num].hasImage.text = "Yes";
				}
				else
				{
					rows[num].hasImage.text = "No";
				}
				rows[num].avatar.GetComponent<Renderer>().enabled = true;
				if (playerById.hasIconImage && playerById.icon != null)
				{
					rows[num].avatar.GetComponent<Renderer>().material.mainTexture = playerById.icon;
				}
				else
				{
					rows[num].avatar.GetComponent<Renderer>().material.mainTexture = defaulttexture;
				}
			}
			num++;
			if (num > 5)
			{
				break;
			}
		}
	}

	private void FixedUpdate()
	{
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			if (SA_Singleton<GooglePlayManager>.instance.player.icon != null)
			{
				avatar.GetComponent<Renderer>().material.mainTexture = SA_Singleton<GooglePlayManager>.instance.player.icon;
			}
		}
		else
		{
			avatar.GetComponent<Renderer>().material.mainTexture = defaulttexture;
		}
		string text = "Connect";
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			text = "Disconnect";
			DefaultPreviewButton[] connectionDependedntButtons = ConnectionDependedntButtons;
			foreach (DefaultPreviewButton defaultPreviewButton in connectionDependedntButtons)
			{
				defaultPreviewButton.EnabledButton();
			}
		}
		else
		{
			DefaultPreviewButton[] connectionDependedntButtons2 = ConnectionDependedntButtons;
			foreach (DefaultPreviewButton defaultPreviewButton2 in connectionDependedntButtons2)
			{
				defaultPreviewButton2.DisabledButton();
			}
			text = ((GooglePlayConnection.State != GPConnectionState.STATE_DISCONNECTED && GooglePlayConnection.State != 0) ? "Connecting.." : "Connect");
		}
		connectButton.text = text;
	}

	public void LoadFriendsList()
	{
		SA_Singleton<GooglePlayManager>.instance.LoadFriends();
	}

	private void OnFriendListLoaded(GooglePlayResult result)
	{
		GooglePlayManager.ActionFriendsListLoaded -= OnFriendListLoaded;
		SA_StatusBar.text = "Load Friends Result:  " + result.Response;
	}

	private void OnPlayerDisconnected()
	{
		SA_StatusBar.text = "Player Disconnected";
		playerLabel.text = "Player Disconnected";
	}

	private void OnPlayerConnected()
	{
		SA_StatusBar.text = "Player Connected";
		playerLabel.text = SA_Singleton<GooglePlayManager>.instance.player.name;
	}

	private void OnConnectionResult(GooglePlayConnectionResult result)
	{
		SA_StatusBar.text = "ConnectionResul:  " + result.code;
		Debug.Log(result.code.ToString());
	}
}
