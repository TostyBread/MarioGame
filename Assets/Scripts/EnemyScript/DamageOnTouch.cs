using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    private EnemyMovement enemyMovement; // Needed to access EnemyMovement for isHit 

    public Transform RaycastCheckFront; //RaycastCheck Left
    public Transform RaycastCheckBack; //RaycastCheck Right
    public Transform RaycastCheckTop; //RaycastCheck Top

    Player player; // Mention component from Player.cs

    // Check whether the Player/NPC is dead
    public bool isDead = false;

    public LayerMask RaycastDamageCheck; //The Layer that was to be checked

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>(); // Need to grab EnemyMovement script first
        player = GetComponent<Player>(); // grab component from Player.cs
    }

    private void Update()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D hit = Physics2D.CircleCast(RaycastCheckFront.position, 0.5f, Vector2.left, 0.1f, RaycastDamageCheck);

        if (hit)
        {
            if (!isDead)
            {
                print("Touched on Front");
                enemyMovement.ChangeDirection();
                player.isKilled = true;
            }
            return;
        }

        hit = Physics2D.CircleCast(RaycastCheckBack.position, 0.5f, Vector2.right, 0.1f, RaycastDamageCheck);

        if (hit)
        {
            if (!isDead)
            {
                print("Touched on Back");
                player.isKilled = true;
            }
            return;
        }

        hit = Physics2D.CircleCast(RaycastCheckTop.position, 0.5f, Vector2.up, 0.1f, RaycastDamageCheck);

        if (hit)
        {
            if (!isDead)
            {
                print("Damage on Top");
                enemyMovement.isHit = true;
            }
            return;
        }
    }
}
