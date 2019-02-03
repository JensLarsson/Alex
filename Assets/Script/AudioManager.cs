using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    static public AudioManager instance = null;

    public AudioSource musicSource, sfxSource;
    public AudioClip sceneMusic;
    

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            instance.sceneMusic = sceneMusic;
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
        if (sceneMusic != null)
        {
            instance.playLoopingMusicClip(sceneMusic);
        }
    }

    public void playLoopingMusicClip(AudioClip clip)
    {
        musicSource.loop = true;
        if (musicSource.clip != clip)
        {
            Debug.Log("changing clip to " +clip.name);
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();
        }
    }


    public void playSFXClip (AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}
