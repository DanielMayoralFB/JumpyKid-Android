using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBox : MonoBehaviour
{
    #region Variables
    #region Components
    [Header("Components")]
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] GameObject brokenParts;
    [SerializeField] GameObject boxCollider;
    [SerializeField] GameObject fruitLinked;
    private Collider2D col2D;
    #endregion

    #region Parameters
    [Header("Parameters")]
    public float jumpForce = 4f;
    public int lifes = 1;
    #endregion
    #endregion

    #region Unity Methods
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col2D = GetComponentInChildren<Collider2D>();

    }
    #endregion

    #region Collision Methods
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * jumpForce;
            StartCoroutine(LosseLifeAndHit());
        }
    }
    #endregion

    #region CoRoutines
    /// <summary>
    /// When the player hit the box, subtract one life and play the animation.
    /// If the box doesn't have more lifes it is deactivated and activate the spirte of broken parts and the fruit of it is inside.
    /// Then and after 1 seconds the game object is destroy
    /// </summary>
    /// <returns></returns>
    IEnumerator LosseLifeAndHit()
    {
        lifes--;
        animator.SetBool("Golpeada", true);
        yield return new WaitForSeconds(0.3f);
        if (CheckLife())
        {
            animator.SetBool("Golpeada", false);
            yield return null;
        }
        else
        {
            
            boxCollider.SetActive(false);
            col2D.enabled = false;
            brokenParts.SetActive(true);
            fruitLinked.GetComponent<SpriteRenderer>().enabled = true;
            fruitLinked.GetComponent<BoxCollider2D>().enabled = true;
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.5f);
            Invoke("DestroyBox", 0.5f);
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Check that the box has lifes
    /// </summary>
    /// <returns></returns>
    public bool CheckLife()
    {
        if (lifes <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void DestroyBox()
    {
        Destroy(gameObject);

    }
    #endregion
}
