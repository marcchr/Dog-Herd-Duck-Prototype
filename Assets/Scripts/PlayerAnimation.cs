using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0) // || Input.GetAxis("Vertical") != 0)
        {
            Debug.Log("animate walk left");
            spriteRenderer.flipX = false;
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWalkingRight", false);
            facingRight = false;


        }
        else if (Input.GetAxis("Horizontal") > 0 ) // || Input.GetAxis("Vertical") != 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingRight", true);
            facingRight = true;
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
        
    }
}
