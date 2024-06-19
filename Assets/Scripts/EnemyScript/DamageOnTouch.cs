using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    private EnemyMovement enemyMovement; // Needed to access EnemyMovement for isHit 

    public Transform RaycastCheckFront; //RaycastCheck Left
    public Transform RaycastCheckBack; //RaycastCheck Right
    public Transform RaycastCheckTop; //RaycastCheck Top

    //Uses bool to check for player touch
    private bool isFront = false;
    private bool isBack = false;
    private bool isTop = false;

    // Check whether the PLayer/NPC is dead
    public bool isDead = false;

    public LayerMask RaycastDamageCheck; //The Layer that was to be checked

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>(); // Need to grab EnemyMovement script first
    }

    void FixedUpdate()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        // Raycast will check and state whether its true or false
        isFront = Physics2D.CircleCast(RaycastCheckFront.position, 0.5f, Vector2.left, 0.1f, RaycastDamageCheck);
        isBack = Physics2D.CircleCast(RaycastCheckBack.position, 0.5f, Vector2.right, 0.1f, RaycastDamageCheck);
        isTop = Physics2D.CircleCast(RaycastCheckTop.position, 0.5f, Vector2.up, 0.1f, RaycastDamageCheck);

        if (!isDead) // if enemy isnt dead, they can still deal damage
        {
            if (isFront) // When Raycast detect something on Front
            {
                print("Touched on Front");
                enemyMovement.ChangeDirection();
            }

            if (isBack) // When Raycast detect something on Back
            {
                print("Touched on Back");
            }

            if (isTop) // When Raycast detect something on Top
            {
                //print("Damage on Top");
                enemyMovement.isHit = true; // tell EnemyMovement to stop once getting hit
            }
        }
        else return;
    }
}
