using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLevel : MonoBehaviour
{
    #region Variables
    private int timeSecs = 0;
    private int timeMin = 0;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        timeSecs = 0;
        timeMin = 0;
    }
    private void Start()
    {
        timeSecs = 0;
        timeMin = 0;
    }

    private void FixedUpdate()
    {
        timeSecs++;
        if(timeSecs >= 60)
        {
            timeMin++;
            timeSecs = 0;
        }
    }
    #endregion

    #region Methods
    public string getTime()
    {
        return "Time: " + timeMin + ":" + timeSecs;
    }
    #endregion
}
