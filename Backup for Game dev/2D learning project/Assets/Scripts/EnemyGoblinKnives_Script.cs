using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblinKnives_Script : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb2d;
    Animator enemyAnim;
    public int Goblino1Health;

    
    public GameObject goblin1_script;
    public GameObject goblin1_boxcollide;
    public GameObject goblin1_circlecollide;
    public GameObject goblin1_script_melee;
    public GameObject exclamationMark;
    public GameObject playerscript;

    [SerializeField]
    internal Goblin_Knives_melee goblin1_melee;
    [SerializeField]
    internal EnemyGoblinKnives_Collision goblin1_collision;

    public Transform groundCheck;
    public Transform groundCheckL;
    public Transform groundCheckR;

    [SerializeField]
    Transform visrayOrigin_front;
    [SerializeField]
    Transform visrayOrigin_back;
    [SerializeField]
    Transform visray_front;
    [SerializeField]
    Transform visray_back;

    
    bool isSpotted;
    public bool isFacingLeft;
    public bool isAgro = false;
    bool isSearching;
    bool isMoving;
    internal bool isAlive = true;

    [SerializeField]
    Transform playerPos;

    public float agroRange;
    public float circleAgroRange;
    public float agroCircleRadius;
    public Transform agroCircle;
    public LayerMask enemies;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!goblin1_collision.isGrounded)
        {
            enemyAnim.Play("Enemy_Goblin_jump");
        }
        else if (goblin1_collision.isGrounded && isMoving == false)
        {
            enemyAnim.Play("Enemy_Goblin_idle");
        }
        if (CanSeePlayer(agroRange))
        {
            isAgro = true;
            
        }
        else
        {
            if (isAgro)
            {
                exclamationMark.GetComponent<SpriteRenderer>().enabled = false;
                if (!isSearching)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 4);
                }
            }
        }
        if (isAgro)
        {
            ChasePlayer();
            goblin1_melee.GoblinAttack();
        }
    }
    bool CanSeePlayer(float Distance)
    {
        bool val = false;
        var castDist = Distance;
        if (isFacingLeft)
        {
            castDist = -Distance;
        }

        Vector2 endPos_front = visrayOrigin_front.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(visrayOrigin_front.position, endPos_front, 1 << LayerMask.NameToLayer("Player"));
        Collider2D[] agrocircle = Physics2D.OverlapCircleAll(agroCircle.position, agroCircleRadius, 1 << LayerMask.NameToLayer("Player"));
        for (int j = 0; j < agrocircle.Length; j++)
        {
            if (agrocircle[j].GetComponent<PlayerController_2D>())
            {
                val = true;
                if (!isAgro)
                {
                    exclamationMark.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                val = true;
                if (!isAgro)
                {
                    exclamationMark.GetComponent<SpriteRenderer>().enabled = true;
                }

                Collider2D[] alertcircle = Physics2D.OverlapCircleAll(agroCircle.position, circleAgroRange, enemies);
                for (int i = 0; i < alertcircle.Length; i++)
                {
                    if (alertcircle[i].GetComponent<EnemyGoblinKnives_Script>())
                    {
                        alertcircle[i].GetComponent<EnemyGoblinKnives_Script>().isAgro = true;
                    }
                }
            }
            else
            {
                val = false;
            }
        }
        return val;
    }
   
    void ChasePlayer()
    {
        if (!goblin1_melee.isAttack)
        {
            if (transform.position.x < playerscript.transform.position.x && goblin1_collision.isGrounded)
            {
                rb2d.velocity = new Vector2(2.5f, rb2d.velocity.y);
                transform.localScale = new Vector2(6.652866f, 5.603442f);
                isFacingLeft = false;
                isMoving = true;
                if (goblin1_collision.isGrounded)
                {
                    enemyAnim.Play("Enemy_Goblin_walk");
                }
            }
            else if (transform.position.x > playerscript.transform.position.x && goblin1_collision.isGrounded)
            {
                rb2d.velocity = new Vector2(-2.5f, rb2d.velocity.y);
                transform.localScale = new Vector2(-6.652866f, 5.603442f);
                isFacingLeft = true;
                isMoving = true;
                if (goblin1_collision.isGrounded)
                {
                    enemyAnim.Play("Enemy_Goblin_walk");
                }
            }
            else
            {
                isMoving = false;
            }
            if (transform.position.y < playerscript.transform.position.y)
            {
                Invoke("JumpAfterPlayer", 0.8f);
            }
        }
    }
     void JumpAfterPlayer()
    {
        if (goblin1_collision.isGrounded && isAlive)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 5);
            enemyAnim.Play("Enemy_Goblin_jump");
        }
    } 

    void StopChasingPlayer()
    {
        isAgro = false;
        isSearching = false;
        rb2d.velocity = new Vector2(0, 0);
        if (isAlive)
        {
            enemyAnim.Play("Enemy_Goblin_idle");
        }
    }
    public void GoblinKnivesTakeDmg(int damage)
    {
        Goblino1Health -= damage;
        if ( Goblino1Health <= 0 )
        {
            playerscript.GetComponent<PlayerController_2D>().enemyKilledRecently = true;
            isAlive = false;
            goblin1_script.GetComponent<EnemyGoblinKnives_Script>().enabled = false;
            goblin1_script_melee.GetComponent<Goblin_Knives_melee>().enabled = false;
            goblin1_boxcollide.GetComponent<BoxCollider2D>().enabled = false;
            goblin1_circlecollide.GetComponent<CircleCollider2D>().enabled = false;
            exclamationMark.GetComponent<SpriteRenderer>().enabled = false;
            enemyAnim.SetBool("isDead", true);
        }
    }
}
