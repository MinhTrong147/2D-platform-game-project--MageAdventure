using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{

    public static int totalCoins = 0;
    AudioSource pickupSource; 
    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
        pickupSource=GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        
        //Detroy gameObject coin when player take coin
        if (collider2D.CompareTag("Player"))
        {
            //Add coin to counter
            totalCoins++;
            AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume); 
            Destroy(gameObject);
        }

    }
    //public GameObject scoreBox;

    //void OnTriggerEnter()
    //{
    //    GlobalScoreObject.currentSCore += 5;
    //    Destroy(gameObject);
    //}
}
