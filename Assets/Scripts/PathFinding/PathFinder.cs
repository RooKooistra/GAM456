using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public GridMap gridMap = null;

    public GameObject startWorldPoint;
    public GameObject endWorldPoint;
    PlayerMovement playerMovement;

	private void Start()
	{
        playerMovement = startWorldPoint.GetComponent<PlayerMovement>();
	}

	List<Node> GetkNeighbours(Node node) // Ask Cam if this should be in GridMap class
	{
        List<Node> neighbours = new List<Node>();

        // check the 8 squares immediately around the active node
        for(int x = -1; x <= 1; x++)
		{
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0) continue;

                // get node references
                int thisRowX = Mathf.RoundToInt(node.worldPosition.x + x);
                int thisRowZ = Mathf.RoundToInt(node.worldPosition.z + z);

                // check to see if node is inside the grid
                if (thisRowX >= 0 && thisRowX < gridMap.gridRowX && thisRowZ >= 0 && thisRowZ < gridMap.gridRowZ)
				{
                    neighbours.Add(gridMap.grid[thisRowX, thisRowZ]);
				}

            }
        }
        return neighbours;
	}

    float GetDistanceBetweenNodes(Node fromNode,Node toNode)
	{
        float distance = Vector3.Distance(fromNode.worldPosition, toNode.worldPosition);
        return distance;
	}

    void GetFinalPath(Node fromNode, Node toNode)
    {
        // works backwards from endNode then reverse order to get path
        List<Node> finalPath = new List<Node>();

        Node activeNode = toNode;
        finalPath.Add(activeNode);

		while (activeNode != fromNode)
		{
            finalPath.Add(activeNode.parentNode);
            activeNode = activeNode.parentNode;
		}

        finalPath.Reverse();
        gridMap.finalPath = finalPath;
        playerMovement.TravelPath(finalPath);
    }


    public void GetPath(Vector3 from, Vector3 to)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
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
         * foreach neightbour of the current node               -- DONE .. MADE GetNEIGHBOURS
         *  if neighbour is not traversable or neighbour is in closed       
         *      skip to next neightbour                         -- DONE
         * 
         * if new path to neighbour is shorter or neighbour is not in open -- DONE ** not done right.. check over
         *  set f_cost of neighbour (need to set g and h cost as f is calculated)   -- DONE
         *  set parent of neighbour to current      -- DONE
         *  if neighbour is not in open
         *      add neighbour to open       -- DONE
         *
         */

        Node startNode = gridMap.WorldPositiontoNodeReference(from);
        Node endNode = gridMap.WorldPositiontoNodeReference(to);

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        openList.Add(startNode); // add start node at the begining before the while loop

		while (openList.Count != 0)
		{
            Node activeNode = openList[0];

            for(int i = 1; i < openList.Count; i++) // loops through all in current openlist (dont check self)
			{
				if (activeNode.fCost() > openList[i].fCost() || activeNode.fCost() == openList[i].fCost() && activeNode.hCost > openList[i].hCost)
				{
                    activeNode = openList[i]; // roll through the openlist and make the node withe the lowest fcost or lowest hcost if equal fcost active
				}
			}

            openList.Remove(activeNode);
            closedList.Add(activeNode);

            if (activeNode == endNode) // path has been found... 
            {
                GetFinalPath(startNode, endNode);
                
                stopWatch.Stop();
                print(stopWatch.Elapsed.TotalMilliseconds);
                return; 
            }

            foreach(Node neighbour in GetkNeighbours(activeNode)) 
			{
                if (!neighbour.walkable || closedList.Contains(neighbour)) continue; // ignore if blocked or already processed

                if(neighbour.gCost > (activeNode.gCost + GetDistanceBetweenNodes(neighbour, activeNode)) || !openList.Contains(neighbour)) 
				{
                    neighbour.gCost = activeNode.gCost + GetDistanceBetweenNodes(neighbour, activeNode); // update gcost
                    neighbour.hCost = GetDistanceBetweenNodes(neighbour, endNode);
                    neighbour.parentNode = activeNode;

                    if (!openList.Contains(neighbour)) openList.Add(neighbour); // add neighbour to the openList
                }
			}

		}
        
    }
}
