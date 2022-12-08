using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreSceneManagment : MonoBehaviour
{
    #region Variables
    private string sceneNameGoTo;

    [SerializeField] private Text nombreJugador;
    //[SerializeField] private Text timeLevel;
    [SerializeField] private Text levelPoints;
    [SerializeField] private Text totalPoints;

    [SerializeField] private Image playerSprite;
    #endregion

    #region Unity Methods
    
    /// <summary>
    /// The score scene find the canvas objects that at show in the ui and assign the values that correspond.
    /// </summary>
    private void Start()
    {
        nombreJugador.text = GameManager.instance.getPlayerName();
        levelPoints.text = "Level Points: " + GameManager.instance.getLevelPoints()+"/"+GameManager.instance.getTotalLevel();
        //timeLevel.text = "Time: ";
        totalPoints.text = "Total Points: " + GameManager.instance.getPlayerPoints();
        playerSprite.sprite = GameManager.instance.getJugadorLista(GameManager.instance.getPlayerChoice()).GetComponent<SpriteRenderer>().sprite;
        //StartCoroutine(GoToScene());
    }
    #endregion

    #region Methods
    /// <summary>
    /// Go to Load Scene
    /// </summary>
    public void nextScene() { SceneManager.LoadScene("Loading"); }
    #endregion

    #region CoRoutines
    IEnumerator GoToScene()
    {
        yield return new WaitForSeconds(10.0f);
        SceneManager.LoadScene(GameManager.instance.getNextScene(), LoadSceneMode.Single);
    }
    #endregion
}
