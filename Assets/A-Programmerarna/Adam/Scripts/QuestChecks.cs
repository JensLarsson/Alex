using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChecks : MonoBehaviour {

    public bool isTrigger;
    public QuestCheck[] questChecks;

	// Use this for initialization
	void Start () {
        if (isTrigger)
        {
            if (gameObject.GetComponent<Collider2D>() == null)
            {
                CircleCollider2D cc2D = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
            }
            gameObject.GetComponent<Collider2D>().isTrigger = isTrigger;
        }
    }

    public void getQuests()
    {
        foreach (QuestCheck questCheck in questChecks)
        {
            questCheck.onTrigger();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            if (other.tag == "Player")
            {
                foreach (QuestCheck questCheck in questChecks)
                {
                    questCheck.onTrigger();
                }
            }
        }
    }
}
