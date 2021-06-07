using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : MonoBehaviour
{
	
	public class Node

	{
		public bool walkable;
		public Vector3 worldPosition;
		public Node(bool isWalkable, Vector3 nodeWorldPosition)
		{
			walkable = isWalkable;
			worldPosition = nodeWorldPosition;
		}
	} 

	public Node[,] grid;

	public float boxSize =.9f;
	public LayerMask layerMask;
	public GameObject startPoint;
	public GameObject endPoint;

	public void BuildGrid(int columns, int rows)
	{
		// build grid

		grid = new Node[columns,rows];

		for (int i = 0; i < columns; i++)
		{
			for(int j = 0; j < rows; j++)
			{
				Vector3 worldPoint = new Vector3(i, 0, j);

				bool walkable = !(Physics.CheckSphere(worldPoint, (boxSize / 2), layerMask));

				grid[i, j] = new Node(walkable, worldPoint);

			}
		}


		// get list of neighbours


		// use grin for path finding shenanningans

	}



	private void OnDrawGizmos()
	{
		if(grid != null)
		{
			foreach(Node n in grid)
			{
				Gizmos.color = (n.walkable) ? Color.green : Color.red;
				Gizmos.DrawWireCube(n.worldPosition, new Vector3(boxSize, boxSize, boxSize));
			}
		}
	} 

}
