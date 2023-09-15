using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    TMP_Text counterText;   
    
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TMP_Text>();
        Coin.totalCoins = 0;
    }
     // Update is called once per frame
    void Update()
    {
        //Set the current number of coins to display
        if (counterText.text != Coin.totalCoins.ToString())
        {
            counterText.text = Coin.totalCoins.ToString();
        }
    }

}
