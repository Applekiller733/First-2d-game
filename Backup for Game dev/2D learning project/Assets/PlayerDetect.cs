using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    public GameObject eye;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && eye.GetComponent<Shadoweye>().isAlive)
        {
            eye.GetComponent<Shadoweye>().inRange = true;
        }
    }
}
