using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMusic : MonoBehaviour
{    
    private AudioSource AudioSource;

    private void Awake()
    {
        int y = SceneManager.GetActiveScene().buildIndex;

        if (y == 0)
        {
            AudioSource.enabled = true;
        }
        else 
        {
            AudioSource.enabled = false;
        }
    }
}
