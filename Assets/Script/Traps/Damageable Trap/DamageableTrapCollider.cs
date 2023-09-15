using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableTrapCollider : MonoBehaviour
{
    public int damageableTraps = 20;  // The maximum attack power of the enemy
    public Vector2 knockback = Vector2.zero;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PH damageable = collision.GetComponent<PH>();

        if (damageable != null)
        {
            float attackDamage = damageableTraps;
            Vector2 PushOut = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damageable.TakeDamage(attackDamage, PushOut);

        }
    }

}
