using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Koopa : MonoBehaviour
{

    Animator animator;
    EnemyMovement enemyMovement;
    PlayerMovement playerMovement;
    new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    [SerializeField] PhysicsMaterial2D bounce;
    bool shelled, shellShot;

    private void Awake() {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible() {
        enabled = true;
    }
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            float distanceAbove = (other.transform.position.y - transform.position.y);
            float direction = (other.transform.position.x - transform.position.x);
            if ((distanceAbove) > 0.25f && !shelled) {
                StartCoroutine(nameof(Shelled));
            }
            if ( shelled ) {
                ShootShell(direction);
                // free up velocity for bounce
            } 
        }
        if (shellShot) {
            if (other.gameObject.tag == "Player") {
                playerMovement.PlayerDeath();
            }
            if (other.gameObject.tag == "Enemy") {
                enemyMovement.EnemyDeath();
            }
        }
    }


    IEnumerator Shelled() {
        enemyMovement.enabled = false;
        playerMovement.BounceOffEnemy();
        animator.Play("KoopaShell");
        yield return new WaitForSeconds(0.25f);
        shelled = true;
    }

    void ShootShell( float direction){
        rigidbody2D.velocity = Vector2.left * 1000 * Time.deltaTime * Mathf.Sign(direction);

        rigidbody2D.sharedMaterial = bounce;
        shellShot = true;
        gameObject.layer = LayerMask.NameToLayer("Shell");
    }



}
