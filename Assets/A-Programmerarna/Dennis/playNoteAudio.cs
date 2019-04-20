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
        if(!canBeSelected)
        {
            if(gameObject.transform.GetChild(0).GetComponent<specialButton>() != null)
            {
                StartCoroutine(gameObject.transform.GetChild(0).GetComponent<specialButton>().playSound());
            }
            else
            {
                Debug.LogWarning("no specialButton script was found");
            }

        }
        else
        {
            AudioManager.instance.playSFXClip(NoteAudio);
            StartCoroutine(playAduioClipp());
        }
    }
    IEnumerator playAduioClipp()
    {
        isPlaying = true;
        yield return new WaitForSeconds(NoteAudio.length);
        isPlaying = false;
    }
}
