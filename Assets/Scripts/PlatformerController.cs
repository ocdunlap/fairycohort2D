using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{

    //Speed of character movement and height of the jump. Set these values in the inspector.
    public float speed;
    public float jumpHeight;

    //Assigning a variable where we'll store the Rigidbody2D component.
    private Rigidbody2D rb;
    private Animator anim;

    private bool canJump;


    // Start is called before the first frame update
    void Start()
    {
        //Sets our variable 'rb' to the Rigidbody2D component on the game object where this script is attached.
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 jumpValue = Vector2.up * jumpHeight;
        
        //If we're able to jump and the player has pressed the space bar, then we jump!
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            rb.velocity = Vector2.up * jumpHeight;
        }

        //This is our movement function that checks for key presses, and updates the rigidbody's velocity accordingly
        UpdateVelocity();

    }

    #region Movement
    private void UpdateVelocity()
    {
        //declare a new Vector2 (an x direction and a y direction), and set it equal to the rigidbody's current velocity
        Vector2 newVelocity = rb.velocity; 

        if (Input.GetKey(KeyCode.LeftArrow)) //If we are pressing the left arrow
        {
            newVelocity.x = -speed; //set to x value to be negative speed (because speed is a number)
        }

        else if (Input.GetKey(KeyCode.RightArrow)) // If you are pressing the right arrow
        {
            newVelocity.x = speed; //Set the x value to be speed
        }
        
        else //If we are not pressing either the left or right arrow keys...
        {
            newVelocity.x = 0; // remove velocity
        }

        anim.SetFloat("MoveSpeed", newVelocity.magnitude);

        //When we've worked out what the velocity should be, we write the value back onto our rigidbody so it will move
        rb.velocity = newVelocity;
    }

    #endregion

    #region collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If we collide with an object tagged "ground" then our jump resets and we can now jump.
        if (collision.gameObject.tag == "ground")
        {
            canJump = true;
            //print statements print to the Console panel in Unity. 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //If we exit our collision with the "ground" object, then we are unable to jump.
        if (collision.gameObject.tag == "ground")
        {
            canJump = false;
        }
    }
    #endregion

}
