using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteThis : MonoBehaviour
{

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
