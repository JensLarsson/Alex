using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playNoteAudio : MonoBehaviour
{

    public AudioClip NoteAudio;
    public bool canBeSelected = true;
    public bool hasBeenSelected = false;
    [HideInInspector] public bool isPlaying = false;  
    
    
    public void PlayNoteAudio()
    {
        AudioManager.instance.playSFXClip(NoteAudio);
        StartCoroutine(playAduioClipp());
        if(!canBeSelected)
        {
            if(gameObject.transform.GetChild(0).GetComponent<specialButton>() != null)
            {
                StartCoroutine(gameObject.transform.GetChild(0).GetComponent<specialButton>().playSound(NoteAudio.length));
            }
            else
            {
                Debug.LogWarning("no specialButton script was found");
            }

        }
    }
    IEnumerator playAduioClipp()
    {
        isPlaying = true;
        yield return new WaitForSeconds(NoteAudio.length);
        isPlaying = false;
    }
}
