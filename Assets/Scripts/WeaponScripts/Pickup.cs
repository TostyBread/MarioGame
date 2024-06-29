using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject playerWeaponHolder; // The player’s weapon holder where the picked-up weapon will be attached
    public AudioClip PickUpSFX;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("PickedUp", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("PickedUp", true);
            PickupWeapon(other.transform);
            PlaySoundAtPoint(PickUpSFX, transform.position);
        }
    }

    private void PlaySoundAtPoint(AudioClip clip, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("PickUp");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(soundGameObject, clip.length); // destroy the sound game object after the clip is done playing
    }

    void PickupWeapon(Transform Player)
    {
        // Attach the weapon to the player's weapon holder
        transform.SetParent(playerWeaponHolder.transform);
        transform.localPosition = Vector3.zero; // Adjust the position relative to the weapon holder
        transform.localRotation = Quaternion.identity; // Reset rotation

        // Optionally, disable the weapon's collider so it doesn't interfere with the player
        GetComponent<Collider2D>().enabled = false;

        // If the player already has a weapon, you may want to replace it or destroy it
        Gun existingWeapon = playerWeaponHolder.GetComponentInChildren<Gun>();
        if (existingWeapon != null && existingWeapon.gameObject != gameObject)
        {
            Destroy(existingWeapon.gameObject);
        }

        // Activate the weapon script if it was disabled
        GetComponent<Gun>().enabled = true;
    }
}
