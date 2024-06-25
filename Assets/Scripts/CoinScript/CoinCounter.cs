using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public TMP_Text scoreText; // Reference to the UI text displaying the score (TMP_Text)

    private int coinCollected;

    public void AddCoin()
    {
        coinCollected += 1;
        scoreText.text = coinCollected.ToString();

    }
}
