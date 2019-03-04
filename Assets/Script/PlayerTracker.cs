using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTracker : MonoBehaviour {

    public static PlayerTracker Instance = null;

	// Use this for initialization
	void Awake () {
        Instance = this;
	}

    private void Start()
    {
        string id = SceneManager.GetActiveScene().name;
        Vector3 vec = new Vector3();
        if (SceneController.instance.lastScenePosition.TryGetValue(id, out vec))
        {
            transform.position = vec;
        }
    }
}
