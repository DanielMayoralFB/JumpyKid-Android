using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Variables
    private GameObject character;
    private Vector3 relativePosition;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        relativePosition = transform.position - character.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = character.transform.position + relativePosition;
    
    }
    #endregion
}
