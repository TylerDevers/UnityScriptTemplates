using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    for MarioBros-like 2d platformer. 
    - Player game object has childed empty ground check playerobject , placed at characters feet.
    - changes player velocity to keep stock unity physics.
    - rigidbody settings = no drag, gravity of 5.
*/
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    Animator animator;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 21f;
    float acceleration;
    private float horizontalInput;
    bool jumping;
    bool rightFacing = true;
    



    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        acceleration = speed * 3;
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        Jump();
        AnimatePlayer();
        FlipSprite();
        IsGrounded();
    }


    private void FixedUpdate()
    {
        // for responsive turn around
        // rigidbody.velocity = new Vector2(horizontalInput * speed, rigidbody.velocity.y);

        // use MoveTowards to enable slide when player turns around
        float currentXvelocity = rigidbody.velocity.x;
        rigidbody.velocity = new Vector2(Mathf.MoveTowards(currentXvelocity, horizontalInput * speed, acceleration * Time.deltaTime),
             rigidbody.velocity.y);
        
    }


    bool IsGrounded() {
        
        // jumpReleased = false;
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (grounded) {
            jumping = false;
        } else {
            jumping = true;
        }
        return grounded;
    }


    void Jump() {
        // full jump press yields full jump
        if (Input.GetButtonDown("Jump") && !jumping)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpingPower);
            jumping = true;
        }

        // early jump release yields partial jump
        if (Input.GetButtonUp("Jump") && rigidbody.velocity.y > 0f)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 0.5f);
        }
    }


    void FlipSprite() {
        Vector3 tempScale = transform.localScale;

        if (horizontalInput > 0f) {
            rightFacing = true;
            tempScale.x = 1f;
            transform.localScale = tempScale;
        }

        if (horizontalInput < 0f) {
            rightFacing = false;
            tempScale.x = -1f;
            transform.localScale = tempScale;
        }
    }  

    void AnimatePlayer() {

        if (!jumping) {
            if ((horizontalInput > 0f && rigidbody.velocity.x < 0) || (horizontalInput < 0f && rigidbody.velocity.x > 0)) { // slide left
                // animator.SetBool("Slide", true);
                animator.Play("MarioSmallSlide");
            } else if (horizontalInput > 0f || horizontalInput < 0f) {
                // animator.SetBool("Run", true);
                animator.Play("MarioSmallRun");
            } else if (horizontalInput == 0) {
                // animator.SetBool("Run", false);
                animator.Play("MarioSmallIdle");
            }
        }

        if (jumping) {
            animator.Play("MarioSmallJump");
        }
        
    
    }
   

    void StayInBoundsOfCamera() {
        // TODO
    }











}
