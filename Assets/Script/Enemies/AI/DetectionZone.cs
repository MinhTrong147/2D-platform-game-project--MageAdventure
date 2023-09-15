using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent NoCollinderRemain;
    public List<Collider2D> detectedCollider2D = new List<Collider2D>();
    Collider2D col2d;

 
    private void Awake()
    {
        col2d = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedCollider2D.Add(collision);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCollider2D.Remove(collision);

        if(detectedCollider2D.Count <= 0)
        {
            NoCollinderRemain.Invoke();
        }
    }


}
