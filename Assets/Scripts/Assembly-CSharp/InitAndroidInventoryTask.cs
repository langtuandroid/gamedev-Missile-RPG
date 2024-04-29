using System;
using UnityEngine;

public class InitAndroidInventoryTask : MonoBehaviour
{
	public event Action ActionComplete = delegate
	{
	};

	public event Action ActionFailed = delegate
	{
	};

	public static InitAndroidInventoryTask Create()
	{
		return new GameObject("InitAndroidInventoryTask").AddComponent<InitAndroidInventoryTask>();
	}

	public void Run()
	{
		Debug.Log("InitAndroidInventoryTask task started");
		if (SA_Singleton<AndroidInAppPurchaseManager>.Instance.IsConnected)
		{
			OnBillingConnected(null);
			return;
		}
		AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;
		if (!SA_Singleton<AndroidInAppPurchaseManager>.Instance.IsConnectingToServiceInProcess)
		{
			SA_Singleton<AndroidInAppPurchaseManager>.Instance.LoadStore();
		}
	}

	private void OnBillingConnected(BillingResult result)
	{
		Debug.Log("OnBillingConnected");
		if (result == null)
		{
			OnBillingConnectFinished();
			return;
		}
		AndroidInAppPurchaseManager.ActionBillingSetupFinished -= OnBillingConnected;
		if (result.isSuccess)
		{
			OnBillingConnectFinished();
			return;
		}
		Debug.Log("OnBillingConnected Failed");
		this.ActionFailed();
	}

	private void OnBillingConnectFinished()
	{
		Debug.Log("OnBillingConnected COMPLETE");
		if (SA_Singleton<AndroidInAppPurchaseManager>.instance.IsInventoryLoaded)
		{
			Debug.Log("IsInventoryLoaded COMPLETE");
			this.ActionComplete();
			return;
		}
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += OnRetrieveProductsFinised;
		if (!SA_Singleton<AndroidInAppPurchaseManager>.Instance.IsProductRetrievingInProcess)
		{
			SA_Singleton<AndroidInAppPurchaseManager>.Instance.RetrieveProducDetails();
		}
	}

	private void OnRetrieveProductsFinised(BillingResult result)
	{
		Debug.Log("OnRetrieveProductsFinised");
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetrieveProductsFinised;
		if (result.isSuccess)
		{
			Debug.Log("OnRetrieveProductsFinised COMPLETE");
			this.ActionComplete();
		}
		else
		{
			Debug.Log("OnRetrieveProductsFinised FAILED");
			this.ActionFailed();
		}
	}
}
