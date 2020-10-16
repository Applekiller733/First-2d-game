using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Goblin_Mace_melee : MonoBehaviour
{
    [SerializeField]
    Enemy_Goblin_Mace_Script goblin2Main;

    Animator enemyAnim;
    public GameObject attackPos;
    public LayerMask whatisplayer;
    public int Goblinodmg;
    internal bool isAttack;
    internal bool isReadying;
    private float timetoAttack;
    public float starttimetoAttack;

    public float attackRange;
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }
    void RdytoAttk()
    {
        if (goblin2Main.isAlive)
        {
            isReadying = false;
            if (!goblin2Main.noHelm)
            {
                enemyAnim.Play("Enemy_Goblin_Mace_melee");
            }
            else if (goblin2Main.noHelm)
            {
                enemyAnim.Play("Enemy_Goblin_Mace_nohelm_attack");
            }
            Collider2D[] playertoDmg1 = Physics2D.OverlapCircleAll(attackPos.transform.position, attackRange, whatisplayer);
            if (playertoDmg1.Length > 0)
            {
                if (playertoDmg1[1].GetComponent<PlayerController_2D>())
                {
                    playertoDmg1[1].GetComponent<PlayerController_2D>().PlayerTakeDamage(Goblinodmg);
                }
            }
        }
    }
    internal void GoblinAttack()
    {
        Collider2D[] playertoDmg = Physics2D.OverlapCircleAll(attackPos.transform.position, attackRange, whatisplayer);
        if (playertoDmg.Length > 0 && !isReadying)
        {
            if (timetoAttack >= 0)
            {
                isReadying = true;
                if (isReadying &&!goblin2Main.noHelm)
                {
                    enemyAnim.Play("Enemy_Goblin_Mace_ready");
                }
                else if (isReadying && goblin2Main.noHelm)
                {
                    enemyAnim.Play("Enemy_Goblin_Mace_nohelm_ready");
                }
                Invoke("RdytoAttk", 2);
            }
            else
            {
                isReadying = false;
                starttimetoAttack -= Time.deltaTime;
            }

        }
        
    }
}
