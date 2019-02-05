using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCallOnDeath : MonoBehaviour
{

    public string eventID = "";
    private void OnDisable()
    {
        EventManager.TriggerEvent(eventID);
    }
}
