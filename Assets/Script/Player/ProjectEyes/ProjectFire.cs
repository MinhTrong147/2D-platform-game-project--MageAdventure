using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ProjectFire : MonoBehaviour
{
    public float damage = 30.0f;
    public Vector2 moveSpeed = new Vector2(10f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    public float timeToFade = 1f;



    private float timeElapsed = 0f;
    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }
    private void Update()
    {
        transform.Translate(rb.velocity * Time.deltaTime);
        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeToFade)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth damageable = collision.GetComponent<EnemyHealth>();
        if (damageable != null)
        {
            Vector2 PushOut = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damageable.TakeDamage(damage, PushOut);
            Destroy(gameObject);
        }

    }
}
