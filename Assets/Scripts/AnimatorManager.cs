using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatorManager : MonoBehaviour
{
    public static AnimatorManager Instance { get; private set; }
    public RuntimeAnimatorController selectedAnimatorController; // The selected Animator Controller

    private void Awake()
    {
        // Ensure only one instance of this script exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            // Destroy this instance if another one exists
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the scene is Level 2 (adjust the name or index as necessary)
        if (scene.name == "Level 2" || scene.buildIndex == 2)
        {
            ApplySelectedAnimatorController();
        }
    }

    public void ApplySelectedAnimatorController()
    {
        if (selectedAnimatorController != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Animator animator = player.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.runtimeAnimatorController = selectedAnimatorController;
                    Debug.Log("Animator Controller set: " + selectedAnimatorController.name);
                }
            }
        }
    }

    public void SetSelectedAnimatorController(RuntimeAnimatorController animatorController)
    {
        selectedAnimatorController = animatorController;
    }
}
