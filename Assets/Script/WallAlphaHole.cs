using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAlphaHole : MonoBehaviour
{

    public Material mat;
    Transform tran;
    private void OnLevelWasLoaded(int level)
    {
        tran = PlayerTracker.Instance.gameObject.transform;
    }

    private void Start()
    {
        tran = PlayerTracker.Instance.gameObject.transform;
    }

    void Update()
    {
        mat.SetFloat("_MaskPosX", tran.position.x);
        mat.SetFloat("_MaskPosY", tran.position.y);
    }
}
