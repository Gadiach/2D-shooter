using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsPanel;
    public GameObject pauseButtons;
    public Toggle volume;
    public Slider sliderVol;
    public Toggle music;
    public Slider sliderMus;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (sliderVol.value == 0)
        {
            volume.isOn = false;
        }
        else if (sliderVol.value > 0)
        {
            volume.isOn = true;
        }


        if (sliderMus.value == 0)
        {
            music.isOn = false;
        }
        else if (sliderMus.value > 0)
        {
            music.isOn = true;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
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

    public void Settings(bool state)
    {
        pauseButtons.SetActive(false);
        settingsPanel.SetActive(state);
    }
    public void Cross()
    {
        pauseButtons.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
