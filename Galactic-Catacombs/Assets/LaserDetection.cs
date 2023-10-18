using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetection : MonoBehaviour
{ 




    public float damage = 20f;

    void Awake() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Boundary")) {
            Destroy(gameObject);
        }


        if(other.gameObject.CompareTag("Enemy")) {
            EnemyManager enemy = other.gameObject.GetComponent<EnemyManager>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("AdvancedEnemy")) {
            AdvancedEnemyManager AdvancedEnemy = other.gameObject.GetComponent<AdvancedEnemyManager>();
            AdvancedEnemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("EnemyProjectile")) {
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("Boss")) {
            BossManager boss = other.gameObject.GetComponent<BossManager>();
            boss.TakeDamage(5f);
            Destroy(gameObject);
        }



    

    }





}
