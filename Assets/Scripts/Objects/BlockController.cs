using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    #region Variables
    private Animator animator;
    private Vector2 posInicial;
    private BoxCollider2D colliderParent;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        posInicial = transform.position;
        StartCoroutine(blinkCO());
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,posInicial) < 0.1f)
        {
            animator.SetBool("moveUp", false);
        }
    }
    #endregion

    #region CoRoutines
    IEnumerator blinkCO()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isBlinked", true);
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("isBlinked", false);
        yield return new WaitForSeconds(10.0f);
        StartCoroutine(blinkCO());
    }
    #endregion

    #region Collision Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            animator.speed = 1;
            animator.SetBool("moveDown", false);
            animator.SetBool("moveUp", true);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !animator.GetBool("moveUp"))
        {
            animator.SetBool("moveDown", true);
            animator.speed = 0.4f;
        }
    }
    #endregion



}
