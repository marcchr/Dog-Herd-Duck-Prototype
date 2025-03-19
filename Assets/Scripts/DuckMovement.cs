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

    private Vector2 lastPos;
    private Vector2 currentPos;
    enum Direction { Right, Left, Still };

    private SpriteRenderer spriteRenderer;
    public Animator animator;

    private void Start()
    {
        // targetPosition = transform.position;
        targetLayer = LayerMask.GetMask("Ducks");
        StartCoroutine(Wander());
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(DirectionCheck());
    }

    private IEnumerator DirectionCheck()
    {
        while (true)
        {
            CheckMoveDirection();
            yield return new WaitForSeconds(0.1f);
        }
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
        lastPos = currentPos;
        currentPos = transform.position;


    }

    void CheckMoveDirection()
    {
        if (currentPos.x - lastPos.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        if (currentPos.x - lastPos.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        
    }


}
