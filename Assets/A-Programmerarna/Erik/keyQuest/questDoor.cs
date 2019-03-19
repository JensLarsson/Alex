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
        for (int i = 0; i < keyQuestMain.Instance.nameOfAllDoors.Length; i++)
        {
            if (thisDoorGO == keyQuestMain.Instance.nameOfAllDoors[i])
            {

                keyQuestMain.Instance.updateKeyQuest(i);
            }
        }
    }
}