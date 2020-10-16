using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadoweye : MonoBehaviour
{
    public bool inRange;
    public float rotationSpeed;
    public Transform player;
    public int health = 20;
    bool isAttacking = false;
    private float timeBtwAttack;
    public float starttimeBtwAttack;
    public Transform firep;
    public GameObject shadowlaser;
    public bool isAlive = true;
    Animator animator;
    public BoxCollider2D box;
   /* private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player") && isAlive)
        {
            Rotationtowardsplayer();
            Attack();
        } 
    } */
    private void Start()
    {
        animator.GetComponent<Animator>();
        box.GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        //to be put into separate function.
        if (inRange && isAlive)
        {
            Rotationtowardsplayer();
            Attack();
        }

    }
    public void Rotationtowardsplayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    public void Attack()
    {

        if (timeBtwAttack <= 0)
        {
            Instantiate(shadowlaser, firep.position, firep.rotation);
                timeBtwAttack = starttimeBtwAttack;
            //animator.Play("Enemy_Shadoweyeattack");
                isAttacking = true;
        }
        else
        {
            //animator.Play("Shadoweye_idle"); - doesn't fuckn work for some reason. Too bad!
            isAttacking = false;
            timeBtwAttack -= Time.deltaTime;
        }
    }
    public void Takedmg(int dmg)
    {
        health -= dmg;
        HealthCheck();
    }
    public void HealthCheck()
    {
        if (health <= 0)
        {
            isAlive = false;
            box.enabled = false;
            Destroy(this.gameObject, 0.5f);
        }
    }
}

