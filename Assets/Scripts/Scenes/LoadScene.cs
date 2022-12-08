using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void sceneLoad(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
