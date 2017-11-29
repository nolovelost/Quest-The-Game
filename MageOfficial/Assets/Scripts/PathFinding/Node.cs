using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>﻿{
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
    int heapIndex;
    public Node(bool pathable, Vector3 position,int posX, int posY)
    {
        walkable = pathable;
        worldPosition = position;
        gridX = posX;
        gridY = posY;
    }
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
