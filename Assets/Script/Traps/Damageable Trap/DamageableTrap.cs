using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableTrap : MonoBehaviour
{
    public int damagePerSecond = 10; // damage dealt per second
    public Vector2 knockback = Vector2.zero;
    public float damageInterval = 1f; // per second
    private float damageTimer = 0f; // timer to keep track of elapsed time

    private void OnTriggerStay2D(Collider2D collision)
    {
        PH damageable = collision.GetComponent<PH>();

        if (damageable != null)
        {
            // increment the damage timer
            damageTimer += Time.deltaTime;

 
            if (damageTimer >= damageInterval)
            {
                float attackDamage = damagePerSecond * damageInterval; 
                Vector2 pushOut = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                damageable.TakeDamage(attackDamage, pushOut);

                // reset the damage timer
                damageTimer = 0f;
            }
        }
    }

}
