using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicPlayer : MonoBehaviour
{

    public void nextScene()
    {
        GameManager.instance.setNextScene("Scene1");

        SceneManager.LoadScene("Loading");
    }
}
