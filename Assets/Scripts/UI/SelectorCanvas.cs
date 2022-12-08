using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorCanvas : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject selectorCanvas;
    [SerializeField] GameObject canvasOwner;
    #endregion

    /// <summary>
    /// Changes the menu canvas to the selector canvas
    /// </summary>
    #region Methods
    public void goToSelector()
    {
        selectorCanvas.SetActive(true);
        canvasOwner.SetActive(false);
    }
    #endregion
}
