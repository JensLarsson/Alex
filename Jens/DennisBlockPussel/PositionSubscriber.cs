using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSubscriber : MonoBehaviour
{

    private void OnEnable()
    {
        PositionManager.Instance.addOccupant(this.gameObject);
    }

    private void OnDisable()
    {
        PositionManager.Instance.removeOccupant(this.gameObject);
    }
}
