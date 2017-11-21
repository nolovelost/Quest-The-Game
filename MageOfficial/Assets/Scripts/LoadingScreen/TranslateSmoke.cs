using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateSmoke : MonoBehaviour {

    public float speed = 160.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (speed < 0 && gameObject.GetComponent<RectTransform>().position.x > -350)
            gameObject.GetComponent<RectTransform>().transform.Translate(speed * Time.deltaTime, 0, 0);
        if (speed > 0 && gameObject.GetComponent<RectTransform>().position.x < 390)
            gameObject.GetComponent<RectTransform>().transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
