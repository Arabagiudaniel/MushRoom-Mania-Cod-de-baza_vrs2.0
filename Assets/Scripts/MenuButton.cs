using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public PlayerManager playerManager;
    public int playerIndex;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => playerManager.ChangePlayer(playerIndex));
    }
}
