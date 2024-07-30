using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void LoadLevel(string levelName)
    {
        // Load the specified level
        SceneManager.LoadScene(levelName);
    }
}
