using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    public AudioClip coinSound; // coin collected sound

    private void OnTriggerEnter2D(Collider2D target) // when player touches the coin
    {
        if (target.gameObject.tag == "Player")
        {
            PlaySoundAtPoint(coinSound, transform.position); // plays a sound
            //print("Coins Collected");

            GameManager.instance.AddCoin(); // GameManager Records
            Destroy(gameObject); // destroy the coin after collected
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
}
