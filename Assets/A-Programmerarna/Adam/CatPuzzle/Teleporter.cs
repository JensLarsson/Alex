using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleporter : MonoBehaviour {

    public GameObject outTeleporter;

    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = outTeleporter.transform.position;
        if (other.tag == "Cat")
        {
            other.GetComponent<CatAI>().MakePathRequest(other.transform.position, other.transform.position, other.GetComponent<CatAI>().speed);
        }
    }
}
