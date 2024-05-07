using UnityEngine;

public class Uranium_Gift_Popup : MonoBehaviour
{
	public int URANIUM_ID;

	public UILabel Word_Label;

	public UILabel Reward_Label;

	public void Setting(int ID)
	{
		URANIUM_ID = ID;
		if (Now_Data.me.HIDDEN[ID].Equals(0))
		{
			SoundManager.me.Congretu();
			Word_Label.text = Localization.Get(string.Format("HIDDEN_WORD_{0:000}", ID));
			Reward_Label.text = string.Format("100 {0}", Localization.Get("GETTING"));
			UI_Master.me.Popup(base.gameObject);
		}
	}

	public void OnDisable()
	{
		Now_Data.me.HIDDEN[URANIUM_ID] = 1;
		Security.SetInt(string.Format("HIDDEN_{0:000}", URANIUM_ID), Now_Data.me.HIDDEN[URANIUM_ID]);
		Now_Data.me.MEDAL_Change(100);
		Now_Data.me.ALL_HIDDEN++;
		Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_HIDDEN);
		Security.SetInt("ALL_HIDDEN", Now_Data.me.ALL_HIDDEN);
	}
}
