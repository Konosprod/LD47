using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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
    public List<AudioSource> sfxSources;
    public AudioMixerGroup bgmgroup;
    public AudioMixerGroup sfxgroup;

    public Slider sliderSfx;
    public Slider sliderBgm;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        AudioMixer mixer = Resources.Load("Master") as AudioMixer;

        bgmgroup = mixer.FindMatchingGroups("BGM")[0];
        sfxgroup = mixer.FindMatchingGroups("SFX")[0];
        loopSource = gameObject.AddComponent<AudioSource>();
        introSource = gameObject.AddComponent<AudioSource>();
        sfxSources = new List<AudioSource>();


        //0 : Gatling
        //1 : Mortar
        //2 : Mortar Shell
        //3 : UI
        for (int i = 0; i < 4; i++)
        {
            AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.outputAudioMixerGroup = sfxgroup;
            sfxSources.Add(sfxSource);
        }

        introSource.outputAudioMixerGroup = bgmgroup;
        loopSource.outputAudioMixerGroup = bgmgroup;
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

    public void PlayGatlingSound(AudioClip clip)
    {
        if (!sfxSources[0].isPlaying)
            sfxSources[0].PlayOneShot(clip, .8f);
    }

    public void PlayMortar(AudioClip clip)
    {
        if (!sfxSources[1].isPlaying)
            sfxSources[1].PlayOneShot(clip, .8f);
    }

    public void PlayExplosion(AudioClip clip)
    {
        if (!sfxSources[2].isPlaying)
            sfxSources[2].PlayOneShot(clip, .8f);
    }


    public void PlayLevelMusic(AudioClip introduced, AudioClip loop)
    {

        introSource.clip = introduced;
        loopSource.clip = loop;
        loopSource.loop = true;

        double duration = (double)introduced.samples / introduced.frequency;
        double startTime = AudioSettings.dspTime + 0.2;

        introSource.PlayScheduled(startTime);
        loopSource.PlayScheduled(startTime + duration);
    }

    public void PlaySfx(AudioClip sfxClip, int channel, bool stop=false)
    {
        if(stop)
        {
            for(int i = 0; i < sfxSources.Count; i++)
            {
                sfxSources[i].Stop();
            }
        }
        sfxSources[channel].PlayOneShot(sfxClip);
    }


    public void PlaySfx(AudioClip sfxClip, int channel, float volume)
    {
        sfxSources[channel].PlayOneShot(sfxClip, volume);
    }

    public void SetVolume()
    {
        SetMusicVolume(sliderBgm.value);
        SetSfxVolume(sliderSfx.value);
    }

    public void SetMusicVolume(float volume)
    {
        introSource.volume = volume;
        loopSource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        for (int i = 0; i < sfxSources.Count; i++)
        {
            sfxSources[i].volume = volume;
        }
    }
}