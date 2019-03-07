using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class addFunc
{
    public float xPos, yPos;
    public GameObject objectToSpawn;
}
public class add : MonoBehaviour {

    [SerializeField] List<QuestSO> completeTheseQuestsToAddItems;
    [SerializeField] addFunc[] amountOfObjectsToSpawn;
    bool isDone = true;

    // Update is called once per frame
    void Update()
    {
        if (isDone)
        {
            if (QuestManager.Instance.questsExistsInCompletedQuests(completeTheseQuestsToAddItems))
            {
                for (int i = 0; i < amountOfObjectsToSpawn.Length; i++)
                {
                    Instantiate(amountOfObjectsToSpawn[i].objectToSpawn, new Vector3(amountOfObjectsToSpawn[i].xPos, amountOfObjectsToSpawn[i].yPos, 1), Quaternion.identity);
                }
                isDone = false;
            }
        }
    }
}
