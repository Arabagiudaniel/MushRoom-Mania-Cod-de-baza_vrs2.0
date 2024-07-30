using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public GameObject[] playerSkins; // Array to hold player prefabs
    private GameObject currentSkin;

    private void Start()
    {
        // Instantiate the default skin at the start
        ChangeSkin(0);
    }

    public void ChangeSkin(int skinIndex)
    {
        if (currentSkin != null)
        {
            Destroy(currentSkin);
        }

        // Instantiate the selected skin prefab
        currentSkin = Instantiate(playerSkins[skinIndex], transform.position, transform.rotation);
        currentSkin.transform.SetParent(transform); // Optional: to keep hierarchy clean
    }
}
