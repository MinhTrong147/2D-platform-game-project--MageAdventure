using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AI_EnemyFindPlayer : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float attackRadius = 1f;
    public float speed = 2f;
    public UnityEvent NoCollinderRemain;
    public List<Collider2D> detectedCollider2D = new List<Collider2D>();
    bool shouldChasePlayer = false;

    Transform playerTransform; // Assuming the player has a "Player" tag
    private Vector2 startingPosition;
    private bool movingRight = true;

    void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (shouldChasePlayer && playerTransform != null)
        {
            // Add enemy movement code here to follow the player
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    playerTransform = collider.transform;
                    break;
                }
            }
        }
        else
        {
            // Add enemy movement code here to turn back
            // Move towards the player
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= attackRadius)
            {
                // Attack the player
                Debug.Log("Attacking player!");
            }
            else
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                transform.position += (Vector3)direction * speed * Time.deltaTime;
            }
        }
        // Check if the enemy needs to turn around
        if (transform.position.x > startingPosition.x + detectionRadius && movingRight)
        {
            movingRight = false;
            Flip();
        }
        else if (transform.position.x < startingPosition.x - detectionRadius && !movingRight)
        {
            movingRight = true;
            Flip();
        }
    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedCollider2D.Add(collision);

        if (collision.CompareTag("Player"))
        {
            shouldChasePlayer = true;
            playerTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCollider2D.Remove(collision);

        if (collision.CompareTag("Player"))
        {
            playerTransform = null;
            shouldChasePlayer = detectedCollider2D.Any(c => c.CompareTag("Player"));
            if (!shouldChasePlayer)
            {
                NoCollinderRemain.Invoke();
            }
        }

    }

}