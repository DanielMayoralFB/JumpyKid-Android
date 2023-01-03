using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region Enums
public enum ControllerType
{
    Gamepad,
    Keyboard
}

public enum CharacterType
{
    Ninja,
    Frog,
    Maskerade
}
#endregion
public class GameManager : MonoBehaviour
{
    #region Variables
    #region Player Variables
    [SerializeField]private int playerPoints;
    private int playerLifes;
    private float actualLife;
    private string playerName;
    private int playerChoice;
    private Vector3 respawnPosition;
    #endregion

    #region Game Variables
    private ControllerType controller;
    public static GameManager instance;
    [SerializeField] private List<GameObject> listaJugadores;
    #endregion

    #region Level Variables
    private int levelPoints;
    private string nextScene;
    private int timeLevel;
    private int totalLevel;
    #endregion

    #endregion

    #region Instances
    public GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    #region Unity Methods
    void Start()
    {
        controller = ControllerType.Keyboard;
        actualLife = 1;
        playerPoints = 0;
        playerLifes = 3;
        playerName = "";
        levelPoints = 0;
    }

    private void Awake()
    {
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
    #endregion

    #region Getters y Setters
    /// <summary>
    /// Set the character that the player had chosen
    /// </summary>
    /// <param name="value"></param>
    public void setCharacter(int value)
    {
        playerChoice = value;
    }

    /// <summary>
    /// Return the player lifes. This is a int variable, represent the entire lifes that player has
    /// </summary>
    /// <returns></returns>
    public int getPlayerLifes() { return playerLifes; }

    /// <summary>
    /// Return the value of the actual life. This life represent the life that player use at this moment. 
    /// It's a float variable because it has decimal part
    /// </summary>
    /// <returns></returns>
    public float getActualLife() { return actualLife; }

    /// <summary>
    /// Return the points that the player has
    /// </summary>
    /// <returns></returns>
    public int getPlayerPoints() { return playerPoints; }

    /// <summary>
    /// Return the name of the player
    /// </summary>
    /// <returns></returns>
    public string getPlayerName() { return playerName; }

    /// <summary>
    /// Return the points that the player collected in the actual level
    /// </summary>
    /// <returns></returns>
    public int getLevelPoints() { return levelPoints; }

    /// <summary>
    /// Return what character the player choose
    /// </summary>
    /// <returns></returns>
    public int getPlayerChoice() { return playerChoice; }

    /// <summary>
    /// Return the next scene that the player is moved to
    /// </summary>
    /// <returns></returns>
    public string getNextScene() { return nextScene; }

    /// <summary>
    /// Return the time that the player need to pass the level
    /// </summary>
    /// <returns></returns>
    public int getTimeLevel() { return timeLevel; }

    /// <summary>
    /// Return the total points that the player can collect in the level
    /// </summary>
    /// <returns></returns>
    public int getTotalLevel() { return totalLevel; }

    /// <summary>
    /// Return the GameObject of the character that the player choose
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject getJugadorLista(int position) { return listaJugadores[position].gameObject; }

    /// <summary>
    /// Return the respawn position
    /// </summary>
    /// <returns></returns>
    public Vector3 getRespawnPosition() { return respawnPosition; }

    /// <summary>
    /// Return what controller is using
    /// </summary>
    /// <returns></returns>
    public ControllerType getController() { return controller; }

    /// <summary>
    /// Take the damage that player suffer and subtract for the actual life
    /// </summary>
    /// <param name="damage"></param>
    public void damagePlayer(float damage) 
    { 
        actualLife -= damage; 
        if (actualLife < 0) 
        {
            actualLife = 0;
        } 
    }

    /// <summary>
    /// Subtract one life to the player lifes and reset actual life variable to 1
    /// </summary>
    public void decreaseLifes() { playerLifes -= 1; actualLife = 1; }

    /// <summary>
    /// Add lifes to the player lifes
    /// </summary>
    /// <param name="lifes"></param>
    public void increaseLifes(int lifes) { playerLifes += lifes; }

    /// <summary>
    /// Increase the player points 
    /// </summary>
    /// <param name="pointsLevel"></param>
    public void setPlayerPoints(int pointsLevel) { playerPoints += pointsLevel; }

    /// <summary>
    /// Set the points that player has in the level
    /// </summary>
    /// <param name="pointsLevel"></param>
    public void setLevelPoints(int pointsLevel) { levelPoints = pointsLevel; }

    /// <summary>
    /// Set the next scene
    /// </summary>
    /// <param name="scene"></param>
    public void setNextScene(string scene) { nextScene = scene; }

    /// <summary>
    /// Set level time
    /// </summary>
    /// <param name="time"></param>
    public void setLevelTime(int time) { timeLevel = time; }

    /// <summary>
    /// Set the points of the level
    /// </summary>
    /// <param name="total"></param>
    public void setTotalLevel(int total) { totalLevel = total; }

    /// <summary>
    /// Set player's name
    /// </summary>
    /// <param name="name"></param>
    public void setPlayerName(string name) { playerName = name; }

    /// <summary>
    /// Set player's choice
    /// </summary>
    /// <param name="choice"></param>
    public void setPlayerChoice(int choice) { playerChoice = choice; }

    /// <summary>
    /// Set the controller
    /// </summary>
    /// <param name="newController"></param>
    public void setController(ControllerType newController) { controller = newController; }
    /// <summary>
    /// Increase the actual life
    /// </summary>
    /// <param name="newValue"></param>
    public void restoreLife(float newValue) 
    { 
        actualLife += newValue;
        if (actualLife > 1 && playerLifes < 3)
        {
            playerLifes++;
            actualLife = 0.25f;
        }
        else if(actualLife > 1 && playerLifes == 3)
        {
            actualLife = 1;
        }
    }

    /// <summary>
    /// Set the new respawn position
    /// </summary>
    /// <param name="newPosition"></param>
    public void setRespawnPosition(Vector3 newPosition) { respawnPosition = newPosition; }
    #endregion

    #region Methods
    /// <summary>
    /// Function that is called when the player doesn't have any lifes. Load GameOverScene
    /// </summary>
    public void endGame() 
    {
        nextScene = "GameOverScene";
        SceneManager.LoadScene("Loading");
    }

    /// <summary>
    /// Function called when the game is over. Even when the player wins or he loses.
    /// Try to save the new scores and then reset all variables
    /// </summary>
    public void resetVariables()
    {
        //Guardamos la información del Score antes de resetear las variables
        var playerData = new PlayerScore(playerName,playerPoints, System.DateTime.Now.ToString("dd/MM/yyyy"));
        SavePlayerScore.instance.Save(playerData);

        playerChoice = 0;
        actualLife = 1;
        playerLifes = 3;
        playerName = "";
        playerPoints = 0;
        levelPoints = 0;
        timeLevel = 0;
        controller = ControllerType.Keyboard;
    }
    #endregion
}
