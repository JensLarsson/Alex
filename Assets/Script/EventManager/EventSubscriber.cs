using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSubscriber : MonoBehaviour {

    public string eventID;
    public UnityEvent unEvent;

    UnityAction action;
    
    void Start()
    {
        action += invoke;
        EventManager.Instance.Subscribe(eventID, action);
    }

    private void OnDisable()
    {
        unsubscribe();
    }


    public void unsubscribe()
    {
        EventManager.Instance.Unsubscribe(eventID, action);
    }
    void invoke()
    {
        unEvent.Invoke();
    }
}
