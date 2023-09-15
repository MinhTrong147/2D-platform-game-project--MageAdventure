using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTrapSpike : MonoBehaviour
{
    private float DeadDamage = 1000f;
    public Vector2 knockback = Vector2.zero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PH damageable = collision.GetComponent<PH>();
        EnemyHealth damageableE = collision.GetComponent<EnemyHealth>();
        if (damageable != null)
        {
            float attackDamage = DeadDamage;
            Vector2 PushOut = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damageable.TakeDamage(attackDamage, PushOut);

        }
        if (damageableE != null)
        {
            float attackDamage = DeadDamage;
            Vector2 PushOut = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damageableE.TakeDamage(attackDamage, PushOut);

        }
    }    
       
}
