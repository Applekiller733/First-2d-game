using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodentrap : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerController_2D>().playerHealth -= 5;
            player.GetComponent<PlayerController_2D>().bloodTest.Emit(8);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 7);
        }
    }
}