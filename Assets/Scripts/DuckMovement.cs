using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckMovement : MonoBehaviour
{
    public LayerMask targetLayer;
    public Collider2D[] rangeCheck;

    public float searchRadius = 5f;
    public float moveSpeed = 2f;
    public float waitTimeMin = 1f, waitTimeMax = 6f;
    private Vector3 targetPosition;
    private Rigidbody2D rb2D;

    private Vector2 lastPos;
    private Vector2 currentPos;

    [SerializeField] Image healthBar;
    [SerializeField] private float maxHealth = 30;
    private float currentHealth;

    public bool isHiding = false;
    public bool isFound = false;
    //public bool isCaught = false;
    //public bool isWandering = true;
    //public GameObject catcher;

    private SpriteRenderer spriteRenderer;
    public Animator animator;

    public float quackChance = 0.1f;
    [SerializeField] private AudioClip[] quacks;
    [SerializeField] private AudioClip duckCry;
    private void Start()
    {
        currentHealth = maxHealth;
        rb2D = GetComponent<Rigidbody2D>();
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
        while (true) //isCaught == false && isWandering == true)
        {
            GetRandomPoint();
            SearchOtherDucks();

            

            float elapsedTime = 0f;


            while (elapsedTime <= 1f)
            {
                if (isHiding == false)
                {
                    animator.SetBool("isWalking", true);
                }

                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed*Time.deltaTime);
                yield return null;
            }
            animator.SetBool("isWalking", false);
            
            if (Random.Range(0f, 1f) < quackChance)
            {
                SoundFXManager.Instance.PlayRandomSoundFXClip(quacks, transform, 1f);
            }


            float waitTime = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(waitTime);
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fox"))
        {
            
            //if (isCaught == false)
            //{
            //    isCaught = true;
            //    isWandering = false;
            //    catcher = collision.gameObject;
            //}
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        healthBar.fillAmount = currentHealth / maxHealth;
        SoundFXManager.Instance.PlaySoundFXClip(duckCry, transform, 1f);
    }

    private void GetRandomPoint()
    {
        float randomX = Random.Range(-4, 4);
        float randomY = Random.Range(-4, 4);
        targetPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);

        if (isFound == true)
        {
            Vector2 direction = (transform.position - new Vector3(0, 0).normalized) * moveSpeed * 3f;
            targetPosition = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
        }
    }

    private void SearchOtherDucks()
    {
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, searchRadius, targetLayer);
        if (rangeCheck.Length > 4)
        {
            targetPosition = rangeCheck[Random.Range(0, rangeCheck.Length-1)].transform.position;
        }

        if (isFound == true)
        {
            Vector2 direction = ((new Vector3(0, 0)- transform.position).normalized) * moveSpeed * 3f;
            targetPosition = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
        }
    }

    private void Update()
    {
        lastPos = currentPos;
        currentPos = transform.position;

        if (isHiding == true)
        {
            targetPosition = transform.position;
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        

        if (currentHealth <= 0)
        {
            Debug.Log("Duck died");
            LevelManager.Instance.LoseLife();
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        //if (isCaught == true)
        //{
        //    isWandering = false;
        //    transform.position = new Vector2(catcher.transform.position.x, catcher.transform.position.y);
        //}
        //else if (isCaught == false && isWandering == false)
        //{
        //    isWandering = true;
        //    StartCoroutine(Wander());
        //}
        
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
