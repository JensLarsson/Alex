using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{

    public string button = "Submit";

    void Update()
    {
        if (button != "" && Input.GetButtonDown(button))
        {
            resetScene();
        }
    }

    public void resetScene()
    {
        SceneController.instance.loadScene(SceneManager.GetActiveScene().name);
    }
}
