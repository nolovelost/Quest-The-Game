using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour {

    Quaternion CurrentRotation;
    public float speed = 100.0f;

	// Use this for initialization
	void Start ()
    {
        //CurrentRotation = gameObject.GetComponent<RectTransform>().rotation();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //CurrentRotation = gameObject.GetComponent<RectTransform>().rotation();
        //CurrentRotation = Quaternion.eulerAngles(CurrentRotation.euler.x, CurrentRotation.euler.y, CurrentRotation.euler.z + (speed * Time.deltaTime));

        gameObject.GetComponent<RectTransform>().transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
