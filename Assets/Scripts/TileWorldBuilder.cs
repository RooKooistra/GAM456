using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWorldBuilder : MonoBehaviour
{
	[SerializeField]
	private Camera MainCamera;
	[SerializeField]
	private GameObject BuildingBlockPrefab = null;
	[SerializeField]
	private GameObject Floor = null;
	[SerializeField]

	public List<GameObject> WorldBuildingBlocks = new List<GameObject>();

	private void Start()
	{
		MainCamera = Camera.main;
	}


	public void BuildWorld(int rows, int columns, float blockDensity)
	{
		if (WorldBuildingBlocks.Count != 0)
		{
			Debug.Log("World already built"); return;
		}
			
		int cameraSize = rows; 
		float cameraSizeModifier = 1.5f;

		if (columns > rows)
		{
			cameraSize = columns;
			cameraSizeModifier = 1.9f;
		}
		Debug.Log(cameraSizeModifier);
			
		MainCamera.orthographicSize = (cameraSize / cameraSizeModifier);
		MainCamera.transform.position = new Vector3((float) columns / 2, 10f, (float) rows / 2);

		for (int i = 0; i < columns; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				GameObject FloorInstance = Instantiate(Floor, new Vector3(i, -.5f, j), Quaternion.identity);
				// FloorInstance.transform.localScale = new Vector3(.1f, .1f, .1f);
				WorldBuildingBlocks.Add(FloorInstance);

				bool Boolean = (Random.value <= blockDensity);
				if (Boolean)
				{
					GameObject BuildingBlockInstance = Instantiate(BuildingBlockPrefab, new Vector3(i, 0, j), Quaternion.identity);
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
