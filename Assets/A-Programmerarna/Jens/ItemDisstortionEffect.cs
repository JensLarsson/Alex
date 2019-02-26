using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisstortionEffect : MonoBehaviour
{


    SpriteRenderer sr;
    Timer timer = new Timer();

    public float minWait=0.1f, maxWait = 1.0f, minDuration = 0.1f, maxDuration = 0.3f;
    public Material baseMat, effectMat;


    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        timer.Time += Time.deltaTime;
        if (timer.expireRepeat(Random.Range(minWait, maxWait)))
        {
            StartCoroutine(useEffect());
        }
    }

    IEnumerator useEffect()
    {
        sr.material = effectMat;
        yield return new WaitForSeconds(Random.Range(minDuration, maxDuration));
        sr.material = baseMat;
    }
}
