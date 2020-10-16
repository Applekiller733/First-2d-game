using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireballobj : MonoBehaviour
{
    public Rigidbody2D rb2dbull;
    public GameObject player;
    public float fbspeed = 8f;
    public ParticleSystem Sparks;
    public GameObject ImpactEffectEnemy;
    public GameObject ImpactEffectGround;
     void Start()
    {
        Sparks = GetComponent<ParticleSystem>();
        if (player.GetComponent<PlayerController_2D>().FacingRight)
        {
            rb2dbull.velocity = transform.right * fbspeed;
        }
        else 
        {
            rb2dbull.velocity = transform.right * -fbspeed;
        }
        Destroy(this.gameObject, 5f);
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            
            if (hit.GetComponent<EnemyGoblinKnives_Script>())
            {
                hit.GetComponent<EnemyGoblinKnives_Script>().GoblinKnivesTakeDmg(15);
            }
            else if (hit.GetComponent<Enemy_Goblin_Mace_Script>())
            {
                hit.GetComponent<Enemy_Goblin_Mace_Script>().GoblinMaceTakeDmg(15);
            }
            else if (hit.GetComponent<Enemyplant1>())
            {
                hit.GetComponent<Enemyplant1>().Plant1TakeDamage(15);
            }
            else if (hit.GetComponent<Shadoweye>())
            {
                hit.GetComponent<Shadoweye>().Takedmg(15);
            }
            Instantiate(ImpactEffectEnemy, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (hit.CompareTag("Ground"))
        {
            Instantiate(ImpactEffectGround, transform.position, transform.rotation);
            //Instantiate(ImpactEffectEnemy, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        Sparks.Emit(1);
    }
}
