using UnityEngine;

public class ScreenToggle : MonoBehaviour
{
    // Method to enable fullscreen mode
    public void SetFullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Debug.Log("Fullscreen mode enabled");
    }

    // Method to disable fullscreen mode
    public void SetWindowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Debug.Log("Windowed mode enabled");
    }
}
