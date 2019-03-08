using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    Transform pos;
    void Start()
    {
        pos = PlayerTracker.Instance.gameObject.transform;
        DontDestroyOnLoad(gameObject);
    }
    private void OnLevelWasLoaded(int level)
    {
        pos = PlayerTracker.Instance.gameObject.transform;
    }
}
