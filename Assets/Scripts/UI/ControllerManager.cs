using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour
{
    #region Varibles
    private GameManager gameManager;
    [SerializeField] Dropdown dropdown;
    #endregion

    #region Unity Methods
    private void Start()
    {
        gameManager = GameManager.FindObjectOfType<GameManager>();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Change between controller types
    /// </summary>
    public void cambioController()
    {
        switch (dropdown.value)
        {
            case 0:
                gameManager.setController(ControllerType.Keyboard);
                break;
            case 1:
                gameManager.setController(ControllerType.Gamepad);
                break;
        }
    }
    #endregion
}
