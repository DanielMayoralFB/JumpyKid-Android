using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backMainMenu : MonoBehaviour
{

    [SerializeField] private GameObject canvasMainMenu;
    [SerializeField] private GameObject canvasSelector;

    /// <summary>
    /// Back to Main Menu Canvas from Selector Canvas
    /// </summary>
    public void backToMain()
    {
        canvasMainMenu.SetActive(true);
        canvasSelector.SetActive(false);
    }
}
