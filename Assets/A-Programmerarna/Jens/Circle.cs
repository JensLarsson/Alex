using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    const int size = 500;
    Vector3[] outerCircePositions = new Vector3[size];
    Vector3[] innerCirclePositions = new Vector3[size / 2];
    public Material material;
    public float radius = 5;
    Color col = new Color(1, 0, 1);
    // Use this for initialization
    void Start()
    {
        float pi = Mathf.PI * 2;

        float f = pi / outerCircePositions.Length;
        for (int i = 0; i < outerCircePositions.Length; i++)
        {
            outerCircePositions[i].y = Mathf.Sin(f * i) * radius + 1.7f;
            outerCircePositions[i].x = Mathf.Cos(f * i) * radius - 5.5f;
        }

        f = pi / innerCirclePositions.Length;
        for (int i = 0; i < innerCirclePositions.Length; i++)
        {
            innerCirclePositions[i].y = Mathf.Sin(f * i) * radius / 2 + 1.7f;
            innerCirclePositions[i].x = Mathf.Cos(f * i) * radius / 2 - 5.5f;
        }
    }



    public void OnPostRender()
    {
        //q += Mathf.Sin(Time.time * 000000.1f);
        for (int i = 0; i < size; i++)
        {
            int j = (int)(i * Time.time * 0.1f);
            while (j >= size * 0.5f)
            {
                j -= (int)(size * 0.5f);
            }
            drawLines(outerCircePositions[i], innerCirclePositions[j]);
        }

    }

    void drawLines(Vector3 pointA, Vector3 pointB)
    {
        GL.Begin(GL.LINES);
        material.SetPass(0);
        GL.Color(col);
        GL.Vertex(pointA);
        GL.Color(Color.cyan);
        GL.Vertex(pointB);

        GL.End();
    }
}