using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/



public class RigidbodyMovement : MonoBehaviour
{

    public float speed = 10f;
    Vector2 move;
    new Rigidbody2D rigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate() {
        /*************** AddForce() ****************************/
        // for rocket ships, floaters, cars, etc. Slowly gets up to speed.
        // rigidbody2D.AddForce(move * speed *  Time.deltaTime);


        /************** CHANGING VELOCITY ***********************/
        // gravity still works as usual.
        //For Platformers:
        // rigidbody2D.velocity = new Vector2(move.x * speed * Time.deltaTime, rigidbody2D.velocity.y);

        // for Galaca style space shooter
        // rigidbody2D.velocity = new Vector2(move.x * speed * Time.deltaTime, move.y * speed * Time.deltaTime);
        // same as:
        // rigidbody2D.velocity = move * speed * Time.deltaTime;


        /**************** MovePosition()**************************/
        // overwrites physics, still good collision detection., gravity issues, no friction.
        // rigidbody2D.MovePosition(rigidbody2D.position + (move * speed * Time.deltaTime)); 




    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        /************** Transform Movement***********************/
        /* Translate does not use physics. 
        No Rigidbody is attached, meaning no physics or interactions by default. They my be coded/simulated.
        will be buggy if object interacts with other physics objects, so only use if not going to interact, 
        like float points, coins, etc.
        Physics is resource expensive, so use transorm.translate when possible.
        */
        // vertical movement will be buggy if a rigidbody is attached.
        transform.Translate(move * speed * Time.deltaTime);

    }
}
