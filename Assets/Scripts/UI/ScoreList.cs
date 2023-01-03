using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreList : MonoBehaviour
{
    #region Variables
    [SerializeField] Text lista;
    #endregion

    #region Unity Methods
    /// <summary>
    /// Show the list of the best scores
    /// </summary>
    void Start()
    {
        foreach(PlayerScore score in SavePlayerScore.instance.getListaScores())
        {
            lista.text += score.playerName + ": " + score.playerScore + " (" + score.fecha+")\n";
        }
    }
    #endregion
}
