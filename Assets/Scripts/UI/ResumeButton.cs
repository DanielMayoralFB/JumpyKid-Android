using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject canvasPause;
    #endregion

    #region Methods
    /// <summary>
    /// Resume the game an deactivate the pause canvas
    /// </summary>
    public void resume()
    {
        Time.timeScale = 1;
        canvasPause.SetActive(false);
        
    }
    #endregion
}
