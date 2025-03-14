using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HerdAgent : MonoBehaviour
{
    Herd agentHerd;
    public Herd AgentHerd { get { return agentHerd; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get {  return agentCollider; } }

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialize(Herd herd)
    {
        agentHerd = herd;
    }

   public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;

        sprite.transform.up = Vector3.up;

        if (velocity.x > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

}
