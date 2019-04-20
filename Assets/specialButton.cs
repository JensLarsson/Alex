using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class specialButton : MonoBehaviour {
    [SerializeField] Sprite activeB;
    [SerializeField] Sprite deactiveB;
    NoteBehaviour thisNB;
    CalculateNotePosition thisCNP;
    [SerializeField] float timeOfset = -0.2f;
    Color beforeChangeColor;
    [SerializeField] Color correctOrderColor;

    List<int> correctOrder;
    List<AudioClip> soundInCorrectOrder = new List<AudioClip>();
    List<GameObject> pianoNotes = new List<GameObject>();


    // Use this for initialization
    void Start ()
    {
        thisNB = transform.parent.transform.parent.GetComponent<NoteBehaviour>();
        thisCNP = transform.parent.transform.parent.GetComponent<CalculateNotePosition>();
        correctOrder = thisNB.correctNoteOrder;
        pianoNotes = thisCNP.pianoNotes;

        soundInCorrectOrder.Clear();
        for(int index = 0; index < correctOrder.Count; index++)
        {
            AudioClip newAdioClipp = pianoNotes[correctOrder[index]].gameObject.GetComponent<playNoteAudio>().NoteAudio;
            soundInCorrectOrder.Add(newAdioClipp);
        }

      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public IEnumerator playSound()
    {
        gameObject.GetComponent<Image>().sprite = activeB;

        for (int i = 0; i < soundInCorrectOrder.Count; i++)
        {
           GameObject currentNote = thisNB.pianoNotes[correctOrder[i]];
           beforeChangeColor = currentNote.GetComponent<Image>().color;

            currentNote.GetComponent<Image>().color = correctOrderColor;
            //AudioManager.instance.playSFXClip(thisNB.pianoNotes[correctOrder[i]].GetComponent<playNoteAudio>().NoteAudio);
             AudioManager.instance.playSFXClip(soundInCorrectOrder[i]);
            yield return new WaitForSeconds(soundInCorrectOrder[i].length + timeOfset);
            currentNote.GetComponent<Image>().color = beforeChangeColor;

        }
        gameObject.GetComponent<Image>().sprite = deactiveB;
        
    }

}
