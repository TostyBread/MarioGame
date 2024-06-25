using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4f; //player speed
    protected float jumpspeed = 7.561f; //player jump

    private bool isjumping; // check if player is jumping
    private bool isGrounded; // check if player is grounded
    public bool hasWon; // disable player controls when player win

    //SceneLoader levelLoader; // Calls the SceneLoader
    Rigidbody2D myBody; //Rigidbody
    Animator anim; //animation

    public Transform RaycastCheck; //RaycastCheck
    public LayerMask RaycastGroundCheck; //The Layer that was to be checked

    void Awake() // Grabs and set component when it starts
    {
        hasWon = false;
        isjumping = false;
        isGrounded = false;

        //levelLoader = GetComponent<SceneLoader>();
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate() // Reads PlayerWalk in a constant rate
    {
        if (hasWon) // "Disables" controls when won
        {
            return;
        }
        PlayerWalk();
        PlayerJump();
        CheckGround();
    }

    private void CheckGround()
    {
        //Debug.DrawRay(RaycastCheck.position, Vector2.down, Color.red, 0.1f); //Raycast Debug DrawRay
        
        //isGrounded = Physics2D.Raycast(RaycastCheck.position, Vector2.down, 0.1f, RaycastGroundCheck); //Raycast check ground collision
        isGrounded = Physics2D.CircleCast(RaycastCheck.position, 0.33f, Vector2.down, 0.1f, RaycastGroundCheck); // Circle Raycast check ground 

        if (isGrounded && isjumping)
        {
            AllowJump();
        }
    }

    private void AllowJump()
    {
        // Jumped before
        isjumping = false;
        anim.SetBool("Jump", false);
    }

    private void PlayerJump() // Spacebar jump movement
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (!hasWon) // check if player has won
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
            return;
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
            StopMoving();
        }
        AnimationStatus();
    }

    public void AnimationStatus() // Changes animation that can access from other scripts
    {
        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x)); // Mathf.Abs retains either 0 or 1, so it can be useful when running animation
    }
    public void StopMoving() // Make player stop moving, and it can be access by other scripts
    {
        myBody.velocity = new Vector2(0f, myBody.velocity.y);
        AnimationStatus();
    }

    private void ChangeDirection(int direction) // changes the scale of the sprite left or right
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }
}
