using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : MonoBehaviour
{
    public float minAtk = 15.0f;  // The minimum amount of health the pickup can restore
    public float maxAtk = 25.5f;  // The maximum amount of health the pickup can restore
    public Vector3 spinRotationSpeed = new Vector3(0, 0, 180);

    AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float BuffDamage = Random.Range(minAtk, maxAtk);
        PHB Buff = collision.GetComponent<PHB>();
        if (Buff)
        {
            Buff.IncreaseAtk(BuffDamage);

                if (pickupSource)
                {
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                }

                Destroy(gameObject);           

        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
