using UnityEngine;

public class UIManager1 : MonoBehaviour
{
    public GameObject settingsCanvas;
    public GameObject volumeCanvas;
    public GameObject levelsCanvas;
    public GameObject shopCanvas;
    public GameObject homeCanvas;

    // This method hides all canvases
    private void HideAllCanvases()
    {
        settingsCanvas.SetActive(false);
        volumeCanvas.SetActive(false);
        levelsCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        homeCanvas.SetActive(false);
    }

    // This method shows the specified canvas
    public void ShowCanvas(GameObject canvas)
    {
        HideAllCanvases();
        canvas.SetActive(true);
    }
}
