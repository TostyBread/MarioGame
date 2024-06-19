using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 4f; //player speed
    protected float jumpspeed = 7.5f; //player jump
    private bool isjumping = false; // check if player is jumping
    private bool isGrounded = false; // check if player is grounded

    Rigidbody2D myBody; //Rigidbody
    Animator anim; //animation

    public Transform RaycastCheck; //RaycastCheck
    public LayerMask RaycastGroundCheck; //The Layer that was to be checked
    //public LayerMask RaycastEnemyCheck; // The layer meant for checking whether you stomp on enemy back

    void Awake() // Grabs component when it starts
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

     void Update()
    {
        //if (Physics2D.Raycast(RaycastCheck.position, Vector2.down, 0.01f, RaycastGroundCheck)) // positon, raycast shooting direction, range, LayerMaskCheck
        //{
        //    print("Detected ground"); // Check if ground was detected
        //}
    }

    void FixedUpdate() // Reads PlayerWalk in a constant rate
    {
        PlayerWalk();
        PlayerJump();
        CheckGround();
    }

    private void CheckGround()
    {
        Debug.DrawRay(RaycastCheck.position, Vector2.down, Color.red, 0.1f); //Raycast Debug DrawRay
        
        //isGrounded = Physics2D.Raycast(RaycastCheck.position, Vector2.down, 0.1f, RaycastGroundCheck); //Raycast check ground collision
        isGrounded = Physics2D.CircleCast(RaycastCheck.position, 0.2f, Vector2.down, 0.1f, RaycastGroundCheck); // Circle Raycast check ground

        if (isGrounded && isjumping)
        {
            // Jumped before
            isjumping = false;
            anim.SetBool("Jump", false);
        }
    }

    private void PlayerJump() // Spacebar jump movement
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (isjumping == true)
            {
                return;
            }
            else // if player isnt jumping
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpspeed);
                
                isjumping = true;
            }
            anim.SetBool("Jump", true);
        }
    }

    private void PlayerWalk() // Left Right movement and animation
    {

        float h = Input.GetAxisRaw("Horizontal"); //detects x axis from key input (A, D movement)

        if (h > 0) // When player moves right
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);

            ChangeDirection(1);
        }
        else if (h < 0) // When player moves left
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);

            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }
        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x)); // Mathf.Abs retains either 0 or 1, so it can be useful when running animation
    }

    private void ChangeDirection(int direction) // changes the scale of the sprite left or right
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }
    //void OnCollisionEnter2D(Collision2D target) // OnCollisionEnter2D
    //{
    //    if (target.gameObject.tag == "Ground")
    //    {
    //        print("Entered a collision");
    //    }
    //}

    //void OnTriggerEnter2D(Collider2D target) // OnTriggerEnter2D
    //{
    //    if (target.tag == "Ground")
    //    {
    //        print("Triggered a collision");
    //    }
    //}
}
