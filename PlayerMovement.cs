using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        acceleration = speed * 2;
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        Jump();
        AnimatePlayer();
        FlipSprite();
        IsGrounded();
        print(rigidbody.velocity);
    }


    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(horizontalInput * speed, rigidbody.velocity.y);
        
    }


    bool IsGrounded() {
        
        // jumpReleased = false;
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (grounded) {
            jumping = false;
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
            if (horizontalInput > 0f && rigidbody.velocity.x < 0) {
                animator.SetBool("Slide", true);
            }
            else if (horizontalInput > 0f || horizontalInput < 0f) {
                animator.SetBool("Run", true);
            } else if (horizontalInput == 0) {
                animator.SetBool("Run", false);
            }

        }
        

        
        if (horizontalInput > 0f && rigidbody.velocity.x < 0f ) {
            // skid direction
        }

        if (horizontalInput < 0f && rigidbody.velocity.x > 0f) {
            // skid other direction
        }
    }
   

    void StayInBoundsOfCamera() {
        // TODO
    }











}
