using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GhostFlight : MonoBehaviour
{
    public float detectionRange = 5f;
    public float flightSpeed = 3f;
    public DetectionZone ScratchDetectionZone;
    public List<Transform> flypoints;
    public Transform player;


    private int currentFlyPoint = 0;
    private Vector3 originalScale;
    private bool playerDetected = false;

    Animator animator;
    Rigidbody2D rb;
    EnemyHealth EnemyHP;

    public bool hasTarget = false;
    public bool HasTarget
    {
        get { return hasTarget; }
        private set
        {
            hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }
    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EnemyHP = GetComponent<EnemyHealth>();
    }
    void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        HasTarget = ScratchDetectionZone.detectedCollider2D.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (!EnemyHP.Dead )
        {            
                Flight();           
        }
        else
        {
            rb.gravityScale = 1f;
            rb.velocity = new Vector2 (0,rb.velocity.y);
        }
    }

    private void Flight()
    {
        // Check if player is within detection range
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected)
        {
                transform.position = Vector2.MoveTowards(transform.position, player.position, flightSpeed * Time.deltaTime);            
        }
        else
        {
            // Move along the original path
            Transform currentTarget = flypoints[currentFlyPoint];
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, flightSpeed * Time.deltaTime);

            // If the enemy reaches the current target, switch to the next target in the path
            if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                currentFlyPoint = (currentFlyPoint + 1) % flypoints.Count;
            }
        }

        // Turn left or right based on movement direction
        Vector2 movement = playerDetected ? player.position - transform.position : flypoints[currentFlyPoint].position - transform.position;
        float horizontalMovement = movement.x;
        transform.localScale = new Vector3(Mathf.Sign(horizontalMovement) * originalScale.x, originalScale.y, originalScale.z);
    }
    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere around the enemy to show the detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
