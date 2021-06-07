using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWorldBuilder : MonoBehaviour
{
	[SerializeField]
	private GameObject BuildingBlockPrefab = null;
	[SerializeField]
	private GameObject Floor = null;
	[SerializeField]

	public List<GameObject> WorldBuildingBlocks = new List<GameObject>();

	public static event Action<int,int> WorldBeingBuilt;


	public void BuildWorld(int rows, int columns, float blockDensity, bool warpBlocks)
	{
		WorldBeingBuilt?.Invoke(rows, columns);

		if (WorldBuildingBlocks.Count != 0) DestroyWorld();

		for (int i = 0; i < columns; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				GameObject FloorInstance = Instantiate(Floor, new Vector3(i, -.5f, j), Quaternion.identity);
				WorldBuildingBlocks.Add(FloorInstance);

				bool Boolean = UnityEngine.Random.value <= blockDensity;
				if (Boolean)
				{
					GameObject BuildingBlockInstance = Instantiate(BuildingBlockPrefab, new Vector3(i, 0, j), Quaternion.identity);
					if(warpBlocks)
					{
						BuildingBlockInstance.transform.localScale = new Vector3(UnityEngine.Random.Range(.25f, 2.5f), 1f, UnityEngine.Random.Range(.25f, 2.5f));
						BuildingBlockInstance.transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 360f), 0);
					}
							
					WorldBuildingBlocks.Add(BuildingBlockInstance);
				}

			}
		}

	}

	public void DestroyWorld()
	{
		foreach(GameObject WorldBuildingBlock in WorldBuildingBlocks)
		{
			Destroy(WorldBuildingBlock);
		}
		WorldBuildingBlocks.Clear();
	}

}
