using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSideScroller : MonoBehaviour
{
    public Transform player;               // Reference to the player transform
    public float scrollSpeed = 2f;          // Speed at which the background scrolls
    public float parallaxEffect = 0.5f;     // How much the background moves relative to the player

    private Vector3 previousPlayerPosition; // Previous position of the player

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Finds the player by tag
        }
        previousPlayerPosition = player.position;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 deltaMovement = player.position - previousPlayerPosition;
            if (Mathf.Abs(deltaMovement.x) > 0.01f) // Checks if the player has moved significantly
            {
                // Move the background based on the player's movement
                transform.position += new Vector3(deltaMovement.x * parallaxEffect, 0, 0);
            }
            previousPlayerPosition = player.position;
        }
    }
}
