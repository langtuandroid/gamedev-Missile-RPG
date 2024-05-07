using UnityEngine;

public class AnalyticsUseExample : MonoBehaviour
{
	private void Awake()
	{
		SA_Singleton<AndroidGoogleAnalytics>.instance.StartTracking();
	}

	private void Start()
	{
		SA_Singleton<AndroidGoogleAnalytics>.instance.SendView("Home Screen");
		SA_Singleton<AndroidGoogleAnalytics>.instance.SendEvent("Category", "Action", "label");
		SA_Singleton<AndroidGoogleAnalytics>.instance.SendEvent("Category", "Action", "label", 100L, "screen", "main");
		SA_Singleton<AndroidGoogleAnalytics>.instance.SendTiming("App Started", (long)Time.time);
		SA_Singleton<AndroidGoogleAnalytics>.instance.SetKey("SCREEN", "MAIN");
		SA_Singleton<AndroidGoogleAnalytics>.instance.EnableAdvertisingIdCollection(true);
		PurchaseTackingExample();
	}

	public void PurchaseTackingExample()
	{
		SA_Singleton<AndroidGoogleAnalytics>.instance.CreateTransaction("0_123456", "In-app Store", 2.1f, 0.17f, 0f, "USD");
		SA_Singleton<AndroidGoogleAnalytics>.instance.CreateItem("0_123456", "Level Pack: Space", "L_789", "Game expansions", 1.99f, 1, "USD");
	}
}
