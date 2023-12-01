using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    public float laserSpeed = 20f;
    public Transform LaserSpawn;

    public void Throw()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        GameObject laser = ProjectilePoolManager.Instance.GetProjectile();
        
        laser.transform.position = LaserSpawn.position;
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        rb.velocity = direction * laserSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}