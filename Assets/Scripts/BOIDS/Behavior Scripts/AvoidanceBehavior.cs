using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredHerdBehavior
{
    public override Vector2 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)

            return Vector2.zero;

        //add all points together and average
        Vector2 avoidanceMove = Vector2.zero;

        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            Vector3 closestPoint = item.gameObject.GetComponent<Collider2D>().ClosestPoint(agent.transform.position);
            if (Vector2.SqrMagnitude(closestPoint - agent.transform.position) < herd.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);

            }
        }
      if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;

    }
}
