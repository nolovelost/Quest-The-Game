using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{
    public bool walkable;
    public Vector3 worldPosition;
    //position in the grid for neighbour calculations
    public int gridX;
    public int gridY;
    //parent variable for path storing
    public Node parent;

    //distance from start node
    public int gCost;
    //distance from end node
    public int hCost;
    public Node(bool pathable, Vector3 position,int posX, int posY)
    {
        walkable = pathable;
        worldPosition = position;
        gridX = posX;
        gridY = posY;
    }
    public int FCost()
    {
        return gCost + hCost;
    }
}
