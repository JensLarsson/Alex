using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleter : MonoBehaviour
{
    public enum QuestMode { Try, Force };
    public QuestMode questMode = QuestMode.Try;

    public QuestSO quest;

    public void CompleteQuest()
    {
        switch (questMode)
        {
            case QuestMode.Try:
                QuestManager.Instance.CompleteQuestfromCurrent(quest);
                break;
            case QuestMode.Force:
                QuestManager.Instance.ForceCompletedQuest(quest);
                break;
        }
    }
}
