using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PigController : EnemyController
{
    #region Variables
    #region Components
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    #endregion

    #region Parameters
    private Vector3 initalPosition;
    private bool timeToChange;
    [Header("Parameters")]
    [SerializeField] float speed;
    [SerializeField] float speedRunning;
    [SerializeField] float distanceWalk;
    [SerializeField] float distanceSee;
    [SerializeField] float damage;
    #endregion

    #region Ray Parameters
    [Header("RayParameters")]
    private Vector3 directionView;
    [SerializeField] GameObject originRay;
    #endregion
    #endregion

    #region Unity Methods
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        initalPosition = transform.position;
    }

    private void Start()
    {
        rb.velocity = new Vector2(-speed, 0);
        directionView = new Vector3(-1, 0, 0);

        UpdateRay(originRay.transform.position, directionView);
    }

    /// <summary>
    /// The ray is updated in every frame. Checks if the player is in front of the pig. If it is then the pig is angry and start the animation of running and apply the speed of running.
    /// If it is not, the pig move in a normal speed.
    /// Also checks if the pig is angry. If it is not, the pig move between two position that are at the same distance to initial position in the positive and negative x axis.
    /// If the pig has pass this points, then he turn to the other direction of the axis.
    /// </summary>
    private void FixedUpdate()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && lifes > 0)
        {
            animator.ResetTrigger("Hit");


            UpdateRay(originRay.transform.position, directionView);
            if (IsPlayerInFront(distanceSee) && 
                DifferenceBetweenPosition(playerPosition(distanceSee),initalPosition) < 
                distanceWalk)
            {
                animator.SetBool("isRunning", true);
                rb.velocity = new Vector2(-speedRunning, 0);
            }
            else
            {
                animator.SetBool("isRunning", false);
                rb.velocity = new Vector2(-speed, 0);
            }

                if (DifferenceBetweenPosition(transform.position, initalPosition) >= distanceWalk && timeToChange)
                {
                    rb.velocity = new Vector2(-rb.velocity.x, 0);
                    directionView.x = -directionView.x;
                    speed = -speed;
                    speedRunning = -speedRunning;
                    transform.RotateAround(transform.position, transform.up, 180f);
                    timeToChange = false;
                }

                if (DifferenceBetweenPosition(transform.position, initalPosition) < distanceWalk && !timeToChange)
                {
                    timeToChange = true;
                }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }


    /// <summary>
    /// Check if the lifes of the enemy are less than or equals to 0. If it is, then destroy the enemy
    /// </summary>
    private void Update()
    {
        if(lifes <= 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Die");
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
            var forcePlayerPreCollision = collision.rigidbody.mass * Mathf.Pow(collision.gameObject.GetComponent<PlayerMovement>().getSpeedMove(),2) / 2;
            var forcePigPreCollision = GetComponent<Rigidbody2D>().mass * Mathf.Pow(speedRunning,2) / 2;

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
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,10);
            animator.SetTrigger("Hit");
            takeDamage();
        }
    }
    #endregion

    #region Other Methods
    public void deadPig()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().setPuntos(points);
        Destroy(gameObject);
    }
    #endregion
}
