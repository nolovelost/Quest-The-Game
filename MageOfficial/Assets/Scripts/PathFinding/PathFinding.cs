using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinding : MonoBehaviour {

    Grid grid;
    PathRequester manager;
   // public Transform seeker;
   // public Transform target;
    void Awake()
    {
        //must be on the same game object
        grid = GetComponent<Grid>();
        manager = GetComponent<PathRequester>();
    }
    
   
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos,targetPos));
    }
    IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        //for the manager
        Vector3[] waypoints = new Vector3[0];
        bool pathFound = false;
        //get the start and end path
        Node startNode = grid.NodeFromWorldPosition(startPosition);
        Node targetNode = grid.NodeFromWorldPosition(targetPosition);
        //OPTIMIZATION ONLY RUN WHEN BOTH WALKABLE
        if (startNode.walkable && targetNode.walkable)
        {


            /*
            ALGORITHM START 
             */

            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);            //no order no duplication high preformance data 
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);
            //while open not empty
            while (openSet.Count > 0)
            {   //first element
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    pathFound = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                    }
                }
            }
            yield return null;
            if (pathFound)
            {
                waypoints = RetracePath(startNode, targetNode);
                //  manager.FinishedProcessingEnemyPath(waypoints, pathFound);
            }

            
        manager.FinishedProcessingPath(waypoints, pathFound);
    }
    /*
     
         TRY DUPLICATING
         
         */
    public void StartFindEnemyPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindEnemyPath(startPos, targetPos));
    }

    IEnumerator FindEnemyPath(Vector3 startPosition, Vector3 targetPosition)
    {
        //for the manager
        Vector3[] waypoints = new Vector3[0];
        bool pathFound = false;
        //get the start and end path
        Node startNode = grid.NodeFromWorldPosition(startPosition);
        Node targetNode = grid.NodeFromWorldPosition(targetPosition);
        //OPTIMIZATION ONLY RUN WHEN BOTH WALKABLE
        if (startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);


            /*
            ALGORITHM START 
             */

           
            //no order no duplication high preformance data 
            
            //while open not empty
            while (openSet.Count > 0)
            {   //first element
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                  
                  
                    pathFound = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
        yield return null;
        if (pathFound)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        manager.FinishedProcessingEnemyPath(waypoints, pathFound);
    }
    /*



    DUPLICATION END


    */

    Vector3[]  RetracePath(Node startNode, Node endNode)
    {
        //the nodes making the path
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        //trace parents
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        //simplify
       Vector3[] waypoints =  SimplifyPath(path);
        //its the other way around
        //path.Reverse();
        Array.Reverse(waypoints);
        grid.path = path;
        return waypoints;
    }
    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints =  new List<Vector3>();
        Vector2 oldDirection = Vector2.zero;
        //check if direction changes and only add waypoint if it does
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 newDirection = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (newDirection != oldDirection)
            {//add the last one as well so -1
                waypoints.Add(path[i-1].worldPosition);
            }
            oldDirection = newDirection;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node a, Node b)
    {
        //how many nodes away on x, and on y, Y gives us diagonal
        
        int distanceX = Mathf.Abs(a.gridX - b.gridX);
        int distanceY = Mathf.Abs(a.gridY - b.gridY);
        //check which is bigger. use the pythagoras maths -> diagonal 1.4 all multiplied by 10
        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }

    }

}
