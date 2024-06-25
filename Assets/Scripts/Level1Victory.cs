using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryZone : MonoBehaviour
{
    public AudioClip victoryMusic; // Victory music clip
    private float moveDuration = 2.8f; // Duration for moving the player
    private bool isPlayerWon = false; // To check if player has already won

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player" && !isPlayerWon)
        {
            Player player = target.GetComponent<Player>();

            if (player != null)
            {
                isPlayerWon = true;
                player.hasWon = true; // Toggle hasWon to true
                StartCoroutine(VictorySequence(player));
            }
        }
    }

    private IEnumerator VictorySequence(Player player)
    {
        AudioSource audioSource = player.gameObject.AddComponent<AudioSource>();
        audioSource.clip = victoryMusic;
        audioSource.Play();

        float elapsedTime = 0f;
        Vector2 originalVelocity = player.GetComponent<Rigidbody2D>().velocity;

        // Move player to the right for moveDuration seconds
        while (elapsedTime < moveDuration)
        {
            Debug.Log("Still walking");
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.speed, originalVelocity.y);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.GetComponent<Rigidbody2D>().velocity = originalVelocity;

        player.StopMoving();

        // Wait until the victory music finishes playing
        yield return new WaitForSeconds(victoryMusic.length);

        // Load the next level (replace "NextLevel" with the actual name of the next level)
        SceneManager.LoadScene("Level1");
    }
}
