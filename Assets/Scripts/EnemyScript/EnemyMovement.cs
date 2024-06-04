using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 3f; //EnemySpeed
    private bool moveLeft = true;

    Rigidbody2D enemyBody; //Rigidbody
    Animator anim; //animation

    public Transform RaycastCheck; //RaycastCheck
    public LayerMask RaycastGroundCheck; //The Layer that was to be checked

    void Awake() // Grabs component when it starts
    {
        enemyBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        moveLeft = true; // Move left first when active
    }

    void Update()
    {
        if (moveLeft) // if moveLeft isnt false
        {
            enemyBody.velocity = new Vector2(-speed, enemyBody.velocity.y);
        }
        else
        { 
            enemyBody.velocity = new Vector2(speed, enemyBody.velocity.y); 
        }
        CheckCollision();
    }

    void CheckCollision()
    {
        if (!Physics2D.Raycast(RaycastCheck.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    //private void CheckGround()
    //{
    //    isGrounded = Physics2D.Raycast(RaycastCheck.position, Vector2.down, 0.1f, RaycastGroundCheck);

    //    if (!isGrounded && moveLeft)
    //    {
    //        moveLeft = false;
    //        ChangeDirection(-bodyScale);
    //    }
    //    else if (!isGrounded && !moveLeft)
    //    {
    //        moveLeft = true;
    //        ChangeDirection(bodyScale);
    //    }
    //}

    void ChangeDirection() // changes the scale of the sprite left or right
    {
        moveLeft = !moveLeft;

        Vector3 scale = transform.localScale;
        if (moveLeft)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

 
     }
}
