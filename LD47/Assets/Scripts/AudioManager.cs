using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();

                if (_instance == null)
                {
                    _instance = new GameObject("Spwaned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }

    public AudioSource introSource;
    public AudioSource loopSource;
    public AudioSource sfxSource;
    public AudioMixerGroup bgmgroup;
    public AudioMixerGroup sfxgroup;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (loopSource == null)
        {
            AudioMixer mixer = Resources.Load("Master") as AudioMixer;

            bgmgroup = mixer.FindMatchingGroups("BGM")[0];
            sfxgroup = mixer.FindMatchingGroups("SFX")[0];
            loopSource = gameObject.AddComponent<AudioSource>();
            introSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            introSource.outputAudioMixerGroup = bgmgroup;
            loopSource.outputAudioMixerGroup = bgmgroup;
            sfxSource.outputAudioMixerGroup = sfxgroup;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMusic(AudioClip musicClip)
    {
        introSource.clip = musicClip;
        introSource.Play();
        introSource.volume = 1.0f;
    }

    public void PlayLevelMusic(AudioClip introduced, AudioClip loop)
    {

        introSource.clip = introduced;
        loopSource.clip = loop;

        double duration = (double)introduced.samples / introduced.frequency;
        double startTime = AudioSettings.dspTime + 0.2;

        introSource.PlayScheduled(startTime);
        loopSource.PlayScheduled(startTime + duration);
    }

    public void PlaySfx(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }

    public void PlaySfx(AudioClip sfxClip, float volume)
    {
        sfxSource.PlayOneShot(sfxClip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        introSource.volume = volume;
        loopSource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}