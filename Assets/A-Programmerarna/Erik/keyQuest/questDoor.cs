using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questDoor : MonoBehaviour
{
    public void unlockDoor()
    {
        string thisDoorGO = gameObject.name;
        for (int i = 0; i < keyQuestMain.Instance.nameOfAllDoors.Length; i++)
        {
            if(thisDoorGO == keyQuestMain.Instance.nameOfAllDoors[i])
            {
                keyQuestMain.Instance.updateKeyQuest(i);
            }
        }
    }
}