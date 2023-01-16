using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 21f;
    float acceleration;
    private float horizontalInput;
    bool jumping;
    bool rightFacing = true;
    



    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        acceleration = speed * 2;
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // full jump press yields full jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpingPower);
        }

        // early jump release yields partial jump
        if (Input.GetButtonUp("Jump") && rigidbody.velocity.y > 0f)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 0.5f);
        }

        FlipSprite();
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
