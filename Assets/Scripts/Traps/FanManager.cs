using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class FanManager : MonoBehaviour
{
    #region Variables
    private Animator animator;
    [SerializeField] GameObject smoke;
    
    #endregion

    #region Unity Methods
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(activateFan());
    }
    #endregion

    #region CoRoutines
    /// <summary>
    /// Activate the fan and create some particles to effects. It is activated for 10 seconds and wait 2 seconds to restart coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator activateFan() {

        yield return new WaitForSeconds(5.0f);
        animator.SetBool("activate", true);
        smoke.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        animator.SetBool("activate", false);
        smoke.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(activateFan());
    }
    #endregion

    



}
