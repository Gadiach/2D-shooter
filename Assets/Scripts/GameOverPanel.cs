using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverPanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Putin putin;
    public PauseMenu pauseMenu;
    public bool gameIsPaused;
 
    private void Update()
    {
        if(putin.health <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;            
        }
        else if (putin.health > 0)
        {
            if(!pauseMenu.GameIsPaused)
            {
                Time.timeScale = 1f;
            }
            
            gameOverPanel.SetActive(false);            
        }
    }
    public void PlayAgain()
    {
        putin.health = 3;
        gameOverPanel.SetActive(false);       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);               
    }
    public void Quit()
    {
        Application.Quit();
    }

}
