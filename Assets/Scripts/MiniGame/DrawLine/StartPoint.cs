using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawLine(GridPosition start, GridPosition end)
    {
        Vector3 startPos = new Vector3(start.x, start.y, 10);
        Vector3 endPos = new Vector3(end.x, end.y, 10);

        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
