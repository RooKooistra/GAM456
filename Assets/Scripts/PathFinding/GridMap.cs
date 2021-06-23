using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
	/* public class Node  // may seperate this to its own script later

	{
		public bool walkable;
		public bool cover = false;
		public Vector3 worldPosition;
		public Node(bool isWalkable, Vector3 nodeWorldPosition)
		{
			walkable = isWalkable;
			worldPosition = nodeWorldPosition;
		}
	} */

	public Node[,] grid;

	public List<Node> openList = new List<Node>();
	public List<Node> closedList = new List<Node>();
	public int gridRowX;
	public int gridRowZ;

	public float boxSize = .9f;
	public LayerMask layerMask;
	// public GameObject startPoint;
	// public GameObject endPoint; -- Move these to pathFinding script

	public void BuildGrid(int rowsX, int rowsZ)
	{
		gridRowX = rowsX; gridRowZ = rowsZ; // set these variables to be accessed from pathfinder in the neighbourcheck;

		// build grid
		grid = new Node[rowsX, rowsZ];

		for (int x = 0; x < rowsX; x++)
		{
			for (int z = 0; z < rowsZ; z++)
			{
				Vector3 worldPos = new Vector3(x, 0, z);
				bool walkable = !(Physics.CheckSphere(worldPos, (boxSize / 2), layerMask));

				grid[x, z] = new Node(walkable, worldPos);

			}
		}
	}

	// convert worldPosition to node reference

	public Node WorldPositiontoNodeReference(Vector3 worldPos)
	{
		return grid[Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.z)];
	}


	// get list of neighbours


	// use grid for path finding shenanningans - Do path finding in another script - think about multiple agents.

	


	private void OnDrawGizmos()
	{
		if (grid != null)
		{
			foreach (Node node in grid)
			{
				Gizmos.color = (node.walkable) ? Color.green : Color.red;
				Gizmos.DrawWireCube(node.worldPosition, new Vector3(boxSize, boxSize, boxSize));
			}
		}
	}

}
