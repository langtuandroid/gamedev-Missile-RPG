using System;
using UnityEngine;

public class QuestAndEventsExample : MonoBehaviour
{
	private const string EVENT_ID = "CgkIipfs2qcGEAIQDQ";

	private const string QUEST_ID = "CgkIipfs2qcGEAIQDg";

	public GameObject avatar;

	private Texture defaulttexture;

	public Texture2D pieIcon;

	public DefaultPreviewButton connectButton;

	public SA_Label playerLabel;

	public DefaultPreviewButton[] ConnectionDependedntButtons;

	private void Start()
	{
		playerLabel.text = "Player Disconnected";
		defaulttexture = avatar.GetComponent<Renderer>().material.mainTexture;
		GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
		GooglePlayEvents instance = SA_Singleton<GooglePlayEvents>.instance;
		instance.OnEventsLoaded = (Action<GooglePlayResult>)Delegate.Combine(instance.OnEventsLoaded, new Action<GooglePlayResult>(OnEventsLoaded));
		GooglePlayQuests instance2 = SA_Singleton<GooglePlayQuests>.instance;
		instance2.OnQuestsAccepted = (Action<GP_QuestResult>)Delegate.Combine(instance2.OnQuestsAccepted, new Action<GP_QuestResult>(OnQuestsAccepted));
		GooglePlayQuests instance3 = SA_Singleton<GooglePlayQuests>.instance;
		instance3.OnQuestsCompleted = (Action<GP_QuestResult>)Delegate.Combine(instance3.OnQuestsCompleted, new Action<GP_QuestResult>(OnQuestsCompleted));
		GooglePlayQuests instance4 = SA_Singleton<GooglePlayQuests>.instance;
		instance4.OnQuestsLoaded = (Action<GP_QuestResult>)Delegate.Combine(instance4.OnQuestsLoaded, new Action<GP_QuestResult>(OnQuestsLoaded));
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
			SA_Singleton<GooglePlayConnection>.instance.Disconnect();
		}
		else
		{
			SA_StatusBar.text = "Connecting to Play Service...";
			SA_Singleton<GooglePlayConnection>.instance.Connect();
		}
	}

	private void SendInvitation()
	{
		GP_AppInviteBuilder gP_AppInviteBuilder = new GP_AppInviteBuilder("Test Title");
		gP_AppInviteBuilder.SetMessage("Test Message");
		gP_AppInviteBuilder.SetDeepLink("http://testUrl");
		gP_AppInviteBuilder.SetCallToActionText("Test Text");
		GP_AppInvitesController.ActionAppInvitesSent += HandleActionAppInvitesSent;
		SA_Singleton<GP_AppInvitesController>.Instance.StartInvitationDialog(gP_AppInviteBuilder);
	}

	private void HandleActionAppInvitesSent(GP_SendAppInvitesResult res)
	{
		if (res.IsSucceeded)
		{
			Debug.Log("Invitation was sent to " + res.InvitationIds.Length + " people");
		}
		else
		{
			Debug.Log("App invite failed" + res.Message);
		}
		GP_AppInvitesController.ActionAppInvitesSent -= HandleActionAppInvitesSent;
	}

	private void GetInvitation()
	{
		GP_AppInvitesController.ActionAppInviteRetrieved += HandleActionAppInviteRetrieved;
		SA_Singleton<GP_AppInvitesController>.Instance.GetInvitation(true);
	}

	private void HandleActionAppInviteRetrieved(GP_RetrieveAppInviteResult res)
	{
		GP_AppInvitesController.ActionAppInviteRetrieved -= HandleActionAppInviteRetrieved;
		if (res.IsSucceeded)
		{
			Debug.Log("Invitation Retrieved");
			GP_AppInvite appInvite = res.AppInvite;
			Debug.Log("Invitation Id: " + appInvite.Id);
			Debug.Log("Invitation Deep Link: " + appInvite.DeepLink);
			Debug.Log("Is Opened From PlayStore: " + appInvite.IsOpenedFromPlayStore);
		}
		else
		{
			Debug.Log("No invitation data found");
		}
	}

	private void FixedUpdate()
	{
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			if (SA_Singleton<GooglePlayManager>.instance.player.icon != null)
			{
				avatar.GetComponent<Renderer>().material.mainTexture = SA_Singleton<GooglePlayManager>.Instance.player.icon;
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

	public void LoadEvents()
	{
		SA_Singleton<GooglePlayEvents>.instance.LoadEvents();
	}

	public void IncrementEvent()
	{
		SA_Singleton<GooglePlayEvents>.instance.SumbitEvent("CgkIipfs2qcGEAIQDQ");
	}

	public void ShowAllQuests()
	{
		SA_Singleton<GooglePlayQuests>.instance.ShowQuests();
	}

	public void ShowAcceptedQuests()
	{
		SA_Singleton<GooglePlayQuests>.instance.ShowQuests(GP_QuestsSelect.SELECT_ACCEPTED);
	}

	public void ShowCompletedQuests()
	{
		SA_Singleton<GooglePlayQuests>.instance.ShowQuests(GP_QuestsSelect.SELECT_COMPLETED);
	}

	public void ShowOpenQuests()
	{
		SA_Singleton<GooglePlayQuests>.instance.ShowQuests(GP_QuestsSelect.SELECT_OPEN);
	}

	public void AcceptQuest()
	{
		SA_Singleton<GooglePlayQuests>.instance.AcceptQuest("CgkIipfs2qcGEAIQDg");
	}

	public void LoadQuests()
	{
		SA_Singleton<GooglePlayQuests>.instance.LoadQuests(GP_QuestSortOrder.SORT_ORDER_ENDING_SOON_FIRST);
	}

	private void OnEventsLoaded(GooglePlayResult result)
	{
		Debug.Log("Total Events: " + SA_Singleton<GooglePlayEvents>.instance.Events.Count);
		AN_PoupsProxy.showMessage("Events Loaded", "Total Events: " + SA_Singleton<GooglePlayEvents>.instance.Events.Count);
		SA_StatusBar.text = "OnEventsLoaded:  " + result.Response;
		foreach (GP_Event @event in SA_Singleton<GooglePlayEvents>.instance.Events)
		{
			Debug.Log(@event.Id);
			Debug.Log(@event.Description);
			Debug.Log(@event.FormattedValue);
			Debug.Log(@event.Value);
			Debug.Log(@event.IconImageUrl);
			Debug.Log(@event.icon);
		}
	}

	private void OnQuestsAccepted(GP_QuestResult result)
	{
		AN_PoupsProxy.showMessage("On Quests Accepted", "Quests Accepted, ID: " + result.GetQuest().Id);
		SA_StatusBar.text = "OnQuestsAccepted:  " + result.Response;
	}

	private void OnQuestsCompleted(GP_QuestResult result)
	{
		Debug.Log("Quests Completed, Reward: " + result.GetQuest().RewardData);
		AN_PoupsProxy.showMessage("On Quests Completed", "Quests Completed, Reward: " + result.GetQuest().RewardData);
		SA_StatusBar.text = "OnQuestsCompleted:  " + result.Response;
	}

	private void OnQuestsLoaded(GooglePlayResult result)
	{
		Debug.Log("Total Quests: " + SA_Singleton<GooglePlayQuests>.instance.GetQuests().Count);
		AN_PoupsProxy.showMessage("Quests Loaded", "Total Quests: " + SA_Singleton<GooglePlayQuests>.instance.GetQuests().Count);
		SA_StatusBar.text = "OnQuestsLoaded:  " + result.Response;
		foreach (GP_Quest quest in SA_Singleton<GooglePlayQuests>.instance.GetQuests())
		{
			Debug.Log(quest.Id);
		}
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

	private void OnDestroy()
	{
		GooglePlayConnection.ActionPlayerConnected -= OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected -= OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived -= OnConnectionResult;
	}
}
