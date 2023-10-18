using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
public float health = 100f;
    public float moveSpeed = 1f; 

    private Transform playerTransform;  
    private WaveManager waveManager;
    

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        waveManager = FindObjectOfType<WaveManager>();
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
        health -= damage;

        if (health <= 0)
        {
            waveManager.EnemyDied();
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
                PlayerInput player = collision.gameObject.GetComponent<PlayerInput>();
                player.damageTaken(1f);
            }
    }

            
}
