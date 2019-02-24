using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAlphaHole : MonoBehaviour {

    public Material mat;
    public Transform tran;

    void Update () {
        mat.SetFloat("_MaskPosX", tran.position.x);
        mat.SetFloat("_MaskPosY", tran.position.y);
	}
}
