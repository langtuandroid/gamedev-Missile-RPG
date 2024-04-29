using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager me;

	public AudioSource[] CH;

	public AudioSource[] Skill_CH;

	public AudioSource BGM_AUDIO;

	public int CH_NUMBER;

	public int Skill_CH_NUMBER;

	public AudioClip[] BGMs;

	public float SOUND_Volume = 1f;

	public float BGM_Volume = 1f;

	public AudioClip hit_sound;

	public AudioClip[] Attack_CH_sound;

	public AudioClip Player_Shot_sound;

	public AudioClip Boom_sound;

	public AudioClip Booster_A_sound;

	public AudioClip Booster_B_sound;

	public AudioClip Punch_sound;

	public AudioClip Portal_Die_sound;

	public AudioClip SteamPACK_sound;

	public AudioClip cancel_sound;

	public AudioClip congretu_sound;

	public AudioClip warning_sound;

	public AudioClip gameover_sound;

	public AudioClip Fail_sound;

	public AudioClip die_sound;

	public AudioClip[] boss_die_sound;

	public AudioClip[] Diablo_Hurt_sound;

	public AudioClip[] Diablo_Skill_sound;

	public AudioClip[] Tongue_Skill_sound;

	public AudioClip[] Fairy_sound;

	public AudioClip FIRST_START_sound;

	public AudioClip Click_sound;

	public AudioClip Unit_Upgrade_sound;

	public AudioClip Unit_Upgrade_Unlock_Ability_sound;

	public AudioClip Missile_Upgrade_sound;

	public AudioClip Artifact_Upgrade_sound;

	public AudioClip Skill_Upgrade_sound;

	public AudioClip[] Skill_Use_Sound;

	public AudioClip nEW_MISSILE_sound;

	public AudioClip FIREShot_sound;

	private void Awake()
	{
		me = this;
		SOUND_Volume = Security.GetFloat("SOUND_Volume", 1f);
		BGM_Volume = Security.GetFloat("BGM_Volume", 1f);
		Setting();
	}

	public void SOUND_PLAY()
	{
		CH[CH_NUMBER].Play();
		CH_NUMBER++;
		if (CH_NUMBER >= CH.Length)
		{
			CH_NUMBER = 0;
		}
	}

	public void Skill_SOUND_PLAY()
	{
		Skill_CH[Skill_CH_NUMBER].Play();
		Skill_CH_NUMBER++;
		if (Skill_CH_NUMBER >= Skill_CH.Length)
		{
			Skill_CH_NUMBER = 0;
		}
	}

	public void SOUND_Volume_Change()
	{
		if (SOUND_Volume.Equals(1f))
		{
			SOUND_Volume = 0f;
		}
		else
		{
			SOUND_Volume = 1f;
		}
		Setting();
	}

	public void Setting()
	{
		Security.SetFloat("SOUND_Volume", SOUND_Volume);
		Security.SetFloat("BGM_Volume", BGM_Volume);
		for (int i = 0; i < CH.Length; i++)
		{
			CH[i].volume = SOUND_Volume;
		}
		for (int j = 0; j < Skill_CH.Length; j++)
		{
			Skill_CH[j].volume = SOUND_Volume;
		}
		BGM_AUDIO.volume = BGM_Volume;
	}

	public void BGM_Volume_Change()
	{
		if (BGM_Volume.Equals(1f))
		{
			BGM_Volume = 0f;
		}
		else
		{
			BGM_Volume = 1f;
		}
		Setting();
	}

	public void BGM_Play(int ID)
	{
		BGM_AUDIO.clip = BGMs[ID];
		BGM_AUDIO.Play();
	}

	public void Hit()
	{
		CH[CH_NUMBER].clip = hit_sound;
		SOUND_PLAY();
	}

	public void Attack_CH(int ID)
	{
		if (Attack_CH_sound[ID] != null)
		{
			CH[CH_NUMBER].clip = Attack_CH_sound[ID];
			SOUND_PLAY();
		}
	}

	public void Player_Shot()
	{
		CH[CH_NUMBER].clip = Player_Shot_sound;
		SOUND_PLAY();
	}

	public void Boom()
	{
		CH[CH_NUMBER].clip = Boom_sound;
		SOUND_PLAY();
	}

	public void Booster_A()
	{
		CH[CH_NUMBER].clip = Booster_A_sound;
		SOUND_PLAY();
	}

	public void Booster_B()
	{
		CH[CH_NUMBER].clip = Booster_B_sound;
		SOUND_PLAY();
	}

	public void Punch()
	{
		CH[CH_NUMBER].clip = Punch_sound;
		SOUND_PLAY();
	}

	public void Portal_Die()
	{
		Skill_CH[Skill_CH_NUMBER].clip = Portal_Die_sound;
		Skill_SOUND_PLAY();
	}

	public void SteamPACK()
	{
		Skill_CH[Skill_CH_NUMBER].clip = SteamPACK_sound;
		Skill_SOUND_PLAY();
	}

	public void Cancel()
	{
		CH[CH_NUMBER].clip = cancel_sound;
		SOUND_PLAY();
	}

	public void Congretu()
	{
		Skill_CH[Skill_CH_NUMBER].clip = congretu_sound;
		Skill_SOUND_PLAY();
	}

	public void Warning()
	{
		CH[CH_NUMBER].clip = warning_sound;
		SOUND_PLAY();
	}

	public void GameOver()
	{
		Skill_CH[Skill_CH_NUMBER].clip = gameover_sound;
		Skill_SOUND_PLAY();
	}

	public void Fail()
	{
		Skill_CH[Skill_CH_NUMBER].clip = Fail_sound;
		Skill_SOUND_PLAY();
	}

	public void Die()
	{
		CH[CH_NUMBER].clip = die_sound;
		SOUND_PLAY();
	}

	public void BOSS_Die()
	{
		Skill_CH[Skill_CH_NUMBER].clip = boss_die_sound[Random.Range(0, boss_die_sound.Length)];
		Skill_SOUND_PLAY();
	}

	public void Diablo_Hurt(int ID)
	{
		Skill_CH[Skill_CH_NUMBER].clip = Diablo_Hurt_sound[ID];
		Skill_SOUND_PLAY();
	}

	public void Diablo_Skill(int ID)
	{
		Skill_CH[Skill_CH_NUMBER].clip = Diablo_Skill_sound[ID];
		Skill_SOUND_PLAY();
	}

	public void Tongue_Skill()
	{
		Skill_CH[Skill_CH_NUMBER].clip = Tongue_Skill_sound[Random.Range(0, Tongue_Skill_sound.Length)];
		Skill_SOUND_PLAY();
	}

	public void Fairy_Touch()
	{
		CH[CH_NUMBER].clip = Fairy_sound[Random.Range(0, Fairy_sound.Length)];
		SOUND_PLAY();
	}

	public void FIRST_START()
	{
		CH[CH_NUMBER].clip = FIRST_START_sound;
		SOUND_PLAY();
	}

	public void Click()
	{
		CH[CH_NUMBER].clip = Click_sound;
		SOUND_PLAY();
	}

	public void Unit_Upgrade()
	{
		CH[CH_NUMBER].clip = Unit_Upgrade_sound;
		SOUND_PLAY();
	}

	public void Unit_Upgrade_Unlock_Ability()
	{
		CH[CH_NUMBER].clip = Unit_Upgrade_Unlock_Ability_sound;
		SOUND_PLAY();
	}

	public void Missile_Upgrade()
	{
		CH[CH_NUMBER].clip = Missile_Upgrade_sound;
		SOUND_PLAY();
	}

	public void Artifact_Upgrade()
	{
		CH[CH_NUMBER].clip = Artifact_Upgrade_sound;
		SOUND_PLAY();
	}

	public void Skill_Upgrade()
	{
		CH[CH_NUMBER].clip = Skill_Upgrade_sound;
		SOUND_PLAY();
	}

	public void Skill_Use(int ID)
	{
		Skill_CH[Skill_CH_NUMBER].clip = Skill_Use_Sound[ID];
		Skill_SOUND_PLAY();
	}

	public void NEW_MISSILE()
	{
		CH[CH_NUMBER].clip = nEW_MISSILE_sound;
		SOUND_PLAY();
	}

	public void FIREShot()
	{
		CH[CH_NUMBER].clip = FIREShot_sound;
		SOUND_PLAY();
	}
}
