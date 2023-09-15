using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms;


public class EnemyOnLand : MonoBehaviour

{
    public float walkSpeed = 30f;
    public float maxSpeed = 3f;
    public float stopWalkRate = 0.6f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public GameObject player;  

            


    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;

    public enum WalkAbleDirection { Right, Left }

    private WalkAbleDirection walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkAbleDirection WalkDirection
    {
        get { return walkDirection; }
        set
        {
            if (walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkAbleDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkAbleDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            walkDirection = value;
        }
    }
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
    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
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
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        HasTarget = attackZone.detectedCollider2D.Count > 0;

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        //if (CanSeePlayer(IsRange))
        //{

        //    ChasePlayer();
        //}
        //else
        //{
        //    StopChasingPlayer();

        //}
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (CanMove && touchingDirections.IsGrounded)
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (walkSpeed * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
        else
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, stopWalkRate), rb.velocity.y);
    }

    //bool CanSeePlayer(float distance)
    //{
    //    bool val = false;
    //    float castDist = distance;

    //    if(isFacingleft)
    //    {
    //        castDist = -distance; 
    //    }

    //    Vector2 endPos = rayCast.position + Vector3.right * distance;
    //    RaycastHit2D hit = Physics2D.Linecast(rayCast.position, endPos, 1 <<  LayerMask.NameToLayer("Player")) ;

    //    if(hit.collider != null)
    //    {
    //        if(hit.collider.gameObject.CompareTag("Player")) 
    //        {
    //            // Lets Agro the Enemy
    //            val= true;
    //        }
    //        else
    //        {
    //            val= false;
    //        }
    //        Debug.DrawLine(rayCast.position, hit.point, Color.red);
    //    }
    //    else
    //    {
    //        Debug.DrawLine(rayCast.position, endPos, Color.green);
    //    }
    //    return val;
    //}
    //void ChasePlayer()
    //{
    //    if (transform.position.x < player.position.x)
    //    {
    //        //enemy is to the left side of the player will move right
    //        rb.velocity = new Vector2(runSpeed, 0);
    //        transform.localScale = new Vector2(1, 1);
    //        isFacingleft = false;
    //    }
    //    else
    //    {
    //        //enemy is to the right side of the player will move left
    //        rb.velocity = new Vector2(-runSpeed, 0);
    //        transform.localScale = new Vector2(-1, 1);
    //        isFacingleft = true;
    //    }
    //    //animator.Play("Dark_Elves_Run");
    //}
    //void StopChasingPlayer()
    //{
    //    rb.velocity = new Vector2(0, 0); 
    //}
    private void FlipDirection()
    {


        if (WalkDirection == WalkAbleDirection.Right)
        {
            WalkDirection = WalkAbleDirection.Left;

        }
        else if (WalkDirection == WalkAbleDirection.Left)
        {
            WalkDirection = WalkAbleDirection.Right;
        }
        else
        {
            Debug.LogError("not flip right or left");
        }
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        if (player != null)
        {
            float playerDirection = Mathf.Sign(player.transform.position.x - transform.position.x);
            if ((playerDirection > 0 && WalkDirection == WalkAbleDirection.Left) ||
                (playerDirection < 0 && WalkDirection == WalkAbleDirection.Right))
            {
                FlipDirection();
            }
        }
    }

}
