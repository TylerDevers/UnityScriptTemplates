using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
    uses small proximity sensing gameobject at edge of sprite to sense triggered collisions. 
*/


public class EnemyMovement : MonoBehaviour
{
    
    Rigidbody2D mybody;
    private float walkSpeed = 3f;
    float deltaTimeModifier = 50f;
    Vector2 direction;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform proximitySensor;


    private void Awake() {
        mybody = GetComponent<Rigidbody2D>();
        direction = new Vector2(-1, mybody.velocity.y);
        enabled = false;
    }

    private void OnBecameVisible() {
        enabled = true;
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void Update() {
        
    }

    private void FixedUpdate() {
        // mybody.velocity = direction * walkSpeed * Time.deltaTime * deltaTimeModifier;
        mybody.velocity = new Vector2(direction.x * walkSpeed, mybody.velocity.y) * Time.deltaTime * deltaTimeModifier;
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Blocks")) {
            ChangeDirection();
        }
    }


    private void ChangeDirection()
    {
        Vector3 tempScale = transform.localScale;

        if (mybody.velocity.x > 0f) {
            tempScale.x = 1f;
            direction = new Vector2(-1, mybody.velocity.y);
            transform.localScale = tempScale;
        }

        if (mybody.velocity.x < 0f) {
            tempScale.x = -1f;
            direction = new Vector2(1, mybody.velocity.y);
            transform.localScale = tempScale;
        }
    }


    public void EnemyDeath() {
        print(gameObject.name);
        GetComponent<SpriteRenderer>().sortingLayerName = "Death";
        mybody.velocity += Vector2.up * 10;
        GetComponent<Collider2D>().enabled = false;
    }




}
