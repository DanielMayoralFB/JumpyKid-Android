using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingManager : MonoBehaviour
{
    #region Variables
    private Animator animator;
    private Rigidbody2D rb;
    private Vector3 initialPosition;
    [SerializeField] float fallTime;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        initialPosition = transform.position;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Collision Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(fall());
        }
    }
    #endregion

    #region Coroutine Methods
    /// <summary>
    /// Coroutine that wait for the fall time to drop the platform. Then after 2.5 seconds this returns to his initial position
    /// </summary>
    /// <returns></returns>
    IEnumerator fall()
    {
        yield return new WaitForSeconds(fallTime);
        rb.isKinematic = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2.5f);
        transform.position = initialPosition;
        GetComponent<BoxCollider2D>().enabled = true;
        rb.isKinematic = true;
        rb.velocity = new Vector3(0, 0, 0);
        yield return null;
    }

    #endregion
}
