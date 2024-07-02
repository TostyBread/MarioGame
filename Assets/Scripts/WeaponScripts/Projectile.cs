using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f;

    public AudioClip HitEnemySFX;
    public AudioClip HitMiscSFX;
    public AudioClip HitTrapsSFX;

    private TilemapHandler tilemapHandler;
    DamageOnTouch damageOnTouch;

    void Start()
    {
        Destroy(gameObject, lifetime); // initiates the bullet and lifetime
        tilemapHandler = FindObjectOfType<TilemapHandler>(); // find the tilemap handler
        damageOnTouch = FindObjectOfType<DamageOnTouch>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // When hit enemy
        {
            Destroy(collision.gameObject);
            PlaySoundAtPoint(HitEnemySFX, transform.position);
            if (!damageOnTouch.isDead) // If enemy isnt dead, it will add the kills into the scoreboard
            {
                GameManager.instance.AddKill();
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Traps")) // When hit traps
        {
            // Only remove the specific tile hit by the projectile
            tilemapHandler.RemoveTile(transform.position);
            PlaySoundAtPoint(HitTrapsSFX, transform.position);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground")) // When hit ground
        {
            PlaySoundAtPoint(HitMiscSFX, transform.position);
            Destroy(gameObject);
        }
    }

    private void PlaySoundAtPoint(AudioClip clip, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("WeaponSFX");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(soundGameObject, clip.length);
    }
}
