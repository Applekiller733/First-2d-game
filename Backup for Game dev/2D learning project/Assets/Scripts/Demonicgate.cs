using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demonicgate : MonoBehaviour
{
    public bool areActivated;
    public bool altar1 = false;
    public bool altar2 = false;
    public BoxCollider2D dmgtrigger;
    public BoxCollider2D collide;
    public GameObject player;
    Animator anim;
    void Start()
    {
        anim.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        
        if (altar1 && altar2)
        {
            areActivated = true;
        }
        else if (altar1)
        {
            anim.Play("Cursed_Gate1active");
        }
        if (areActivated)
        {
            /*  anim.Play("Cursed_Gate");
              dmgtrigger.enabled = false;
              collide.enabled = false; */
            Destroy(this.gameObject, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerController_2D>().playerHealth -= 5;
            
        }
    }
}
