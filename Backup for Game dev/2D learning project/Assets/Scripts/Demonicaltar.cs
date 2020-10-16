using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demonicaltar : MonoBehaviour
{
    public GameObject demonGate;
    Animator animator;
    private void Start()
    {
        animator.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!demonGate.GetComponent<Demonicgate>().altar1)
                     demonGate.GetComponent<Demonicgate>().altar1 = true;
            else demonGate.GetComponent<Demonicgate>().altar2 = true;
            animator.SetBool("activated", true);
        }
    }
    
}
