using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Goblin_Mace_Script : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb2d;
    Animator enemyAnim;
    public int Goblino2Health;


    public GameObject goblin2_script;
    public GameObject goblin2_boxcollide;
    public GameObject goblin2_circlecollide;
    public GameObject goblin_mace_script_melee;
    public GameObject exclamationMark;
    public GameObject playerscript;
   

    [SerializeField]
    internal Enemy_Goblin_Mace_melee goblin2_melee;
    [SerializeField]
    internal Enemy_Goblin_Mace_collision goblin2_collision;

    public Transform groundCheck;
    public Transform groundCheckL;
    public Transform groundCheckR;

    [SerializeField]
    Transform visrayOrigin_front;

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

    [SerializeField]
    internal ParticleSystem parteffhelm1;
    [SerializeField]
    internal ParticleSystem parteffhelm2;
    [SerializeField]
    internal ParticleSystem parteffplate1;
    [SerializeField]
    internal ParticleSystem parteffplate2;
    [SerializeField]
    internal ParticleSystem parteffplate3;
    public bool noHelm;
    public bool noPlate;
    
     void Awake()
    {
        if (noHelm)
        {
            Goblino2Health = 40;
        }
        if (noPlate)
        {
            Goblino2Health = 20;
        }
    } 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        HealthCheck();
        if (!goblin2_collision.isGrounded && !goblin2_melee.isReadying && !noHelm)
        {
            enemyAnim.Play("Enemy_Goblin_Mace_jump");
        }
        else if (!goblin2_collision.isGrounded && !goblin2_melee.isReadying && noHelm)
        {
            enemyAnim.Play("Enemy_Goblin_Mace_nohelm_jump");
        }
        else if (goblin2_collision.isGrounded && isMoving == false && !goblin2_melee.isReadying && !noHelm)
        {
            enemyAnim.Play("Enemy_Goblin_Mace_idle");
        }
       /* else if (!goblin2_collision.isGrounded && !goblin2_melee.isReadying && noHelm)
        {
            enemyAnim.Play("Enemy_Goblin_Mace_")
        } */
        else if (goblin2_collision.isGrounded && isMoving == false && !goblin2_melee.isReadying && noHelm)
        {
            enemyAnim.Play("Enemy_Goblin_Mace_nohelm_idle");
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
            goblin2_melee.GoblinAttack();
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
                    else if (alertcircle[i].GetComponent<Enemy_Goblin_Mace_Script>())
                    {
                        alertcircle[i].GetComponent<Enemy_Goblin_Mace_Script>().isAgro = true;
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
        if (!goblin2_melee.isAttack && !goblin2_melee.isReadying)
        {
            if (transform.position.x < playerPos.position.x && goblin2_collision.isGrounded)
            {
                rb2d.velocity = new Vector2(1.5f, rb2d.velocity.y);
                transform.localScale = new Vector2(5.065513f, 4.979502f);
                isFacingLeft = false;
                isMoving = true;
                if (goblin2_collision.isGrounded && !noHelm)
                {
                    enemyAnim.Play("Enemy_Goblin_Mace_run");
                }
                else if (goblin2_collision.isGrounded && noHelm)
                {
                    enemyAnim.Play("Enemy_Goblin_Mace_nohelm_run");
                }
            }
            else if (transform.position.x > playerPos.position.x && goblin2_collision.isGrounded)
            {
                rb2d.velocity = new Vector2(-1.5f, rb2d.velocity.y);
                transform.localScale = new Vector2(-5.065513f, 4.979502f);
                isFacingLeft = true;
                isMoving = true;
                if (goblin2_collision.isGrounded && !noHelm)
                {
                    enemyAnim.Play("Enemy_Goblin_Mace_run");
                }
                else if (goblin2_collision.isGrounded && noHelm)
                {
                    enemyAnim.Play("Enemy_Goblin_Mace_nohelm_run");
                }
            }
            else
            {
                isMoving = false;
            }
            /* if (transform.position.y < playerPos.position.y)
            {
                Invoke("JumpAfterPlayer", 0.8f);
            } */
        }
    }
   /* void JumpAfterPlayer()
    {
        if (goblin2_collision.isGrounded && isAlive)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 5);
            enemyAnim.Play("Enemy_Goblin_jump");
        }
    } */

    void StopChasingPlayer()
    {
        isAgro = false;
        isSearching = false;
        rb2d.velocity = new Vector2(0, 0);
        if (isAlive)
        {
            enemyAnim.Play("Enemy_Goblin_Mace_idle");
        }
        else if (isAlive && noHelm)
        {
            enemyAnim.Play("Enemy_Goblin_Mace_nohelm_idle");
        }
    }
    public void HealthCheck()
    {
        if (Goblino2Health <= 40 && !noHelm)
        {
            noHelm = true;
            parteffhelm1.Emit(1);
            parteffhelm2.Emit(1);
        }
        else if (Goblino2Health <= 20 && !noPlate)
        {
            noPlate = true;
            parteffplate1.Emit(1);
            parteffplate2.Emit(1);
            parteffplate3.Emit(1);
        } 
    } 
    public void GoblinMaceTakeDmg(int damage)
    {
        Goblino2Health -= damage;
        /*if (Goblino2Health <= 40 && !noHelm)
        {
            noHelm = true;
            parteffhelm1.Emit(1);
            parteffhelm2.Emit(1);
        }
        else if (Goblino2Health <= 20 && !noPlate)
        {
            noPlate = true;
            parteffplate1.Emit(1);
            parteffplate2.Emit(1);
            parteffplate3.Emit(1);
        } */
        if (Goblino2Health <= 0)
        {
            playerscript.GetComponent<PlayerController_2D>().enemyKilledRecently = true;
            isAlive = false;
            goblin2_script.GetComponent<Enemy_Goblin_Mace_Script>().enabled = false;
            goblin2_melee.GetComponent<Enemy_Goblin_Mace_melee>().enabled = false;
            goblin2_boxcollide.GetComponent<BoxCollider2D>().enabled = false;
            goblin2_circlecollide.GetComponent<CircleCollider2D>().enabled = false;
            exclamationMark.GetComponent<SpriteRenderer>().enabled = false;
            enemyAnim.SetBool("isDead", true);
        }
    }
}
