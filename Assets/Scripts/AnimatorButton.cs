using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatorButton : MonoBehaviour
{
    public GameObject prefabToActivate; // Reference to the prefab you want to activate

    public void OnButtonPress()
    {
        // Instantiate or activate the prefab in all scenes
        Scene[] scenes = SceneManager.GetAllScenes();
        foreach (Scene scene in scenes)
        {
            // Check if the scene is loaded
            if (scene.isLoaded)
            {
                // Instantiate or activate the prefab
                GameObject instantiatedPrefab = Instantiate(prefabToActivate);
                SceneManager.MoveGameObjectToScene(instantiatedPrefab, scene);
            }
        }

        Debug.Log("Prefab activated in all scenes");
    }
}
