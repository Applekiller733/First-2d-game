using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatekey : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public BoxCollider2D collide;
    public Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.GetComponent<PlayerController_2D>().keyacquired)
        {
            
            animator.Play("Red_Keygateopen");
            Invoke("Stopcollision", 1f);
        }
    }
    private void Stopcollision()
    {
        collide.enabled = false;
    }
}
