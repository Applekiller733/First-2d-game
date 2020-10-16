using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadText : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Muite");
        }
    }
}
