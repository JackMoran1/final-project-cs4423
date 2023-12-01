using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health = 100f;
    public float moveSpeed = 1f; 
    private bool isDead = false;
    public float damageDone = 1f;

    public GameObject xpDropPrefab;
    private Transform playerTransform;  
    private WaveManager waveManager;
    

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
    if (isDead) return;

    health -= damage;
    if (health <= 0)
    {
        isDead = true;
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
