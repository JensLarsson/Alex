using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestAction : MonoBehaviour {

    public QuestSO[] currentQuestsToCheck;
    public QuestSO[] completedQuestsToCheck;

    public UnityEvent doIfQuestsExists;
    // Use this for initialization
    void Start () {
        if (QuestManager.Instance.questsExistsInCurrentQuests(currentQuestsToCheck) && QuestManager.Instance.questsExistsInCompletedQuests(completedQuestsToCheck))
        {
            doIfQuestsExists.Invoke();
        }
	}
}
