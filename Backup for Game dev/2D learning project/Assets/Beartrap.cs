using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beartrap : MonoBehaviour
{
    public GameObject player;
    bool wasActivated = false;
    Animator trapAnim;
    private void Start()
    {
        trapAnim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && wasActivated == false)
        {
            player.GetComponent<PlayerController_2D>().playerHealth -= 10;
            player.GetComponent<PlayerController_2D>().bloodTest.Emit(8);
            wasActivated = true;
            trapAnim.Play("Trap_Beartrap");
        }
    }
}
