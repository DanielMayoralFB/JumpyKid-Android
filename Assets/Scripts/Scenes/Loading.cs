using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    #region Variables
    [SerializeField] Slider barraProgreso;
    #endregion

    #region Unity Methods
    /// <summary>
    /// First check if the next scene is the final scene or the game over scene. If it is, call the resetVariables function.
    /// If it is not, star the loadscene coroutine
    /// </summary>
    private void Start()
    {
        if(GameManager.instance.getNextScene().Equals("MenuScene"))
        {
            GameManager.instance.resetVariables();
        }
        StartCoroutine(this.LoadingScene(GameManager.instance.getNextScene()));
    }
    #endregion

    #region CoRoutine Methods
    /// <summary>
    /// Coroutine that load the next scene and doesn't finish until the next scene is completed loaded
    /// </summary>
    /// <param name="nextScene"></param>
    /// <returns></returns>
    IEnumerator LoadingScene(string nextScene)
    {
        //yield return new WaitForSeconds(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        if(nextScene.Equals("Scene1") || nextScene.Equals("Scene2") || nextScene.Equals("Scene3"))
        {
            if(GameManager.instance.GetComponent<Audio>().isNotPlayingBattleTheme())
                GameManager.instance.GetComponent<Audio>().changeToBattleSong();
        }
        else
        {
            if (!GameManager.instance.GetComponent<Audio>().isNotPlayingBattleTheme())
                GameManager.instance.GetComponent<Audio>().changeToMenuTheme();
        }


        while(!operation.isDone)
        {
            float progess = Mathf.Clamp01(operation.progress / .9f);
            barraProgreso.value = progess;
            yield return null;
        }
    }
    #endregion
}
