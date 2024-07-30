using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    public static ResolutionManager Instance { get; private set; }

    public Button plusButton;
    public Button minusButton;
    public Button windowButton;
    public Button fullscreenButton;

    private int currentResolutionIndex;
    private bool isFullscreen = true;

    private Resolution[] resolutions = new Resolution[]
    {
        new Resolution(640, 360),
        new Resolution(854, 480),
        new Resolution(960, 540),
        new Resolution(1024, 576),
        new Resolution(1280, 720),
        new Resolution(1366, 768),
        new Resolution(1600, 900),
        new Resolution(1920, 1080)
    };

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        currentResolutionIndex = resolutions.Length - 1; // Start at the highest resolution
        if (plusButton != null) plusButton.onClick.AddListener(IncreaseResolution);
        if (minusButton != null) minusButton.onClick.AddListener(DecreaseResolution);
        if (windowButton != null) windowButton.onClick.AddListener(SetWindowedMode);
        if (fullscreenButton != null) fullscreenButton.onClick.AddListener(SetFullscreenMode);

        SetResolution(currentResolutionIndex);
    }

    public void UpdateButtons(Button plusBtn, Button minusBtn, Button windowBtn, Button fullscreenBtn)
    {
        if (plusButton != null) plusButton.onClick.RemoveListener(IncreaseResolution);
        if (minusButton != null) minusButton.onClick.RemoveListener(DecreaseResolution);
        if (windowButton != null) windowButton.onClick.RemoveListener(SetWindowedMode);
        if (fullscreenButton != null) fullscreenButton.onClick.RemoveListener(SetFullscreenMode);

        plusButton = plusBtn;
        minusButton = minusBtn;
        windowButton = windowBtn;
        fullscreenButton = fullscreenBtn;

        if (plusButton != null) plusButton.onClick.AddListener(IncreaseResolution);
        if (minusButton != null) minusButton.onClick.AddListener(DecreaseResolution);
        if (windowButton != null) windowButton.onClick.AddListener(SetWindowedMode);
        if (fullscreenButton != null) fullscreenButton.onClick.AddListener(SetFullscreenMode);
    }

    void IncreaseResolution()
    {
        if (currentResolutionIndex < resolutions.Length - 1)
        {
            currentResolutionIndex++;
            SetResolution(currentResolutionIndex);
        }
    }

    void DecreaseResolution()
    {
        if (currentResolutionIndex > 0)
        {
            currentResolutionIndex--;
            SetResolution(currentResolutionIndex);
        }
    }

    void SetWindowedMode()
    {
        isFullscreen = false;
        SetResolution(currentResolutionIndex);
    }

    void SetFullscreenMode()
    {
        isFullscreen = true;
        SetResolution(currentResolutionIndex);
    }

    void SetResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
    }

    private struct Resolution
    {
        public int width;
        public int height;

        public Resolution(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
