using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseInventory : MonoBehaviour
{
    public GameObject inventory;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !menuManager.IsInMenu)
        {

            inventory.SetActive(true);
        }
        else if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(false);
        }
    }
}
