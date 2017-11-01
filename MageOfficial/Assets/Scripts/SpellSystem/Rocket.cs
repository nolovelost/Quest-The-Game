using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    public Rigidbody2D rocket;
    public float speed = 10f;

    void FireRocket()
    {
        Rigidbody2D rocketClone = (Rigidbody2D)Instantiate(rocket, transform.position, transform.rotation);
        rocketClone.velocity = transform.forward * speed;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("CastSpell"))
        {
            FireRocket();
        }
    }
}
