using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGlitchout : MonoBehaviour
{

    public Material material;
    public float glitchTime = 1.0f;
    public AudioClip[] clip = new AudioClip[0];
    Timer timer = new Timer();

    private void OnEnable()
    {
        timer.Duration = glitchTime;
        AudioManager.instance.playSFXClip(clip[Random.Range(0, clip.Length)], true);
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
