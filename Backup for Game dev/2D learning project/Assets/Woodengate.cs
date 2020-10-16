using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodengate : MonoBehaviour
{
    public BoxCollider2D gate;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gate.enabled = true;
            anim.SetBool("activated", true);
        }
    }
}
