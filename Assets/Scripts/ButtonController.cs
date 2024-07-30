using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public string sceneName; // Name of the scene where the prefab will be instantiated

    public GameObject prefabToInstantiate; // Reference to the prefab

    public void OnButtonClick()
    {
        // Check if the scene is loaded
        Scene targetScene = SceneManager.GetSceneByName(sceneName);
        if (targetScene.isLoaded)
        {
            // Instantiate the prefab in the target scene
            GameObject instantiatedPrefab = Instantiate(prefabToInstantiate);
            SceneManager.MoveGameObjectToScene(instantiatedPrefab, targetScene);
        }
        else
        {
            Debug.LogWarning($"Scene '{sceneName}' is not loaded. Make sure it is added to build settings and loaded additively.");
        }
    }
}
