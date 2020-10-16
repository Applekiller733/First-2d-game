using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
     PlayerController_2D playerMain;

    void Start()
    {
        
    }

    internal void FixedUpdate()
    {
        if ((Physics2D.Linecast(transform.position, playerMain.groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, playerMain.groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, playerMain.groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            playerMain.isGrounded = true;
            playerMain.animator.SetBool("isGr", true);
        }
        else
        {
            playerMain.isGrounded = false;
            playerMain.animator.SetBool("isGr", false);
        }
    }
   internal void OnCollisionwithTakedmg()
    {
        if ((Physics2D.Linecast(transform.position, playerMain.groundCheck.position, 1 << LayerMask.NameToLayer("Enemy"))) ||
            (Physics2D.Linecast(transform.position, playerMain.groundCheckL.position, 1 << LayerMask.NameToLayer("Enemy"))) ||
            (Physics2D.Linecast(transform.position, playerMain.groundCheckR.position, 1 << LayerMask.NameToLayer("Enemy"))))
        {
            playerMain.playerHealth = playerMain.playerHealth - 10;
            Debug.Log(playerMain.playerHealth);
            playerMain.rb2d.velocity = new Vector2(playerMain.rb2d.velocity.x, 6);
            playerMain.healthBar.SetHealth(playerMain.playerHealth);
            playerMain.bloodTest.Emit(5);
        }
    }
}
