using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    static public AudioManager instance = null;

    public enum TargetAudio { A = 0, B };
    public TargetAudio targetAudio = TargetAudio.A;

    public AudioSource[] musicSource = new AudioSource[2];
    public AudioSource sfxSource;
    public AudioClip sceneMusic;
    public float fadeInIncrememnt = 0.1f;
    float volume = 1.0f;

    void Start()
    {
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
            instance.sceneMusic = this.sceneMusic;
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);


        if (sceneMusic != null)
        {
            instance.changeSong(sceneMusic);
        }
    }

    //Replaces current song (if one is playing) with new one
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
    //Uses secondary AudioSource for soundeffects 
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
    //  Crossfading mellan Audiosources på de två musikobjekten.
    //
    public void fadeOut(TargetAudio source)
    {
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
            musicSource[(int)source].volume -= fadeInIncrememnt;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator fadeInC(TargetAudio source)
    {
        while (musicSource[(int)source].volume < volume)
        {
            musicSource[(int)source].volume += fadeInIncrememnt;
            yield return new WaitForSeconds(0.1f);
        }
    }


    //Växlar mellan de två AudioSource enumIndex målen
    void switchAudioEnumTarget()
    {
        switch (targetAudio)
        {
            case TargetAudio.A:
                targetAudio = TargetAudio.B;
                break;
            case TargetAudio.B:
                targetAudio = TargetAudio.A;
                break;
        }
    }
}