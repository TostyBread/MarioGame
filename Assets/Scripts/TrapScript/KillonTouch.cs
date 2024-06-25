using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class KillonTouch : MonoBehaviour
{
    //Player player;

    private void Awake()
    {
        //player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            
        }
    }
}
