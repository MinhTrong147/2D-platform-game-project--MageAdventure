using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;



public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 100f;
    public float currentHealth;
    Animator animator;
    public Invincible Invincible;
    public UnityEvent<float, Vector2> damageableHit;
    // Start is called before the first frame update


    public bool Dead
    {
        get { return animator.GetBool(AnimationStrings.Dead); }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();      
    }

    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    public void TakeDamage(float damage, Vector2 knockback)
    {
        if(!Dead && Invincible)
        {
            currentHealth -= damage;
            animator.SetTrigger(AnimationStrings.Hurt);   // Enemy hurt anim
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        //Debug.Log("Enemy died"); //Log
        animator.SetBool(AnimationStrings.Dead, true);  //Die anim
        GetComponent<PolygonCollider2D>().enabled = false; // Disable the collider2d enemy  
        this.enabled = false;
    }


}
