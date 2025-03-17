using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (Input.GetAxis("Horizontal") < 0)
        {
            //spriteRenderer.flipX = false;
            //animator.SetBool("isWalking", true);
      
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            //spriteRenderer.flipX = true;
            //animator.SetBool("isWalking", true);

        }



        //SightRaycast();
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
    }

}
