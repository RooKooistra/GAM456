using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public float playerRotationSpeed = 10;
	public float avoidRotationSpeed = 2;
	public float playerMaxForce = 3;
	public float playerMaxSpeed = 2;
	public float distanceToNodeTolerance = 1f;

	[SerializeField]
	[Range(1,10)] int baseFeelerDistance = 2;
	[SerializeField]
	[Range(1, 10)] int feelerMaxDistanceForward = 5;
	[SerializeField]
	[Range(2, 10)] int numberOfFeelersPerSide = 10;

	public LayerMask layerMask;

	public Text debugData;

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

		int activeIndex = 0;
		while (true)
		{
			
			if (Vector3.Distance(transform.position, path[activeIndex].worldPosition) < distanceToNodeTolerance)
			{
				activeIndex++;
				if (activeIndex >= path.Count) yield break;
			}

			LookAtTarget(path[activeIndex].worldPosition);
			WallCheck();

			rigidBody.AddForce(transform.forward * playerMaxForce);
			rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, playerMaxSpeed);

			activeIndex = CheckAhead(path, activeIndex); // run check head to see if any further nodes are visible

			yield return null;
		}

	}

	void LookAtTarget(Vector3 lookAtVector)
	{
		Vector3 transDirection = transform.InverseTransformPoint(lookAtVector).normalized;
		float torqueAmount = (transDirection.z >= 0) ? transDirection.x : (transDirection.x > 0) ? 1 : -1; // if target is behind, max out torque value
		rigidBody.AddTorque(new Vector3(0, torqueAmount * playerRotationSpeed), ForceMode.Force);
	}

	// make an array of raycasts small on the sides, longer forwards and push in the oposite x direction
	void WallCheck()
	{
		for(int x = -1; x <= 1; x++)
		{
			for (float z = 0; z <= (float)feelerMaxDistanceForward; z = z + (float)feelerMaxDistanceForward / (float)numberOfFeelersPerSide)
			{
				if (x == 0) continue; // eliminate raycast straight forward
				var rayDirection = transform.TransformDirection(new Vector3((float)x, 0, (float)z)).normalized;
				Debug.DrawRay(transform.position, rayDirection * (z + (float)baseFeelerDistance), Color.magenta);
				RaycastHit hit;
				if (Physics.Raycast(transform.position, rayDirection, out hit, z + (float)baseFeelerDistance, layerMask))
				{
					// rotational force divided by hit length (more force when closer) ... Longer feelers have weaker strength
					rigidBody.AddTorque(new Vector3(0, -x / hit.distance * avoidRotationSpeed / (z+1f)), ForceMode.Force); 
				}
			}
		}

	}

	int CheckAhead(List<Node> path, int currentIndex)
	{
		
		int intToReturn = currentIndex;

		for (int i = currentIndex; i < path.Count; i++)
		{
			

			var direction = (path[i].worldPosition - transform.position).normalized;
			float distance = Vector3.Distance(path[i].worldPosition, transform.position);

			RaycastHit hit;
			if (!Physics.Raycast(transform.position, direction, out hit, distance, layerMask))
			{
				intToReturn = i;
			}
			else
			{
				return intToReturn;
			}
		}
		return intToReturn;
	}


}
