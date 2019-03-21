using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeIn : MonoBehaviour
{

    SpriteRenderer sr;
    public float fadeInSpeed = 1.0f;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = sr.color;
        color.a += Time.deltaTime * fadeInSpeed;
        sr.color = color;
    }
}
