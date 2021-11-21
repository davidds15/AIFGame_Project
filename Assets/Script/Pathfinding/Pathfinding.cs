using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    Grid gridRef;
    public Transform startPos;
    public Transform targetPos;

    public List<Node> finalPath;
    private void Awake()
    {
        gridRef = GetComponent<Grid>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FindPath(startPos.position, targetPos.position);
    }
    void FindPath(Vector3 startPosInp, Vector3 targetPosInp)
    {
        Node startNode = gridRef.NodeFromWorldPoint(startPosInp);
        Node targetNode = gridRef.NodeFromWorldPoint(targetPosInp);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);
        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].TotalCost < currentNode.TotalCost || openList[i].TotalCost == currentNode.TotalCost)
                {
                    if (openList[i].heuristicCost < currentNode.heuristicCost)
                    {
                        currentNode = openList[i];
                    }
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            if (currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
                return;
            }
            foreach (Node neighborNode in gridRef.GetNeighboringNodes(currentNode))
            {
                if (neighborNode.IsWall || closedList.Contains(neighborNode))
                {
                    continue;
                }
                int costToNeihbor = currentNode.moveCost + GetManhattanDistance(currentNode, neighborNode);

                if (costToNeihbor < neighborNode.moveCost || !openList.Contains(neighborNode))
                {
                    neighborNode.moveCost = costToNeihbor;
                    neighborNode.heuristicCost = GetManhattanDistance(neighborNode, targetNode);
                    neighborNode.ParentNode = currentNode;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }
    }
    void GetFinalPath(Node startNodeInp, Node endNodeInp)
    {
        finalPath = new List<Node>();
        Node currentNode = endNodeInp;

        while (currentNode != startNodeInp)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }
        finalPath.Reverse();
        gridRef.finalPath = finalPath;
    }
    int GetManhattanDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return distanceX + distanceY;
    }
}
