using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    PlayerController_2D playerMain;

    private float timeBtwAttack;
    public float starttimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatisEnemies;
    public int meleeDamage;
    internal bool attacking;

     void Start()
    {
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatisEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].GetComponent<Enemyplant1>())
                    {
                        enemiesToDamage[i].GetComponent<Enemyplant1>().Plant1TakeDamage(meleeDamage);
                        playerMain.playerMana += 5;
                        playerMain.manaBar.SetMana(playerMain.playerMana);
                    }
                    else if (enemiesToDamage[i].GetComponent<EnemyGoblinKnives_Script>())
                    {
                        enemiesToDamage[i].GetComponent<EnemyGoblinKnives_Script>().GoblinKnivesTakeDmg(meleeDamage);
                        playerMain.playerMana += 5;
                        playerMain.manaBar.SetMana(playerMain.playerMana);
                    }
                    else if (enemiesToDamage[i].GetComponent<Enemy_Goblin_Mace_Script>())
                    {
                        enemiesToDamage[i].GetComponent<Enemy_Goblin_Mace_Script>().GoblinMaceTakeDmg(meleeDamage);
                        playerMain.playerMana += 5;
                        playerMain.manaBar.SetMana(playerMain.playerMana);
                    }
                    else if (enemiesToDamage[i].GetComponent<Shadoweye>())
                    {
                        enemiesToDamage[i].GetComponent<Shadoweye>().Takedmg(meleeDamage);
                        playerMain.playerMana += 5;
                        playerMain.manaBar.SetMana(playerMain.playerMana);
                    }
                    
                }
                timeBtwAttack = starttimeBtwAttack;
                attacking = true;
                playerMain.animator.SetBool("attac", true);
            }
        }
        else
        {
            attacking = false;
            playerMain.animator.SetBool("attac", false);
            timeBtwAttack -= Time.deltaTime;
        }
    }
}
