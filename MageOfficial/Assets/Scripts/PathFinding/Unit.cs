﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Unit : MonoBehaviour
{
   
    public Transform target;
    public float speed = 10;
    Vector3[] path;
    int targetIndex;
   public Vector3 clickTarget = Vector3.zero;
    int clickDebug = 0;
     public Camera cam;
    //used in drawPanel to stop movement
    public bool canMove = true;
    //animation setup
    bool isMoving = false;
    Animator playerAnim;
    //used by panle draw when the panel is Up
   public  bool isCasting = false;
    //   CameraFloow offsetPass;
    //testing smoothing
    Catmul smoothing;
    bool leftFacing = true;
    bool RightFacing = false;

    //idnicator
    public GameObject indicator;
    GameObject current;
    Vector3 pos;


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
        playerAnim = this.transform.GetComponent<Animator>();
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
        isMoving = true;
        Vector3 currentWaypoint = path[0];
        pos = path[path.Length-1];
        StartCoroutine("Indicate");
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

    void Update()
    {

        //animation here 
        playerAnim.SetBool("isMoving", isMoving);
        playerAnim.SetBool("isCasting", isCasting);
             if (Input.GetMouseButtonUp(0) && canMove)
                    {
            clickDebug++;
                        clickTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickTarget.z = transform.position.z;
            pos = clickTarget;
          //  StartCoroutine("Indicate");
            
            //move the camera depending on direction going
           
         //   clickTarget.z = transform.position.z;
            Enemy_movement.recalculate = true;
          //  bool leftFacing = true;
         //   bool RightFacing = false;
            if (clickTarget.x < this.transform.position.x)
            { RightFacing = false;
                if (!leftFacing)
                {
                 this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
                                leftFacing = true;
                               
                }
               
            }
            else
            {leftFacing = false;
                if (!RightFacing)
                {
                    this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
                    RightFacing = true;
                }
                
                

            }

                /*
                            if (clickTarget.x < this.transform.position.x)
                            {
                    if (!leftFacing)
                    {
                        this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
                    }
                    leftFacing = true;
                    RightFacing = false;
                  //  this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
                                //this.transform.GetComponent<SpriteRenderer>().flipX = false;
                    //move the camera depending on direction going
                  //  offsetPass.offset.x = -5;
                            }
                            else
                            {
                    if (!RightFacing)
                    {
                        this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
                    }
                    RightFacing = true;
                    leftFacing = false;
                  //  this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
                    //  this.transform.GetComponent<SpriteRenderer>().flipX = true;
                    //  offsetPass.offset.x = 5;

                }
                            */
                PathRequester.RequestPath(this.transform.position, clickTarget, OnPathFound);
               
                    }
        Debug.Log(clickDebug);
        
       //redrawing the gizmos for testing
        SceneView.RepaintAll();

    }
    IEnumerator Indicate()
    {//destroy old one
        if (current != null)
        {
            Destroy(current);
        }
       
        current = Instantiate(indicator, pos, Quaternion.identity);
        Destroy(current, 1.5f);
        yield return null;


    }
}
