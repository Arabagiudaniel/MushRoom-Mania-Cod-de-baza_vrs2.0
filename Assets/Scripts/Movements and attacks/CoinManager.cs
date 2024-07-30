using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public Text cointText;
    public GameObject door;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      cointText.text = coinCount.ToString();

        if(coinCount == 6)
        {
          
            Destroy(door);
        }
    }
}
