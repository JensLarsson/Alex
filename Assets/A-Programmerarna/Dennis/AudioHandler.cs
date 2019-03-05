using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

    [SerializeField]
    private AudioClip[] footSteps;

    private Rigidbody2D rb2d;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        PlayFootsteps();
	}

    private void PlayFootsteps()
    {
        if (rb2d.velocity.x != 0 || rb2d.velocity.y != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = footSteps[Random.Range(0, footSteps.Length)];
                audioSource.Play();
            }
        }
    }
}
