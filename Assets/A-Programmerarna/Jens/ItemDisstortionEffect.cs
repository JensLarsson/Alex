using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisstortionEffect : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] Image image;
    public float minWait = 0.1f, maxWait = 1.0f, minDuration = 0.1f, maxDuration = 0.3f;
    public Material baseMat, effectMat;
    public AudioClip[] clip = new AudioClip[0];

    public enum TypeOfElement
    {
        SpriteR,
        Image
    }
    public TypeOfElement typeOfElement = TypeOfElement.SpriteR;

    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
        startEffect();
    }

    public void startEffect()
    {

        StartCoroutine(effectStepOne());
    }


    IEnumerator effectStepOne()
    {
        Debug.LogWarning("step 1");
        yield return new WaitForSeconds(Random.Range(minWait, maxWait));
        if (clip.Length > 0)
        {
            int rand = Random.Range(0, clip.Length);
            AudioManager.instance.playSFXClip(clip[rand], true);
            maxDuration = clip[rand].length;
            maxDuration = clip[rand].length;
        }
        if (typeOfElement == TypeOfElement.SpriteR)
        {
            StartCoroutine(useEffect());
        }
        else if (typeOfElement == TypeOfElement.Image)
        {
            StartCoroutine(useImageEffect());
        }
    }
    IEnumerator useImageEffect()
    {
        Debug.LogWarning("step 2");
        image.material = effectMat;
        yield return new WaitForSeconds(Random.Range(minDuration, maxDuration));
        image.material = null;
        StartCoroutine(effectStepOne());
    }
    IEnumerator useEffect()
    {
        sr.material = effectMat;
        yield return new WaitForSeconds(Random.Range(minDuration, maxDuration));
        sr.material = baseMat;
        StartCoroutine(effectStepOne());
    }
}
