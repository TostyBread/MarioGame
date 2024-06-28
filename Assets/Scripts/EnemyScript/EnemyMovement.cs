using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private DamageOnTouch damageOnTouch; //Bring in DamagOnTouch
    
    public float speed = 3f; //EnemySpeed
    private bool moveLeft = true;
    public bool isHit = false;
    public bool isShell = false; // Condition to check whether enemy is snail
    private float bodyExpireTimer = 1.5f; // time till dead body despawn
    private float currentTime; // tracks with deltaTime

    Rigidbody2D enemyBody; //Rigidbody
    Animator anim; //animation

    public Transform RaycastCheck; //RaycastCheck
    public LayerMask RaycastGroundCheck; //The Layer that was to be checked

    void Awake() // Grabs component when it starts
    {
        damageOnTouch = GetComponent<DamageOnTouch>();
        enemyBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        moveLeft = true; // Move left first when active
    }

    void Update()
    {

        if (isHit)
        {
            EnemyHit();

            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = bodyExpireTimer;
            }
        }
        else // if Enemy isnt hit, keep moving
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
    }

    void EnemyHit()
    {
        anim.SetBool("IsHit", true);
        damageOnTouch.isDead = true;

        if (currentTime < 0 && !isShell) // if timer is up for the non-shelled enemy, despawn their body
        {
            Destroy(this.gameObject);
        }
    }

    void CheckCollision()
    {
        Debug.DrawRay(RaycastCheck.position, Vector2.down, Color.magenta, 0.2f); // debug check ground check
        if (!Physics2D.Raycast(RaycastCheck.position, Vector2.down, 0.2f)) // When Raycast cant detect ground anymore
        {
            ChangeDirection();
            damageOnTouch.ToggleDirection();
        }
    }

    public void ChangeDirection() // changes the scale of the sprite left or right
    {
        // MoveLeft will be false when ChangeDirection was initiated, but when ChangeDirection was initiate again, MoveLeft will be true
        // Basically acts like switch (C# code is a mess)
        moveLeft = !moveLeft; 

        Vector3 scale = transform.localScale; // Vector3's "scale" is grabbed from whatever your ingame sprite scale value was.
        if (moveLeft)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale; // This is when "scale" will be sent back and override to whatever your ingame sprite scale was.

 
     }
}
