using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    // Reference to the UI Slider
    public Slider volumeSlider;
    // Reference to the Audio Source
    public AudioSource audioSource;

    void Start()
    {
        // Initialize the slider value to the current audio source volume
        volumeSlider.value = audioSource.volume;
        // Add listener to handle value change on the slider
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
    }

    // Method to handle the slider value change
    void OnVolumeSliderChanged(float value)
    {
        // Adjust the audio source volume
        audioSource.volume = value;
    }
}