using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PHB : MonoBehaviour
{

    public float minAttack = 10.45f ;  // The minimum attack power of the enemy
    public float maxAttack = 20.5f;  // The maximum attack power of the enemy
    public Vector2 knockback = Vector2.zero;
    Animator animator;

    public bool Dead
    {
        get { return animator.GetBool(AnimationStrings.Dead); }
    }
    //public Animator anim;
    //public Transform hitBox;
    //public LayerMask enemiesLayers;
    //public float attackRate = 1.5f;
    //float nextAttackTime = 0f;


    //private Enemy enemy;
    //void Update()
    //{
    //    //if (!CanMove)
    //    //{
    //    //    if (Time.time >= nextAttackTime)
    //    //    {
    //    //        if (Input.GetButtonDown("Fire1"))
    //    //        {
    //    //            anim.SetTrigger("Slashing");
    //    //            nextAttackTime = Time.time + 1f / attackRate;
    //    //        }
    //    //    }
    //    //}

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {

        EnemyHealth damageable = collision.GetComponent<EnemyHealth>();

        if (damageable != null )
        {
            float attackDamage = Random.Range(minAttack, maxAttack);
            Vector2 PushOut = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damageable.TakeDamage(attackDamage, PushOut);
        }     
    }

    public void IncreaseAtk(float DamageRise)
    {
            minAttack += DamageRise;
            maxAttack += DamageRise;
            CharacterEvents.characterBuffDamaged(gameObject, DamageRise);

    }
    //void Attack()
    //{

    //    //anim.SetTrigger("Slashing");
    //    ////Debug.Log(anim);

    //    //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemiesLayers);

    //    //foreach (Collider2D enemy in hitEnemies)
    //    //{

    //    //    //Debug.Log("Hit" + enemy.name);  //Debug log

    //    //   //enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
    //    //}
    //}

    //void OnDrawGizmosSelected()
    //{
    //    if (hitBox == null)
    //        return;
    //     Gizmos.DrawWireSphere(hitBox.position, attackRange);

    //}

}
