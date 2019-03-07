using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove : MonoBehaviour {
    [SerializeField] List<QuestSO> completeTheseQuestsToRemoveThisGameObject;
	
	// Update is called once per frame
	void Update ()
    {
        if(QuestManager.Instance.questsExistsInCompletedQuests(completeTheseQuestsToRemoveThisGameObject))
        {
            Destroy(this.gameObject);
        }		
	}
}
