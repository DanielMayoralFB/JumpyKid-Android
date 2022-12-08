using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{

    #region Variables
    private Animator animator;
    #endregion


    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.Play("Credits");
    }
    #endregion
}
