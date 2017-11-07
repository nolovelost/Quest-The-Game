﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Unit : MonoBehaviour
{
    bool moving = false;
    public Transform target;
    public float speed = 10;
    Vector3[] path;
    int targetIndex;
    Vector3 clickTarget = Vector3.zero;
    int clickDebug = 0;
     public Camera cam;
    //used in drawPanel to stop movement
    public bool canMove = true;
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
        //  PathRequester.RequestPath(this.transform.position, target.position, OnPathFound);
        //  offsetPass = cam.GetComponent<CameraFloow>();
        smoothing = this.transform.GetComponent<Catmul>();
    }
    public void OnPathFound(Vector3[] newPath, bool successful)
    {
        if (successful)
        {
            path = newPath;
            //in case its running
          //  path = smoothing.CatmulRomCalculate(new Vector2(path[0].x,path[0].y),new Vector2(path[path.Length-1].x, path[path.Length-1].y));
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    IEnumerator FollowPath()
    {//start at the beginning
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
                    yield break;
                }
                else
                {
                    currentWaypoint = path[targetIndex];
                }
            }
            transform.position = Vector3.MoveTowards(this.transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }

    }

    void Update()
    {
       
             if (Input.GetMouseButtonUp(0) && canMove)
                    {
            clickDebug++;
                        clickTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //move the camera depending on direction going
           
            clickTarget.z = transform.position.z;
                        if (clickTarget.x < this.transform.position.x)
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
                        PathRequester.RequestPath(this.transform.position, clickTarget, OnPathFound);
               
                    }
        Debug.Log(clickDebug);
        
       //redrawing the gizmos for testing
        SceneView.RepaintAll();

    }
}
