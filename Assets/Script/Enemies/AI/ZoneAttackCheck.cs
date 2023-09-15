//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class ZoneAttackCheck : MonoBehaviour
//{
//    private EnemyPatrol enemyPatrol;
//    private bool inRange;
//    private Animator animator;

//    private void Awake()
//    {
//        enemyPatrol = GetComponentInParent<EnemyPatrol>();
//        animator = GetComponentInParent<Animator>();
//    }
//    private void Update()
//    {
//        if (inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationStrings.Attack))
//        {
//            enemyPatrol.Flip();
//        }
//    }
//    private void OnTriggerEnter2D(Collider2D collider)
//    {
//        if (collider.gameObject.CompareTag("Player"))
//        {
//            inRange = true;
//        }
//    }

//    private void OnTriggerExit2D(Collider2D collider)
//    {
//        if (collider.gameObject.CompareTag("Player"))
//        {
//            inRange = false;
//            gameObject.SetActive(false);
//            enemyPatrol.triggerArea.SetActive(true);
//            enemyPatrol.inRange = false;
//            enemyPatrol.SelectTarget();
//        }
//    }
//}
