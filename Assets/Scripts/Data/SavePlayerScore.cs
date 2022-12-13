using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;


/// <summary>
/// Class that save the information of the player. That is the name and the points that he has at the end of the game
/// </summary>
[System.Serializable]
public class PlayerScore
{
    public PlayerScore(string playerName, int playerPoints)
    {
        this.playerName = playerName;
        this.playerScore = playerPoints;
    }

    public string playerName;
    public int playerScore;
}

/// <summary>
/// Class to store a list for the best player's scores
/// </summary>
/// <typeparam name="PlayeScore"></typeparam>
[System.Serializable]
public class SerializableList<PlayeScore>
{
    public List<PlayerScore> scoreList = new List<PlayerScore>();
}

/// <summary>
/// 
/// </summary>
public class SavePlayerScore : MonoBehaviour
{
    #region Variables
    private SerializableList<PlayerScore> listScores = new SerializableList<PlayerScore>();
    public static SavePlayerScore instance;
    private string path;
    #endregion

    #region Instances
    public SavePlayerScore Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        path = Application.persistentDataPath + Path.DirectorySeparatorChar + "Scores.json";
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Search for a file that exists in the path. If it's not, then create it.
    /// Open the file and read it. Then convert this information from a json that is store in the list of scores
    /// </summary>
    private void Start()
    {
        if (!File.Exists(path))
        {
            File.Create(path);
        }
        File.OpenRead(path);
        string txt = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(txt, listScores.scoreList);

    }
    #endregion

    #region Other Methods
    /// <summary>
    /// Return the list with the player's scores
    /// </summary>
    /// <returns></returns>
    public List<PlayerScore> getListaScores()
    {
        return listScores.scoreList;
    }

    /// <summary>
    /// Save the new score of the game. First, check if the file is complete, but if it's not add the player score.
    /// If the file is completed, check if the new score is better than te last score in the file. If it is, then save this new score.
    /// At last, reorder the list by descending for the scores, convert to json and save it in the file.
    /// </summary>
    /// <param name="playerData"></param>
    public void Save(PlayerScore playerData)
    {
        if (listScores.scoreList.Count < 5)
        {
            listScores.scoreList.Add(playerData);
        }
        else
        {
            var lastScore = listScores.scoreList[listScores.scoreList.Count - 1];
            if (lastScore.playerScore < playerData.playerScore)
            {
                listScores.scoreList.RemoveAt(listScores.scoreList.Count - 1);
                listScores.scoreList.Add(playerData);
                
            }
            else
            {
                Debug.Log("El nuevo score es menor que el anterior");
            }

        }

        listScores.scoreList = listScores.scoreList.OrderByDescending(x => x.playerScore).ToList();
        string txt = JsonUtility.ToJson(listScores);
        Debug.Log(JsonUtility.ToJson(listScores));
        File.WriteAllText(path, txt);
        
    }
    #endregion
}
