using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public GameObject winPanel;
    public GameObject bzk;
    public GameObject bullet;


    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public AudioClip[] jumpClips;
    public float jumpForce = 1000f;
    public AudioClip[] taunts;
    public float tauntProbability = 50f;
    public float tauntDelay = 1f;


    private int tauntIndex;
    private Transform groundCheck;
    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;


    public int balo = 0;

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));


        if (grounded)
        {
            anim.SetTrigger("Idle");
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

    }


    void FixedUpdate()
    {
        // lay gia tri -1 hoac 1 => trai hoac phai 
        float h = Input.GetAxis("Horizontal");

        //Set animation cho nhan vat
        anim.SetFloat("Speed", Mathf.Abs(h));

        //di chuyen
        if (h * rb2d.velocity.x < maxSpeed)
        {
            rb2d.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }


        //Quay trai va quay phai
        if (h > 0.1)
        {
            facingRight = true;
            transform.localScale = new Vector3(2.5f, 2.5f, 1f);
        }

        else if (h < -0.1)
        {
            facingRight = false;
            transform.localScale = new Vector3(-2.5f, 2.5f, 1f);
        }

    }


    public void Jump()
    {
        //Set aniamtion
        anim.SetTrigger("Jump");

       int i = Random.Range(0, jumpClips.Length);

        //Ham` nay dung de nhay
        rb2d.AddForce(new Vector2(0f, jumpForce));
    }


    public IEnumerator Taunt()
    {
        AudioSource audio = GetComponent<AudioSource>();

        float tauntChance = Random.Range(0f, 100f);
        if (tauntChance > tauntProbability)
        {

            yield return new WaitForSeconds(tauntDelay);


            if (!audio.isPlaying)
            {

                tauntIndex = TauntRandom();


                audio.clip = taunts[tauntIndex];
                audio.Play();
            }
        }
    }


    int TauntRandom()
    {

        int i = Random.Range(0, taunts.Length);


        if (i == tauntIndex)

            return TauntRandom();
        else

            return i;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            balo += 1;
        }

        else if (collision.gameObject.CompareTag("door") && balo == 2)
        {
            winPanel.SetActive(true);
        }

        else if (collision.gameObject.CompareTag("door") && balo == 0 && balo == 1)
        {
            Debug.Log("Need to find Key");
        }

    }


}
