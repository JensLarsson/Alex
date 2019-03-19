using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playNoteAudio : MonoBehaviour
{

    [SerializeField] private AudioClip NoteAudio;

    public void PlayNoteAudio()
    {
        AudioManager.instance.playSFXClip(NoteAudio);
    }
}
