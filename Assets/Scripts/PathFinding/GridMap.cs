using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
	public Node[,] grid;

	public List<Node> openList = new List<Node>();
	public List<Node> closedList = new List<Node>();
	public int gridRowX;
	public int gridRowZ;

	public GameObject player;

	public float boxSize = .9f;
	public LayerMask layerMask;

	public List<Node> finalPath = new List<Node>();


	PathFinder pathfinder;


	private void Start()
	{
		pathfinder = GetComponent<PathFinder>();
	}

	public void BuildGrid(int rowsX, int rowsZ)
	{
		gridRowX = rowsX; gridRowZ = rowsZ; // set these variables to be accessed from pathfinder in the neighbourcheck;

		if (grid != null) grid = null;

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

		while (true)
		{
			Node node = grid[Random.Range(0, rowsX), Random.Range(0, rowsZ)];
			if(node.walkable == true)
			{
				player.transform.position = node.worldPosition;
				break;
			}
		}
	}

	// convert worldPosition to node reference

	public Node WorldPositiontoNodeReference(Vector3 worldPos)
	{
		return grid[Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.z)];
	}

	// use grid for path finding shenanningans - Do path finding in another script - think about multiple agents.
	
	private void OnDrawGizmos()
	{
		if (grid != null)
		{
			foreach (Node node in grid)
			{
				Gizmos.color = (node.walkable) ? Color.green : Color.red;
				if (finalPath != null && finalPath.Contains(node)) Gizmos.color = Color.magenta;
				Gizmos.DrawWireCube(node.worldPosition, new Vector3(boxSize, boxSize, boxSize));
			}
		}
	}
	
}
