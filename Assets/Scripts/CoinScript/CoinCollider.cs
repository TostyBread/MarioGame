using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCollider : MonoBehaviour
{
    [SerializeField] private CoinCounter coinCounter;

    private void OnTriggerEnter2D(Collider2D target) // when player touches the coin
    {
        if (target.gameObject.tag == "Player")
        {
            //print("Coins Collected");

            coinCounter.AddCoin();

            Destroy(gameObject); // destroy the coin after collected
        }
    }

}
