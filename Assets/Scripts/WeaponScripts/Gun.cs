using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public AudioClip ShootSFX;
    public AudioClip NoAmmoSFX;
    public int maxAmmo = 10;
    public float projectileSpeed = 20f;
    public TextMeshProUGUI ammoText;  // Reference to the TMPro Text element
    public GameObject ammoImage;  // Reference to the Image GameObject

    private int currentAmmo;
    private Player player;
    private Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        currentAmmo = maxAmmo;

        // Ensure the script is disabled at start
        this.enabled = false;

        // Ensure the ammo text and image are initially hidden
        if (ammoText != null)
        {
            ammoText.gameObject.SetActive(false);
        }

        if (ammoImage != null)
        {
            ammoImage.SetActive(false);
        }
    }

    void Update()
    {
        if (player.isKilled || player.disableControl)
        {
            return;
        }

        AimWeapon();

        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
            PlaySoundAtPoint(ShootSFX, transform.position);
            anim.SetBool("Shoot", true); // Trigger the shooting animation
            anim.SetBool("Shoot", false);
        }

        if (currentAmmo <= 0)
        {
            PlaySoundAtPoint(NoAmmoSFX, transform.position);
            DiscardWeapon();
        }

        // Update the ammo text and image
        UpdateAmmoTextAndImage();
    }

    private void PlaySoundAtPoint(AudioClip clip, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("Gun");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(soundGameObject, clip.length); // destroy the sound game object after the clip is done playing
    }

    void AimWeapon()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * projectileSpeed;
        currentAmmo--;
    }

    void DiscardWeapon()
    {
        // Hide the ammo text and image when the weapon is discarded
        if (ammoText != null)
        {
            ammoText.gameObject.SetActive(false);
        }

        if (ammoImage != null)
        {
            ammoImage.SetActive(false);
        }

        Destroy(gameObject);
    }

    void UpdateAmmoTextAndImage()
    {
        if (ammoText != null)
        {
            if (currentAmmo > 0)
            {
                ammoText.text = currentAmmo.ToString();
                if (!ammoText.gameObject.activeSelf)
                {
                    ammoText.gameObject.SetActive(true);
                }
            }
            else
            {
                ammoText.gameObject.SetActive(false);
            }
        }

        if (ammoImage != null)
        {
            if (currentAmmo > 0)
            {
                if (!ammoImage.activeSelf)
                {
                    ammoImage.SetActive(true);
                }
            }
            else
            {
                ammoImage.SetActive(false);
            }
        }
    }

    public void EnableWeapon()
    {
        this.enabled = true;
        currentAmmo = maxAmmo;

        // Show the ammo text and image when the weapon is enabled
        if (ammoText != null)
        {
            ammoText.gameObject.SetActive(true);
            UpdateAmmoTextAndImage();
        }

        if (ammoImage != null)
        {
            ammoImage.SetActive(true);
            UpdateAmmoTextAndImage();
        }
    }
}



