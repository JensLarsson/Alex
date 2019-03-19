using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinding : MonoBehaviour {

    PathRequestManager pathRequestManager;
    Grid grid;

	List<Vector3> targetPositionFails = new List<Vector3>();
	List<Vector3> targetPositionSuccess = new List<Vector3>();

    void Awake()
    {
        pathRequestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }

    public void StartFindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        StartCoroutine(FindPath(startPosition, targetPosition));
    }

    IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromeWorldPoint(startPosition);
        Node targetNode = grid.NodeFromeWorldPoint(targetPosition);
		if (!targetNode.walkable) {
			targetPositionFails.Add (targetPosition);
		}

		if (targetNode.walkable)
        {
			targetPositionSuccess.Add (targetPosition);
			/*if (!startNode.walkable) {
				startNode = FindNearestWakableNode (startNode);
			}*/

            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
				{
					Debug.Log ("wtf");
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = getDistance(currentNode, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            wayPoints = RetracePath(startNode, targetNode);
        }
        pathRequestManager.FinishedProcessingPath(wayPoints, pathSuccess);
    }

	Node FindNearestWakableNode(Node node)
	{
		Node temp = node;
		foreach (Node neighbour in grid.GetNeighbours(node)) {
			if (neighbour.walkable) {
				temp = neighbour;
				break;
			}
		}
		return temp;
	}

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i-1].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int getDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }

	/*public void OnDrawGizmos()
	{
		if (targetPositionFails != null)
		{
			for (int i = 0; i < targetPositionFails.Count; i++)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawCube(targetPositionFails[i], new Vector3(0.1f, 0.1f));
			}
		}
		if (targetPositionSuccess != null) {
			for (int i = 0; i < targetPositionSuccess.Count; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(targetPositionSuccess[i], new Vector3(0.1f, 0.1f));
			}
		}
	}*/
}
