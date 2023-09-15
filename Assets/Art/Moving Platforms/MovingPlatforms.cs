using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{

    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int pointNum;

       
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[pointNum].position)<0.02f)
        {
            pointNum++; // increase the point
            if (pointNum == points.Length)
            {
                pointNum = 0; // reset pointNum
            }
        }
        // moving the platform to the point with index"pointNum"
        transform.position = Vector2.MoveTowards(transform.position, points[pointNum].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player")) 
        {
            collision.transform.SetParent(transform);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null); 
        }
        
    }
}
