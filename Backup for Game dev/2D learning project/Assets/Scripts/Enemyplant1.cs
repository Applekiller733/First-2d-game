using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyplant1 : MonoBehaviour
{
    
    public int Enemyhealth;
    Rigidbody2D rb2d;
    Animator enemyAnim;
    [SerializeField]
    Transform player;
    [SerializeField]
    float agroRange;

    public GameObject enemy_plant1_script;
    public GameObject enemy_plant1_collider;
    public GameObject playerTakeDmg;
    public GameObject playerscript;

    public ParticleSystem plant1Blood;
    

    public int plant1_dmg;
    private float plant1_timeBtwAttack;
    public float plant1_starttimeBtwAttack;
    private float disttoPlayer;
    int enemyDmg;
    

    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        enemyDmg = plant1_dmg;
    }

    void FixedUpdate()
    {
         disttoPlayer = Vector2.Distance(transform.position, player.position);
        if (plant1_timeBtwAttack <= 1)
        {
            if (disttoPlayer <= agroRange)
            {
                if (transform.position.x < player.position.x)
                {
                    transform.localScale = new Vector2(-4.64647f, 4.725721f);
                    enemyAnim.Play("Enemy_plant1_attack");
                    plant1_timeBtwAttack = plant1_starttimeBtwAttack;
                    Debug.Log("enemy attacked!");
                    playerTakeDmg.GetComponent<PlayerController_2D>().PlayerTakeDamage(enemyDmg);
                }
                else
                {
                    transform.localScale = new Vector2(4.64647f, 4.725721f);
                    enemyAnim.Play("Enemy_plant1_attack");
                    plant1_timeBtwAttack = plant1_starttimeBtwAttack;
                    Debug.Log("enemy attacked!");
                    playerTakeDmg.GetComponent<PlayerController_2D>().PlayerTakeDamage(enemyDmg);
                }
            }
            else
            {
                enemyAnim.Play("Enemy_plant1_idle");
            }
        }
        else
        {
            plant1_timeBtwAttack -= Time.deltaTime;
        }
    }
    public void Plant1TakeDamage(int damage)
    {
        Enemyhealth = Enemyhealth - damage;
        plant1Blood.Emit(5);
        if(Enemyhealth <= 0)
        {
            playerscript.GetComponent<PlayerController_2D>().enemyKilledRecently = true;
            enemyAnim.Play("Enemy_plant1_death");
            enemy_plant1_script.GetComponent<Enemyplant1>().enabled = false;
            enemy_plant1_collider.GetComponent<BoxCollider2D>().enabled = false;
        }
        Debug.Log("Enemy hit!");
    }
    
}
