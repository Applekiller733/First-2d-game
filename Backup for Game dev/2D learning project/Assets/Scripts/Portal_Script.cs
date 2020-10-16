using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal_Script : MonoBehaviour
{
    int nxtscnBuildIndex;
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
        {
            nxtscnBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nxtscnBuildIndex);
        }
    }

}
