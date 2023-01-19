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
    float direction;

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
            direction = (other.transform.position.x - transform.position.x);
            if ((distanceAbove) > 0.25f && !shelled) {
                StartCoroutine(nameof(Shelled));
            } else if (!shelled) {
                playerMovement.PlayerDeath();
            }
            if ( shelled ) {
                StartCoroutine(nameof(ShootShell));
                
            } 
        }
        if (shellShot) {
            if (other.gameObject.tag == "Player") {
                playerMovement.PlayerDeath();
            }
            if (other.gameObject.tag == "Enemy") {
                enemyMovement.EnemyDeath();
                other.gameObject.GetComponent<EnemyMovement>().EnemyDeath();
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

    IEnumerator ShootShell(){
        rigidbody2D.velocity = Vector2.left * 1000 * Time.deltaTime * Mathf.Sign(direction);
        rigidbody2D.sharedMaterial = bounce;
        gameObject.layer = LayerMask.NameToLayer("Shell");
        yield return new WaitForSeconds(0.25f);
        shellShot = true;
        
    }



}
