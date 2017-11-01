using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbitraryMove : MonoBehaviour {

    public float speed = 6.0f;            // The speed that the player will move at.

    Vector3 movement;
    Rigidbody2D playerRigidbody;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
	}

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, v, 0.0f);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }
}
