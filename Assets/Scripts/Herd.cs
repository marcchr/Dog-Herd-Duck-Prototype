using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herd : MonoBehaviour
{
    public HerdAgent agentPrefab;
    List<HerdAgent> agents = new List<HerdAgent>();
    public HerdBehavior behavior;

    [Range(10, 100)]
    public int startingCount = 25;
    const float AgentDensity = 0.1f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 2f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }



    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {

            var spawnPoint = new Vector3 (Random.Range(-22, 22), Random.Range(-12,12), 0);
            if (spawnPoint.x > -8 && spawnPoint.x < 0 && spawnPoint.y > -5 && spawnPoint.y < 5)
            {
                spawnPoint.x = -8;
                spawnPoint.y = 9;
            }
            if (spawnPoint.x >= 0 && spawnPoint.x < 8 && spawnPoint.y > -5 && spawnPoint.y < 5)
            {
                spawnPoint.x = 8;
                spawnPoint.y = -9;
            }

            HerdAgent newAgent = Instantiate(
                agentPrefab,
                spawnPoint,
                // Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                ) ;
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add( newAgent );
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (HerdAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(HerdAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
