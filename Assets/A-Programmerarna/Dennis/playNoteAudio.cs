using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playNoteAudio : MonoBehaviour
{

    public AudioClip NoteAudio;
    public bool canBeSelected = true;
    public bool hasBeenSelected = false;
    
    
    public void PlayNoteAudio()
    {
        AudioManager.instance.playSFXClip(NoteAudio, true);
    }
}
