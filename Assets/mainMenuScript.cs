using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuScript : MonoBehaviour {
    [SerializeField] GameObject test;


    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Submit"))
        {
            SceneController.instance.loadScene("Alex");

            //StartCoroutine(SceneController.instance.SceneTransition("Alex"));
            Instantiate(test);
        }
	}
}
