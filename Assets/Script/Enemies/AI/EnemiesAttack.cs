using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesAttack : MonoBehaviour
{
    public float minAttack = 25.5f;  // The minimum attack power of the enemy
    public float maxAttack = 31.9f;  // The maximum attack power of the enemy
    public Vector2 knockback = Vector2.zero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PH damageable = collision.GetComponent<PH>();

        if(damageable != null)
        {
            float attackDamage = Random.Range(minAttack, maxAttack);
            Vector2 PushOut = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damageable.TakeDamage(attackDamage, PushOut);

        }
    }
}
