using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAvailable : MonoBehaviour
{
    public GameObject weaponPickupPrefab;
    public GameObject weaponPickupPrefabBETTER; // A better weapon when player collects more coins

    public void SpawnWeaponPickup()
    {
        weaponPickupPrefab.SetActive(true);
    }

    public void SpawnBetterWeaponPickup()
    {
        weaponPickupPrefabBETTER.SetActive(true);
    }
}
