using UnityEngine;

public class GamePlayExample : MonoBehaviour
{
	public GameObject[] objectToEnbaleOnInit;

	public GameObject[] objectToDisableOnInit;

	public DefaultPreviewButton[] initBoundButtons;

	public SA_Label coinsLable;

	public SA_Label boostLabel;

	private void Awake()
	{
		GameBillingManagerExample.init();
	}

	private void FixedUpdate()
	{
		coinsLable.text = "Total Coins: " + GameDataExample.coins;
		if (GameDataExample.IsBoostPurchased)
		{
			boostLabel.text = "Boost Enabled";
		}
		else
		{
			boostLabel.text = "Boost Disabled";
		}
		if (GameBillingManagerExample.isInited)
		{
			GameObject[] array = objectToEnbaleOnInit;
			foreach (GameObject gameObject in array)
			{
				gameObject.SetActive(true);
			}
			GameObject[] array2 = objectToDisableOnInit;
			foreach (GameObject gameObject2 in array2)
			{
				gameObject2.SetActive(false);
			}
			DefaultPreviewButton[] array3 = initBoundButtons;
			foreach (DefaultPreviewButton defaultPreviewButton in array3)
			{
				defaultPreviewButton.EnabledButton();
			}
		}
		else
		{
			DefaultPreviewButton[] array4 = initBoundButtons;
			foreach (DefaultPreviewButton defaultPreviewButton2 in array4)
			{
				defaultPreviewButton2.DisabledButton();
			}
		}
	}

	public void AddCoins()
	{
		GameBillingManagerExample.purchase("small_coins_bag");
	}

	public void Boost()
	{
		GameBillingManagerExample.purchase("coins_bonus");
	}
}
