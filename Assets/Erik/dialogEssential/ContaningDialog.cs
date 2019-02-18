using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEditor;


[System.Serializable]
public class ActivateDialog
{
    public bool onCollisionEnter;
    public bool onCollisionStayWithDelay;
    public float delay;
    public bool onCollisionAndKeyDown;
    [Tooltip("ifall ni vill starta dialogen vid ex man går höger använd scriptet 'interactOnKeyPress' ")]
    public bool onFunctionCall;
}

public class ContaningDialog : MonoBehaviour
{
	[SerializeField] string onChoseText;
	[SerializeField] ActivateDialog activateDialogWith;

	[Tooltip("om inget ljud finns kommer det helt enkelt inte spelas upp något")]
	[SerializeField] AudioClip[] diffrentStartSounds;
	[SerializeField] float startSoundPitchRange;


	[SerializeField] List<Dialogs> newDialog = new List<Dialogs>();
	[SerializeField] bool canRepeatTheDialog = false;
	[HideInInspector] public bool canBeActivated = true;
	[HideInInspector] public bool hasBeenRead = false;
	[HideInInspector] public List<GameObject> siblings = new List<GameObject>();
	[SerializeField] GameObject[] answers;
	[SerializeField] UnityEvent doAfterDialgue;
	bool isInDialogueTrigger = false;
    //start delay is the delay from active dialogue at the first frame (a reset)
	float StartDelay;
	float soundDelay;
    [HideInInspector] public bool canPlaySound = true;


    void Start()
    {
        StartDelay = activateDialogWith.delay;
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
        if (activateDialogWith.onFunctionCall && activateDialogWith.delay < 0 || !activateDialogWith.onFunctionCall)
        {
            if (!DialogManager.Instance.isInDialogue)
            {
                //startDialogueSounds();
                DialogManager.Instance.queNewDialog(diffrentStartSounds, startSoundPitchRange, newDialog, answers, doAfterDialgue, onChoseText, this.gameObject);
            }
        }
    }
    public void resetDialogue(bool chosedAnother)
    {
        //respawn the dialogue if the player picked another answer
        //or if the dialogue can be played multiply times
        if (chosedAnother || canRepeatTheDialog)
        {
            Vector3 pos = transform.position;

            GameObject newDialogue = Instantiate(gameObject);

            newDialogue.transform.position = pos;
            newDialogue.name = gameObject.name;
            newDialogue.GetComponent<ContaningDialog>().activateDialogWith.delay = StartDelay;

            if (transform.parent != null)
            {
                Transform parent = transform.parent.transform;
                newDialogue.transform.SetParent(parent);
            }
            if (activateDialogWith.onCollisionEnter)
            {
                newDialogue.GetComponent<ContaningDialog>().canBeActivated = false;
            }
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (isInDialogueTrigger)
        {
            if (activateDialogWith.onCollisionAndKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startConversation();
                }
            }

            if (DialogManager.Instance.activeDialog != null)
            {
                activateDialogWith.delay -= Time.deltaTime;
                if (activateDialogWith.delay < 0)
                {
                    if (activateDialogWith.onCollisionStayWithDelay)
                    {
                        activateDialogWith.delay = StartDelay;
                        startConversation();
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        isInDialogueTrigger = true;
        if (canBeActivated)
        {
            if (activateDialogWith.onCollisionEnter)
            {
                if (col.tag == "Player")
                {
						startConversation();
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isInDialogueTrigger = false;
            canBeActivated = true;
        }
    }
}
