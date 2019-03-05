using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleter : MonoBehaviour
{

    public QuestSO quest;

    public void CompleteQuest()
    {
        QuestManager.Instance.CompleteQuestNew(quest);
    }
}
