using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHP : MonoBehaviour
{
    public float minHeal = 10.0f;  // The minimum amount of health the pickup can restore
    public float maxHeal = 15.0f;  // The maximum amount of health the pickup can restore
   

    AudioSource pickupSource;
    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float healthIncrease = Random.Range(minHeal, maxHeal);
        PH healed = collision.GetComponent<PH>();
        if (healed)
        {
            bool wasIncrease = healed.IncreaseHealth(healthIncrease);
            if (wasIncrease)
            {
                if (pickupSource)
                {
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                }

                Destroy(gameObject);
            }

        }
    }
}
