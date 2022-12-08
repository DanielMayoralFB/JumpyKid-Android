using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BajarPlataformas : MonoBehaviour
{
    #region Variables
    private PlatformEffector2D platform;
    private BoxCollider2D colision;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
        colision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("s") && Input.GetKey("b"))
        {
            //platform.enabled = false;
            StartCoroutine(bajarPlataforma());
        }
    }
    #endregion

    #region CoRoutines
    IEnumerator bajarPlataforma()
    {
        platform.enabled = false;
        colision.enabled = false;
        yield return new WaitForSeconds(0.5f);
        platform.enabled = true;
        colision.enabled = true;
    }
    #endregion
}
