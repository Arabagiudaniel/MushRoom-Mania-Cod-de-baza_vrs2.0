using UnityEngine;

public class FinishLine : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        // Add your end game logic here
        Debug.Log("Game Finished!");
        // Show finish UI, stop the game, etc.
    }
}
