using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public LayerMask targetLayer;
    public Collider2D[] rangeCheck;

    public float searchRadius = 5f;
    public float moveSpeed = 2f;
    private Vector2 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
        targetLayer = LayerMask.GetMask("Ducks");
        StartCoroutine(Wander());
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        float force = 1f;
        if (collision.gameObject.tag == "Dog")
        {
            Vector3 direction = collision.transform.position - transform.position;

            direction = -direction.normalized;

            gameObject.GetComponent<Rigidbody>().AddForce(direction * force);
        }
    }

    private IEnumerator Wander()
    {
        WaitForSeconds wait = new WaitForSeconds(Random.Range(1f, 6f));

        while (true)
        {
            yield return wait;
            SetRandomTarget();
            SearchOtherDucks();

        }
    }

    private void SetRandomTarget()
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized;
        targetPosition = new Vector2(transform.position.x + randomPoint.x, transform.position.y + randomPoint.y);
         // (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        
    }

    private void SearchOtherDucks()
    {
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, searchRadius, targetLayer);

    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (rangeCheck.Length > 0 )
        {
            targetPosition = rangeCheck[0].gameObject.transform.position;
        }

    }


}
