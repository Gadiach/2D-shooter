using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Toggle volume;
    public Slider sliderVol;
    public Toggle music;
    public Slider sliderMus;

    public GameObject settingsPanel;
    public GameObject buttonsContainer;


    private void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
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
}
