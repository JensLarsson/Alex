using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleter : MonoBehaviour
{
    enum QuestMode { Try, Force };
    QuestMode questMode = QuestMode.Try;

    public QuestSO quest;

    public void CompleteQuest()
    {
        QuestManager.Instance.CompleteQuestfromCurrent(quest);
    }
}
