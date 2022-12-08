using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkController : EnemyController
{
    #region Variables
    #region Components
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletSpawn;
    private Animator animator;
    #endregion
    #region Parameters
    [Header("Parameters")]
    [SerializeField] float speed;
    [SerializeField] float speedRunning;
    [SerializeField] float distanceWalk;
    [SerializeField] float distanceSee;
    [SerializeField] float damage;
    [SerializeField] float speedShoot;
    #endregion
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(trucksMove());
    }

    /// <summary>
    /// Check if the lifes of the enemy are less than or equals to 0. If it is, then destroy the enemy
    /// </summary>
    private void Update()
    {
        if (lifes <= 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Die");
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            animator.ResetTrigger("Hit");
        }
    }
    #endregion

    #region CoRoutine Methods
    IEnumerator trucksMove()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            yield return new WaitForSeconds(speedShoot);
            animator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("isAttacking", false);
            StartCoroutine(trucksMove());
        }
    }
    #endregion

    #region Physics Methods
    /// <summary>
    /// If the pig collied with the player, then is applied the energy consevation to have a velocity in the player.
    /// This velocity depends for the speed of both bodies and her mass. Also it's depend for the final speed of the pig that it is 0.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            damageToPlayer(damage, speedRunning, collision.GetContact(0).normal);
            //conservación de la energía
            //Ma * Va + Mb * Vb = Ma * Va' + Mb * Vb'
            //a es el personahje y b es el cerdo
            var forcePlayerPreCollision = collision.rigidbody.mass * Mathf.Pow(collision.gameObject.GetComponent<PlayerMovement>().getSpeedMove(), 2) / 2;
            var forcePigPreCollision = GetComponent<Rigidbody2D>().mass * Mathf.Pow(speedRunning, 2) / 2;

            var forcePreCollision = forcePlayerPreCollision - forcePigPreCollision;

            var playerVelocityPostCollision = 2 * forcePreCollision / collision.rigidbody.mass;
            var pVelPostSqrt = Mathf.Sqrt(Mathf.Abs(playerVelocityPostCollision));
            collision.rigidbody.velocity = new Vector2(pVelPostSqrt * collision.GetContact(0).normal.x, Mathf.Abs(pVelPostSqrt));
        }
    }

    /// <summary>
    /// If the player enter in the trigger collider that is over the pig head, this trigger the hit animation and decrease the lifes of the pig.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            animator.SetTrigger("Hit");
            takeDamage();
        }
    }
    #endregion

    #region Other Methods
    /// <summary>
    /// Instantiate a bullet
    /// </summary>
    public void Shoot()
    {
        bullet.GetComponent<BulletScript>().setDamage(0.75f);
        Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
    }
    public void deadTrunk()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().setPuntos(points);
        Destroy(gameObject);
    }
    #endregion
}
