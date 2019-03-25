using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class specialButton : MonoBehaviour {
    [SerializeField] Sprite activeB;
    [SerializeField] Sprite deactiveB;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public IEnumerator playSound(float lengthOfClipp)
    {
        gameObject.GetComponent<Image>().sprite = activeB; 
        yield return new WaitForSeconds(lengthOfClipp);
        gameObject.GetComponent<Image>().sprite = deactiveB;
    }

}
