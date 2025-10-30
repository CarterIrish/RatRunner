using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize slider value
        volumeSlider.onValueChanged.AddListener(SetVolume);
        volumeSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float sliderValue)
    {
        // Adjust mixer volume based on slider
        AudioListener.volume = volumeSlider.value;
    }
}
