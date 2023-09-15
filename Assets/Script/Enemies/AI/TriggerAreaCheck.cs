//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using static UnityEngine.GraphicsBuffer;
//using UnityEngine;
//using System.Threading;

//public class TriggerAreaCheck : MonoBehaviour
//{
//    private EnemyPatrol enemyPatrol;

//    private void Awake()
//    {
//        enemyPatrol = GetComponentInParent<EnemyPatrol>();
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            gameObject.SetActive(false);
//            enemyPatrol.target = collision.transform;
//            enemyPatrol.inRange = true;
//            enemyPatrol.zoneCanSee.SetActive(true);
//        }
//    }
//}
