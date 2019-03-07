using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conversationCollection : MonoBehaviour
{
   public ActivateDialog activateDialogWith;
    [SerializeField] List<GameObject> thisCharacterConversations = new List<GameObject>();

    public bool isInDialogueTrigger = false;
    [HideInInspector] public bool isActive = false;
    public float StartDelay;
    // Use this for initialization
    void Start ()
    {
        StartDelay = activateDialogWith.delay;

    }
    public void isRemoved(GameObject removedGO)
    {
        for (int i = 0; i < thisCharacterConversations.Count; i++)
        {
            if (thisCharacterConversations[i] == removedGO)
            {
                thisCharacterConversations.Remove(thisCharacterConversations[i]);
            }
        }
    }
    public void onFunctionCall()
    {
        sendConversationsToDialogManager();
    }
	
    void sendConversationsToDialogManager()
    {
        isActive = true;
        for (int i = 0; i < thisCharacterConversations.Count; i++)
        {
            
            bool isDialogueAcceible = true;
            ContaningDialog con = thisCharacterConversations[i].gameObject.GetComponent<ContaningDialog>();

            if (QuestManager.Instance.questsExistsInCompletedQuests(con.removeDialogIfQuestsHasCompleted)
                 && con.removeDialogIfQuestsHasCompleted.Count > 0)
            {

                //isRemoved(thisCharacterConversations[i].gameObject);
                Destroy(thisCharacterConversations[i].gameObject);
                thisCharacterConversations.Remove(thisCharacterConversations[i]);
                return;
                i = 0;
            }
            if (!QuestManager.Instance.questsExistsInCompletedQuests(con.instantiateDialogIfQuestsExistsInCompleted))
            {
                isDialogueAcceible = false;
            }
            if (!QuestManager.Instance.questsExistsInCurrentQuests(con.instantiateDialogIfQuestsExistsInCurrent))
            {
                isDialogueAcceible = false;
            }

            //Debug.Log(isDialogueAcceible);
            if (isDialogueAcceible)
            {
                thisCharacterConversations[i].GetComponent<ContaningDialog>().startConversation();
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (!DialogManager.Instance.isInDialogue && !isActive)
        {
            if (isInDialogueTrigger)
            {
                if (Input.GetButtonDown("Submit") && activateDialogWith.onCollisionAndKeyDown)
                {
                    sendConversationsToDialogManager();
                }
                if (activateDialogWith.onCollisionStayWithDelay && !DialogManager.Instance.isInDialogue)
                {
                    StartDelay -= Time.deltaTime;
                    if (StartDelay < 0)
                    {
                        StartDelay = activateDialogWith.delay;
                        sendConversationsToDialogManager();
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isInDialogueTrigger = true;
            if(activateDialogWith.onCollisionEnter)
            {
                sendConversationsToDialogManager();
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isInDialogueTrigger = false;
        }
    }
}
