using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPosition : MonoBehaviour
{

    private void OnEnable()
    {
        TailManager.positionOccupation.Add(this.gameObject);
    }


    private void OnDisable()
    {
        //for (int i = TailManager.positionOccupation.Count - 1; i >= 0; i--)
        for (int i = 0; i < TailManager.positionOccupation.Count; i++)
        {
            if (TailManager.positionOccupation[i] == this.gameObject)
            {
                Debug.Log("Derp" + i);
                TailManager.positionOccupation.RemoveAt(i);
                break;
            }
        }
    }
}