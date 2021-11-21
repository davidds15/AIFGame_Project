using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public int gridX;
    public int gridY;

    public bool IsWall;
    public Vector3 worldPosition;
    public Node ParentNode;

    public int moveCost;
    public int heuristicCost;

    public int TotalCost
    {
        get
        {
            return moveCost + heuristicCost;
        }
    }

    public Node(bool IswallInp, Vector3 worldPositionInp, int gridXInp, int gridYinp)
    {
        IsWall = IswallInp;
        worldPosition = worldPositionInp;
        gridX = gridXInp;
        gridY = gridYinp;
    }

}
