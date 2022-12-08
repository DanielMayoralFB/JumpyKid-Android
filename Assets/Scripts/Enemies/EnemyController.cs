using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Variables
    protected bool isPlayerInFront;
    protected Ray rayEyes;
    protected RaycastHit2D raycastHit;
    [Header("Enemy Parameter")]
    [SerializeField] protected int lifes;
    [SerializeField] protected int points;
    #endregion

    #region Methods
    /// <summary>
    /// Return true when the player is hit with the ray that enemy cast.
    /// That ray has a origin, a direction and a distance. The distance is the param.
    /// </summary>
    /// <param name="distanceToView"></param>
    /// <returns></returns>
    protected bool IsPlayerInFront(float distanceToView)
    {
        raycastHit = Physics2D.Raycast(rayEyes.origin, rayEyes.direction, distanceToView);
        if (raycastHit)
        {
            if (raycastHit.transform.CompareTag("Player"))
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    protected Vector2 playerPosition(float distanceToView)
    {
        raycastHit = Physics2D.Raycast(rayEyes.origin, rayEyes.direction, distanceToView);
        if (raycastHit)
        {
            if (raycastHit.transform.CompareTag("Player"))
                return new Vector2(raycastHit.transform.position.x,raycastHit.transform.position.y);
            else
                return new Vector2(0, 0);
        }
        else
        {
            return new Vector2(0,0);
        }
    }

    /// <summary>
    /// Return the difference in the x axis between two positions
    /// </summary>
    /// <param name="posAct"></param>
    /// <param name="posInit"></param>
    /// <returns></returns>
    protected float DifferenceBetweenPosition(Vector3 posAct, Vector3 posInit)
    {
        return Mathf.Abs(posAct.x - posInit.x);
    }

    /// <summary>
    /// Update the direction and the origin of the ray that it is used in the IsPlayerInFront
    /// </summary>
    /// <param name="newOrigin"></param>
    /// <param name="newDirection"></param>
    protected void UpdateRay(Vector3 newOrigin, Vector3 newDirection)
    {
        rayEyes.origin = newOrigin;
        rayEyes.direction = newDirection;
    }

    protected Ray getRay() { return rayEyes; }

    /// <summary>
    /// Pass the damage that enemy cause to the player
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="speed"></param>
    /// <param name="pos"></param>
    protected void damageToPlayer(float damage, float speed, Vector3 pos)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().restarVida(damage,speed, pos);
    }


    /// <summary>
    /// Decrease in one the enemy lifes
    /// </summary>
    public void takeDamage()
    {
        lifes -= 1;
    }

    public int getPoints() { return points; }
    #endregion
}
