using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalSceneManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Text playerName;
    [SerializeField] private Text playerScore;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        playerName.text = GameManager.instance.getPlayerName();
        playerScore.text = "Total Points: " + GameManager.instance.getPlayerPoints();
    }
    #endregion
}
