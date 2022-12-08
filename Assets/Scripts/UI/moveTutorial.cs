using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTutorial : MonoBehaviour
{
    [SerializeField] private GameObject canvasTutorial1;
    [SerializeField] private GameObject canvasTutorial2;

    public void changeToTutorial2()
    {
        canvasTutorial1.SetActive(false);
        canvasTutorial2.SetActive(true);
    }

    public void changeToTutorial1()
    {
        canvasTutorial1.SetActive(true);
        canvasTutorial2.SetActive(false);
    }
}
