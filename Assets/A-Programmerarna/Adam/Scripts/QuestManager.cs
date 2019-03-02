using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour{

    public static QuestManager Instance = null;

    public List<QuestSO> currentQuests;
	public List<QuestSO> completedQuests;

	private string currentQuestsSaves = "currentQuestsSaves";
	private string completedQuestsSaves = "completedQuestsSaves";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public void addToCurrentQuests(QuestSO quest)
    {
        if (questExistsInCurrentQuests(quest))
        {
            Debug.Log(quest._name + " already exist in current quests");
        }
        else if (questExistsInCompletedQuests(quest._name))
        {
            Debug.Log(quest._name + " already exsist in completed quests");
        }
        else
        {
            Debug.Log("Added " + quest._name + " to current quests");
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
            QuestSO quest = findQuestInCurrentQuests(name);
		    if (quest == null) {
                Debug.Log(name + " dosen't exist in current quests");
		    }
            else
            {
			    currentQuests.Remove (quest);
			    completedQuests.Add(quest);
            }
        }
	}

    public void addToCompletedQuests(QuestSO quest)
    {
        if (questExistsInCompletedQuests(quest))
        {
            Debug.Log(quest._name + " already exsist in completed quests");
        }
        else
        {
            if (questExistsInCurrentQuests(quest))
            {
                Debug.Log(quest._name + " dosen't exist in current quests");
            }
            else
            {
                currentQuests.Remove(quest);
                completedQuests.Add(quest);
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
    public bool questExistsInCompletedQuests(string name){
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

    public void SaveQuests()
    {
        List<Quest> tempCurrentQuests = new List<Quest>();
        foreach (QuestSO questSO in currentQuests)
        {
            Quest quest = new Quest();
            quest._name = questSO._name;
            quest.description = questSO.description;
            tempCurrentQuests.Add(quest);
        }
        List<Quest> tempCompletedQuests = new List<Quest>();
        foreach (QuestSO questSO in completedQuests)
        {
            Quest quest = new Quest();
            quest._name = questSO._name;
            quest.description = questSO.description;
            tempCompletedQuests.Add(quest);
        }
        XMLManger.Instance.Savequests (tempCurrentQuests, currentQuestsSaves);
        XMLManger.Instance.Savequests (tempCompletedQuests, completedQuestsSaves);
    }

    public void LoadQuests()
    {
        List<Quest> tempCurrentQuests = XMLManger.Instance.Loadquests(currentQuestsSaves);
        foreach (Quest quest in tempCurrentQuests)
        {
            QuestSO questSO = new QuestSO();
            questSO._name = quest._name;
            questSO.description = quest.description;
            currentQuests.Add(questSO);
        }
        List<Quest> tempCompletedQuests = XMLManger.Instance.Loadquests(completedQuestsSaves);
        foreach (Quest quest in tempCompletedQuests)
        {
            QuestSO questSO = new QuestSO();
            questSO._name = quest._name;
            questSO.description = quest.description;
            completedQuests.Add(questSO);
        }
    }

    QuestSO findQuestInCurrentQuests(string name){
		return findQuestByName(name, currentQuests);
	}

    QuestSO findQuestInCurrentQuests(QuestSO quest)
    {
        return findQuestByQuest(quest, currentQuests);
    }
    QuestSO findQuestInCompletedQuests(string name){
		return findQuestByName (name, completedQuests);
	}

    QuestSO findQuestInCompletedQuests(QuestSO quest)
    {
        return findQuestByQuest(quest, completedQuests);
    }

    QuestSO findQuestByName(string name, List<QuestSO> quests)
	{
		if (quests.Count < 1) {
			Debug.Log("No quests are in " + quests + ".");
			return null;
		}
		foreach (QuestSO quest in quests) {
			if (quest._name == name) {
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
            Debug.Log("No quests are in " + quests + ".");
            return null;
        }
        foreach (QuestSO forQuest in quests)
        {
            if (forQuest._name == quest._name)
            {
                return quest;
            }
        }
        Debug.Log("Couldn't find a quest with the name '" + quest._name + "'.");
        return null;
    }
}