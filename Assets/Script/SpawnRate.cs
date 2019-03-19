using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRate : MonoBehaviour
{

    [Range(0.0f, 1.0f)] public float spawnRate = 0.5f;
    public GameObject[] children = new GameObject[2];
    private void Start()
    {
        if (spawnRate > Random.Range(0.0f, 0.99f))
        {
            foreach (GameObject gObject in children)
            {
                Debug.Log("Derp");
                gObject.SetActive(true);
            }
        }
    }

}
