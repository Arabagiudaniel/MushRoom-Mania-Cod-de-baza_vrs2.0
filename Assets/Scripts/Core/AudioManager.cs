using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        if (backgroundMusic != null)
        {
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        if (!isPlaying)
        {
            backgroundMusic.loop = true;
            backgroundMusic.Play();
            isPlaying = true;
            Debug.Log("Playing music: " + backgroundMusic.clip.name);
        }
    }

    public void StopMusic()
    {
        if (isPlaying)
        {
            backgroundMusic.Stop();
            isPlaying = false;
            Debug.Log("Stopped music: " + backgroundMusic.clip.name);
        }
    }

    public void PauseMusic()
    {
        if (isPlaying)
        {
            backgroundMusic.Pause();
            Debug.Log("Paused music: " + backgroundMusic.clip.name);
        }
    }

    public void UnpauseMusic()
    {
        if (isPlaying)
        {
            backgroundMusic.UnPause();
            Debug.Log("Unpaused music: " + backgroundMusic.clip.name);
        }
    }
}
