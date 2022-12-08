using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideSkull : MonoBehaviour
{
    #region Collision Methods
    /// <summary>
    /// Applies in the player a velocity in Y axis and the enemy takes damage. Then start the stun coroutine.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<Animator>().SetTrigger("Hit");
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            GetComponentInParent<SkullController>().takeDamage();
            StartCoroutine(stunCoRoutine());
        }
    }
    #endregion

    #region Coroutine Methods
    /// <summary>
    /// Coroutine that stun the enemy. Deactivate the rigidbody and the trigger collider. 
    /// That's allow to the player to trespass the enemy and don't hit it in 5 secs.
    /// </summary>
    /// <returns></returns>
    IEnumerator stunCoRoutine()
    {
        var speed = GetComponentInParent<SkullController>().getSpeed();
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponentInParent<SkullController>().GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponentInParent<SkullController>().getChildrenCollider().enabled = false;
        GetComponentInParent<SkullController>().setSpeed(0);
        yield return new WaitForSeconds(5);
        GetComponent<PolygonCollider2D>().enabled = true;
        GetComponentInParent<SkullController>().GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponentInParent<SkullController>().getChildrenCollider().enabled = true;
        GetComponentInParent<SkullController>().setSpeed(speed);
    }
    #endregion
}
