using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    private Vector2 vectorDirection;
    private float speed;
    private int timeLife;
    private float damage;
    [SerializeField] private GameObject destroyParts;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timeLife = 0;
    }
    void Start()
    {
        if (!gameObject.CompareTag("SkullBullet"))
        {
            rb.velocity = new Vector2(-25, 0);
        }
        
    }

    private void Update()
    {
        if (gameObject.CompareTag("SkullBullet"))
        {
            transform.Translate(vectorDirection * speed * Time.deltaTime);
        }

        if(timeLife >= 1000)
        {
            Destroy(gameObject);
        }

        timeLife++;
    }
    #endregion

    #region Physics Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().restarVida(damage, 0, new Vector3(0, 0, 0));
        }

        if (!collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(destroyBullet());
        }
    }
    #endregion

    #region CoRoutine Methods
    IEnumerator destroyBullet()
    {
        if (!gameObject.CompareTag("SkullBullet"))
        {
            destroyParts.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(5);
            Destroy(destroyParts);
        }
        Destroy(gameObject);
    }
    #endregion

    #region Other Methods
    public void setMovesParameters(Vector2 direction, float newSpeed)
    {
        vectorDirection = direction;
        speed = newSpeed;
    }

    public void setDamage(float newValue)
    {
        damage = newValue;
    }
    #endregion
}
