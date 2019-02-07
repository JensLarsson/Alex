using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


[System.Serializable]
public class ActivateDialog
{
    public bool onCollision;
    public bool onCollisionStayWithDelay;
    public float delay;
    public bool onCollisionAndKeyDown;
    [Tooltip("ifall ni vill starta dialogen vid ex man går höger använd scriptet 'interactOnKeyPress' ")]
    public bool onFunctionCall;
}
public class ContaningDialog : MonoBehaviour
{
    [SerializeField] ActivateDialog activateDialogWith;

    [Tooltip("om inget ljud finns kommer det helt enkelt inte spelas upp något")]
    [SerializeField] AudioClip[] diffrentStartSounds;
    [SerializeField] float startSoundPitchRange;

    [SerializeField] List<Dialogs> newDialog = new List<Dialogs>();
    [SerializeField] bool canRepeatTheDialog = false;
    [HideInInspector] public bool canBeActivated = true;
    public bool hasBeenRead = false;
    [HideInInspector] public List<GameObject> siblings = new List<GameObject>();
    bool isInDialogueTrigger = false;
    float delay;


    void Start()
    {
        delay = activateDialogWith.delay;
    }
    void startDialogueSounds()
    {
        if (diffrentStartSounds.Length > 0)
        {
            int random = Random.Range(0, diffrentStartSounds.Length);

            AudioManager.instance.playSFXRandomPitch(
                diffrentStartSounds[random],
                startSoundPitchRange);
        }
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
        if (activateDialogWith.onFunctionCall && activateDialogWith.delay < 0)
        {
            DialogManager.Instance.queNewDialog(newDialog, gameObject);
            startDialogueSounds();
            exitDialogue();
        }
        else if (!activateDialogWith.onFunctionCall)
        {
            DialogManager.Instance.queNewDialog(newDialog, gameObject);
            startDialogueSounds();
            exitDialogue();
        }
    }
    void exitDialogue()
    {
        if (canRepeatTheDialog)
        {
            Vector3 pos = transform.position;
            Transform parent = transform.parent.transform;

            GameObject newDialogue = Instantiate(gameObject);
            newDialogue.transform.SetParent(parent);
            newDialogue.transform.position = pos;
            newDialogue.name = gameObject.name;
            newDialogue.GetComponent<ContaningDialog>().activateDialogWith.delay = delay;

            newDialogue.GetComponent<ContaningDialog>().canBeActivated = false;
        }
        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        if (isInDialogueTrigger)
        {
            if (!DialogManager.Instance.isInDialogue)
            {
                activateDialogWith.delay -= Time.deltaTime;
                if (activateDialogWith.delay < 0)
                {
                    if (activateDialogWith.onCollisionStayWithDelay)
                    {
                        activateDialogWith.delay = delay;
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
            if (activateDialogWith.onCollision)
            {
                if (col.tag == "Player")
                {
                    startConversation();
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            if (activateDialogWith.onCollisionAndKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Space))
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
