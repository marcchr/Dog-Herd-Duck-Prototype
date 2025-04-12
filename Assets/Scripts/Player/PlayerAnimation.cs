using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    bool facingRight;
    bool lastInputLeft;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastInputLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0) // || Input.GetAxis("Vertical") != 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWalkingRight", false);
            facingRight = false;
            lastInputLeft = true;

        }
        else if (Input.GetAxis("Horizontal") > 0 ) // || Input.GetAxis("Vertical") != 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingRight", true);
            facingRight = true;
            lastInputLeft = false;
        }
        else
        {
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingRight", false);
            if (facingRight == true)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

        if (Input.GetAxis("Vertical") != 0 && lastInputLeft == true)
        {
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWalkingRight", false);
        }
        else if (Input.GetAxis("Vertical") != 0 && lastInputLeft == false)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingRight", true);
        }
        
    }
}
