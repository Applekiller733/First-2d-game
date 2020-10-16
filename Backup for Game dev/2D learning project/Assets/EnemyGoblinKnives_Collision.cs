using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblinKnives_Collision : MonoBehaviour
{
    [SerializeField]
    EnemyGoblinKnives_Script goblin1_Main;
    internal bool isGrounded;
    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if ((Physics2D.Linecast(transform.position, goblin1_Main.groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
           (Physics2D.Linecast(transform.position, goblin1_Main.groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
           (Physics2D.Linecast(transform.position, goblin1_Main.groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (!goblin1_Main.isAlive)
        {
            if (isGrounded)
            {
                rb2d.Sleep();
            }
        }
    }
}
