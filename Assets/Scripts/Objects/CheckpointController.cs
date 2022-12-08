using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    #region Variables
    private Animator animator;
    private bool isChecked;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        isChecked = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    #endregion

    #region Collision Methods
    /// <summary>
    /// When the player pass through it, it is assign like the respawn point and start the animation with te flag
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isChecked)
        {
            GameManager.instance.setRespawnPosition(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
            animator.SetBool("isCheck", true);
            isChecked = true;
        }
    }
    #endregion
}
