using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    #region Variables
    [SerializeField] private float life;
    private Animator animator;
    #endregion

    #region Unity Methods
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Play the idle animation
    /// </summary>
    void Update()
    {
        animator.Play("HeartIdle");
    }
    #endregion

    #region Collision Methods
    /// <summary>
    /// Restore the player's actual life with the value of life
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.restoreLife(life);
            Destroy(gameObject);
        }
    }
    #endregion
}
