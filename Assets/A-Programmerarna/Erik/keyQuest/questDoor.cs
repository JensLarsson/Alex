using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct QuestDoorAction
{
    public AudioClip clip;
    public UnityEvent _event;
}

public class questDoor : MonoBehaviour
{
    public QuestDoorAction rightDoor, wrongDoor;

    public void unlockDoor()
    {
        string thisDoorGO = gameObject.name;
        if (thisDoorGO == keyQuestMain.Instance.nameOfAllDoors[keyQuestMain.Instance.currentDoor])
        {
            rightDoor._event.Invoke();
            keyQuestMain.Instance.currentDoor++;
            //keyQuestMain.Instance.updateKeyQuest(i);
        }
        else
        {
            keyQuestMain.Instance.currentDoor = 0;
            wrongDoor._event.Invoke();
        }
    }
}