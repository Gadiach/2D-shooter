using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsPanel;
    public GameObject buttonsContainer;
    public Toggle volume;
    public Slider sliderVol;
    public Toggle music;
    public Slider sliderMus;
    public GameObject FAQImage;
    
    

    void Update()
    {        

        if (Input.GetKeyDown(KeyCode.Escape) && GameIsPaused)
        {
            Resume();              
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !GameIsPaused)
        {          
            Pause();            
        }        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        buttonsContainer.SetActive(false);
        
    }
    public void Cross()
    {        
        settingsPanel.SetActive(false);
        buttonsContainer.SetActive(true);
    }

    public void FAQ()
    {
        buttonsContainer.SetActive(false);
        FAQImage.SetActive(true);
    }

    public void Cross2()
    {
        buttonsContainer.SetActive(true);
        FAQImage.SetActive(false);
    }

}
