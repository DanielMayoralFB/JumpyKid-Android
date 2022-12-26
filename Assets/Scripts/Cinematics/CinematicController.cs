using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CinematicController : MonoBehaviour
{
    [SerializeField] private List<PlayableAsset> cinematicParts= new List<PlayableAsset>();
    private PlayableDirector directorControl;

    private void Awake()
    {
        directorControl= GetComponent<PlayableDirector>();
    }
    private void Start()
    {
        //directorControl.playableAsset = cinematicParts[0];
    }
    private void Update()
    {
        if (directorControl.playableAsset.name.Equals(cinematicParts[1].name) &&
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            
            directorControl.playableAsset = cinematicParts[2];
            directorControl.Play();
        }
    }

    public void changeToPart2() 
    {
        directorControl.playableAsset= cinematicParts[1];
        directorControl.Play();
    }

    
}
