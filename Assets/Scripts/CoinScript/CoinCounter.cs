using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public AudioClip coinSound; // coin collected sound
    public TMP_Text scoreText; // Reference to the UI text displaying the score (TMP_Text)

    private int coinCollected;

    public void AddCoin()
    {
        AudioSource.PlayClipAtPoint(coinSound, transform.position); // plays a sound
        coinCollected += 1;
        scoreText.text = coinCollected.ToString();

    }
}
