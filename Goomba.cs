using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    Control Animations for Goomba.

    TODO ?? is this needed? should I use movement script??
*/

public class Goomba : MonoBehaviour
{

    Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            float posDifference = (other.transform.position.y - transform.position.y);
            if ((posDifference) > 0.5f) {
                print(posDifference);
                StartCoroutine(nameof(Death));
            }
        }
    }

    IEnumerator Death() {
        //stops physics
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<CircleCollider2D>().enabled = false;
        FindObjectOfType<PlayerMovement>().BounceOffEnemy();
        animator.Play("GoombaFlat");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }


}
