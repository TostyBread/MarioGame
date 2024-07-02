using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    WeaponAvailable spawnweapon;

    public int playerKills;
    public int coinsCollected;
    public int deathCount;
    public int requiredCoinsForWeaponPickup = 25;
    public int MORETHANrequiredCoinsForWeaponPickup = 35; // 2nd condition if player collects more coins

    public TextMeshProUGUI killsText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI deathsText;
    public GameObject uiImage; // Reference to the image GameObject

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initially hide the UI elements
        SetUIActive(false);
        UpdateUI();
    }

    public void AddKill()
    {
        playerKills++;
        UpdateUI();
    }

    public void AddCoin()
    {
        coinsCollected++;
        UpdateUI();
    }

    public void AddDeath()
    {
        deathCount++;
        UpdateUI();
    }

    public void ResetLevel()
    {
        playerKills = 0;
        coinsCollected = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }

        // Activate the UI elements when the game is won
        UpdateUI();
        SetUIActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the WeaponAvailable component in the new scene
        spawnweapon = GameObject.FindGameObjectWithTag("Weaponspawner")?.GetComponent<WeaponAvailable>();

        // Only attempt to spawn the weapon if the player has collected enough coins and the WeaponAvailable component is found
        if (spawnweapon != null)
        {
            if (coinsCollected >= requiredCoinsForWeaponPickup)
            {
                spawnweapon.SpawnWeaponPickup();
            }

            if (coinsCollected >= MORETHANrequiredCoinsForWeaponPickup)
            {
                spawnweapon.SpawnBetterWeaponPickup();
            }
        }

        // Reset the scoreboard after the weapon is spawned
        playerKills = 0;
        coinsCollected = 0;
        deathCount = 0;
    }

    public void ToggleUI()
    {
        SetUIActive(true);
    }

    private void UpdateUI()
    {
        if (killsText != null)
        {
            killsText.text = playerKills.ToString();
        }

        if (coinsText != null)
        {
            coinsText.text = coinsCollected.ToString();
        }

        if (deathsText != null)
        {
            deathsText.text = deathCount.ToString();
        }
    }

    private void SetUIActive(bool isActive)
    {
        if (killsText != null)
        {
            killsText.gameObject.SetActive(isActive);
        }

        if (coinsText != null)
        {
            coinsText.gameObject.SetActive(isActive);
        }

        if (deathsText != null)
        {
            deathsText.gameObject.SetActive(isActive);
        }

        if (uiImage != null)
        {
            uiImage.SetActive(isActive);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event
    }
}






