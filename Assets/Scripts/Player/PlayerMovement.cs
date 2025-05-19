using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float runSpeedMultiplier = 3f;

    [SerializeField] GameObject barkEffect;
    [SerializeField] float barkCooldown;
    [SerializeField] private AudioClip[] barks;
    [SerializeField] private AudioClip[] foxCries;

    private GameObject nearestHidingDuck;
    [SerializeField] float smellRadius = 15f;
    [SerializeField] GameObject smellTrail;
    [SerializeField] LayerMask targetLayer;
    public Collider2D[] rangeCheck;
    float distance;
    float nearestDistance = 1000;


    private bool canBark = true;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    public Dialogue tutorial;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canBark = true;
    }

    // Update is called once per frame
    void Update()
    {
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, smellRadius, targetLayer);
        SearchNearestHiddenDuck();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            runSpeedMultiplier = 2f;
        }
        else
        {
            runSpeedMultiplier = 1f;
        }

        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && canBark == true)
        {
                canBark = false;
                StartShowBark();
        }

        if (Input.GetKeyDown(KeyCode.E) && LevelManager.Instance.feathers > 0)
        {
            var trail = Instantiate(smellTrail, nearestHidingDuck.transform.position, Quaternion.identity);
            LevelManager.Instance.UseFeather();
        }
    }

    private void SearchNearestHiddenDuck()
    {
        for (int i = 0; i < rangeCheck.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, rangeCheck[i].gameObject.transform.position);
            if (distance < nearestDistance && rangeCheck[i].GetComponent<DuckMovement>().isHiding == true)
            {
                nearestHidingDuck = rangeCheck[i].gameObject;
                nearestDistance = distance;
            }
        }
        nearestDistance = 100;
    }

    private void StartShowBark()
    {
        StartCoroutine(ShowBark());
        if (FieldOfView.Instance.foxInFOV != null)
        {
            foreach (Transform fox in FieldOfView.Instance.foxInFOV)
            {
                fox.gameObject.GetComponent<FoxMovement>().currentState = FoxMovement.foxState.Retreat;
                SoundFXManager.Instance.PlayRandomSoundFXClip(foxCries, fox, 1f);
            }
        }

        if (FieldOfView.Instance.objectsInFOV != null)
        {
            foreach (Transform duck in FieldOfView.Instance.objectsInFOV)
            {
                duck.gameObject.GetComponent<DuckMovement>().isHiding = false;
            }
        }
        //if (FieldOfView.Instance.objectsInFOV != null)
        //{
        //    foreach(Transform duck in FieldOfView.Instance.objectsInFOV)
        //    {
        //        duck.gameObject.GetComponent<DuckMovement>().isCaught = false;
        //    }
        //}
    }

    private IEnumerator ShowBark()
    {
        SoundFXManager.Instance.PlayRandomSoundFXClip(barks, transform, 1f);

        while (canBark == false)
        {
            float elapsedTime = 0f;
            elapsedTime += Time.deltaTime;
            barkEffect.SetActive(true);

            yield return new WaitForSeconds(barkCooldown);
            canBark = true;
            barkEffect.SetActive(false);
        }        
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed * runSpeedMultiplier;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Feather"))
        {
            LevelManager.Instance.AddFeather();
            collision.gameObject.SetActive(false);
        }
    }
}
