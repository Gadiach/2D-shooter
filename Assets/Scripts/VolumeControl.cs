using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    private AudioSource audioSource; 
    [SerializeField] private Toggle musToggle;
    [SerializeField] private Toggle volToggle;
    [SerializeField] private Slider musSlider;
    [SerializeField] private Slider volSlider;
    [SerializeField] private bool IsOnMus;
    private float musicVolume = 1f;

    private void Awake()
    {
        musToggle.onValueChanged.AddListener(delegate { OnToggleStateChange(musToggle);});
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        musSlider.value = GetFloat(musSlider.name);
    }

    void Update()
    {              
        
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
        audioSource.volume = musicVolume;

        if (musicVolume == 0)
        {
            musToggle.isOn = false;
        }

        if (musicVolume > 0)
        {
            musToggle.isOn = true;
        }

        if (musToggle.isOn == false)
        {
            musicVolume = 0;
        }
    }  
    
    public void OnToggleStateChange(Toggle toggle)
    {
        if(toggle.isOn)
        {
            musicVolume = 0.5f;           
        }
        else
        {
            musicVolume = 0f;            
        }

        musSlider.value = musicVolume;
    } 
    
    public void setSliderFloat()
    {
        SetFloat(musSlider.name, musSlider.value);
    }

    public void SetFloat(string KeyName, float Value)
    {
        PlayerPrefs.SetFloat(KeyName, Value);  
    }

    public float GetFloat(string KeyName)
    {
        return PlayerPrefs.GetFloat(KeyName);
    }
}
