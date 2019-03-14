using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public static QuestManager Instance = null;

    public List<QuestSO> currentQuests;
    public List<QuestSO> completedQuests;

    private string currentQuestsSaves = "currentQuestsSaves";
    private string completedQuestsSaves = "completedQuestsSaves";

    public bool CompleteQuestfromCurrent(QuestSO quest)
    {
        if (currentQuests.Contains(quest))
        {
            currentQuests.Remove(quest);
            completedQuests.Add(quest);
            return true;
        }

        return false;
    }
    public void ForceCompletedQuest(QuestSO quest)
    {
        if (!completedQuests.Contains(quest))
        {
            if (currentQuests.Contains(quest))
            {
                currentQuests.Remove(quest);
                completedQuests.Add(quest);
            }
            else
            {
                completedQuests.Add(quest);
            }
        }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void addToCurrentQuests(QuestSO quest)
    {
        if (questExistsInCurrentQuests(quest))
        {
            Debug.Log(quest.name + " already exist in current quests");
        }
        else if (questExistsInCompletedQuests(quest))
        {
            Debug.Log(quest.name + " already exsist in completed quests");
        }
        else
        {
            Debug.Log("Added " + quest.name + " to current quests");
            currentQuests.Add(quest);
        }
    }

    public void addToCompletedQuests(string name)
    {
        if (questExistsInCompletedQuests(name))
        {
            Debug.Log(name + " already exsist in completed quests");
        }
        else
        {
            if (questExistsInCurrentQuests(name))
            {
                QuestSO quest = findQuestInCurrentQuests(name);
                currentQuests.Remove(quest);
                completedQuests.Add(quest);
            }
            else
            {
                Debug.Log(name + " dosen't exist in current quests");
            }
        }
    }

    public void addToCompletedQuests(QuestSO quest)
    {
        if (questExistsInCompletedQuests(quest))
        {
            Debug.Log(quest.name + " already exsist in completed quests");
        }
        else
        {
            if (questExistsInCurrentQuests(quest))
            {
                currentQuests.Remove(quest);
                completedQuests.Add(quest);
            }
            else
            {
                Debug.Log(quest.name + " dosen't exist in current quests");
            }
        }
    }

    public bool questExistsInCurrentQuests(string name)
    {
        return currentQuests.Contains(findQuestInCurrentQuests(name));
    }

    public bool questExistsInCurrentQuests(QuestSO quest)
    {
        return currentQuests.Contains(findQuestInCurrentQuests(quest));
    }

    public bool questsExistsInCurrentQuests(List<QuestSO> quests)
    {
        int numberofRightQuests = 0;
        foreach (QuestSO quest in quests)
        {
            if (questExistsInCurrentQuests(quest))
            {
                numberofRightQuests++;
            }
        }
        return numberofRightQuests == quests.Count;
    }

    public bool questsExistsInCurrentQuests(QuestSO[] quests)
    {
        int numberofRightQuests = 0;
        foreach (QuestSO quest in quests)
        {
            if (questExistsInCurrentQuests(quest))
            {
                numberofRightQuests++;
            }
        }
        return numberofRightQuests == quests.Length;
    }

    public bool questExistsInCompletedQuests(string name)
    {
        return completedQuests.Contains(findQuestInCompletedQuests(name));
    }

    public bool questExistsInCompletedQuests(QuestSO quest)
    {
        return completedQuests.Contains(findQuestInCompletedQuests(quest));
    }

    public bool questsExistsInCompletedQuests(List<QuestSO> quests)
    {
        int numberofRightQuests = 0;
        foreach (QuestSO quest in quests)
        {
            if (questExistsInCompletedQuests(quest))
            {
                numberofRightQuests++;
            }
        }
        return numberofRightQuests == quests.Count;
    }

    public bool questsExistsInCompletedQuests(QuestSO[] quests)
    {
        int numberofRightQuests = 0;
        foreach (QuestSO quest in quests)
        {
            if (questExistsInCompletedQuests(quest))
            {
                numberofRightQuests++;
            }
        }
        return numberofRightQuests == quests.Length;
    }

    public void SaveQuests()
    {
        XMLManger.Instance.Savequests(currentQuests, currentQuestsSaves);
        XMLManger.Instance.Savequests(completedQuests, completedQuestsSaves);
    }

    public void LoadQuests()
    {
        currentQuests = XMLManger.Instance.Loadquests(currentQuestsSaves);
        completedQuests = XMLManger.Instance.Loadquests(completedQuestsSaves);
    }

    QuestSO findQuestInCurrentQuests(string name)
    {
        return findQuestByName(name, currentQuests);
    }

    QuestSO findQuestInCurrentQuests(QuestSO quest)
    {
        return findQuestByQuest(quest, currentQuests);
    }
    QuestSO findQuestInCompletedQuests(string name)
    {
        return findQuestByName(name, completedQuests);
    }

    QuestSO findQuestInCompletedQuests(QuestSO quest)
    {
        return findQuestByQuest(quest, completedQuests);
    }

    QuestSO findQuestByName(string name, List<QuestSO> quests)
    {
        if (quests.Count < 1)
        {
            Debug.Log("No quests are in " + quests + ".");
            return null;
        }
        foreach (QuestSO quest in quests)
        {
            if (quest.name == name)
            {
                return quest;
            }
        }
        Debug.Log("Couldn't find a quest with the name '" + name + "'.");
        return null;
    }

    QuestSO findQuestByQuest(QuestSO quest, List<QuestSO> quests)
    {
        if (quests.Count < 1)
        {
            return null;
        }
        foreach (QuestSO forQuest in quests)
        {
            //Debug.Log(forQuest._name);
            if (forQuest == quest)
            {
                return quest;
            }
        }
        Debug.Log("Couldn't find a quest with the name '" + quest.name);
        return null;
    }
}