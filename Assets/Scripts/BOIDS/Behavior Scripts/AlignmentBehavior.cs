using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Herd/Behavior/Alignment")]
public class AlignmentBehavior : FilteredHerdBehavior
{
    public override Vector2 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        //if no neighbors, maintain current alignment
        if (context.Count == 0)

            return agent.transform.up;

        //add all points together and average
        Vector2 alignmentMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= context.Count;

        return alignmentMove;

    }
}
