using UnityEngine;

public class PiranhaLightControl : MonoBehaviour
{
    private Light piranhaLight;

    private void Awake()
    {
        piranhaLight = GetComponent<Light>();
    }

    private void Update()
    {
        // Exemplu: Modifică intensitatea luminii în funcție de un factor extern (de exemplu, distanța față de jucător)
        // Implementarea logicii de modificare a luminii aici
    }
}
