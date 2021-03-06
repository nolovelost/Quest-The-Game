﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour {

    private GameObject lockedTarget;
    private Rigidbody2D RocketRigidBody;
    private Vector3 HomingDirection;
    private bool isTargetAcquired;

    public float speed = 1.0f;

    void Awake()
    {
        RocketRigidBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {
        AcquireTarget();
	}
	
	// Update is called once per frame
	void Update () {
        if (lockedTarget)
        {
            AcquireTarget();
            TurnToTarget();
            MoveToTarget();
            if (TargetIsReached())
            {
                DestroyObject(this.gameObject);
            }
        }
        else
        {
            Debug.LogError("lockedTarget is null!");
        }
    }

    private void AcquireTarget()
    {
        //find the game controller
        lockedTarget = GameObject.Find("Player");
        
        if (lockedTarget)
        {
            isTargetAcquired = true;

            HomingDirection = lockedTarget.GetComponent<Transform>().position - transform.position;
        }
    }

    void TurnToTarget()
    {
        // For human readable rotation calculation
        Vector3 TempDir = lockedTarget.transform.InverseTransformDirection(HomingDirection);
        float angle = Mathf.Atan2(TempDir.y, TempDir.x) * Mathf.Rad2Deg;

        // The actual rotator
        float rot_z = Mathf.Atan2(HomingDirection.y, HomingDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

        // print(angle);   // Prints the angle in degrees between the two [-180, 180]
    }

    void MoveToTarget()
    {
        // Normalise the movement vector and make it proportional to the speed per second.
        HomingDirection = HomingDirection.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        RocketRigidBody.MovePosition(transform.position + HomingDirection);
    }

    bool TargetIsReached()
    {
        if ((int)transform.position.x == (int)lockedTarget.transform.position.x && (int)transform.position.y == (int)lockedTarget.transform.position.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }




    /** Find the smallest angle between two headings (in degrees)
     * 
     *          Unused Code
     *  */
    float FindDeltaAngleDegrees(float A1, float A2)
    {
        // Find the difference
        float Delta = A2 - A1;

        // If change is larger than 180
        if (Delta > 180.0f)
        {
            // Flip to negative equivalent
            Delta = Delta - (360.0f);
        }
        else if (Delta < -180.0f)
        {
            // Otherwise, if change is smaller than -180
            // Flip to positive equivalent
            Delta = Delta + (360.0f);
        }

        // Return delta in [-180,180] range
        return Delta;
    }

    /*
            My UNREAL c++ code. Used for reference .

    float FindLookAtAngle2D(Vector3 Start, Vector3 Target)
    {
	    Vector3 Normal = (Target - Start).normalized;
	    if (Normal != new Vector3(0.0f, 0.0f, 0.0f))
	    {
		    return RadiansToDegrees(Atan2(Normal.y, Normal.x));
	    }
	    return 0.0f;
    }
    */

}

