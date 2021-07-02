using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public float playerRotationSpeed = 20;
	public float playerMaxForce = 1;
	public float playerMaxSpeed = 1;
	public float distanceToNodeTolerance = 0.5f;

	public Text debugData;

	//List<Node> pathToTravel = new List<Node>();
	Rigidbody rigidBody;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	public void TravelPath(List<Node> node)
	{
		StartCoroutine(TravelPathGo(node));
	}

	IEnumerator TravelPathGo(List<Node> path)
	{
		//pathToTravel = path;
		
		int index = 0;
		while (true)
		{
			

			if (Vector3.Distance(transform.position, path[index].worldPosition) < distanceToNodeTolerance)
			{
				index++;

				if (index >= path.Count) yield break;
			}

			Vector3 targetDirection = (path[index].worldPosition - transform.position).normalized;

			Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, playerRotationSpeed * Time.deltaTime, 0.0f);

			Debug.DrawRay(transform.position, newDirection, Color.red);

			transform.rotation = Quaternion.LookRotation(newDirection);

			rigidBody.AddForce(transform.forward * playerMaxForce);
			rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, playerMaxSpeed);

			debugData.text = path.Count.ToString();
			index = CheckAhead(path, index); // run check head to see if any further nodes are visible

			yield return null;
		}

	}

	int CheckAhead(List<Node> path, int currentIndex)
	{
		int intToReturn = currentIndex;
		for(int i = currentIndex; i <= path.Count; i++)
		{
			var direction = (path[i].worldPosition - transform.position).normalized;

			RaycastHit hit;
			if (Physics.Raycast(transform.position, direction, out hit, float.MaxValue))
			{
				if (hit.transform.position == path[i].worldPosition) intToReturn = i;
			}
		}

		//debugData.text = ("Index to return " + intToReturn);
		return intToReturn;
	}


}
