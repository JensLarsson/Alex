﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    //Håller reda på vilka positioner objekt är placerade på

    public static PositionManager Instance = null;
    
    private List<GameObject> positionOccupant = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public bool isPositionOccupied(Vector3 pos)
    {
        foreach (GameObject gObject in positionOccupant)
        {
            if (gObject.transform.position == pos)
            {
                return true;
            }
        }
        return false;
    }


    public void addOccupant(GameObject gObject)
    {
        positionOccupant.Add(gObject);
    }


    public void removeOccupant(GameObject gObject)
    {
        for (int i = 0; i < positionOccupant.Count; i++)
        {
            if (gObject == positionOccupant[i])
            {
                positionOccupant.RemoveAt(i);
                break;
            }
        }
    }
}
