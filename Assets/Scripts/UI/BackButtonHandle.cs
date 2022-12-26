using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonHandle : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.setPlayerPoints(FindObjectOfType<PlayerMovement>().getPuntos());
            GameManager.instance.resetVariables();
            Application.Quit();
        }
    }
}
