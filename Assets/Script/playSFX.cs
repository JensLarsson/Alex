using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSFX : MonoBehaviour
{

    public AudioClip clip;

    public void playClip()
    {
        AudioManager.instance.playSFXClip(clip);
    }
}
