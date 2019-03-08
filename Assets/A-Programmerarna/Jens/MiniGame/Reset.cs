using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{

    public string button = "Submit";

    void Update()
    {
        if (Input.GetButtonDown(button))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
