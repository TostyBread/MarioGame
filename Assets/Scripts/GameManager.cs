using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerKills;
    public int coinsCollected;
    public int deathCount;
    public int requiredCoinsForWeaponPickup;
    public int MORETHANrequiredCoinsForWeaponPickup; // 2nd condition if player collects more coins
    public GameObject weaponPickupPrefab;
    public GameObject weaponPickupPrefabBETTER; // A better weapon when player collects more coins
    public Transform weaponSpawnPoint;

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
        if (coinsCollected >= requiredCoinsForWeaponPickup) // if player meets expectation
        {
            SpawnWeaponPickup();
        }

        if (coinsCollected >= MORETHANrequiredCoinsForWeaponPickup) // if player meets above expectation
        {
            SpawnBetterWeaponPickup();
        }

        playerKills = 0;
        coinsCollected = 0;
        deathCount = 0;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("No more scenes in Build Settings.");
        }

        // Activate the UI elements when the game is won
        UpdateUI();
    }

    public void ToggleUI()
    {
        SetUIActive(true);
    }

    private void SpawnWeaponPickup()
    {
        if (weaponSpawnPoint != null && weaponPickupPrefab != null)
        {
            Instantiate(weaponPickupPrefab, weaponSpawnPoint.position, Quaternion.identity);
        }
    }

    private void SpawnBetterWeaponPickup()
    {
        if (weaponSpawnPoint != null && weaponPickupPrefabBETTER != null)
        {
            Instantiate(weaponPickupPrefabBETTER, weaponSpawnPoint.position, Quaternion.identity);
        }
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
}





