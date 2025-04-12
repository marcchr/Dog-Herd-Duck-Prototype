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

    private bool canBark = true;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canBark = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            runSpeedMultiplier = 1.5f;
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
    }

    private void StartShowBark()
    {
        StartCoroutine(ShowBark());
        //play bark sound effect
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

}
