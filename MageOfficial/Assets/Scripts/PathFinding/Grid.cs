using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public LayerMask unwalkableMask;
    // 2 dimensional array
    Node[,] grid;
    public Vector2 gridSize;
    public float nodeRadius;

    public Transform player;

    float nodeDiameter;
        int gridSizeX;
        int gridSizeY;

    public List<Node> path;
    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }
    
   void OnDrawGizmos()
    {   Node playerNode = NodeFromWorldPosition(player.position);
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));
        if (grid!= null)
        {
            foreach (Node n in grid)
            {
                if (n.walkable)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                if (n == playerNode)
                {
                    Gizmos.color = Color.cyan;
                }
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.01f));
            }
           
        }
    }
    
    // Use this for initialization
    void Awake () {
        nodeDiameter = nodeRadius * 2;
        //so we dont get halves
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
   
       

    void CreateGrid()
    {
        grid = new Node[gridSizeX,gridSizeY];
        //helper,get bottom left corner

        //forward to up

        Vector3 bottomLeft = transform.position-Vector3.right*gridSize.x/2 - Vector3.up* gridSize.y/2 ;

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                //build the grid from bottom left node by node
                Vector3 worldPoint = bottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.up * (j *nodeDiameter + nodeRadius);
                //check for collisions, here for 2D use Physics2D.OverlapCircle, for 3D  checkSphere﻿
                //if there is true collision return walkable false
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius,unwalkableMask));
                //make new node at each position
                grid[i, j] = new Node(walkable, worldPoint,i,j);
            }

        }
    }
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        //3 by 3 square check (all around so -1 to 1),centre 0,0 can be ignored
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                int checkX = node.gridX + i;
                int checkY = node.gridY + j;
                //is in in grid ? then is a neighbour
                if (checkX >=0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;

        //where is the node
    }
 public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        
        //0 to 100 from left to right
        float percentX = (worldPosition.x + gridSize.x / 2) / gridSize.x;
        //from bottom to top
        float percentY = (worldPosition.y + gridSize.y / 2) / gridSize.y;
        //clamp so it doesnt go wonky
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        //Grid position
        //ist an array so 0 => 1
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return (grid[x, y]);
    }

}
