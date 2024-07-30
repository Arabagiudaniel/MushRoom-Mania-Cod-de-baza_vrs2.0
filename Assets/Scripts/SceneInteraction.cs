using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInteraction : MonoBehaviour
{
    void Start()
    {
        Scene scene1 = SceneManager.GetSceneByName("Scene1");
        Scene scene2 = SceneManager.GetSceneByName("Scene2");

        if (scene1.isLoaded && scene2.isLoaded)
        {
            GameObject[] scene1Objects = scene1.GetRootGameObjects();
            GameObject[] scene2Objects = scene2.GetRootGameObjects();

            // Example: Move all objects in Scene2 by 10 units
            foreach (GameObject obj in scene2Objects)
            {
                obj.transform.position += new Vector3(10, 0, 0);
            }
        }
    }
}
