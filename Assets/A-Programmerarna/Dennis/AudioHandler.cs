using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] footSteps;


    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootsteps()
    {

        if (!audioSource.isPlaying)
        {
            audioSource.clip = footSteps[Random.Range(0, footSteps.Length)];
            audioSource.Play();
        }

    }
}
