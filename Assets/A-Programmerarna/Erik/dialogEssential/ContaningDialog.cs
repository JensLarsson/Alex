using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Events;



[System.Serializable]
public class ActivateDialog
{
    //this is hidden due to malfunction; since focus is else were contact me if this is needs to be in game 
    [HideInInspector] public bool onCollisionEnter;
    public bool onCollisionStayWithDelay;
    public float delay;
    public bool onCollisionAndKeyDown;
    [Tooltip("ifall ni vill starta dialogen vid ex man går höger använd scriptet 'interactOnKeyPress' ")]
    public bool onFunctionCall;
}

public class ContaningDialog : MonoBehaviour
{
    [SerializeField] public string dialogueName;

    [Header("Quests")]
    public List<QuestSO> instantiateDialogIfQuestsExistsInCurrent;
    public List<QuestSO> instantiateDialogIfQuestsExistsInCompleted;
    public List<QuestSO> removeDialogIfQuestsHasCompleted;
    

    [Header("Start dialogue sound")]
    [Tooltip("om inget ljud finns kommer det helt enkelt inte spelas upp något")]
    [SerializeField] AudioClip[] diffrentStartSounds;
    [SerializeField] float startSoundPitchRange;
    [HideInInspector] public bool canPlaySound = true;

    [Header("Unique dialogue traits")]
    [SerializeField] List<Dialogs> speechBubbles = new List<Dialogs>();
    
  // public bool canBeActivated = true;
    [HideInInspector] public bool hasBeenRead = false;
    [HideInInspector] public List<GameObject> siblings = new List<GameObject>();
    [SerializeField] GameObject[] answers;
    [SerializeField] UnityEvent doAfterDialgue;
    //start delay is the delay from active dialogue at the first frame (a reset)
  
    float soundDelay;
  



    void Start()
    {
       
    }

    void OnDestroy()
    {
        for (int x = 0; x < siblings.Count; x++)
        {
            Destroy(siblings[x].gameObject);
        }
    }
    public void startConversation()
    {
            if (!DialogManager.Instance.isInDialogue)
            {
                DialogManager.Instance.queNewDialog(
                    diffrentStartSounds, 
                    startSoundPitchRange,
                    speechBubbles, 
                    answers,
                    doAfterDialgue,
                    dialogueName, 
                    this.gameObject,
                    hasBeenRead);
            }
    }
    public void resetDialogue(bool wasSelected)
    {
        if(wasSelected)
        {
            Debug.Log("should search");
            gameObject.transform.parent.GetComponent<conversationCollection>().isRemoved(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}

