﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conversationCollection : MonoBehaviour
{
    public ActivateDialog activateDialogWith;
    [SerializeField] List<GameObject> thisCharacterConversations = new List<GameObject>();

    public bool isInDialogueTrigger = false;
    public float StartDelay;
    // Use this for initialization
    void Start()
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
        StartCoroutine(sendConversation());
    }

    IEnumerator sendConversation()
    {
        yield return new WaitForSeconds(StartDelay);
        sendConversationsToDialogManager();
    }

    void sendConversationsToDialogManager()
    {
        if (menuManager.Instance.menuState == menuManager.MenuState.noMenu)
        {
            List<GameObject> tempList = new List<GameObject>();

            //for (int i = thisCharacterConversations.Count - 1; i >= 0; i--)
            foreach (GameObject dialogs in thisCharacterConversations)
            {

                bool isDialogueAcceible = true;
                ContaningDialog con = dialogs.gameObject.GetComponent<ContaningDialog>();


                if (!QuestManager.Instance.questsExistsInCompletedQuests(con.instantiateDialogIfQuestsExistsInCompleted))
                {
                    isDialogueAcceible = false;
                }
                if (!QuestManager.Instance.questsExistsInCurrentQuests(con.instantiateDialogIfQuestsExistsInCurrent))
                {
                    isDialogueAcceible = false;
                }
                if (QuestManager.Instance.questsExistsInCompletedQuests(con.removeDialogIfQuestsHasCompleted)
                  && con.removeDialogIfQuestsHasCompleted.Count > 0)
                {
                    isDialogueAcceible = false;
                }

                if (isDialogueAcceible)
                {
                    Debug.Log(dialogs.gameObject.GetComponent<ContaningDialog>().dialogueName + " is accesable");
                    tempList.Add(dialogs);
                }
            }

            for (int i = 0; i < tempList.Count; i++)
            {
                tempList[i].GetComponent<ContaningDialog>().startConversation();
            }
            tempList.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!DialogManager.Instance.isInDialogue)
        {
            if (isInDialogueTrigger)
            {
                if (Input.GetButtonDown("Submit") && activateDialogWith.onCollisionAndKeyDown)
                {
                    Debug.Log("test");
                    sendConversationsToDialogManager();
                }
                if (activateDialogWith.onCollisionStayWithDelay && !DialogManager.Instance.isInDialogue && menuManager.Instance.menuState == menuManager.MenuState.noMenu)
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
            if (activateDialogWith.onCollisionEnter)
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
