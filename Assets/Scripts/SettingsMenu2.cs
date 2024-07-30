using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu2 : MonoBehaviour
{
    public Button plusButton;
    public Button minusButton;
    public Button windowButton;
    public Button fullscreenButton;

    void OnEnable()
    {
        if (ResolutionManager.Instance != null)
        {
            ResolutionManager.Instance.UpdateButtons(plusButton, minusButton, windowButton, fullscreenButton);
        }
    }
}
