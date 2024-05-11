using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; //Make Rigidbody2d public so that we can freely edit it in unity
    private SpriteRenderer spriteRenderer; //Will be used in Update() to flip character sprite

    [Header("Movement")] //Displays back on the object and makes keeping track of the public parameters easier
    public float moveSpeed = 5f; //Contorls the speed at which the character moves horizontally
    float horizontalMovement; //Movement is across the X-axis

    [Header("Jumping")]
    public float jumpPower = 10f; //Contorls the speed at which the character moves vertically
    public int maxJumps = 2;
    int jumpsRemaining;
   
    [Header("GroundCheck")]
    public Transform groundCheckPosition;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer; //Checks if it is touching anything that is tagged with ground
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        GroundCheck();
        
        if (Input.GetKey(KeyCode.D)) //| KeyCode.RightArrow)
        { 
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.A)) //| KeyCode.LeftArrow
        {
            spriteRenderer.flipX = false;

        }

        if (Input.GetKey(KeyCode.RightArrow)) //| KeyCode.RightArrow)
        { 
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) //| KeyCode.LeftArrow
        {
            spriteRenderer.flipX = false;

        }
        //spriteRenderer.flipX = rb.velocity.x > 0.0f ; //Flips the character sprite if the movment value is greater then 0 (Eg. Moving to the right)

    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context) //Passes in the input action
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed) //If the Jump button is fully pressed down
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower); //Pass in the current x-value(rb.velocity) and pass in/update the y-value with jumpower
                jumpsRemaining--;
            }
            else if (context.canceled) //If the button pressed down but not fully
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); //Updates the y velocity by 0.5 instead of 10f(jumpPower)
                jumpsRemaining--;

            }
        }
    }

    private void GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPosition.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }
    
    private void OnDrawGizmosSelected() //Visualizes a white cube in unity where the ground checker is
    {
        Gizmos.color = Color.white; //Establishes the color
        Gizmos.DrawWireCube(groundCheckPosition.position, groundCheckSize); //Creates the cube
    }
}
