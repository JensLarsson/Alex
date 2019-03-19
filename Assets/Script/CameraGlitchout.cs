using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGlitchout : MonoBehaviour
{

    public Material material;
    public float glitchTime = 1.0f;
    Timer timer = new Timer();

    private void OnEnable()
    {
        timer.Duration = glitchTime;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

    void Update()
    {
        timer.Time += Time.deltaTime;
        if (timer.expired)
        {
            Debug.Log("Derp");
            GetComponent<CameraGlitchout>().enabled = false;
        }
    }
}
