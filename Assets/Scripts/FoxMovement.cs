using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMovement : MonoBehaviour
{
    public LayerMask targetLayer;
    public Collider2D[] rangeCheck;
    public GameObject nearestObject;

    public float searchRadius = 5f;
    public float moveSpeed = 4f;
    [SerializeField] float speedMultiplier = 1f;
    public bool caughtPrey = false;
    [SerializeField] GameObject foxDen;

    private CircleCollider2D circleCollider;
    private Vector3 targetPosition;
    float distance;
    float nearestDistance = 1000;
    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        targetLayer = LayerMask.GetMask("Ducks");
        StartCoroutine(StalkPrey());
    }
    private IEnumerator StalkPrey()
    {

        //notes: add animations, sound cues fox and duck, barking mechanics for dog
        while (caughtPrey == false)
        {
            yield return new WaitForSeconds(3f);
            SearchNearestDuck();

            float elapsedTime = 0f;

            while (elapsedTime <= 5f)
            {
                //animator.SetBool("isWalking", true);
                
                if (elapsedTime <= 2f)
                {
                    speedMultiplier = 0.3f;
                    Debug.Log(moveSpeed);

                }

                else if (elapsedTime > 2f && elapsedTime <= 5f)
                {
                    speedMultiplier = 2f;
                    Debug.Log(moveSpeed);

                }
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * speedMultiplier * Time.deltaTime);
                yield return null;
            }
        }
    }

    private void SearchNearestDuck()
    {
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, searchRadius, targetLayer);
        for (int i = 0; i < rangeCheck.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, rangeCheck[i].gameObject.transform.position);
            if (distance < nearestDistance)
            {
                nearestObject = rangeCheck[i].gameObject;
            }
        }
        targetPosition = nearestObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Duck"))
        {
            if (caughtPrey == false)
            {
                caughtPrey = true;
            }
        }
    }
    void Update()
    {
        if (caughtPrey == true)
        {
            circleCollider.enabled = false;
            Vector2 direction = (new Vector3(foxDen.transform.position.x, foxDen.transform.position.y + 0.2f) - transform.position).normalized;
            speedMultiplier = 1.5f;
            transform.position += (Vector3)direction * moveSpeed * speedMultiplier * Time.deltaTime;
        }
        else
        {
            circleCollider.enabled = true;
        }
        //transform.position = Vector2.MoveTowards(targetPosition, transform.position, moveSpeed * Time.deltaTime);
        // if (GameManager.Instance.herdTotal-GameManager.Instance.herdedNum <= 10) -> spawn fox (put in fox spawner)
    }

}
