using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    #region Variables
    private AudioSource source;
    [SerializeField] Slider slider;
    #endregion

    #region Unity Methods
    private void Start()
    {
        source = FindObjectOfType<AudioSource>();
    }
    #endregion

    #region Method
    /// <summary>
    /// Changes the volumne with value of the slider
    /// </summary>
    public void cambioVolumen()
    {
        source.volume = slider.value;
    }
    #endregion
}
