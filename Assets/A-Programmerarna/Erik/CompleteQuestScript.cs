using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteQuestScript : MonoBehaviour {
    public QuestSO[] test;

    public void completeQuest()
    {
        for (int i = 0; i < test.Length; i++)
        {
            QuestManager.Instance.addToCompletedQuests(test[i]);
        }
    }
}
