using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyQuestMain : MonoBehaviour
{
    private static keyQuestMain instance;
    public static keyQuestMain Instance { get { return instance; } }

    public int currentDoor = 0;
    [Tooltip("Det är viktigt att dörrarna inte heter samma")]
    public string[] nameOfAllDoors;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There is too many keyQuestMain placed on scene");
        }
    }


    public void updateKeyQuest(int theDoor)
    {
        if (currentDoor < nameOfAllDoors.Length)
        {

            GameObject door = GameObject.Find(nameOfAllDoors[theDoor]);
            if (door != null)
            {
                door.GetComponent<SceneTrigger>().unlock();
            }


            if (theDoor == currentDoor)
            {
                currentDoor++;
            }
            else
            {
                currentDoor = 0;
            }

        }
        if (currentDoor == nameOfAllDoors.Length)
        {

            QuestManager.Instance.addToCompletedQuests("Door");
        }

    }
}
