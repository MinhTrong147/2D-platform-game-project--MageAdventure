//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor;
//using UnityEditor.Tilemaps;
//using UnityEngine;
//using UnityEngine.Scripting.APIUpdating;
//using static EnemyOnLand;

//public class EnemyPatrol : MonoBehaviour
//{
    

//    public float attackDistance; //Minimum distance for attack
//    public float moveSpeed;
//    public float timer; //Timer for cooldown between attacks
//    [HideInInspector] public Transform target;
//    [HideInInspector] public bool inRange; //Check if Player is in range
//    public Transform leftLimit;
//    public Transform rightLimit;
//    public GameObject zoneCanSee;
//    public GameObject triggerArea;
//    public GameObject player;

//    private Rigidbody2D rb;
//    private Animator animator;
//    private float distance; //Store the distance b/w enemy and player
//    private bool attackMode;
    

//    private bool cooling; //Check if Enemy is cooling after attack
//    private float intTimer;


//    void Awake()
//    {
//        SelectTarget();
//        intTimer = timer; //Store the inital value of timer
//        animator = GetComponent<Animator>();
//        rb = GetComponent<Rigidbody2D>();
        
//    }

//    void Update()
//    {
//        if (!attackMode)
//        {
//            Move();
//        }

//        if (!InsideOfLimits() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationStrings.Attack))
//        {
//            SelectTarget();
//        }          

//        if (inRange)
//        {
//            EnemyLogic();
//        }

//    }  

//    void EnemyLogic()
//    {
//        distance = Vector2.Distance(transform.position, target.position);

//        if (distance > attackDistance)
//        {
//            StopAttack();
//        }
//        else if (attackDistance >= distance && cooling == false)
//        {
//            Attack();
//        }

//        if (cooling)
//        {
//            Cooldown();
//            animator.SetBool(AnimationStrings.Attack, false);
//        }
//    }

//    void Move()
//    {
//        animator.SetBool(AnimationStrings.Walk, true);

//        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationStrings.Attack))
//        {
//            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

//            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
//        }
//    }

//    void Attack()
//    {
//        timer = intTimer; //Reset Timer when Player enter Attack Range
//        attackMode = true; //To check if Enemy can still attack or not

//        animator.SetBool(AnimationStrings.Walk, false);
//        animator.SetBool(AnimationStrings.Attack, true);
//    }

//    void Cooldown()
//    {
//        timer -= Time.deltaTime;

//        if (timer <= 0 && cooling && attackMode)
//        {
//            cooling = false;
//            timer = intTimer;
//        }
//    }

//    void StopAttack()
//    {
//        cooling = false;
//        attackMode = false;
//        animator.SetBool(AnimationStrings.Attack, false);
//    }



//    public void TriggerCooling()
//    {
//        cooling = true;
//    }

//    private bool InsideOfLimits()
//    {
//        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
//    }

//    public void SelectTarget()
//    {
//        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
//        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

//        if (distanceToLeft > distanceToRight)
//        {
//            target = leftLimit;
//        }
//        else
//        {
//            target = rightLimit;
//        }

//        //Ternary Operator
//        //target = distanceToLeft > distanceToRight ? leftLimit : rightLimit;

//        Flip();
//    }

//    public void Flip()
//    {
//        Vector3 rotation = transform.eulerAngles;
//        if (transform.position.x > target.position.x)
//        {
//            rotation.y = 180;
//        }
//        else
//        {         
//            rotation.y = 0;
//        }

//        //Ternary Operator
//        //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

//        transform.eulerAngles = rotation;
//    }
//    public void OnHit(float damage, Vector2 knockback)
//    {
        
//        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

//    }


//}
