using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{

    public string button = "Submit";
    public string instantbutton = "";

    void Update()
    {
        if (button != "" && Input.GetButtonDown(button))
        {
            resetScene();
        }
        if (instantbutton != "" && Input.GetButtonDown(instantbutton))
        {
            instantReset();
        }
    }

    public void resetScene()
    {
        SceneController.instance.loadScene(SceneManager.GetActiveScene().name);
    }
    public void instantReset()
    {
        SceneController.instance.loadScene(SceneManager.GetActiveScene().name, true);
    }
}
