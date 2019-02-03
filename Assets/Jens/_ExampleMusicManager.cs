using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ExampleMusicManager : MonoBehaviour
{

    public List<AudioClip> audioClips;
    public List<AudioSource> audioSources;
    public float maxVolume;
    public float fadeSpeed;
    private int nextStem = 0;

    private void Start()
    {
        //Denna void start kallar på MusicSetup metoden
        MusicSetup();
    }

    void MusicSetup()
    {
        int trackNumber = 0;

        //För varje Ljudkälla i audioSources (en public list).
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = audioClips[trackNumber];
            audioSource.volume = 0;
            audioSource.loop = true; //They must loop... musiken, alltså
            audioSource.Play();

            trackNumber++;
        }
        FadeInCaller();
    }

    public void FadeInCaller()
    {
        if (nextStem < audioSources.Count)
        {
            StartCoroutine(AddStem(audioSources[nextStem])); //Bläddrar mellan stemsen
        }
    }

    IEnumerator AddStem(AudioSource currentTrack)
    {
        //Denna while sekvens förhindrar att stemsen spelas samtidigt
        while (currentTrack.volume < maxVolume)
        {
            currentTrack.volume += fadeSpeed;
            yield return new WaitForSeconds(0.1f);
        }
        nextStem++;
    }

}