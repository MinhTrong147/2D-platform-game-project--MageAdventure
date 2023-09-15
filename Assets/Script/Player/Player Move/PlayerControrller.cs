using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngineInternal;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControrller : MonoBehaviour
{
    //public CharacterController2D Controller2D;
    //float moving = 0f;
    //public float jumpImpulse = 3f;
    //public float moveSpeed = 15.5f;
    //public float run = 40f;
    //bool jump = false;

    public Animator anim;
    public float jumpImnpulse = 10f;
    public float walkSpeed = 5f;
    public float floatingAir = 3f;
    public float runSpeed = 8f;
    public int MainMenuScene;// input scene number
    AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip runSound;

    Vector2 moving;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    
    public float currentMoveSpeed 
    { get
        {
            if(CanMove)
            {

                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {

                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return floatingAir; // action in air
                    }
                }
                else {return 0; /* Idle speed is 0*/}
            }
            else { return 0; /*Movement locked*/ }
        }
       
    }

    private bool isMoving = false;
    public bool IsMoving { 
        get 
        { 
            return isMoving;
        } 
        private set 
        {
            isMoving = value;
            anim.SetBool(AnimationStrings.isMoving, value);
        } 
    }

    private bool isRunning = false;
    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
        private set
        {

            isRunning = value;
            anim.SetBool(AnimationStrings.isRunning, value);
        }
    }



    public bool CanMove
    {
        get { return anim.GetBool(AnimationStrings.canMove); }
    }
    public bool Dead
    {
        get { return anim.GetBool(AnimationStrings.Dead); }
    }
    public float AttackCooldown
    {
        get
        {
            return anim.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            anim.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }
    public float AttackRangedCooldown
    {
        get
        {
            return anim.GetFloat(AnimationStrings.attackRangedCooldown);
        }
        private set
        {
            anim.SetFloat(AnimationStrings.attackRangedCooldown, Mathf.Max(value, 0));
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        audioSource = GetComponent<AudioSource>();
    }
    //void Start()
    //{
    //    //jump = true;

    //}


    void Update()
    {
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        if (AttackRangedCooldown > 0)
        {
            AttackRangedCooldown -= Time.deltaTime;
        }



        //moving = Input.GetAxisRaw("Horizontal") * moveSpeed;   // controller          
        //anim.SetFloat("Speed", Mathf.Abs(moving));            //set animotion and get -1 or 1

        ////Shift + Right or Left to increase speed
        //if (Input.GetKey(KeyCode.LeftShift))               //import key Shift
        //{
        //    moving = Input.GetAxisRaw("Horizontal") * run;

        //}
        //anim.SetFloat("runSpeed", Mathf.Abs(moving));

        //if (Input.GetButtonDown("Jump") && touchingDirections.IsGrounded)
        //{
        //    jump = true;
        //    anim.SetBool("Jumping", true);
        //    anim.SetTrigger(AnimationStrings.jump);
        //    //rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        //}

    }


    private bool isFacingRight = true;
    public bool IsFacingRight { get
    {
            return isFacingRight;
        }
        private set
        { 
           if(isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }        
           isFacingRight = value;
        }
    }

    public bool LockVelocity {
        get { return anim.GetBool(AnimationStrings.lockVelocity); }
    }



    //public void Onlanding()
    //{
    //    anim.SetBool("Jumping", false);
    //}
    void FixedUpdate()
    {
        if (!LockVelocity)
        {
            rb.velocity = new Vector2(moving.x * currentMoveSpeed, rb.velocity.y);
            anim.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        }

        if (IsMoving && touchingDirections.IsGrounded && !IsRunning)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkSound;
                audioSource.Play();
            }

        }
        else if (IsMoving && touchingDirections.IsGrounded && IsRunning)
        {

            if (!audioSource.isPlaying)
            {
                audioSource.clip = runSound;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        // Move our characher
        //Controller2D.Move(moving * Time.fixedDeltaTime, false, jump);              
        //jump = false;

    }

   public void OnMove(InputAction.CallbackContext context)
    {
        moving = context.ReadValue<Vector2>();
        if (!Dead && CanMove)
        {
            IsMoving = moving != Vector2.zero;

            SetFacingDirection(moving);
        }
        else
        {
            IsMoving = false;
        }

    }
    private void SetFacingDirection(Vector2 moving)
    {
        if(moving.x >0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if(moving.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        }else if(context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded && CanMove) {
            anim.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImnpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {            
            anim.SetTrigger(AnimationStrings.Attack);      
        }

    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger(AnimationStrings.rangedAttack);
        }

    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void OnMainMenuGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Exiting Main Menu");
            SceneManager.LoadScene(MainMenuScene);
        }
    }
}
