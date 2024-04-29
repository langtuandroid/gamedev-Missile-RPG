using System;

[Serializable]
public class Unit_Data
{
	public string NAME;

	public string DMG;

	public string TIER_PLUS;

	public int ATK_TYPE;

	public float ATK_SPEED;

	public string FIX_DPS;

	public Unit_Type unit_TYPE;

	public string TIERUP_Price;

	public int[] BUFF_ID = new int[9];

	public float[] BUFF_Value = new float[9];

	public int MAX_LV = 9999;
}
