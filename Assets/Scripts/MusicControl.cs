using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicControl : MonoBehaviour
{  

    private AudioSource AudioSource;

    private void Awake()
    {
        int y = SceneManager.GetActiveScene().buildIndex;

        if (y == 1)
        {
            AudioSource.enabled = true;
        }
        else
        {
            AudioSource.enabled = false;
        }
    }
}
