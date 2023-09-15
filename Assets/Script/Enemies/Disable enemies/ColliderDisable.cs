using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDisable : MonoBehaviour
{
    public Animator animator;


    private void Update()
    {
        if (animator.GetBool("Dead")) {
            
            StartCoroutine(Die());
        }           

    }
    IEnumerator Die()        
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(5); //wait time disable
        GetComponent<Collider2D>().enabled = false;  // Disable Collider
        this.enabled = false;

    }
}
