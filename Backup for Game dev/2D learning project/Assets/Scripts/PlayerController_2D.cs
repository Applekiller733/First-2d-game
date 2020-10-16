using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_2D : MonoBehaviour
{
    internal Animator animator;
    internal Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    public bool keyacquired = false;

    int buildindex;

    
    internal int playerHealth;
    [SerializeField]
    internal int playerMaxHealth;
    [SerializeField]
    internal HealthBar healthBar;
    [SerializeField]
    internal Manabar manaBar;
    internal int playerMana;
    [SerializeField]
    internal int playerMaxMana;

    internal bool isMoving;
    internal bool isGrounded;
    

    public GameObject playerScriptMain;
    public GameObject playerScriptMelee;
    public GameObject playerCircleCollider;

    [SerializeField]
    internal ParticleSystem bloodTest;

    [SerializeField]
    internal PlayerMelee playerMelee;
    [SerializeField]
    internal PlayerAnimations playerAnim;
    [SerializeField]
    internal PlayerCollision playerCollision;
    [SerializeField]
    internal Fireball playerFireball;
    
    
    [SerializeField]
    internal Transform groundCheck;
    [SerializeField]
    internal Transform groundCheckL;
    [SerializeField]
    internal Transform groundCheckR;
    [SerializeField]
    private float jogSpeed = 1.5f;

    public bool enemyKilledRecently;
    public bool FacingRight = true;
    [SerializeField]
    float starttimetoRegenhp;
    
    float timetoRegenhp;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = playerMaxHealth;
        playerMana = playerMaxMana;
        healthBar.SetMaxHealth(playerMaxHealth);
        manaBar.SetMaxMana(playerMaxMana);
        enemyKilledRecently = false;
    }

    private void FixedUpdate()
    {
        //Explanation: if the linecast goes from the player to the ground object in that trajectory and it hits the layer called ground, it will make isgrounded true.
        //NOTE: The ground check gameobject has to be lower than the player, as the linecast shoots a line between the player Y position and the GOB y position.
        HealthRegen();

        if (Input.GetKey("d"))
        {
            rb2d.velocity = new Vector2(jogSpeed, rb2d.velocity.y);
            if (isGrounded)
            {
                isMoving = true;
                animator.SetBool("isMo", true);
            }
            //transform.localScale = new Vector2(5, 5);
            if (!FacingRight)
            {
                FacingRight = true;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else if (Input.GetKey("a"))
        {
            rb2d.velocity = new Vector2(-jogSpeed, rb2d.velocity.y);
            if (isGrounded)
            {
                isMoving = true;
                animator.SetBool("isMo", true);
            }
            //transform.localScale = new Vector2(-5, 5);
            if (FacingRight)
            {
                FacingRight = false;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            isMoving = false;
            animator.SetBool("isMo", false);
        }

        if (Input.GetKeyDown("space") && isGrounded)
        {
            rb2d.velocity = new Vector2(0, 7f);
            isMoving = false;
            animator.SetBool("isMo", false);
        }
        /*if (Input.GetKeyDown("f"))
        {
            playerMana = playerMana - 10;
            manaBar.SetMana(playerMana);
        } */
        //playerCollision.OnCollisionwithTakedmg();
        HealthCheck();
    }
    void HealthRegen()
    {
        if (enemyKilledRecently)
        {
            
                if (playerHealth + 15 < playerMaxHealth)
                {
                    playerHealth += 15;
                }
                else
                {
                    playerHealth = playerMaxHealth;
                }
            
            
        }
        healthBar.SetHealth(playerHealth);
        enemyKilledRecently = false;
    }
    void HealthCheck()
    {
        if(playerHealth <= 0)
        {
            animator.SetInteger("plHealth", 0);
            playerScriptMain.GetComponent<PlayerController_2D>().enabled = false;
            playerScriptMelee.GetComponent<PlayerMelee>().enabled = false;
            playerCircleCollider.GetComponent<CircleCollider2D>().enabled = false;
            animator.Play("Player_Death");
            Invoke("Restart", 3f);
        }
    }
    private void Restart()
    {
        buildindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildindex);
    }
    public void PlayerTakeDamage(int enemyDmg)
    {
        playerHealth -= enemyDmg;
        healthBar.SetHealth(playerHealth);
        Debug.Log("OOF:" + enemyDmg);
        bloodTest.Emit(4);
    }
}
