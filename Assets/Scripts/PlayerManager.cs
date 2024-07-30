using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] playerPrefabs; // Array to hold different player prefabs
    private GameObject currentPlayer; // Reference to the current player instance

    public void ChangePlayer(int playerIndex)
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer); // Remove the current player
        }

        currentPlayer = Instantiate(playerPrefabs[playerIndex], Vector3.zero, Quaternion.identity);
        // Optionally set the player’s position and rotation
    }
}
