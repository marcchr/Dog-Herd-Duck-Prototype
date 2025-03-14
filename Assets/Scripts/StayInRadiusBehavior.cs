using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : HerdBehavior
{

    public Vector2 center;
    public float radius = 15f;

    public override Vector2 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;
        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }
}
