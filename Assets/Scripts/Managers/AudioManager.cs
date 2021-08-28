using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public enum SoundEffects
{
   hit,
   loss,
   waveEnd,
   waveStart,
   win,
   monsterSpawn
};
public class AudioManager : MonoBehaviour
{
    
    public static AudioManager main;

	[SerializeField] private AudioMixer musicMixer = null;
	[SerializeField] private AudioMixer sfxMixer = null;
	
	public float musicVolume;
	public float sfxVolume;
	
	[Space] [Header("Music")] 
	public AudioClip MainMenuMusic;
	public AudioClip MainGameMusic;
	[SerializeField] private bool isInMainMenuScene;

	[Space] [Header("Sounds")] 
	[SerializeField] private AudioClip hit;
	[SerializeField] private AudioClip loss;
	[SerializeField] private AudioClip waveStart;
	[SerializeField] private AudioClip waveEnd;


	[Header("Audio Sources")]
	public AudioSource musicSource;
	public AudioSource sfxSource;

	#region Properties

	public float MusicVolume
	{
		get => musicVolume;
		set => musicVolume = value;
	}

	public float SfxVolume
	{
		get => sfxVolume;
		set => sfxVolume = value;
	}

	public AudioMixer MusicMixer
	{
		get => musicMixer;
	}

	public AudioMixer SfxMixer
	{
		get => sfxMixer;
	}

	#endregion

	private void Awake()
	{
		main = this;
		getMixerVolumes();
	}

	private void Start()
	{
		StartCoroutine(musicLoop(isInMainMenuScene));
	}

	private void getMixerVolumes()
	{
		musicMixer.GetFloat("MusicVolume", out musicVolume);
		sfxMixer.GetFloat("SfxVolume", out sfxVolume);
	}

	private void Update()
	{
		
	}

	public void PlaySoundEffect(SoundEffects effect)
	{
		
		switch (effect)
		{
			case SoundEffects.hit:
				sfxSource.PlayOneShot(hit);
				break;
			case SoundEffects.loss:
				sfxSource.PlayOneShot(loss);
				break;
			case SoundEffects.waveStart:
				sfxSource.PlayOneShot(waveStart);
				break;
			case SoundEffects.win:
				sfxSource.PlayOneShot(waveEnd);
				break;
			case SoundEffects.monsterSpawn:
				sfxSource.PlayOneShot(waveEnd);
				break;
			case SoundEffects.waveEnd:
				sfxSource.PlayOneShot(waveEnd);
				break;

			default: break;
		}
	}

	IEnumerator musicLoop(bool mainMenu)
	{
		while (gameObject.activeSelf)
		{
			if (!musicSource.isPlaying)
			{
				if (!mainMenu)
				{
					musicSource.clip = MainGameMusic;
				}
				else
				{
					musicSource.clip = MainMenuMusic;
				}
				musicSource.Play();
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
