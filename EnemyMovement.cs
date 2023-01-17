using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    Rigidbody2D mybody;
    private float walkSpeed = 3f;
    float deltaTimeModifier = 50f;
    Vector2 direction = Vector2.left;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform proximitySensor;


    private void Awake() {
        mybody = GetComponent<Rigidbody2D>();
    }


    private void Update() {
       
       
    }


    private void FixedUpdate() {
        mybody.velocity = direction * walkSpeed * Time.deltaTime * deltaTimeModifier;
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            print("Mario");
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Blocks")) {
            ChangeDirection();
        }
    }


    private void ChangeDirection()
    {
        Vector3 tempScale = transform.localScale;

        if (mybody.velocity.x > 0f) {
            tempScale.x = 1f;
            direction = Vector2.left;
            transform.localScale = tempScale;
        }

        if (mybody.velocity.x < 0f) {
            tempScale.x = -1f;
            direction = Vector2.right;
            transform.localScale = tempScale;
        }
    }
}
