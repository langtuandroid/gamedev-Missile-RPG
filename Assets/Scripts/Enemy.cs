using System.Collections;
using DG.Tweening;
using Keiwando.BigInteger;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Ch_Type CH_TYPE;

	public int CH_ID;

	public int CH_LV;

	public PlayerState PS;

	public Transform ThisTransform;

	public Transform Hit_Point;

	public Rigidbody2D rigidbody2D;

	public Transform Shot_Point;

	public SpriteRenderer sprite_Renderer;

	public GameObject Hit_transform;

	public UILabel Text;

	public BigInteger HP;

	public CharacterDATA Basic_Data;

	public UILabel HP_text;

	public UISlider HP_bar;

	public UISlider Skill_bar;

	public bool BOSS;

	public bool Golrem;

	public bool Portal;

	public bool DUNGEON_BOSS;

	public bool DUNGEON_BOSS_FIELD;

	public bool DUNGEON_MONSTER;

	public bool GOLREM_HELLSTONE;

	public bool Invincible;

	public Portal portal_data;

	public Dungeon_Boss boss_data;

	public Character attack_target_CH;

	public float Changed_Speed;

	public float Changed_ATK_Speed;

	public GameObject Event_CAP;

	public bool Event_Mon;

	public float Origin_size;

	public float Origin_speed;

	private Character attacked_BY;

	private BigInteger Real_DMG = new BigInteger(0L);

	public float knockback_value;

	public Vector3 directToTarget;

	private float Multiply;

	public Vector3 HIt_Base_Position;

	private float Imsi_Heal_DMG;

	private int Imsi_int;

	private int Imsi_int_R;

	private int Imsi_Limit;

	private BigInteger Imsi_BIG_int;

	private Misile misile;

	public void FixedUpdate()
	{
		if (!Portal)
		{
			ThisTransform.Translate(Vector3.left * Changed_Speed * 0.05f * 0.01f * Fight_Master.me.Game_Speed);
		}
	}

	public void Birth(int LV)
	{
		CH_LV = LV;
		Basic_Data.HP = 0;
		Basic_Data.HP = Monster_DB.me.Monster_HP_by_LV(CH_ID, LV, BOSS, Portal);
		HP = Basic_Data.HP;
		HP_bar.value = 1f;
		HP_bar.gameObject.SetActive(false);
		Basic_Data.Move_Speed = Monster_DB.me.monster_DB[CH_ID].Move_Speed;
		Changed_Speed = Basic_Data.Move_Speed;
		if (HP_text != null)
		{
			HP_text.text = Now_Data.INT_to_ABC(HP);
		}
		PS = PlayerState.Idle;
		base.gameObject.layer = 11;
		Z_Update();
		Fight_Master.me.Live_Enemies.Add(this);
		if (CH_ID.Equals(21) || CH_ID.Equals(25))
		{
			Golrem = true;
		}
		else
		{
			Golrem = false;
		}
		if (Portal)
		{
			portal_data.Setting(false);
			StartCoroutine(One_seconc_Invincible());
		}
		if (Text != null)
		{
			Text.transform.parent.gameObject.SetActive(false);
		}
		if (Hit_transform != null)
		{
			HIt_Base_Position = Hit_transform.transform.localPosition;
		}
		Event_Mon = false;
		if (Event_CAP != null)
		{
			Event_CAP.SetActive(false);
		}
		if (BOSS)
		{
			if (Fight_Master.me.EVENT_CANDY_Possible > 0)
			{
				Fight_Master.me.EVENT_CANDY_Possible--;
				Event_CAP.SetActive(true);
				Event_Mon = true;
			}
		}
		else if (!Portal && !Golrem && !GOLREM_HELLSTONE && Fight_Master.me.EVENT_CANDY_Possible > 1)
		{
			Fight_Master.me.EVENT_CANDY_Possible--;
			Event_CAP.SetActive(true);
			Event_Mon = true;
		}
	}

	public IEnumerator One_seconc_Invincible()
	{
		HP_bar.gameObject.SetActive(true);
		HP_text.gameObject.SetActive(true);
		Invincible = true;
		yield return new WaitForSeconds(1f);
		if (!Fight_Master.me.BOSS_FIGHT)
		{
			Invincible = false;
		}
	}

	public void Birth_Dungeon_A_Boss(int LV)
	{
		CH_LV = LV;
		Basic_Data.HP = Monster_DB.me.Monster_HP_by_LV(20, LV, true, false);
		HP = Basic_Data.HP;
		HP_bar.value = 1f;
		HP_bar.value = 1f;
		HP_text.text = Now_Data.INT_to_ABC(HP);
		PS = PlayerState.Idle;
		base.gameObject.layer = 11;
		Z_Update();
		Fight_Master.me.Live_Enemies.Add(this);
		if (Hit_transform != null)
		{
			HIt_Base_Position = Hit_transform.transform.localPosition;
		}
		if (GOLREM_HELLSTONE)
		{
			Origin_speed = Changed_Speed;
			StartCoroutine(GORLEM_Direct_CHANGE());
		}
	}

	public IEnumerator GORLEM_Direct_CHANGE()
	{
		while (base.gameObject.activeSelf)
		{
			if (ThisTransform.localPosition.x <= 0f)
			{
				Changed_Speed = 0f - Origin_speed;
				Hit_transform.transform.localScale = new Vector3(-0.8f, 0.8f, 1f);
			}
			else if (ThisTransform.localPosition.x >= 10f)
			{
				Changed_Speed = Origin_speed;
				Hit_transform.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
			}
			yield return null;
		}
	}

	public void Birth_Dungeon_C_MONSTER(int LV)
	{
		CH_LV = LV;
		Basic_Data.HP = 0;
		Basic_Data.HP = Monster_DB.me.Monster_HP_by_LV(3, LV, false, false);
		HP = Basic_Data.HP;
		HP_bar.value = 1f;
		Basic_Data.Move_Speed = 0f;
		Changed_Speed = 0f;
		if (HP_text != null)
		{
			HP_text.text = Now_Data.INT_to_ABC(HP);
		}
		PS = PlayerState.Idle;
		base.gameObject.layer = 11;
		Z_Update();
		Fight_Master.me.Live_Enemies.Add(this);
		if (Hit_transform != null)
		{
			HIt_Base_Position = Hit_transform.transform.localPosition;
		}
	}

	public void Z_Update()
	{
		ThisTransform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, ThisTransform.position.y * ThisTransform.position.x * 0.001f);
	}

	public void To_BasedPosition()
	{
		Hit_transform.transform.localPosition = HIt_Base_Position;
	}

	public void Hurt(int MAIN_Ele, BigInteger DMG, float K_Value, Vector3 From, Character FromCH, bool critical)
	{
		if (Hit_transform != null)
		{
			Hit_transform.transform.DOShakePosition(0.03f, new Vector3(0.3f, 0f, 0f), 3, 0f).OnComplete(To_BasedPosition);
		}
		attacked_BY = FromCH;
		if (PS == PlayerState.Death || PS == PlayerState.Pool)
		{
			return;
		}
		Real_DMG = DMG;
		Multiply = 100f;
		if (BOSS)
		{
			Multiply += Now_Data.me.DMG_PLUS_PER_BOSS_TARGET;
		}
		else if (Golrem)
		{
			Multiply += Now_Data.me.DMG_PLUS_PER_GOLREM_TARGET;
		}
		else if (Portal)
		{
			Multiply += Now_Data.me.DMG_PLUS_PER_Portal_TARGET;
		}
		else if (DUNGEON_BOSS)
		{
			Multiply += Now_Data.me.DMG_PLUS_PER_DUNGEON_BOSS_TARGET;
			if (boss_data != null)
			{
				switch (boss_data.boss_type)
				{
				case Stage_Type.Dungeon_A:
					Multiply += Now_Data.me.Dungeon_A_boss_ADD_DMG_PER;
					break;
				case Stage_Type.Dungeon_B:
					Multiply += Now_Data.me.Dungeon_B_boss_ADD_DMG_PER;
					break;
				case Stage_Type.Dungeon_D:
					Multiply += Now_Data.me.Dungeon_D_boss_ADD_DMG_PER;
					break;
				}
			}
		}
		else
		{
			Multiply += Now_Data.me.DMG_PLUS_PER_NORMAL_TARGET;
		}
		Multiply += Now_Data.me.ATTACK_PER_Enemy_Count * (float)Fight_Master.me.Live_Enemies.Count;
		Real_DMG = Real_DMG * new BigInteger((int)Multiply) / 100;
		if (Real_DMG < 1)
		{
			Real_DMG = 1;
		}
		if (ThisTransform.position.x > 12f)
		{
			HP_MINUS(Real_DMG / 10000, false);
		}
		else
		{
			HP_MINUS(Real_DMG, critical);
		}
	}

	public void KNOCK_BACK(float K_Value, Vector3 From)
	{
		knockback_value = (K_Value - Basic_Data.KB_Regist * 0.8f) * 5f;
		if (knockback_value < 0f)
		{
			knockback_value = 1f;
		}
		if (knockback_value > 1500f)
		{
			knockback_value = 1500f;
		}
		rigidbody2D.velocity = new Vector2(0f, 0f);
		rigidbody2D.AddForce(new Vector2(knockback_value, 0f));
	}

	public void HP_MINUS(BigInteger value, bool critical)
	{
		if (!Invincible)
		{
			if (!(value > 0))
			{
				return;
			}
			if (GOLREM_HELLSTONE)
			{
				HP -= (BigInteger)1;
			}
			else
			{
				HP -= value;
				OBJ_Pool.me.DMG_panel[OBJ_Pool.DMG_panel_Number].Setting(value, critical);
				OBJ_Pool.me.DMG_panel[OBJ_Pool.DMG_panel_Number].transform.position = new Vector3(ThisTransform.position.x, HP_bar.transform.position.y + 0.3f, 0f);
				OBJ_Pool.DMG_panel_Number++;
				if (OBJ_Pool.DMG_panel_Number >= OBJ_Pool.me.DMG_panel.Length)
				{
					OBJ_Pool.DMG_panel_Number = 0;
				}
			}
			HP_bar.gameObject.SetActive(true);
			HPUpdate();
			if (DUNGEON_BOSS)
			{
				HP_text.gameObject.SetActive(true);
				if (boss_data.boss_type.Equals(Stage_Type.Dungeon_D))
				{
					boss_data.Parts_LV_Check();
				}
			}
		}
		else
		{
			Fight_Master.me.Mop_delay_ADD = 0f;
		}
	}

	public void Healing(BigInteger DMG)
	{
		if (PS != PlayerState.Death)
		{
			if (HP + DMG > Basic_Data.HP)
			{
				DMG = Basic_Data.HP - HP;
			}
			HP += DMG;
			HPUpdate();
		}
	}

	public void HPUpdate()
	{
		if (HP <= 0)
		{
			HP = 0;
		}
		HP_bar.value = Now_Data.Divide_to_Float(HP, Basic_Data.HP);
		if (HP_text.gameObject.activeSelf)
		{
			HP_text.text = Now_Data.INT_to_ABC(HP);
		}
		if (HP <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		if (base.gameObject.activeSelf && !PS.Equals(PlayerState.Death) && !PS.Equals(PlayerState.Pool))
		{
			StopAllCoroutines();
			TRUE_DEATH();
		}
	}

	public void Drop_GOLD(BigInteger value, int icon_count)
	{
		bool flag = false;
		if ((float)Random.Range(0, 100) < Now_Data.me.Gold_Mineral_PER || CH_ID.Equals(25))
		{
			flag = true;
		}
		if (!flag)
		{
			Now_Data.me.GoldChange(value);
			for (int i = 0; i < icon_count; i++)
			{
				MakeCoin(0);
			}
		}
		else
		{
			Now_Data.me.GoldChange(value * 10);
			for (int j = 0; j < icon_count; j++)
			{
				Make_GOLDCoin(0);
			}
		}
	}

	public void Drop_ITEM(BigInteger value, int icon_count)
	{
		MakeCoin(0);
	}

	public void TRUE_DEATH()
	{
		PS = PlayerState.Death;
		HP = 0;
		Changed_Speed = 0f;
		base.gameObject.layer = 9;
		OBJ_Pool.Make_OBJ(OBJ_Pool.me.effect_die, ref OBJ_Pool.effect_die_Number, Hit_Point.position);
		if (Portal)
		{
			SoundManager.me.Portal_Die();
			OBJ_Pool.Make_OBJ(OBJ_Pool.me.BIG_DeadSpill, ref OBJ_Pool.BIG_DeadSpill_Number, ThisTransform.position);
			if (Random.Range(0, 10) < 2)
			{
				Fight_Master.me.Player.Chat(Localization.Get(string.Format("PLAYER_CHAT_{0:000}", Random.Range(0, 10))));
			}
			Fight_Master.me.Next_LV_START();
			Drop_GOLD(Monster_DB.me.Monster_Gold_by_LV(CH_ID, Now_Data.me.LV, false) * (int)(Now_Data.me.GOLD_BONUS_PER_ALL + Now_Data.me.GOLD_BONUS_PER_PORTAL + (float)Main_Player.me.Mineral_Bonus_ING) / 100, 20);
			if (portal_data.Portal_Color != 0)
			{
				Make_Portal_Parts(portal_data.Portal_Color, portal_data.Drop_Parts);
			}
		}
		else if (BOSS)
		{
			SoundManager.me.BOSS_Die();
			OBJ_Pool.Make_OBJ(OBJ_Pool.me.BIG_DeadSpill, ref OBJ_Pool.BIG_DeadSpill_Number, ThisTransform.position);
			Drop_GOLD(Monster_DB.me.Monster_Gold_by_LV(CH_ID, Now_Data.me.LV, true) * (int)(Now_Data.me.GOLD_BONUS_PER_ALL + Now_Data.me.GOLD_BONUS_PER_BOSS + (float)Main_Player.me.Mineral_Bonus_ING) / 100, 20);
			if (Now_Data.me.Boss_Save_Gold > 0f)
			{
				UI_Master.me.Good_MSG(string.Format("{0}{1}{2}", Localization.Get("BOSS_DROP_GOLD_A"), Now_Data.me.Boss_Save_Gold, Localization.Get("BOSS_DROP_GOLD_B")));
				Now_Data.me.GoldChange(Now_Data.me.GOLD / 100 * (int)Now_Data.me.Boss_Save_Gold);
			}
			if (Now_Data.me.Boss_P_stone > 0f && (float)Random.Range(0, 100) < Now_Data.me.Boss_P_stone)
			{
				Make_P_Stone(0);
				Make_P_Stone(0);
				Now_Data.me.P_STONE_Change(2);
			}
			Fight_Master.me.Next_LV_START();
			Now_Data.me.NOW_BOSSKILL += (BigInteger)1;
			Now_Data.me.ALL_BOSSKILL += (BigInteger)1;
			Security.SetString("NOW_BOSSKILL", Now_Data.me.NOW_BOSSKILL.ToString());
			Security.SetString("ALL_BOSSKILL", Now_Data.me.ALL_BOSSKILL.ToString());
			Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_BOSSKILL);
			if (Now_Data.me.ALL_BOSSKILL < 6)
			{
				UM_GameService.me.ReportArchievement(string.Format("arch_{0}", Now_Data.me.ALL_BOSSKILL));
			}
			for (int i = 0; i < Now_Data.me.sub_quest_count; i++)
			{
				UI_Master.me.sub_quest_ui[i].Txt_Update(Quest_Goal_Type.NOW_BOSSKILL);
			}
		}
		else
		{
			if (Golrem)
			{
				Drop_GOLD(Monster_DB.me.Monster_Gold_by_LV(CH_ID, Now_Data.me.LV, false) * (int)(Now_Data.me.GOLD_BONUS_PER_ALL + Now_Data.me.GOLD_BONUS_PER_GOLREM + (float)Main_Player.me.Mineral_Bonus_ING) / 100, 40);
			}
			SoundManager.me.Die();
			if (!DUNGEON_MONSTER)
			{
				OBJ_Pool.Make_OBJ(OBJ_Pool.me.DeadSpill, ref OBJ_Pool.DeadSpill_Number, ThisTransform.position);
				Drop_GOLD(Monster_DB.me.Monster_Gold_by_LV(CH_ID, Now_Data.me.LV, false) * (int)(Now_Data.me.GOLD_BONUS_PER_ALL + Now_Data.me.GOLD_BONUS_PER_NORMAL + (float)Main_Player.me.Mineral_Bonus_ING) / 100, 1);
			}
			if (DUNGEON_BOSS)
			{
				SoundManager.me.BOSS_Die();
				OBJ_Pool.Make_OBJ(OBJ_Pool.me.BIG_DeadSpill, ref OBJ_Pool.BIG_DeadSpill_Number, ThisTransform.position);
			}
		}
		Remove();
		Fight_Master.me.Live_Enemies.Remove(this);
		Now_Data.me.NOW_KILL += (BigInteger)1;
		Now_Data.me.ALL_KILL += (BigInteger)1;
		if (Now_Data.me.ALL_KILL % 10 == 0)
		{
			Security.SetString("NOW_KILL", Now_Data.me.NOW_KILL.ToString());
			Security.SetString("ALL_KILL", Now_Data.me.ALL_KILL.ToString());
			Now_Data.me.Check_Possible(Quest_Goal_Type.ALL_KILL);
		}
		for (int j = 0; j < Now_Data.me.sub_quest_count; j++)
		{
			UI_Master.me.sub_quest_ui[j].Txt_Update(Quest_Goal_Type.NOW_KILL);
		}
		if (DUNGEON_BOSS)
		{
			boss_data.Kill_CHECK();
		}
		if (DUNGEON_MONSTER)
		{
			if (GOLREM_HELLSTONE)
			{
				Fight_Master.me.Dungeon_E_Hell_stone_BOX_Count++;
				Now_Data.me.BOX_Count[5]++;
				Security.SetInt(string.Format("BOX_Count_{0:000}", 5), Now_Data.me.BOX_Count[5]);
				Make_Hellstone_Box();
			}
			else
			{
				Dungeon_Monster_Check();
			}
			OBJ_Pool.Make_OBJ(OBJ_Pool.me.DUNGEON_DeadSpill, ref OBJ_Pool.DeadSpill_Number, ThisTransform.position);
		}
		if (Event_Mon)
		{
			SoundManager.me.Artifact_Upgrade();
			Now_Data.me.EVENT_Candy_Change(1);
			MakeEVENT_CANDY();
		}
	}

	public void Dungeon_Monster_Check()
	{
		Fight_Master.me.Dungeon_C_monster_Kill_Count--;
		UI_Master.me.Stage_label.text = string.Format("{0}/100", Fight_Master.me.Dungeon_C_monster_Kill_Count);
		if (Fight_Master.me.Dungeon_C_monster_Kill_Count <= 0)
		{
			Debug.Log("던전보스잡음.");
			Fight_Master.me.GS = GameState.End;
			UI_Master.me.Dungeon_Clear_Popup.Setting(true);
		}
	}

	public IEnumerator FadeOut(float Delay)
	{
		yield return new WaitForSeconds(Delay);
		if (sprite_Renderer != null)
		{
			float a = 1f;
			for (int i = 0; i < 10; i++)
			{
				a -= 0.1f;
				sprite_Renderer.color = new Color(1f, 1f, 1f, a);
				yield return new WaitForSeconds(0.05f);
			}
		}
		Remove();
	}

	public void Remove()
	{
		PS = PlayerState.Pool;
		if (sprite_Renderer != null)
		{
			sprite_Renderer.color = Color.white;
		}
		HP = 0;
		base.gameObject.layer = 9;
		base.gameObject.SetActive(false);
	}

	public void MakeCoin(BigInteger value)
	{
		if (OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GET_Possible)
		{
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Get();
		}
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Item_Number = 0;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GGOK_Front = false;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].sprite_img.sprite = Sprite_DB.me.Mineral;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.position = Hit_Point.position;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(true);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.localScale = Vector3.zero;
		OBJ_Pool.Coin_Number++;
		if (OBJ_Pool.Coin_Number >= OBJ_Pool.me.Coin.Length)
		{
			OBJ_Pool.Coin_Number = 0;
		}
	}

	public void Make_GOLDCoin(BigInteger value)
	{
		if (OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GET_Possible)
		{
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Get();
		}
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Item_Number = 0;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GGOK_Front = false;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].sprite_img.sprite = Sprite_DB.me.Gold_Mineral;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.position = Hit_Point.position;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(true);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.localScale = Vector3.zero;
		OBJ_Pool.Coin_Number++;
		if (OBJ_Pool.Coin_Number >= OBJ_Pool.me.Coin.Length)
		{
			OBJ_Pool.Coin_Number = 0;
		}
	}

	public void Make_P_Stone(BigInteger value)
	{
		if (OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GET_Possible)
		{
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Get();
		}
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GGOK_Front = false;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].sprite_img.sprite = Sprite_DB.me.P_stone;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.position = Hit_Point.position;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(true);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.localScale = Vector3.zero;
		OBJ_Pool.Coin_Number++;
		if (OBJ_Pool.Coin_Number >= OBJ_Pool.me.Coin.Length)
		{
			OBJ_Pool.Coin_Number = 0;
		}
	}

	public void Make_Portal_Parts(int Color, int Drop_Parts)
	{
		UI_Master.me.PAUSE_Alram.SetActive(true);
		UI_Master.me.PORTAL_PARTS_Alram.SetActive(true);
		Now_Data.me.portal_Parts[Color].PORTAL_Parts_count[Drop_Parts]++;
		if ((float)Random.Range(0, 100) < Now_Data.me.Portal_Parts_Drop_Per)
		{
			Now_Data.me.portal_Parts[Color].PORTAL_Parts_count[Drop_Parts]++;
		}
		Security.SetInt(string.Format("Portal_{0:000}_Parts_{1:000}_Count", Color, Drop_Parts), Now_Data.me.portal_Parts[Color].PORTAL_Parts_count[Drop_Parts]);
		if (OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GET_Possible)
		{
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Get();
		}
		switch (Color)
		{
		case 1:
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].sprite_img.sprite = Sprite_DB.me.Portal_Parts_Red[Drop_Parts];
			break;
		case 2:
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].sprite_img.sprite = Sprite_DB.me.Portal_Parts_Green[Drop_Parts];
			break;
		case 3:
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].sprite_img.sprite = Sprite_DB.me.Portal_Parts_Blue[Drop_Parts];
			break;
		case 4:
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].sprite_img.sprite = Sprite_DB.me.Portal_Parts_Black[Drop_Parts];
			break;
		}
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Item_Number = 2;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GGOK_Front = true;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.position = Hit_Point.position;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(true);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.localScale = Vector3.zero;
		OBJ_Pool.Coin_Number++;
		if (OBJ_Pool.Coin_Number >= OBJ_Pool.me.Coin.Length)
		{
			OBJ_Pool.Coin_Number = 0;
		}
	}

	public void Make_Hellstone_Box()
	{
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].sprite_img.sprite = Sprite_DB.me.P_stone_BOX;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Item_Number = 3;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GGOK_Front = true;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.position = Hit_Point.position;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(true);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.localScale = Vector3.zero;
		OBJ_Pool.Coin_Number++;
		if (OBJ_Pool.Coin_Number >= OBJ_Pool.me.Coin.Length)
		{
			OBJ_Pool.Coin_Number = 0;
		}
	}

	public void MakeEVENT_CANDY()
	{
		if (OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GET_Possible)
		{
			OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Get();
		}
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].Item_Number = 0;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].GGOK_Front = false;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].sprite_img.sprite = Sprite_DB.me.Event_Candy;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.position = Hit_Point.position;
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].gameObject.SetActive(true);
		OBJ_Pool.me.Coin[OBJ_Pool.Coin_Number].ThisTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		OBJ_Pool.Coin_Number++;
		if (OBJ_Pool.Coin_Number >= OBJ_Pool.me.Coin.Length)
		{
			OBJ_Pool.Coin_Number = 0;
		}
	}

	public void Make_Item()
	{
		if (OBJ_Pool.me.Items[OBJ_Pool.Items_Number].GET_Possible)
		{
			OBJ_Pool.me.Items[OBJ_Pool.Items_Number].Get();
		}
		Imsi_int_R = Random.Range(0, 10000);
		if (Imsi_Limit > 4)
		{
			Imsi_Limit = 4;
		}
		if (Imsi_int_R < 100)
		{
			Imsi_int = 12;
		}
		else if (Imsi_int_R < 500)
		{
			Imsi_int = Basic_Data.Item_Number[Random.Range(8, 8 + Imsi_Limit)];
		}
		else if (Imsi_int_R < 3000)
		{
			Imsi_int = Basic_Data.Item_Number[Random.Range(4, 4 + Imsi_Limit)];
		}
		else
		{
			Imsi_int = Basic_Data.Item_Number[Random.Range(0, Imsi_Limit)];
		}
		if (Imsi_int.Equals(0))
		{
			int num = Random.Range(0, 3);
			if (num != 1)
			{
				Imsi_int = Random.Range(25, 27);
			}
			else
			{
				Imsi_int = Random.Range(41, 45);
			}
		}
		OBJ_Pool.me.Items[OBJ_Pool.Items_Number].ThisTransform.position = ThisTransform.position;
		OBJ_Pool.me.Items[OBJ_Pool.Items_Number].gameObject.SetActive(false);
		OBJ_Pool.me.Items[OBJ_Pool.Items_Number].gameObject.SetActive(true);
		OBJ_Pool.Items_Number++;
		if (OBJ_Pool.Items_Number >= OBJ_Pool.me.Items.Length)
		{
			OBJ_Pool.Items_Number = 0;
		}
	}

	public void Chat(string script)
	{
		if (Text != null)
		{
			Text.transform.parent.gameObject.SetActive(false);
			Text.text = script;
			Text.transform.parent.gameObject.SetActive(true);
		}
	}

	public void OnTriggerEnter2D(Collider2D Target)
	{
		if (PS == PlayerState.Death)
		{
			return;
		}
		if (Target.CompareTag("Misile"))
		{
			misile = Target.gameObject.GetComponent<Misile>();
			if (!(misile != null))
			{
				return;
			}
			SoundManager.me.Hit();
			OBJ_Pool.Make_OBJ(OBJ_Pool.me.effect_Hitted, ref OBJ_Pool.effect_Hitted_Number, (misile.ThisTransform.position + Hit_Point.position) / 2f);
			attacked_BY = misile.owner;
			if (!misile.WORK_Possible)
			{
				return;
			}
			if (GOLREM_HELLSTONE)
			{
				Hurt(-1, 1, misile.KnockBack, misile.ThisTransform.position, misile.owner, misile.Critical);
				if (Random.Range(0, 5).Equals(0))
				{
					Fight_Master.me.Dungeon_E_Hell_stone_Count++;
					Make_P_Stone(0);
					Now_Data.me.P_STONE_Change(1);
				}
				misile.Exit();
				return;
			}
			Hurt(misile.Main_Ele, misile.DMG, misile.KnockBack, misile.ThisTransform.position, misile.owner, misile.Critical);
			if (Golrem)
			{
				Imsi_BIG_int = Basic_Data.HP / 1000;
				if (Imsi_BIG_int < 1)
				{
					Imsi_BIG_int = 1;
				}
				Drop_GOLD(Imsi_BIG_int * (int)(Now_Data.me.GOLD_BONUS_PER_ALL + Now_Data.me.GOLD_BONUS_PER_GOLREM + (float)Main_Player.me.Mineral_Bonus_ING) / 100, 1);
			}
			if (misile.After_Bomb)
			{
				Fight_Master.me.CameraShake_Long();
				SoundManager.me.Hit();
				return;
			}
			if (misile.owner != null && misile.owner.CH_TYPE.Equals(Ch_Type.Player))
			{
				if (Main_Player.me.Add_Mineral_ING)
				{
					Drop_GOLD(Monster_DB.me.Monster_Gold_by_LV(1, Now_Data.me.LV, false) * (int)(Now_Data.me.GOLD_BONUS_PER_ALL + Now_Data.me.GOLD_BONUS_PER_NORMAL + (float)Main_Player.me.Mineral_Bonus_ING + (float)Misile_DB.me.skill_DB[1].Skill_LV[Now_Data.me.Active_Skill_LV[1]].Value) * (100 + Now_Data.me.SKILL_Value_Plus[1]) / 10000, 1);
				}
				if (!BOSS && !Portal && !DUNGEON_BOSS && Now_Data.me.JUST_KILL_20 > 0f && HP * 5 < Basic_Data.HP && (float)Random.Range(0, 100) < Now_Data.me.JUST_KILL_20)
				{
					Die();
					return;
				}
			}
			if (!misile.Piercing)
			{
				if (misile.Oneshot)
				{
					misile.Exit();
				}
				else
				{
					misile.Boom();
				}
			}
			else if (Portal)
			{
				if (misile.Oneshot)
				{
					misile.Exit();
				}
				else
				{
					misile.Boom();
				}
			}
		}
		else if (Target.CompareTag("Player"))
		{
			if (Fight_Master.me.stage_type.Equals(Stage_Type.Normal))
			{
				Fight_Master.me.Stage_Fail();
			}
			else if (Fight_Master.me.SPECIAL_BOSS_B || Fight_Master.me.SPECIAL_BOSS_D)
			{
				Fight_Master.me.Stage_Fail();
			}
			else
			{
				UI_Master.me.Dungeon_Clear_Popup.Setting(false);
			}
		}
	}
}
