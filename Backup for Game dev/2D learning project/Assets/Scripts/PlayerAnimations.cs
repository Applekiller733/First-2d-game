using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    [SerializeField]
    PlayerController_2D playerMain;

    Animator animator;

    void Start()
    {
        animator.GetComponent<Animator>();

    }

    
    void FixedUpdate()
    {
        if (playerMain.isMoving)
        {
            animator.Play("Player_walk");
        }
         if (!playerMain.isGrounded)
        {
            animator.Play("Player_Jump");
        }
         if (playerMain.playerHealth <= 0)
        {
            animator.Play("Player_Death1");
        }
         if (playerMain.playerMelee.attacking)
        {
            animator.Play("Player_melee");
        }
        
    }
}
