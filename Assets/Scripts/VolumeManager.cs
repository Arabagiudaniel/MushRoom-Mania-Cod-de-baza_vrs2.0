using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance { get; private set; }
    private float volume = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolume();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float Volume
    {
        get => volume;
        set
        {
            volume = value;
            PlayerPrefs.SetFloat("Volume", volume);
            PlayerPrefs.Save();
            ApplyVolume();
        }
    }

    private void LoadVolume()
    {
        volume = PlayerPrefs.GetFloat("Volume", 1.0f);
        ApplyVolume();
    }

    private void ApplyVolume()
    {
        AudioListener.volume = volume;
        // Notify all scene audio managers about the volume change
        SceneAudioManager[] sceneManagers = FindObjectsOfType<SceneAudioManager>();
        foreach (var manager in sceneManagers)
        {
            manager.UpdateVolume(volume);
        }
    }
}
