using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public Slider volumeSlider;
    public Slider sensitivitySlider;

    [System.NonSerialized]
    private ThirdPersonCamera thirdPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Find camera if not assigned
        if (thirdPersonCamera == null)
        {
            thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>();
        }

        // Initialize volume slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
        volumeSlider.value = 1;

        // Initialize sensitivity slider
        if (sensitivitySlider != null)
        {
            sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
            // Set slider to match current camera sensitivity
            if (thirdPersonCamera != null)
            {
                sensitivitySlider.value = thirdPersonCamera.MouseSens / 200f; // Normalize to 0-1 range
            }
        }
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

    public void SetSensitivity(float sliderValue)
    {
        // Adjust camera sensitivity based on slider (scale from 0-1 to 0-200)
        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.MouseSens = sliderValue * 200f;
        }
    }
}
