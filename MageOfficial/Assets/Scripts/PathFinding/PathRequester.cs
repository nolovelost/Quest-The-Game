using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequester : MonoBehaviour {

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    //try adding a separate queue for enemies
    Queue<PathRequest> enemyPathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    PathRequest currentEnemyPathRequest;
    //to control and call this class
    static PathRequester instance;
    //to get the actual pathfinding
      PathFinding pathfinder;
    //check flag for queue1
    bool isProcessingPath;
    //for enemy
    bool isProcessingEnemyPath;

    void Awake()
    {
        instance = this;
        pathfinder = GetComponent<PathFinding>();
    }

    public static void RequestPath(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(start, end, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    public static void enemyRequestPath(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
    {
        PathRequest enemyNewRequest = new PathRequest(start, end, callback);
        instance.enemyPathRequestQueue.Enqueue(enemyNewRequest);
        instance.TryProcessEnemyNext();
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
        {
            pathStart = start;
            pathEnd = end;
            this.callback = callback;
        }
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count >0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinder.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }
    //enemy
    void TryProcessEnemyNext()
    {
        if (!isProcessingEnemyPath && enemyPathRequestQueue.Count > 0)
        {
            currentEnemyPathRequest = enemyPathRequestQueue.Dequeue();
            isProcessingEnemyPath = true;
            pathfinder.StartFindPath(currentEnemyPathRequest.pathStart, currentEnemyPathRequest.pathEnd);
        }
    }



    public void FinishedProcessingPath(Vector3[] path,bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    public void FinishedProcessingEnemyPath(Vector3[] path, bool success)
    {
        currentEnemyPathRequest.callback(path, success);
        isProcessingEnemyPath = false;
        TryProcessEnemyNext();
    }
}
