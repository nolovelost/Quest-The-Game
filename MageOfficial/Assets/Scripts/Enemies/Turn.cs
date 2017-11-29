using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {
    public Transform target;
	
	
	// Update is called once per frame
	void Update () {

        if (target.position.x < this.transform.position.x)
        {
            this.transform.GetComponent<SpriteRenderer>().flipX = false;

        }
        else
        {
            this.transform.GetComponent<SpriteRenderer>().flipX = true;
        }

    }
}
