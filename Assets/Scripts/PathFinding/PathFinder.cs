using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public GridMap gridMap = null;

    public GameObject startWorldPoint;
    public GameObject endWorldPoint; 

    List<Node> GetkNeighbours(Node node) // this might be better in gridmap script
	{
        List<Node> neighbours = new List<Node>();

        // check the 8 squares immediately around the active node
        for(int x = -1; x <= 1; x++)
		{
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0) continue;

                // check to see if node is inside the grid
                int thisRowX = Mathf.RoundToInt(node.worldPosition.x) + x;
                int thisRowZ = Mathf.RoundToInt(node.worldPosition.z) + z;

				if (thisRowX >= 0 && thisRowX < gridMap.gridRowX && thisRowZ >= 0 && thisRowZ < gridMap.gridRowZ)
				{
                    neighbours.Add(gridMap.grid[thisRowX, thisRowZ]);
				}

            }
        }
        return neighbours;
	}

    int GetDistanceBetweenNodes(Node fromNode,Node toNode)
	{
        float distance = Vector3.Distance(new Vector3(fromNode.x, 0, fromNode.z), new Vector3(toNode.x, 0, toNode.z));
        return Mathf.RoundToInt(distance);
	}

    void GetPath()
	{

        /* Notes from Reference
         * 
         * OPEN // the set of nods to be evaluated          -- DONE
         * CLOSED // the set of nodes already evaluated     -- DONE
         * add start node to open                           -- DONE
         * 
         * 
         * loop
         * current = node in open with lowest f_cost    -- cost needs to be in Node class?? -- DONE LOGIC BUT NO COST CALCULATIONS
         * remove current from open             -- DONE
         * add current to closed                -- DONE
         * 
         * if current is the target node // path has been found         -- DONE, HAVE NOT DONE LOGIC AFTER FOUND TARGET
         *  return
         * 
         * foreach neightbour of the current node               -- DONE .. MADE CGetNEIGHBOURS
         *  if neighbour is not traversable or neighbour is in closed
         *      skip to next neightbour
         * 
         * if new path to neighbour is shorter or neighbour is not in open
         *  set f_cost of neighbour
         *  set parent of neighbour to current
         *  if neighbour is not in open
         *      add neighbour to open
         *
         */

        Node startNode = gridMap.WorldPositiontoNodeReference(startWorldPoint.transform.position);
        Node endNode = gridMap.WorldPositiontoNodeReference(endWorldPoint.transform.position);

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        openList.Add(startNode); // add start node at the begining before the while loop

		while (openList.Count != 0)
		{
            Node activeNode = openList[0];

            for(int i = 0; i < openList.Count; i++) 
			{
				if (activeNode.fCost() > openList[i].fCost() || activeNode.fCost() == openList[i].fCost() && activeNode.hCost > openList[i].hCost)
				{
                    activeNode = openList[i]; // roll through the openlist and make the node withe the lowest fcost or lowest hcost if equal fcost active
				}
			}

            openList.Remove(activeNode);
            closedList.Add(activeNode);

            if (activeNode == endNode) return; // path has been found... start the process of following the path.

            foreach(Node neighbour in GetkNeighbours(activeNode)) 
			{
                if (!neighbour.walkable || closedList.Contains(neighbour)) continue;
			}
		}
    }

}
