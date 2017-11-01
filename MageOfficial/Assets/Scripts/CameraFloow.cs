using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFloow : MonoBehaviour {
    public Transform target;
    public float smooth = 0.125f;
    public Vector3 offset;
    //edges of screen bounds
    public float boundLeft;
    public float boundRight;
  
    //runs after Update
    void FixedUpdate() {
        if (!target)
        {
            return;
        }

        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, 0) + offset;


        
       // Vector3 desiredPosition = new Vector3(target.position.x,0,target.position.z) + offset;
       Vector3 smoothedPosition = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, 0), desiredPosition, smooth);
        transform.position = smoothedPosition;

       // transform.LookAt(target);
    }
}
