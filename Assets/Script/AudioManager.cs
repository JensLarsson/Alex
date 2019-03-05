using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    static public AudioManager instance = null;

    public enum TargetAudio { A = 0, B, C, D };
    [HideInInspector] public TargetAudio targetAudio = TargetAudio.A;
    void switchAudioEnumTarget()        //Växlar mellan  AudioSource enumIndex målen
    {
        switch (targetAudio)
        {
            case TargetAudio.A:
                targetAudio = TargetAudio.B;
                break;
            case TargetAudio.B:
                targetAudio = TargetAudio.C;
                break;
            case TargetAudio.C:
                targetAudio = TargetAudio.D;
                break;
            case TargetAudio.D:
                targetAudio = TargetAudio.A;
                break;
        }
    }


    public AudioSource[] musicSource = new AudioSource[2];
    public AudioSource sfxSource;
    public AudioSource sfxSourcePitch;
    [Tooltip("volume change per .01sec")]
    [Range(0.001f, 1.0f)]
    public float fadeInIncrememnt = 0.1f;
    float musicVolume = 1.0f;
    float sfxVolume = 1.0f;

    void Awake()
    {
        //Sätter Singleton instance
        if (instance == null)
        {
            instance = this;
            foreach (AudioSource source in musicSource)
            {
                source.volume = 0.0f;
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void setSFXVolume()
    {
        sfxSource.volume = sfxVolume;
        sfxSourcePitch.volume = sfxVolume;
    }



    //Replaces current song (if one is playing) with new one and fades between the two
    public void changeSong(AudioClip clip)
    {
        fadeOut(targetAudio);
        switchAudioEnumTarget();
        fadeIn(targetAudio);

        musicSource[(int)targetAudio].loop = true;

        if (musicSource[(int)targetAudio].clip != clip)
        {
            musicSource[(int)targetAudio].clip = clip;
            musicSource[(int)targetAudio].Play();
        }
    }
    //
    //Uses secondary AudioSources for soundeffects 
    //
    public void playSFXClip(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void playSFXClip(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }
    public void playSFXClip(AudioClip clip, bool stopOtherSFX)
    {
        if (stopOtherSFX) sfxSource.Stop();
        sfxSource.PlayOneShot(clip);
    }
    public void playSFXClip(AudioClip clip, float volume, bool stopOtherSFX)
    {
        if (stopOtherSFX) sfxSource.Stop();
        sfxSource.PlayOneShot(clip, volume);
    }

    //
    //Ändrar pitch på en egen AudioSource innan clip spelas
    //
    public void playSFXRandomPitch(AudioClip clip, float randomRange)
    {
        sfxSourcePitch.pitch = Random.Range(1 - randomRange, 1 + randomRange);
        sfxSourcePitch.PlayOneShot(clip);
    }
    public void playSFXRandomPitch(AudioClip clip, float randomRange, bool stopOtherSFX, bool stopOtherPitchedSFX)
    {
        if (stopOtherSFX) sfxSource.Stop();
        if (stopOtherSFX) sfxSourcePitch.Stop();
        sfxSourcePitch.pitch = Random.Range(1 - randomRange, 1 + randomRange);
        sfxSourcePitch.PlayOneShot(clip);
    }



    //
    //  Crossfading mellan Audiosources.
    //
    public void fadeOut(TargetAudio source)
    {
        if (source == (TargetAudio)(-1))
        {
            StartCoroutine(fadeOutC(TargetAudio.C));
        }
        else
            StartCoroutine(fadeOutC(source));
    }
    public void fadeIn(TargetAudio source)
    {
        StartCoroutine(fadeInC(source));
    }
    IEnumerator fadeOutC(TargetAudio source)
    {
        while (musicSource[(int)source].volume > 0.0f)
        {
            musicSource[(int)source].volume -= fadeInIncrememnt * 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator fadeInC(TargetAudio source)
    {
        while (musicSource[(int)source].volume < musicVolume)
        {
            musicSource[(int)source].volume += fadeInIncrememnt * 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        musicSource[(int)source].volume = musicVolume;
    }

    //Kollar i fall den nya spellistan redan spellas genom att see i fall någon av tracksen matchar vad som just nu spelas
    public void playSongs(List<AudioClip> songs, Playlist.PlayBeaviour playBeaviour)
    {
        bool sameList = false;

        foreach (AudioClip clip in songs)
        {
            if (clip == musicSource[(int)targetAudio].clip)
            {
                sameList = true;
                break;
            }
        }
        if (!sameList)
        {
            StopAllCoroutines();
            fadeOut(targetAudio);
            fadeOut(targetAudio - 1);
            switchAudioEnumTarget();
            fadeIn(targetAudio);
            playPlaylist(songs, playBeaviour);
        }
    }


    //Spelar första clippet i listan som skickats för att sedan loopa det andra (Kan utbyggas för me funktionalitet med hjälp av PlayBehaviour enum
    void playPlaylist(List<AudioClip> songs, Playlist.PlayBeaviour playBeaviour)
    {
        musicSource[(int)targetAudio].clip = songs[0];
        musicSource[(int)targetAudio].Play();
        musicSource[(int)targetAudio].loop = false;

        switchAudioEnumTarget();

        double test = songs[0].length + AudioSettings.dspTime;
        musicSource[(int)targetAudio].clip = songs[1];
        musicSource[(int)targetAudio].PlayScheduled(test);
        Debug.Log(test);
        musicSource[(int)targetAudio].loop = true;
        musicSource[(int)targetAudio].volume = 1.0f;
    }

}