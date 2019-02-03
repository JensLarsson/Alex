using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KnappenKnappen : MonoBehaviour {

    public Scene nextScene;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Laddar scen");
            SceneManager.LoadScene("JensAndraTestScene");
        }
	}
}
