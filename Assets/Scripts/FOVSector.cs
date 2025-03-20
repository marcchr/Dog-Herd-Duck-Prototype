using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVSector : MonoBehaviour
{
    public float radius = 1f;
    public float startAngle = 0f;
    public float endAngle = 90f;
    public int segments = 36;
    public AnimationCurve widthCurve;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.positionCount = segments + 2; // Add 2 for start and end points
        lineRenderer.useWorldSpace = false;
        lineRenderer.widthCurve = widthCurve;
        UpdateSector();
    }

    void UpdateSector()
    {
        float angleIncrement = (endAngle - startAngle) / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = startAngle + angleIncrement * i;
            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
        lineRenderer.SetPosition(segments + 1, new Vector3(radius * Mathf.Cos(endAngle * Mathf.Deg2Rad), radius * Mathf.Sin(endAngle * Mathf.Deg2Rad), 0));
    }
}