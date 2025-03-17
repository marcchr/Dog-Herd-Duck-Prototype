using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public LayerMask targetLayer;
    public Collider2D[] rangeCheck;

    public float searchRadius = 5f;
    public float moveSpeed = 2f;
    public float waitTimeMin = 1f, waitTimeMax = 6f;
    private Vector3 targetPosition;

    private float deltaX;
    private SpriteRenderer spriteRenderer;
    public Animator animator;

    private void Start()
    {
        // targetPosition = transform.position;
        targetLayer = LayerMask.GetMask("Ducks");
        StartCoroutine(Wander());
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private IEnumerator Wander()
    {
        while (true)
        {
            GetRandomPoint();
            SearchOtherDucks();

            

            float elapsedTime = 0f;


            while (elapsedTime <= 1f)
            {
                animator.SetBool("isWalking", true);

                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed*Time.deltaTime);
                yield return null;
            }
            animator.SetBool("isWalking", false);


            float waitTime = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(waitTime);
            
        }

    }

    private void GetRandomPoint()
    {
        float randomX = Random.Range(-4, 4);
        float randomY = Random.Range(-4, 4);
        targetPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);
    }

    private void SearchOtherDucks()
    {
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, searchRadius, targetLayer);
        if (rangeCheck.Length > 4)
        {
            targetPosition = rangeCheck[Random.Range(0, rangeCheck.Length-1)].transform.position;
        }

    }

    private void Update()
    {
        //float currentX = transform.position.x;
        //float previousX = currentX;

        //transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        //currentX = transform.position.x;

        //if (rangeCheck.Length > 1 )
        //{
            //targetPosition = rangeCheck[0].gameObject.transform.position;
            //transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        //}

        //deltaX = currentX - previousX;
        if (targetPosition.x - transform.position.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }


}
