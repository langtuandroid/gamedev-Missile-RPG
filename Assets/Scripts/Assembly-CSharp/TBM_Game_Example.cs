using System.Text;
using UnityEngine;

public class TBM_Game_Example : AndroidNativeExampleBase
{
	public GameObject avatar;

	public GameObject hi;

	public SA_Label playerLabel;

	public SA_Label gameState;

	public SA_Label parisipants;

	public DefaultPreviewButton connectButton;

	public DefaultPreviewButton helloButton;

	public DefaultPreviewButton leaveRoomButton;

	public DefaultPreviewButton showRoomButton;

	public DefaultPreviewButton[] ConnectionDependedntButtons;

	public SA_PartisipantUI[] patrisipants;

	private GP_TBM_Match mMatch;

	private void Start()
	{
		playerLabel.text = "Player Disconnected";
		GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			OnPlayerConnected();
		}
		InitTBM();
	}

	public void Init()
	{
		GooglePlayTBM.ActionMatchUpdated += ActionMatchUpdated;
	}

	public void playTurn()
	{
		string empty = string.Empty;
		string s = "My turn data sample";
		AndroidNativeUtility.ShowPreloader("Loading..", "Sending the tunr data");
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(s);
		SA_Singleton<GooglePlayTBM>.instance.TakeTrun(mMatch.Id, bytes, empty);
	}

	private void ActionMatchUpdated(GP_TBM_UpdateMatchResult result)
	{
	}

	public void InitTBM()
	{
		int variant = 1;
		SA_Singleton<GooglePlayTBM>.instance.SetVariant(variant);
		int exclusiveBitMask = 4;
		SA_Singleton<GooglePlayTBM>.instance.SetExclusiveBitMask(exclusiveBitMask);
		SA_Singleton<GooglePlayTBM>.instance.RegisterMatchUpdateListener();
	}

	public void ShowInboxUI()
	{
		SA_Singleton<GooglePlayTBM>.instance.ShowInbox();
	}

	public void FinishMathc()
	{
	}

	public void findMatch()
	{
		GooglePlayTBM.ActionMatchCreationCanceled += ActionMatchCreationCanceled;
		GooglePlayTBM.ActionMatchInitiated += ActionMatchInitiated;
		int minPlayers = 2;
		int maxPlayers = 2;
		bool allowAutomatch = true;
		SA_Singleton<GooglePlayTBM>.instance.StartSelectOpponentsView(minPlayers, maxPlayers, allowAutomatch);
	}

	private void ActionMatchCreationCanceled(AndroidActivityResult result)
	{
	}

	private void ActionMatchInitiated(GP_TBM_MatchInitiatedResult result)
	{
		if (!result.IsSucceeded)
		{
			AndroidMessage.Create("Match Initi Failed", "Status code: " + result.Response);
			return;
		}
		GP_TBM_Match match = result.Match;
		if (match.Data == null)
		{
		}
	}

	public void LoadAllMatchersInfo()
	{
		SA_Singleton<GooglePlayTBM>.instance.LoadAllMatchesInfo(GP_TBM_MatchesSortOrder.SORT_ORDER_MOST_RECENT_FIRST);
	}

	public void LoadActiveMatchesInfo()
	{
		SA_Singleton<GooglePlayTBM>.instance.LoadMatchesInfo(GP_TBM_MatchesSortOrder.SORT_ORDER_MOST_RECENT_FIRST, GP_TBM_MatchTurnStatus.MATCH_TURN_STATUS_MY_TURN, GP_TBM_MatchTurnStatus.MATCH_TURN_STATUS_THEIR_TURN);
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

	private void DrawParticipants()
	{
	}

	private void FixedUpdate()
	{
		DrawParticipants();
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
