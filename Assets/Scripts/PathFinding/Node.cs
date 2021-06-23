using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

	public bool walkable;
	public bool cover = false;
	public Vector3 worldPosition;
	public Node parentNode = null;

	public int gCost; // distance to start
	public int hCost; // distance to end (heuristic)
	public int fCost()   // combined g and h cost maybe make a getter
	{
		return gCost + hCost;
	}
		public Node(bool isWalkable, Vector3 nodeWorldPosition)
		{
			walkable = isWalkable;
			worldPosition = nodeWorldPosition;
		}

}
