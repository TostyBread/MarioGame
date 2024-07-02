using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSwitcher : MonoBehaviour
{
    public GameObject mainMenuUI; // Main Menu UI toggle
    public GameObject functionMenuUI; // Function Menu UI toggle

    public void OnFunctionButtonClicked() // when fucntion button clicked, run this
    {
        mainMenuUI.SetActive(false);
        functionMenuUI.SetActive(true);
    }
    public void OnMainMenuButtonClicked() // when main menu button clicked, run this
    {
        mainMenuUI.SetActive(true);
        functionMenuUI.SetActive(false);
    }
}
