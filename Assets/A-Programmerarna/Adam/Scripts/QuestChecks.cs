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
        Debug.Log("Derp1");
        foreach (QuestCheck questCheck in questChecks)
        {
            Debug.Log("Derp2");
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
