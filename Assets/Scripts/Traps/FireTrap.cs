using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    #region Variables
    private Animator animator;
    private bool fuegoOn = false;

    private PlayerMovement player;
    [SerializeField] float timeFireOn;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        StartCoroutine(fireOnCO());
    }
    #endregion

    #region CoRoutines
    IEnumerator fireOnCO()
    {
        animator.SetBool("On", true);
        fuegoOn = true;
        yield return new WaitForSeconds(timeFireOn);
        animator.SetBool("On", false);
        fuegoOn = false;
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(fireOnCO());
    }
    #endregion

    #region Collision Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            
            if (fuegoOn)
            {
                player.restarVida(1,0,transform.position);
            }
            else
            {
                animator.SetBool("jugadorEncima", true);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            animator.SetBool("jugadorEncima", false);
        }
    }
    #endregion
}
