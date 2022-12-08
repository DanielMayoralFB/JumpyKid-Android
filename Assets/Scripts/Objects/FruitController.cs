using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FruitController : MonoBehaviour
{
    #region Variables
    private Animator animator;
    [SerializeField] private int puntos;
    private bool isRecollected;

    private PlayerMovement player;
    private AudioSource source;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        isRecollected = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    #endregion

    #region Collision Methods
    /// <summary>
    /// When the player collide with te fruit, add the points to the player poinst count and destroy this gameobject
    /// Has isRecollected because with the delay between the time that the player collects the fruit and this is destroyed, the player can collected it many times.
    /// With this bool the player can collect one time the fruit
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = false;
            if (!isRecollected)
            {
                player.setPuntos(puntos);
                source.Play();
                isRecollected = true;
            }
            Destroy(gameObject, 0.5f);
        }
    }
    #endregion

    #region Getters y Setters
    public int getPuntos() { return puntos; }
    #endregion

}
