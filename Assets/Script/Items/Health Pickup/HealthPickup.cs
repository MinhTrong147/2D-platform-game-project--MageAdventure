using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float minHeal = 20.0f;  // The minimum amount of health the pickup can restore
    public float maxHeal = 30.0f;  // The maximum amount of health the pickup can restore
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    AudioSource pickupSource;
    private void Awake()
    {
        pickupSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
     private void OnTriggerEnter2D(Collider2D collision)
    {
        float healthRestore = Random.Range(minHeal, maxHeal);
        PH healed = collision.GetComponent<PH>();
        if(healed)
        {
            bool washealed = healed.Heal(healthRestore);
            if(washealed ) 
            {
                if (pickupSource) 
                {
                   AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume); }
                    
                Destroy(gameObject);
            }
            
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
