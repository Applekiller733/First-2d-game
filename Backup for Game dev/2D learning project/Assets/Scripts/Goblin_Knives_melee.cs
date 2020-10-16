using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Knives_melee : MonoBehaviour
{
    [SerializeField]
    EnemyGoblinKnives_Script goblin1Main;

    Animator enemyAnim;
    public GameObject attackPos;
    public LayerMask whatisplayer;
    public int goblinoDmg;
    internal bool isAttack;
    private float timetoAttack;
    public float starttimetoAttack;

    public float attackRange;
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }
    internal void GoblinAttack()
    {
            Collider2D[] playertoDmg = Physics2D.OverlapCircleAll(attackPos.transform.position, attackRange, whatisplayer);
        if (playertoDmg.Length > 0)
        {
            if (timetoAttack <= 0)
            {
                playertoDmg[1].GetComponent<PlayerController_2D>().PlayerTakeDamage(goblinoDmg);
                enemyAnim.Play("Enemy_Goblin_melee");
                isAttack = true;
                
            }
            else
            {
                isAttack = false;
                starttimetoAttack -= Time.deltaTime;
            }
            
        }
        else
        {
            isAttack = false;
        }
    }
}
