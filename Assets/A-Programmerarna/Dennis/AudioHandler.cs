using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] footSteps;
    
    public void PlayFootsteps()
    {
        AudioManager.instance.playSFXClip(footSteps[Random.Range(0, footSteps.Length)]);
    }
}
