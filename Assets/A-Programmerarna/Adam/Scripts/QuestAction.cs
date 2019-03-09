using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestAction : MonoBehaviour
{

    public QuestSO[] currentQuestsToCheck;
    public QuestSO[] completedQuestsToCheck;

    public UnityEvent doIfQuestsExists;
    // Use this for initialization
    void Start()
    {
        checkQuest();
    }

    private void OnLevelWasLoaded(int level)
    {
        checkQuest();
    }
    public void checkQuest()
    {
        if (QuestManager.Instance.questsExistsInCurrentQuests(currentQuestsToCheck) && QuestManager.Instance.questsExistsInCompletedQuests(completedQuestsToCheck))
        {
            doIfQuestsExists.Invoke();
        }
    }
}
