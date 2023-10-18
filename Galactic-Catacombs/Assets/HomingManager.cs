using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HomingManager : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
                PlayerInput player = collision.gameObject.GetComponent<PlayerInput>();
                player.damageTaken(1f);
                Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Laser")) {
                LaserDetection laser = collision.gameObject.GetComponent<LaserDetection>();
                Destroy(gameObject);
        }
    }
}