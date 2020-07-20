using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    //necessary for animations and physics
    private Rigidbody2D rb2D;
    private Animator myAnimator;

    private bool facingRight = true;

    //variables to play with
    public float speed = 2.0f;
    public float horizMovement; // can be -1, 1, or 0
 
    private bool playerJump = false;

    [Range(1,10)]
    public float JumpSpeed = 2.0f;


    // Start is called before the first frame update
    private void Start()
    {
        //define the gameobjects found on the player
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    //Handles inputs for the physics
    private void Update()
    {
        //check if the player has input movement
        horizMovement = Input.GetAxis("Horizontal");
        playerJump = Input.GetButtonDown("Jump");

        Jump(playerJump);
    }


    //Updates with the CPU clock
    //handles running the physics
    private void FixedUpdate()
    {
        //move the character left and right. Velocity is a vector and requires two components
        rb2D.velocity = new Vector2(horizMovement * speed * Time.fixedDeltaTime, rb2D.velocity.y);
        Flip(horizMovement);

        //Set the speed value in the animator to the absolute value of the horizontal movement.
        myAnimator.SetFloat("AniSpeed", Mathf.Abs(horizMovement));
        
        //call Jump function
        //Jump(playerJump);
    }


    //Flipping function
    private void Flip(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizMovement > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale; //set a 3 piece vector to the scale values

            theScale.x *= -1;
            transform.localScale = theScale; //update the transform component
        }
    }


    //Jumping Function
    private void Jump(bool jump)
    {
        if (jump)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
            rb2D.velocity += Vector2.up * JumpSpeed;

            //Set the AniJump value in the animator the jump boolean
            myAnimator.SetBool("AniJump", playerJump);
            
            playerJump = false;
        }
    }

}
