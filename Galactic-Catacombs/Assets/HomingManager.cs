using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HomingManager : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float damage = 1f;
    
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void Start() {
        damage = damage * DifficultyManager.DamageFactor;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
                Player player = collision.gameObject.GetComponent<Player>();
                player.subtractHealth(damage);
                Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Laser")) {
                LaserDetection laser = collision.gameObject.GetComponent<LaserDetection>();
                Destroy(gameObject);
        }
    }
}