using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public List<Quest> currentQuests;
    public List<Quest> completedQuests;
    public Quest[] questList;

    string currentQuestsSaves = "currentQuestsSaves";
    string completedQuestsSaves = "completedQuestsSaves";

    void Start()
    {
        //XMLManger.ins.Savequests (currentQuests, currentQuestsSaves);
        //XMLManger.ins.Savequests (completedQuests, completedQuestsSaves);
        //currentQuests = XMLManger.ins.Loadquests (currentQuestsSaves);
        //completedQuests = XMLManger.ins.Loadquests (completedQuestsSaves);
    }

    public void addToCurrentQuests(string name)
    {
        currentQuests.Add(findQuestByName(name, questList));
    }

    public void addToCurrentQuests(Quest quest)
    {
        currentQuests.Add(quest);
    }

    public void addToCompletedQuests(string name)
    {
        Quest quest = findQuestInCurrentQuests(name);
        if (quest != null)
        {
            currentQuests.Remove(quest);
            completedQuests.Add(quest);
        }
    }

    public bool questExistsInCurrentQuests(string name)
    {
        return currentQuests.Contains(findQuestInCurrentQuests(name));
    }
    public bool questExistsInCompletedQuests(string name)
    {
        return completedQuests.Contains(findQuestInCompletedQuests(name));
    }

    Quest findQuestInCurrentQuests(string name)
    {
        return findQuestByName(name, currentQuests);
    }

    Quest findQuestInCompletedQuests(string name)
    {
        return findQuestByName(name, completedQuests);
    }

    Quest findQuestByName(string name, List<Quest> quests)
    {
        if (quests.Count < 1)
        {
            Debug.Log("No quests are in " + quests + ".");
            return null;
        }
        foreach (Quest quest in quests)
        {
            if (quest._name == name)
            {
                return quest;
            }
        }
        Debug.Log("Couldn't find a quest with the name '" + name + "'.");
        return null;
    }

    Quest findQuestByName(string name, Quest[] quests)
    {
        foreach (Quest quest in quests)
        {
            if (quest._name == name)
            {
                return quest;
            }
        }
        Debug.Log("Couldn't find a quest with the name '" + name + "'.");
        return null;
    }
}