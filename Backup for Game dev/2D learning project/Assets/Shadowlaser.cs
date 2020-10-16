using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadowlaser : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    
    private void Start()
    {
        Destroy(this.gameObject, 0.9f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController_2D>().PlayerTakeDamage(10);
        }
    } 
}
