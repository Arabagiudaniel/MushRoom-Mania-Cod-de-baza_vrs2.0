using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu1 : MonoBehaviour
{
    public Slider volumeSlider;
    private AudioSource[] allAudioSources;

    void Start()
    {
        // Find all AudioSources in the scene
        allAudioSources = FindObjectsOfType<AudioSource>();

        // Initialize the slider value
        volumeSlider.value = 1f; // 1 is full volume, 0 is mute

        // Add a listener to the slider
        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnSliderChanged(float value)
    {
        // Set volume for all AudioSources
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.volume = value;
        }
    }
}
