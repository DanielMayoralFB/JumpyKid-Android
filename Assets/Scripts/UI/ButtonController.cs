using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Boton[] botones;
    [SerializeField] private int position = 0;
    #endregion

    #region Unity Methods
    /// <summary>
    /// Move between UI button when press down
    /// </summary>
    void Update()
    {

        if (Input.GetKey("down"))
        {
            botones[position].setSelected(false);
            position++;

            if(position < 0)
            {
                position = botones.Length - 1;
                botones[position].setSelected(true);
                return;
            }
            if(position > botones.Length - 1)
            {
                position = 0;
                botones[position].setSelected(true);
                return;
            }

            botones[position].setSelected(true);
        }
    }
    #endregion
}
