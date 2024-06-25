using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    private EnemyMovement enemyMovement; // Needed to access EnemyMovement for isHit 

    public Transform RaycastCheckFront; //RaycastCheck Left
    public Transform RaycastCheckBack; //RaycastCheck Right
    public Transform RaycastCheckTop; //RaycastCheck Top

    //Player player; // Mention component from Player.cs

    // Check whether the Player/NPC is dead
    public bool isDead = false;

    public LayerMask RaycastDamageCheck; //The Layer that was to be checked

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>(); // Need to grab EnemyMovement script first
        //player = GetComponent<Player>(); // grab component from Player.cs
    }

    private void Update()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        Debug.DrawRay(RaycastCheckFront.position, Vector2.down, Color.red, 0.1f); //Raycast Debug DrawRay
        RaycastHit2D hit = Physics2D.Raycast(RaycastCheckFront.position, Vector2.down, 0.1f, RaycastDamageCheck);

        if (hit)
        {
            if (!isDead)
            {
                print("Touched on Front");
                enemyMovement.ChangeDirection();
            }
            return;
        }

        hit = Physics2D.Raycast(RaycastCheckBack.position, Vector2.down, 0.1f, RaycastDamageCheck);

        if (hit)
        {
            if (!isDead)
            {
                print("Touched on Back");
            }
            return;
        }

        hit = Physics2D.Raycast(RaycastCheckTop.position, Vector2.right, 0.5f, RaycastDamageCheck);

        if (hit)
        {
            if (!isDead)
            {
                enemyMovement.isHit = true;
                print("Damage on Top");
            }
            return;
        }
    }
}
