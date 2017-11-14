using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfInflictDamage_test : MonoBehaviour
{

    RadialPlayerHealth playerHealth;

	// Use this for initialization
	void Start ()
    {
        playerHealth = GetComponent<RadialPlayerHealth>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("ToggleInventory"))
        {
            print("Damaged!!");
            playerHealth.TakeDamage(10);
        }
	}
}
