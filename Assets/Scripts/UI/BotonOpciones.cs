using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonOpciones : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject canvasOptions;
    [SerializeField] private GameObject canvasButtons;
    #endregion

    #region Methods
    /// <summary>
    /// Deactivate options canvas if it is activate or activate it if it is deactivate
    /// </summary>
    public void activarOpciones()
    {
        if (canvasOptions.activeSelf)
        {
            canvasOptions.SetActive(false);
            canvasButtons.SetActive(true);
        }
        else
        {
            canvasOptions.SetActive(true);
            canvasButtons.SetActive(false);
        }
    }
    #endregion

}
