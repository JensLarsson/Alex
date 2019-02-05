using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static EventManager Instance = new EventManager();

    Dictionary<string, UnityEvent> subscribers = new Dictionary<string, UnityEvent>();

    public void Subscribe(string Id, UnityAction action)
    {
        UnityEvent tempEvent = null;
        if (subscribers.TryGetValue(Id, out tempEvent))
        {
            tempEvent.AddListener(action);
        }
        else
        {
            tempEvent = new UnityEvent();
            tempEvent.AddListener(action);
            Instance.subscribers.Add(Id, tempEvent);
        }
    }

    public void Unsubscribe(string Id, UnityAction action)
    {
        UnityEvent tempEvent = null;
        if (Instance.subscribers.TryGetValue(Id, out tempEvent))
        {
            tempEvent.RemoveListener(action);
        }
        else
        {
            Debug.LogError("ID not defined in Events, no unsubscription initiated");
        }
    }

    public static void TriggerEvent(string Id)
    {
        UnityEvent tempEvent = null;
        if (Instance.subscribers.TryGetValue(Id, out tempEvent))
        {
            tempEvent.Invoke();
        }
        else
        {
            Debug.LogError("ID not defined in Events, no Event triggered");
        }
    }
}
