using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    private EnemyMovement enemyMovement; // Needed to access EnemyMovement for isHit 

    public AudioClip enemyKilledSFX; // enemy stomped by player
    public Transform RaycastCheckFront; //RaycastCheck Left
    public Transform RaycastCheckBack; //RaycastCheck Right
    public Transform RaycastCheckTop; //RaycastCheck Top

    public bool isRaycastRight = true;
    private bool isMovingRight = true; // Flag to track the direction of movement

    Player player; // Ask player's death status

    // Check whether the Player/NPC is dead
    public bool isDead = false;

    public LayerMask RaycastDamageCheck; //The Layer that was to be checked

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>(); // Need to grab EnemyMovement script first
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        Debug.DrawRay(RaycastCheckFront.position, Vector2.down, Color.red, 0.1f); //Raycast Debug DrawRay
        RaycastHit2D hit = Physics2D.Raycast(RaycastCheckFront.position, Vector2.down, 0.1f, RaycastDamageCheck); //When something hits in front of enemy

        if (hit)
        {
            if (!isDead)
            {
                //print("Touched on Front");
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    player.disableControl = true;
                    player.isKilled = true;
                    player.Killed();
                }
                enemyMovement.ChangeDirection(); // Cheap way to make enemy bumps together and turn, this includes player
                ToggleDirection(); // after bump, raycast turn direction
            }
            return;
        }

        Debug.DrawRay(RaycastCheckBack.position, Vector2.down, Color.blue, 0.1f); //Raycast Debug DrawRay
        hit = Physics2D.Raycast(RaycastCheckBack.position, Vector2.down, 0.1f, RaycastDamageCheck); //When something hits back of enemy

        if (hit)
        {
            if (!isDead)
            {
                //print("Touched on Back");
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    player.disableControl = true;
                    player.isKilled = true;
                    player.Killed();
                }
            }
            return;
        }

        Vector2 topRayDirection = isMovingRight ? Vector2.right : Vector2.left; // A toggle to either raycast cast left or right
        Debug.DrawRay(RaycastCheckTop.position, topRayDirection, Color.green, 0.7f); //Raycast Debug DrawRay
        hit = Physics2D.Raycast(RaycastCheckTop.position, topRayDirection, 0.5f, RaycastDamageCheck); //When something hits top of enemy

        if (hit)
        {
            if (!isDead)
            {
                //print("Damage on Top");
                enemyMovement.isHit = true;
                GameManager.instance.AddKill();
                PlaySoundAtPoint(enemyKilledSFX, transform.position); // plays death sound
            }
            return;
        }
    }

    private void PlaySoundAtPoint(AudioClip clip, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("CoinSound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(soundGameObject, clip.length); // destroy the sound game object after the clip is done playing
    }

    public void ToggleDirection() // toggle when either bump onto enemy or near an edge
    {
        isMovingRight = !isMovingRight;
    }
}
