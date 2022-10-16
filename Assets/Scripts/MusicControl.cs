using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MusicControl : MonoBehaviour
{  

    public AudioSource AudioSource;

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
    private void Start()
    {
        
    }
    private void Update()
    {
       

    }

   

    
}
