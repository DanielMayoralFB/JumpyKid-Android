using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    #region Variables
    [Header("Scene Variable")]
    [SerializeField] private string escenaCambio;
    #endregion

    #region Methods
    #region Collider Methods
    /// <summary>
    /// When the player collide with finish lane, move to the next scene only when the actual scene is not the 3rd level of the game
    /// If it is, the player is move to the score scene
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            var playerScript = other.GetComponent<PlayerMovement>();
            GameManager.instance.setPlayerPoints(playerScript.getPuntos());
            GameManager.instance.setLevelPoints(playerScript.getPuntos());
            if (!SceneManager.GetActiveScene().name.Equals("Scene3"))
            {
                GameManager.instance.setNextScene(escenaCambio);
                SceneManager.LoadScene("ScoreScene");
            }
            else
            {
                cambioEscene(escenaCambio);
            }
            
        }
    }
    #endregion
    #region Scenes Methods
    /// <summary>
    /// Set the next scene in the GameManager and load the Loading Scene
    /// </summary>
    /// <param name="cambio"></param>
    public void cambioEscene(string cambio)
    {
        if(SceneManager.GetActiveScene().name.Equals("FinalScene") || SceneManager.GetActiveScene().name.Equals("GameOver"))
        {
            //GameManager.instance.resetVariables();
        }
        GameManager.instance.setNextScene(cambio);
        SceneManager.LoadScene("Loading");


    }

    /// <summary>
    /// Close the game
    /// </summary>
    public void quitarJuego()
    {
        Application.Quit();
        Debug.Log("Juego quitado");
    }
    #endregion
    #endregion

    #region Getters y Setters
    public string getScene() { return escenaCambio; }
    #endregion
}
