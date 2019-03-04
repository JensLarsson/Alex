using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorWindowControll : MonoBehaviour
{

    public GameObject window;

    public static ElevatorWindowControll Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void StartElevator()
    {
        window.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            window.gameObject.SetActive(false);
        }
    }

}
