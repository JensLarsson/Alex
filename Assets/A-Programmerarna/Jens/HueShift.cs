using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueShift : MonoBehaviour
{

    SpriteRenderer sr;
    public float speed = 1.0f;
    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float H, S, V;
        Color.RGBToHSV(sr.color, out H, out S, out V);
        H += Time.deltaTime * speed;
        sr.color = Color.HSVToRGB(H, S, V);
    }
}
