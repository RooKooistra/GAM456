using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
	public class Node

	{
		public bool walkable;
		public bool cover = false;
		public Vector3 worldPosition;
		public Node(bool isWalkable, Vector3 nodeWorldPosition)
		{
			walkable = isWalkable;
			worldPosition = nodeWorldPosition;
		}
	}

	public Node[,] grid;

	public List<Node> openList = new List<Node>();
	public List<Node> closedList = new List<Node>();


	public float boxSize = .9f;
	public LayerMask layerMask;
	public GameObject startPoint;
	public GameObject endPoint;

	public void BuildGrid(int rowsX, int rowsZ)
	{
		// build grid

		grid = new Node[rowsX, rowsZ];

		for (int x = 0; x < rowsX; x++)
		{
			for (int z = 0; z < rowsZ; z++)
			{
				Vector3 worldPoint = new Vector3(x, 0, z);
				bool walkable = !(Physics.CheckSphere(worldPoint, (boxSize / 2), layerMask));

				grid[x, z] = new Node(walkable, worldPoint);

			}
		}


		// get list of neighbours


		// use grid for path finding shenanningans

	}



	private void OnDrawGizmos()
	{
		if (grid != null)
		{
			foreach (Node n in grid)
			{
				Gizmos.color = (n.walkable) ? Color.green : Color.red;
				Gizmos.DrawWireCube(n.worldPosition, new Vector3(boxSize, boxSize, boxSize));
			}
		}
	}

}
