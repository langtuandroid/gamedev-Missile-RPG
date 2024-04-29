using UnityEngine;

public class FXQ_SoundController : MonoBehaviour
{
	private static FXQ_SoundController instance;

	public int m_MaxAudioSource = 3;

	public AudioClip m_ButtonBack;

	public AudioClip m_ButtonClick;

	public AudioClip m_ButtonPress;

	public float m_SoundVolume = 1f;

	public static FXQ_SoundController Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Object.FindObjectOfType<FXQ_SoundController>();
				Object.DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			Object.DontDestroyOnLoad(this);
		}
		else if (this != instance)
		{
			InitAudioListener();
			Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		InitAudioListener();
	}

	private void Update()
	{
	}

	private void InitAudioListener()
	{
		AudioListener[] array = Object.FindObjectsOfType<AudioListener>();
		AudioListener[] array2 = array;
		foreach (AudioListener audioListener in array2)
		{
			if (audioListener.gameObject.GetComponent<FXQ_SoundController>() == null)
			{
				Object.Destroy(audioListener);
			}
		}
		AudioListener component = base.gameObject.GetComponent<AudioListener>();
		if (component == null)
		{
			component = base.gameObject.AddComponent<AudioListener>();
		}
	}

	private void PlayMusic(AudioClip pAudioClip)
	{
		if (pAudioClip == null)
		{
			return;
		}
		AudioListener audioListener = Object.FindObjectOfType<AudioListener>();
		if (!(audioListener != null))
		{
			return;
		}
		bool flag = false;
		AudioSource[] components = audioListener.gameObject.GetComponents<AudioSource>();
		if (components.Length > 0)
		{
			for (int i = 0; i < components.Length; i++)
			{
				if (!components[i].isPlaying)
				{
					components[i].loop = true;
					components[i].clip = pAudioClip;
					components[i].ignoreListenerVolume = true;
					components[i].playOnAwake = false;
					components[i].Play();
					break;
				}
			}
		}
		if (!flag && components.Length < 16)
		{
			AudioSource audioSource = audioListener.gameObject.AddComponent<AudioSource>();
			audioSource.rolloffMode = AudioRolloffMode.Linear;
			audioSource.loop = true;
			audioSource.clip = pAudioClip;
			audioSource.ignoreListenerVolume = true;
			audioSource.playOnAwake = false;
			audioSource.Play();
		}
	}

	private void PlaySoundOneShot(AudioClip pAudioClip)
	{
		if (pAudioClip == null || Time.timeSinceLevelLoad < 1.5f)
		{
			return;
		}
		AudioListener audioListener = Object.FindObjectOfType<AudioListener>();
		if (!(audioListener != null))
		{
			return;
		}
		bool flag = false;
		AudioSource[] components = audioListener.gameObject.GetComponents<AudioSource>();
		if (components.Length > 0)
		{
			for (int i = 0; i < components.Length; i++)
			{
				if (!components[i].isPlaying)
				{
					components[i].PlayOneShot(pAudioClip);
					break;
				}
			}
		}
		if (!flag && components.Length < 16)
		{
			AudioSource audioSource = audioListener.gameObject.AddComponent<AudioSource>();
			audioSource.rolloffMode = AudioRolloffMode.Linear;
			audioSource.playOnAwake = false;
			audioSource.PlayOneShot(pAudioClip);
		}
	}

	public void SetSoundVolume(float volume)
	{
		m_SoundVolume = volume;
		AudioListener.volume = volume;
	}

	public void Play_SoundBack()
	{
		PlaySoundOneShot(m_ButtonBack);
	}

	public void Play_SoundClick()
	{
		PlaySoundOneShot(m_ButtonClick);
	}

	public void Play_SoundPress()
	{
		PlaySoundOneShot(m_ButtonPress);
	}
}
