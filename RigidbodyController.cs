using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Wall jump functionality needs to be written
public class RigidbodyController : MonoBehaviour
{

    new Rigidbody2D rigidbody;
    BoxCollider2D boxCollider;
    public LayerMask groundLayer, wallLayer;
    public float speed = 300f, jumpForce = 15f;
    float move;
    [SerializeField] bool grounded, isOnWall;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        // for snappy jumping and falling
        rigidbody.gravityScale = 4;
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        Jump();
        CheckIfTouchingGround();
        CheckIfTouchingWall();
    }

    private void FixedUpdate() {
        Move();
    }

    void CheckIfTouchingWall() {
        Collider2D hit = Physics2D.OverlapBox(transform.position, boxCollider.size, 0, wallLayer);

        if (hit) {
            isOnWall = true;
        } else {
            isOnWall = false;
        }
    }

    void Jump() {

        if (grounded && Input.GetButtonDown("Jump")) {
            if (isOnWall) {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                print("triggered");
            } else {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    void Move() {
        rigidbody.velocity = new Vector2(move * speed * Time.deltaTime, rigidbody.velocity.y);
    }

    void CheckIfTouchingGround() {

        // shift boxcast down 0.1
        Vector2 boxCastOrigin = new Vector2(transform.position.x, transform.position.y - 0.1f);
        Vector2 boxCastSize = new Vector2(boxCollider.size.x - 0.4f, boxCollider.size.y);
        RaycastHit2D hit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0, Vector2.down, 0.1f, groundLayer);

        
        if (hit) {
            grounded = true;
        } else {
            grounded = false;
        }
    
    }

}
