using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetection : MonoBehaviour
{ 




    public float damage = 20f;

    void Awake() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {

        bool isActive = true;
        if(other.gameObject.CompareTag("Boundary")) {
        }


        if(other.gameObject.CompareTag("Enemy")) {
            isActive = false;
            EnemyManager enemy = other.gameObject.GetComponent<EnemyManager>();
            enemy.TakeDamage(damage);
            ReturnToPool();
            
        }

        if(other.gameObject.CompareTag("AdvancedEnemy")) {
            isActive = false;
            AdvancedEnemyManager AdvancedEnemy = other.gameObject.GetComponent<AdvancedEnemyManager>();
            AdvancedEnemy.TakeDamage(damage);
            ReturnToPool();
            
        }

        if(other.gameObject.CompareTag("EnemyProjectile")) {
            isActive = false;
            ReturnToPool();
            
        }

        if(other.gameObject.CompareTag("Boss")) {
            isActive = false;
            BossManager boss = other.gameObject.GetComponent<BossManager>();
            boss.TakeDamage(5f);
            ReturnToPool();
            
        }

        if(isActive) {StartCoroutine(ReturnToPoolAfterDelay(2f));}

        

    }


    IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ProjectilePoolManager.Instance.ReturnProjectile(gameObject);
    }

    void ReturnToPool() {
        ProjectilePoolManager.Instance.ReturnProjectile(gameObject);
    }




}
