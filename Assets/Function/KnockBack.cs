using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    public float attack ;
    public Vector2 knockback = Vector2.zero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PH damageable = collision.GetComponent<PH>();

        if (damageable != null)
        {
            float attackDamage = attack;
            Vector2 PushOut = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damageable.TakeDamage(attackDamage, PushOut);

        }
    }
}
