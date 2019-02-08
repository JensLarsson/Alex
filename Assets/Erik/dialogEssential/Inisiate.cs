using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inisiate : MonoBehaviour {

    public void createObject()
    {
        Instantiate(gameObject);//, position, Quaternion.identity);
    }

}
