using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWorldBuilder : MonoBehaviour
{
	[SerializeField]
	GridMap gridMap;

	[SerializeField]
	private GameObject BuildingBlockPrefab = null;
	[SerializeField]
	private GameObject Floor = null;
	[SerializeField]
	[Range(0.4f, 0.6f)] private float perlinCuttOff = 0.5f;

	public List<GameObject> WorldBuildingBlocks = new List<GameObject>();

	public static event Action<int,int> WorldBeingBuilt;

	public void BuildWorld(int rowsZ, int rowsX, float blockDensity, bool usePerlin)
	{
		WorldBeingBuilt?.Invoke(rowsZ, rowsX);
		if (WorldBuildingBlocks.Count != 0) DestroyWorld();

		for (int x = 0; x < rowsX; x++)
		{
			for (int z = 0; z < rowsZ; z++)
			{
				GameObject FloorInstance = Instantiate(Floor, new Vector3(x, -.5f, z), Quaternion.identity);
				WorldBuildingBlocks.Add(FloorInstance);

				bool Boolean = UnityEngine.Random.value <= blockDensity;
				if (usePerlin) Boolean = Mathf.PerlinNoise((float)x * blockDensity, (float)z * blockDensity) >= perlinCuttOff;
				if (Boolean)
				{
					GameObject BuildingBlockInstance = Instantiate(BuildingBlockPrefab, new Vector3(x, 0, z), Quaternion.identity);
							
					WorldBuildingBlocks.Add(BuildingBlockInstance);
				}

			}
			
		}

		gridMap.BuildGrid(rowsX, rowsZ);
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
