using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
	public GameObject target = null;
    public GridMap gridMap;
    public PathFinder pathFinder;

    void Update()
    {
        RaycastHit hit;
        //NavMeshHit navHit; 
        bool hitSomething = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // set ray from camera to mouse position 
        hitSomething = Physics.Raycast(ray, out hit, 100f);

        if (!hitSomething || !gridMap.grid[(int)hit.transform.position.x, (int)hit.transform.position.z].walkable) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (target != null) target.transform.position = hit.transform.position;
            pathFinder.GetPath(transform.position, hit.transform.position);
        }
    }
}
