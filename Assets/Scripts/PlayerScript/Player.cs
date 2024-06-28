using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public AudioClip deathSFX; // Player killed
    public AudioClip jumpSFX; // Player jump

    public float speed = 4f; //player speed
    protected float jumpspeed = 7.561f; //player jump
    public float delayBeforeRestart = 2.0f; // Delay before restart

    private bool isjumping; // check if player is jumping
    private bool isGrounded; // check if player is grounded
    public bool disableControl; // disable player controls when player win
    public bool isKilled; // check whether player is killed

    BoxCollider2D colliderBody; //Player's collider2D
    Rigidbody2D myBody; //Rigidbody
    Animator anim; //animation
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    public Transform RaycastCheck; //RaycastCheck
    public LayerMask RaycastGroundCheck; //The Layer that was to be checked

    void Awake() // Grabs and set component when it starts
    {
        disableControl = false;
        isjumping = false;
        isGrounded = false;
        isKilled = false;

        colliderBody = GetComponent<BoxCollider2D>();
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() // Reads PlayerWalk in a constant rate
    {
        if (disableControl) // "Disables" controls when won or killed
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
            if (!disableControl) // check if player has won
            {
                if (isjumping == true)
                {
                    return;
                }
                else // if player isnt jumping
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpspeed);
                    PlaySoundAtPoint(jumpSFX, transform.position); // plays jump sound
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
        else // When player stops moving
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

    public void Killed()
    {
        FreezeCamera();
        anim.SetBool("Killed", true);
        myBody.velocity = new Vector2(myBody.velocity.x, jumpspeed); // When killed, player will jump
        colliderBody.enabled = false; // disable 2d collider
        PlaySoundAtPoint(deathSFX, transform.position); // plays death sound
        StartCoroutine(RestartLevelAfterDelay());
    }

    public void FreezeCamera() // Freezes the camera when player died
    {
        if (cinemachineVirtualCamera != null)
        {
            cinemachineVirtualCamera.enabled = false; // Disable the Cinemachine Virtual Camera
        }
    }

    private IEnumerator RestartLevelAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeRestart); // Wait for the specified delay
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void ChangeDirection(int direction) // changes the scale of the sprite left or right
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    private void PlaySoundAtPoint(AudioClip clip, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("CoinSound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(soundGameObject, clip.length); // destroy the sound game object after the clip is done playing
    }
}
