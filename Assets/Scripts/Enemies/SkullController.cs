using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : EnemyController
{

    #region Variables
    #region Parameters
    private Vector3 initalPosition;
    private Quaternion initalRotation;
    private bool timeToChange;
    private bool isAngry;
    private bool isAttacking;
    private bool isStun;

    [Header("Parameters")]
    [SerializeField] float speed;
    [SerializeField] float distanceWalk;
    [SerializeField] float magnitude;
    [SerializeField] float damage;
    [SerializeField] float frequencyAttack;
    [SerializeField] float speedBullet;
    private Vector3 pos;
    #endregion
    #region Components
    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    [Header("Children Collider")]
    [SerializeField]private CircleCollider2D colliderChildren;

    [Header("ShootsPrefabs")]
    [SerializeField] List<GameObject> bulletsList;
    #endregion
    #endregion

    #region Unity Methods
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        initalPosition = transform.position;
    }

    private void Start()
    {
        pos = transform.position;
        StartCoroutine(attackCo());
    }

    /// <summary>
    /// Move the skull with a sine curve if it is not dead.
    /// Also check if the skull has traveled the max distance that it can. If it is, then turn around to the opposite direction.
    /// </summary>
    private void FixedUpdate()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && lifes > 0)
        {
            animator.ResetTrigger("Hit");

            if (!isAttacking)
            {
                if (speed > 0)
                {
                    pos += transform.right * Time.deltaTime * speed;
                    transform.position = pos + transform.up * Mathf.Sin(Time.time * speed * 2) * magnitude;
                }
                else
                {
                    pos -= transform.right * Time.deltaTime * speed;
                    transform.position = pos + transform.up * Mathf.Sin(Time.time * speed * 2) * magnitude;
                }
            }


            if (DifferenceBetweenPosition(transform.position, initalPosition) >= distanceWalk && timeToChange)
            {
                speed = -speed;
                transform.RotateAround(transform.position, transform.up, 180f);
                timeToChange = false;
            }

            if (DifferenceBetweenPosition(transform.position, initalPosition) < distanceWalk && !timeToChange)
            {
                timeToChange = true;
            }
        }
    }

    /// <summary>
    /// Check if the skull is angry. If it is, activate the trigger to change of the angry idle.
    /// Also, check if the skull is dead and activate the trigger to destroy it
    /// </summary>
    private void Update()
    {
        if (isAngry)
        {
            animator.SetTrigger("Detect");
        }

        if (lifes <= 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_N"))
        {
            animator.SetTrigger("Die");
        }
    }
    #endregion

    #region Collisiion Methods
    /// <summary>
    /// When the player collide with the skull's rigidbody, he takes damage and applies a velocity to him.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            damageToPlayer(damage, 0, collision.GetContact(0).normal);

            collision.rigidbody.velocity = new Vector2(0, -10);
        }
    }

    /// <summary>
    /// Check if the player is in its range of view.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAngry = true;
            animator.ResetTrigger("NotAngry");
        }
    }

    /// <summary>
    /// Reset the angry's variables and activate the trigger to change of normal idle when the player exit its range of view
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAngry = false;
            animator.SetTrigger("NotAngry");
            animator.ResetTrigger("Detect");
        }
    }
    #endregion

    #region Coroutine Methods

    /// <summary>
    /// Corotutine that perform an attack. Check if the skull is angry, and perform different attack. At least, call the coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator attackCo()
    {
        animator.ResetTrigger("Detect");
        yield return new WaitForSeconds(frequencyAttack);
        isAttacking = true;
        if (isAngry)
        {
            animator.SetTrigger("FuryAttack");
        }
        else
        {
            animator.SetTrigger("NormalAttack");
        }
        StartCoroutine(attackCo());
    }
    #endregion

    #region Animation Functions

    /// <summary>
    /// Function that is called in the middle of the attack animation. This create a bullet every 45 degrees and put a velocity that depends of the angle.
    /// Use trigonometry to calculate the position that the bullet is created and the velocity that it has.
    /// </summary>
    public void attackPerform()
    {
        if (isAngry)
        {
            for(var i=0; i < 360; i += 45)
            {
                float bulDirX = transform.position.x + Mathf.Sin((i * Mathf.PI) / 180.0f);
                float bulDirY = transform.position.y + Mathf.Cos((i * Mathf.PI) / 180.0f);

                var vectorMove = new Vector3(bulDirX, bulDirY, 0);
                var vectorDirection = (vectorMove - transform.position).normalized;

                var posX = transform.position.x + (circleCollider.radius + 0.1f) * Mathf.Cos((i * Mathf.PI) / 180.0f);
                var posY = transform.position.y + (circleCollider.radius + 0.1f) * Mathf.Sin((i * Mathf.PI) / 180.0f);

                var bullet = Instantiate(bulletsList[0], new Vector3(posX,posY,0), new Quaternion(0, 180, 0, 0));
                bullet.GetComponent<BulletScript>().setMovesParameters(vectorDirection, speedBullet);
                bullet.GetComponent<BulletScript>().setDamage(0.75f);
            }
        }
        else
        {
            for (var i = 0; i < 360; i += 45)
            {
                float bulDirX = transform.position.x + Mathf.Sin((i * Mathf.PI) / 180.0f);
                float bulDirY = transform.position.y + Mathf.Cos((i * Mathf.PI) / 180.0f);

                var vectorMove = new Vector3(bulDirX, bulDirY, 0);
                var vectorDirection = (vectorMove - transform.position).normalized;

                var posX = transform.position.x + (circleCollider.radius + 0.2f) * Mathf.Cos((i * Mathf.PI) / 180.0f);
                var posY = transform.position.y + (circleCollider.radius + 0.2f) * Mathf.Sin((i * Mathf.PI) / 180.0f);

                var bullet = Instantiate(bulletsList[1], new Vector3(posX, posY, 0), new Quaternion(0,180,0,0));
                bullet.GetComponent<BulletScript>().setMovesParameters(vectorDirection, speedBullet);
                bullet.GetComponent<BulletScript>().setDamage(0.5f);
            }
        }
    }

    /// <summary>
    /// Function that is called at the end of the attack animation. 
    /// Reset the animator's triggers and put in false the bool variable that control if the skull is attacking.
    /// </summary>
    public void endAttack()
    {
        animator.ResetTrigger("FuryAttack");
        animator.ResetTrigger("NormalAttack");
        isAttacking = false;
    }

    /// <summary>
    /// Function that is called when the skull lose all of his lifes. Destroy the enemy
    /// </summary>
    public void deadSkull()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().setPuntos(points);
        Destroy(gameObject);
    }
    #endregion

    #region Getters y Setters
    public bool isSkullAngry() { return isAngry; }
    public float getSpeed() { return speed; }
    public void setSpeed(float newValue) { speed = newValue; }
    public CircleCollider2D getChildrenCollider() { return colliderChildren; }
    #endregion
}
