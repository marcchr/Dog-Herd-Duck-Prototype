using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMovement : MonoBehaviour
{
    public LayerMask targetLayer;
    public Collider2D[] rangeCheck;
    public GameObject nearestObject;

    private float timer = 0f;
    public float searchRadius = 5f;
    public float moveSpeed = 4f;
    [SerializeField] float speedMultiplier = 1f;
    private Rigidbody2D rb2D;

    public foxState currentState;
    public enum foxState
    {
        Rest,
        Idle,
        Hunt,
        Retreat
    }

    [SerializeField] private float minWaitTime = 5f;
    [SerializeField] private float maxWaitTime = 10f;
    [SerializeField] private float additionalWaitTime = 5f;
    [SerializeField] private float searchFrequency = 1f;
    [SerializeField] GameObject foxDen;

    [SerializeField] private AudioClip[] foxCalls;
    private bool playSound = true;

    private Vector2 lastPos;
    private Vector2 currentPos;
    //private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isWalking;

    private CircleCollider2D circleCollider;
    private Vector3 targetPosition;
    float distance;
    float nearestDistance = 1000;
    private void Start()
    {
        currentState = foxState.Rest;
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        targetLayer = LayerMask.GetMask("Ducks");
        StartCoroutine(DirectionCheck());

    }
    private IEnumerator DirectionCheck()
    {
        while (true)
        {
            CheckMoveDirection();
            Debug.Log("check direction");
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void SearchNearestDuck()
    {
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, searchRadius, targetLayer);
        for (int i = 0; i < rangeCheck.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, rangeCheck[i].gameObject.transform.position);
            if (distance < nearestDistance && rangeCheck[i].GetComponent<DuckMovement>().isHiding == false)
            {
                nearestObject = rangeCheck[i].gameObject;
                nearestDistance = distance;
            }
        }
        nearestDistance = 100;

        if (rangeCheck.Length == 0 || nearestObject == null)
        {
            currentState = foxState.Idle;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Duck"))
        {
            collision.gameObject.GetComponent<DuckMovement>().TakeDamage();
        }
    }

    void Update()
    {
        lastPos = currentPos;
        currentPos = transform.position;

        switch (currentState)
        {
            case (foxState.Rest):
                {
                    circleCollider.enabled = false;
                    moveSpeed = 0f;
                    timer += Time.deltaTime;
                    if (timer > minWaitTime)
                    {
                        timer = 0f;
                        currentState = foxState.Idle; 
                        break;
                    }
                }
                break;
            case (foxState.Idle):
                {
                    circleCollider.enabled = true;
                    moveSpeed = 2f;
                    targetPosition = new Vector3(foxDen.transform.position.x, foxDen.transform.position.y - 2f);
                    isWalking = true;
                    animator.SetBool("isIdle", false);

                    float distanceToWaitSpot = Vector3.Distance(transform.position, new Vector3(foxDen.transform.position.x, foxDen.transform.position.y - 2f));
                    if (distanceToWaitSpot < 0.1f)
                    {
                        moveSpeed = 0f;
                        animator.SetBool("isIdle", true);
                        animator.SetBool("isWalkingRight", false);
                        animator.SetBool("isWalkingLeft", false);
                        isWalking = false;

                        if (playSound == true)
                        {
                            playSound = false;
                            SoundFXManager.Instance.PlayRandomSoundFXClip(foxCalls, transform, 1f);
                        }
                    }
                    timer += Time.deltaTime;
                    if (timer > minWaitTime)
                    {
                        timer = 0f;
                        currentState = foxState.Hunt;
                        break;
                    }
                }
                break;
            case (foxState.Hunt):
                {
                    SearchNearestDuck();

                    timer += Time.deltaTime;
                    if (timer > searchFrequency)
                    {
                        timer = 0f;
                        targetPosition = nearestObject.transform.position;
                        
                    }

                    moveSpeed = 3f;
                    isWalking = true;
                    animator.SetBool("isIdle", false);

                    
                }
                break;
            case (foxState.Retreat):
                {
                    targetPosition = new Vector3(foxDen.transform.position.x, foxDen.transform.position.y + 0.2f);
                    moveSpeed = 6f;
                    float distanceToDen = Vector3.Distance(transform.position, targetPosition);
                    if (distanceToDen < 0.1f)
                    {
                        animator.SetBool("isIdle", true);
                        animator.SetBool("isWalkingRight", false);
                        animator.SetBool("isWalkingLeft", false);
                        isWalking = false;
                        playSound = true;
                        timer = 0f;
                        currentState = foxState.Rest;
                    }
                }
                break;


        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        rb2D.velocity = direction * moveSpeed;
    }

    void CheckMoveDirection()
    {
        if (currentPos.x - lastPos.x > 0 && isWalking == true)
        {
            animator.SetBool("isWalkingRight", true);
            animator.SetBool("isWalkingLeft", false);
        }
        
        if (currentPos.x - lastPos.x < 0 && isWalking == true)
        {
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWalkingRight", false);
        }
    }

}
