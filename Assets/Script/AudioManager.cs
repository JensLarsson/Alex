using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    static public AudioManager instance = null;

    public enum TargetAudio { A = 0, B };
    [HideInInspector] public TargetAudio targetAudio = TargetAudio.A;

    public AudioSource[] musicSource = new AudioSource[2];
    public AudioSource sfxSource;
    public AudioSource sfxSourcePitch;
    AudioClip sceneMusic;
    [Tooltip("volume change per .01sec")]
    [Range(0.001f, 1.0f)]
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



        if (sceneMusic != null)
        {
            instance.changeSong(sceneMusic);
        }
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
            musicSource[(int)source].volume -= fadeInIncrememnt * 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator fadeInC(TargetAudio source)
    {
        while (musicSource[(int)source].volume < volume)
        {
            musicSource[(int)source].volume += fadeInIncrememnt * 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void playSongs(List<AudioClip> songs, Playlist.PlayBeaviour playBeaviour)
    {
        fadeOut(targetAudio);
        switchAudioEnumTarget();
        fadeIn(targetAudio);
        StartCoroutine(playPlaylist(songs, playBeaviour));
    }

    IEnumerator playPlaylist(List<AudioClip> songs, Playlist.PlayBeaviour playBeaviour)
    {
        for (int i = 0; i < songs.Count; i++)
        {
            float songTime = songs[i].length;
            musicSource[(int)targetAudio].clip = songs[i];
            musicSource[(int)targetAudio].Play();
            yield return new WaitForSeconds(songTime);

            if (playBeaviour == Playlist.PlayBeaviour.loopPlaylist && i == songs.Count - 1)
            {
                i = -1;
            }
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