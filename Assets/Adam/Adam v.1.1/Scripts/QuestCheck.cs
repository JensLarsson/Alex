using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestCheck {

    [Header("Check")]
    public QuestSO[] currentQuestsToCheck;
    public QuestSO[] completedQuestsToCheck;
    [Header("Get")]
    public QuestSO[] questsToGet;

    public bool checkCurrentQuests()
    {
        int numberofRightQuests = 0;
        foreach (QuestSO quest in completedQuestsToCheck)
        {
            if (QuestManager.Instance.questExistsInCompletedQuests(quest))
            {
                numberofRightQuests++;
            }
        }
        if (numberofRightQuests == completedQuestsToCheck.Length)
        {
            return true;
        }
        return false;
    }
    public bool checkCompletedQuests()
    {
        int numberofRightQuests = 0;
        foreach (QuestSO quest in completedQuestsToCheck)
        {
            if (QuestManager.Instance.questExistsInCompletedQuests(quest))
            {
                numberofRightQuests++;
            }
        }
        if (numberofRightQuests == completedQuestsToCheck.Length)
        {
            return true;
        }
        return false;
    }

    public void getQuestsByQuests()
    {
        if (checkCurrentQuests() && checkCompletedQuests())
        {
            foreach (QuestSO quest in questsToGet)
            {
                QuestManager.Instance.addToCurrentQuests(quest);
            }
        }
    }

    public void onTrigger()
    {
        getQuestsByQuests();
    }
}
