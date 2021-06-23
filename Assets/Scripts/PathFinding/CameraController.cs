using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private void Start()
	{
		TileWorldBuilder.WorldBeingBuilt += HandleWorldBeingBuilt;
	}

	void HandleWorldBeingBuilt(int rows, int columns)
	{
		int cameraSize = rows;
		float cameraSizeModifier = 1.5f;

		if (columns > rows)
		{
			cameraSize = columns;
			cameraSizeModifier = 1.9f;
		}

		GetComponent<Camera>().orthographicSize = (cameraSize / cameraSizeModifier);
		transform.position = new Vector3((float)columns / 2, 10f, (float)rows / 2);
	}

}
