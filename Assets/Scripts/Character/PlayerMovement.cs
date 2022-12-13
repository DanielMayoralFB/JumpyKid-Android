using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerMovement : MonoBehaviour
{
    #region Variables
    #region Character Coponents
    private Rigidbody2D myRB;
    private Animator animator;
    private SpriteRenderer mySR;
    private GamepadController gamepad;
    private GameManager gameManager;
    [Header("Collider Filter")]
    [SerializeField] private BoxCollider2D myCollider;
    private ContactFilter2D filter;
    [SerializeField] private Joystick joystick;
    #endregion

    #region UI Variables
    [Header("UI Variables")]
    private Text textoVidas;
    private Text textoPuntos;
    private RawImage[] lifesUI = new RawImage[3];
    [SerializeField] private Sprite[] hearts = new Sprite[5];
    #endregion

    #region Actions Variables
    [Header("Action Variables")]
    [SerializeField] private float speedMove;
    [SerializeField] private float speedJump;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    private int puntos = 0;
    private bool canMove;
    private IEnumerator coroutineRespawn = null;
    private bool doubleJump;
    private bool isJumping;
    public bool isPressing;
    #endregion

    #endregion

    #region Unity Methods
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();


        //textoVidas = GameObject.FindGameObjectWithTag("VidasUI").GetComponent<Text>();
        lifesUI[0] = GameObject.FindGameObjectWithTag("Heart1").GetComponent<RawImage>();
        lifesUI[1] = GameObject.FindGameObjectWithTag("Heart2").GetComponent<RawImage>();
        lifesUI[2] = GameObject.FindGameObjectWithTag("Heart3").GetComponent<RawImage>();
        textoPuntos = GameObject.FindGameObjectWithTag("PuntosUI").GetComponent<Text>();
        //textoVidas.text = "Vidas: " + GameManager.instance.getActualLife();
        textoPuntos.text = "Puntos: " + puntos;

        gameManager = FindObjectOfType<GameManager>();

        joystick = FindObjectOfType<Joystick>();

        filter.minNormalAngle = 45;
        filter.maxNormalAngle = 135;
        filter.useNormalAngle = true;
        filter.layerMask = LayerMask.GetMask("Ground");

        canMove = true;
        doubleJump = false;
    }


    void FixedUpdate()
    {
        if (myRB.velocity.y < 0 && !isGrounded)
        {
            myRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;

        }

        if (myRB.velocity.y > 0)
        {
            if (!isPressing)
            {
                myRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }

    }

    private void Update()
    {
        if (GameManager.instance.getPlayerLifes() > 0)
        {
            textoPuntos.text = "Puntos: " + puntos;
            //textoVidas.text = "Vidas: " + GameManager.instance.getActualLife();
            updateLifesUI();

            controlesJoystick();


        }
        else
        {
            StartCoroutine(endGame());
        }
    }
    #endregion

    #region Physics Methods
    //detect when the player colliled with a block or a spike and then call to respawn coroutine
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("BlockHead") && this.coroutineRespawn == null)
        {
            this.coroutineRespawn = respawn();
            StartCoroutine(this.coroutineRespawn);
        }
        else if (collision.transform.CompareTag("Spike") && this.coroutineRespawn == null)
        {

            this.coroutineRespawn = respawn();
            StartCoroutine(this.coroutineRespawn);
        }
    }

    //detect when a box collider enter in the trigger collider of the player. If this box has Finish tag then star finishLane coroutine.
    // If the box has Vacio tag, then the player respawn. But if the tag is Fan, then applies a velocity in Y axis.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            StartCoroutine(finishLane());
        }

        if (other.CompareTag("Vacio") && this.coroutineRespawn == null)
        {
            this.coroutineRespawn = respawn();
            StartCoroutine(this.coroutineRespawn);
        }
    }

   
    #endregion

    #region CoRoutines Methods
    /// <summary>
    /// Coroutine that is called when player touch the finish of the level. That start the animation of dissapearing and deacivated the player's gameobject.
    /// </summary>
    /// <returns></returns>
    IEnumerator finishLane()
    {
        animator.SetBool("End", true);
        yield return new WaitForSeconds(0.5f);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Coroutine that is called when the player doesn´t have any life. 
    /// Activate the player's animation of dissapering and deactivated the player's gameobject. 
    /// In the end, called to endGame function of the GameManager.
    /// </summary>
    /// <returns></returns>
    IEnumerator endGame()
    {
        animator.SetBool("End", true);
        yield return new WaitForSeconds(0.5f);
        //Destroy(gameObject);
        gameObject.SetActive(false);
        GameManager.instance.setPlayerPoints(puntos);
        GameManager.instance.endGame();

    }

    /// <summary>
    /// Coroutine that is called when the player's respawn in the level.
    /// Check if the player has lifes greater than 0. If that's true, the player respawn in the position that is controlled with GameManager and decrease the lifes in 1.
    /// But if it's false, start endGame coroutine.
    /// </summary>
    /// <returns></returns>
    IEnumerator respawn()
    {
        animator.SetBool("End", true);
        yield return new WaitForSeconds(0.5f);

        if (GameManager.instance.getPlayerLifes() > 0)
        {
            gameObject.transform.position = GameManager.instance.getRespawnPosition();
            GameManager.instance.decreaseLifes();
            Start();
            animator.SetBool("End", false);
            yield return null;
            this.coroutineRespawn = null;
        }
        else
        {
            gameObject.SetActive(false);
            StartCoroutine(endGame());
            this.coroutineRespawn = null;
        }
        
    }

    /// <summary>
    /// Coroutine that is executed when the player recive a hit from an enemy. 
    /// The player can't move for 2.5 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator playerCannotMove()
    {
        canMove = false;
        yield return new WaitForSeconds(2.5f);
        canMove = true;
    }
    #endregion

    #region Getters y Setters
    public void setPuntos(int sumaPuntos)
    {
        this.puntos += sumaPuntos;
    }
    public void setCanMove(bool newValue) { canMove = newValue; }
    public int getPuntos() { return puntos; }
    public bool getCanMove() { return canMove; }
    public float getSpeedMove() { return speedMove; }
    #endregion

    #region Input Methods
    /// <summary>
    /// Function that have all logic of the keyboard's input.
    /// </summary>
    void controlesJoystick()
    {
        if (joystick.Horizontal > 0f)
        {
            myRB.velocity = new Vector2(speedMove, myRB.velocity.y);
            mySR.flipX = false;
            animator.SetBool("Walk", true);
        }
        else if (joystick.Horizontal < 0f)
        {
            myRB.velocity = new Vector2(-speedMove, myRB.velocity.y);
            mySR.flipX = true;
            animator.SetBool("Walk", true);
        }
        else
        {
            if(canMove)
                myRB.velocity = new Vector2(0, myRB.velocity.y);
            //mySR.flipX = false;
            animator.SetBool("Walk", false);
        }

        if (!isGrounded)
        {
            animator.SetBool("Jumping", true);
            animator.SetBool("Walk", false);
        }
        if (isGrounded)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Fall", false);
            animator.SetBool("DoubleJump", false);
        }

        if (myRB.velocity.y < 0 && !isGrounded)
        {
            canMove = true;
            animator.SetBool("Fall", true);

        }

        if (myRB.velocity.y > 0)
        {
            animator.SetBool("Fall", false);
        }


        if(myRB.velocity.y == 0)
        {
            doubleJump = false;
        }
    }

    public void jump()
    {
        if (isGrounded)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, speedJump);
            doubleJump = true;
        }
        else
        {
            if (doubleJump)
            {
                animator.SetBool("DoubleJump", true);
                myRB.velocity = new Vector2(myRB.velocity.x, speedJump);
                doubleJump = false;
            }
            
        }
    }

    
    #endregion

    #region Other Methods
    /// <summary>
    /// Reset the control variables to jump
    /// </summary>
    public void resetJumps()
    {
        doubleJump = false;
    }

    /// <summary>
    /// This function take the damage of the enemy's hit. First, call damagePlayer function from GameManager. 
    /// Then check if the last player life are greater than 0.
    /// If it is true, start playerCannotMove coroutine. But if it is not true, start the respawn coroutine.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="speed"></param>
    /// <param name="colPosition"></param>
    public void restarVida(float damage, float speed, Vector3 colPosition)
    {
        GameManager.instance.damagePlayer(damage);

        if (GameManager.instance.getActualLife() > 0)
        {
            /*var speedDifference = speed - speedMove;
            var force = new Vector2(-speedDifference, 0);
            Debug.Log(force);
            myRB.AddForce(force, ForceMode2D.Impulse);*/
            //start hit animation
            StartCoroutine(playerCannotMove());
        }
        else
        {
            StartCoroutine(respawn());
        }

    }

    /// <summary>
    /// Check if the player's collider touch the ground
    /// </summary>
    bool isGrounded => myCollider.IsTouching(filter);

    /// <summary>
    /// Update the ui hearts that correspond with the actual life of the player and the total lifes that it has
    /// </summary>
    void updateLifesUI()
    {
        var totalLifes = GameManager.instance.getPlayerLifes();
        var actualLife = GameManager.instance.getActualLife();

        switch (totalLifes)
        {
            case 1:
                switch (actualLife)
                {
                    case 0.25f:
                        lifesUI[0].texture = hearts[1].texture;
                        lifesUI[1].texture = hearts[0].texture;
                        lifesUI[2].texture = hearts[0].texture;
                        break;
                    case 0.5f:
                        lifesUI[0].texture = hearts[2].texture;
                        lifesUI[1].texture = hearts[0].texture;
                        lifesUI[2].texture = hearts[0].texture;
                        break;
                    case 0.75f:
                        lifesUI[0].texture = hearts[3].texture;
                        lifesUI[1].texture = hearts[0].texture;
                        lifesUI[2].texture = hearts[0].texture;
                        break;
                    case 1:
                        lifesUI[0].texture = hearts[4].texture;
                        lifesUI[1].texture = hearts[0].texture;
                        lifesUI[2].texture = hearts[0].texture;
                        break;
                }
                break;
            case 2:
                switch (actualLife)
                {
                    case 0.25f:
                        lifesUI[0].texture = hearts[4].texture;
                        lifesUI[1].texture = hearts[1].texture;
                        lifesUI[2].texture = hearts[0].texture;
                        break;
                    case 0.5f:
                        lifesUI[0].texture = hearts[4].texture;
                        lifesUI[1].texture = hearts[2].texture;
                        lifesUI[2].texture = hearts[0].texture;
                        break;
                    case 0.75f:
                        lifesUI[0].texture = hearts[4].texture;
                        lifesUI[1].texture = hearts[3].texture;
                        lifesUI[2].texture = hearts[0].texture;
                        break;
                    case 1:
                        lifesUI[0].texture = hearts[4].texture;
                        lifesUI[1].texture = hearts[4].texture;
                        lifesUI[2].texture = hearts[0].texture;
                        break;
                }
                break;
            case 3:
                switch (actualLife)
                {
                    case 0.25f:
                        lifesUI[0].texture = hearts[4].texture;
                        lifesUI[1].texture = hearts[4].texture;
                        lifesUI[2].texture = hearts[1].texture;
                        break;
                    case 0.5f:
                        lifesUI[0].texture = hearts[4].texture;
                        lifesUI[1].texture = hearts[4].texture;
                        lifesUI[2].texture = hearts[2].texture;
                        break;
                    case 0.75f:
                        lifesUI[0].texture = hearts[4].texture;
                        lifesUI[1].texture = hearts[4].texture;
                        lifesUI[2].texture = hearts[3].texture;
                        break;
                    case 1:
                        lifesUI[0].texture = hearts[4].texture;
                        lifesUI[1].texture = hearts[4].texture;
                        lifesUI[2].texture = hearts[4].texture;
                        break;
                }

                break;
        }
    }
    #endregion
}
