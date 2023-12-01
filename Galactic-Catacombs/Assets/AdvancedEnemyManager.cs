using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedEnemyManager : MonoBehaviour
{
    public float health = 100f;
    public float moveSpeed = 1f; 
    public float homingSpeed = 1f;
    public float fireDistance = 5f;
    private Transform playerTransform; 
    private WaveManager waveManager;
    public GameObject enemyProjectilePrefab;
    public GameObject xpDropPrefab;
    private bool isWaiting = false;
    public float damageDone = 1f;

    

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        waveManager = FindObjectOfType<WaveManager>();
        health = health * DifficultyManager.HealthFactor;
        damageDone = damageDone * DifficultyManager.DamageFactor;
    }

    private void Update()
    {
        MoveTowardsPlayer();

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if(distanceToPlayer <= fireDistance && !isWaiting) {
            StartCoroutine(Shoot());
        }
    }


    IEnumerator Shoot() {
        isWaiting = true;
        moveSpeed = 0f;

        yield return new WaitForSeconds(1f);

        GameObject projectile = Instantiate(enemyProjectilePrefab, transform.position, Quaternion.identity);
        HomingManager homingManager = projectile.GetComponent<HomingManager>();
        if (homingManager != null)
        {
            homingManager.SetTarget(playerTransform);
        }

        moveSpeed = 1f;  

        isWaiting = false;
    }

    void MoveTowardsPlayer()
    {
        if (playerTransform != null) 
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Instantiate(xpDropPrefab, transform.position, Quaternion.identity);
            waveManager.EnemyDied();
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
                Player player = collision.gameObject.GetComponent<Player>();
                player.subtractHealth(damageDone);
            }
    }

            
}
