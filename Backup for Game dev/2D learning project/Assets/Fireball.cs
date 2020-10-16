using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    PlayerController_2D playerMain;
    public Transform firep;
    public GameObject fballPrefab;
    private float TimeBtwFB;
    public float StartTimeBtwFB;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TimeBtwFB <= 0)
        {
            if (Input.GetKeyDown("f") && playerMain.playerMana > 0)
            {
            playerMain.playerMana = playerMain.playerMana - 5;
            playerMain.manaBar.SetMana(playerMain.playerMana);
            Shoot();
                TimeBtwFB = StartTimeBtwFB;
            }
            
        }
        else
        {
            TimeBtwFB -= Time.deltaTime;
        }
        void Shoot()
        {
            Instantiate(fballPrefab, firep.position, firep.rotation);
        }
    }
}
