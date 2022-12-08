using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineManager : MonoBehaviour
{
    #region Variables
    private Animator animator;

    [SerializeField] float jumpForce = 10f;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    #endregion

    #region Collision Methods
    /// <summary>
    /// Play the animation of the trampoline and applies a velocity to the player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * jumpForce);
            animator.Play("Jump");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }
    }
    #endregion

}
