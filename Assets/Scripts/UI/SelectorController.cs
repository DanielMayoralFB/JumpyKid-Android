using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorController : MonoBehaviour
{
    #region Variables
    private GameManager gameManager;
    [SerializeField] private int opcion;
    [SerializeField] private GameObject errorMessage;
    private CambioEscena cargarEscena;
    [SerializeField] private Text playerName;
    #endregion

    #region Unity Methods
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        cargarEscena = GetComponent<CambioEscena>();
        //playerName = GameObject.FindGameObjectWithTag("PlayerName").GetComponent<Text>();

    }
    #endregion

    #region Methods
    /// <summary>
    /// Save the choices of the player like his name and the character that he selects. Then change to next scene
    /// </summary>
    public void selectCharacter()
    {
        if (playerName.text.Equals(""))
        {
            errorMessage.SetActive(true);
        }
        else
        {
            GameManager.instance.setPlayerName(playerName.text);
            GameManager.instance.setCharacter(opcion);
            cargarEscena.cambioEscene(cargarEscena.getScene());
        }
        
    }
    #endregion
}
