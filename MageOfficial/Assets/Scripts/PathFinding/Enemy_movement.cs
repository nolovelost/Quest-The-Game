﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy_movement : MonoBehaviour
{
   
    public Transform target;
    public float speed = 10;
    Vector3[] path;
    int targetIndex;
    //Vector3 clickTarget = Vector3.zero;
    int clickDebug = 0;

    public static bool recalculate = false;
    
   
    //animation setup
    bool isMoving = false;
    Animator playerAnim;
    //used by panle draw when the panel is Up
   public  bool isCasting = false;
    //   CameraFloow offsetPass;
    //testing smoothing
    Catmul smoothing;

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(path[i], Vector3.one* 0.05f);

                //waypoints
                if (i == targetIndex)
                {
                    Gizmos.DrawLine(this.transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
    void Start()
    {

        PathRequester.RequestPath(this.transform.position, target.position, OnPathFound);
        playerAnim = this.transform.GetComponent<Animator>();
     //   InvokeRepeating("Chase", 1, 2f);
    }
    public void OnPathFound(Vector3[] newPath, bool successful)
    {
        if (successful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    IEnumerator FollowPath()
    {//start at the beginning
        isMoving = true;
        Vector3 currentWaypoint = path[0];

        //reset so can be reused ?
        targetIndex = 0;
        while (true)
        {
            if (this.transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    isMoving = false;
                    yield break;
                    
                }
                else
                {
                    currentWaypoint = path[targetIndex];
                }
            }
            transform.position = Vector3.MoveTowards(this.transform.position, currentWaypoint, speed * Time.deltaTime);
            print("Destination2");
            yield return null;
        }
        
    }

    void Chase()
    {
        Vector3 offsetTarget = target.transform.GetComponent<Unit>().clickTarget;
        //add offset so that it stops near not on the player
        if (offsetTarget.x < this.transform.position.x)
        {
            offsetTarget.x += .2f;
        }
        else
        {
            offsetTarget.x -= .2f;
        }
    PathRequester.RequestPath(this.transform.position, offsetTarget, OnPathFound);
        recalculate = false;
    }

    void Update()
    {

        //animation here 
        playerAnim.SetBool("isMoving", isMoving);
        playerAnim.SetBool("isCasting", isCasting);
           
                       
            
           

                        if (target.position.x < this.transform.position.x)
                        {
                            this.transform.GetComponent<SpriteRenderer>().flipX = false;
                //move the camera depending on direction going
              //  offsetPass.offset.x = -5;
                        }
                        else
                        {
                            this.transform.GetComponent<SpriteRenderer>().flipX = true;
              //  offsetPass.offset.x = 5;

                        }

        if (recalculate )
        {
            Chase();
        }
                    
        Debug.Log(clickDebug);
        
       //redrawing the gizmos for testing
        SceneView.RepaintAll();

    }
}
