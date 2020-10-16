using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Goblin_Mace_collision : MonoBehaviour
{
    [SerializeField]
    Enemy_Goblin_Mace_Script goblin_mace_Main;
    internal bool isGrounded;
    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if ((Physics2D.Linecast(transform.position, goblin_mace_Main.groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
           (Physics2D.Linecast(transform.position, goblin_mace_Main.groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
           (Physics2D.Linecast(transform.position, goblin_mace_Main.groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (!goblin_mace_Main.isAlive)
        {
            if (isGrounded)
            {
                rb2d.Sleep();
            }
        }
    }
}
