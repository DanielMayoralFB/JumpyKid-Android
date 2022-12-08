using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject canvasPause;
    #endregion

    #region Methods
    /// <summary>
    /// Resume the game and deactivate the pause canvas
    /// </summary>
    public void resume()
    {
        Time.timeScale = 1;
        canvasPause.SetActive(false);
    }

    /// <summary>
    /// Go to Main Menu
    /// </summary>
    public void mainMenu()
    {
        Time.timeScale = 1;
        GameManager.instance.resetVariables();
        SceneManager.LoadScene("MenuScene");
    }
    #endregion
}
