using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject squarePixel;

    public Vector2Int startPosition = new Vector2Int(0, 0);
    public Vector2Int endPosition = new Vector2Int(6, 9);

    private void Start()
    {
        DrawBresenhamLine(startPosition, endPosition);
    }


    // only works in 1 octant where x is larger than y
    private void DrawBresenhamLine(Vector2Int from, Vector2Int to)
    {
        double deltaX = to.x - from.x;
        double deltaY = to.y - from.y;
        int y = from.y;

        double deltaError = (double) deltaY / deltaX;
        double error = deltaError - 0.5;
        Debug.Log("original error"+error);
        Debug.Log("original deltaError" + deltaError);
       

        for (int x = from.x; x <= to.x; x++)
        {
            Instantiate(squarePixel, new Vector3(x, y, 0), Quaternion.identity);
            error += deltaError;
            Debug.Log(error);
            if (error >= 0.5)
            {
                y += 1;
                error -= 1.0;
            }
        }
    }
}
