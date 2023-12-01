using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public float health = 500f;
    public float moveSpeed = 0.5f;
    public GameObject homingProjectilePrefab;
    public GameObject bossProjectilePrefab;
    public float homingFireRate = 1f;  
    public float specialFireRate = 0.1f;  
    public int specialProjectilesSpawned = 10; 
    private Transform playerTransform;
    private bool specialActive = false;
    private UIManager uiManager;
    public float damageDone = 10f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FireHomingProjectiles());
        StartCoroutine(SpecialAttack());
        uiManager = FindObjectOfType<UIManager>();
        uiManager.ShowBossHP();
        health = health * DifficultyManager.HealthFactor;
        damageDone = damageDone * DifficultyManager.DamageFactor;
    }

    private void Update()
    {
        Movement();
    }

    void Movement() {
        if (playerTransform != null && !specialActive)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator FireHomingProjectiles()
    {
        while (true)
        {
            if (!specialActive)
            {
                GameObject projectile = Instantiate(homingProjectilePrefab, transform.position, Quaternion.identity);
                HomingManager homingProjectile = projectile.GetComponent<HomingManager>();
                if (homingProjectile != null)
                {
                    homingProjectile.SetTarget(playerTransform);
                }
                yield return new WaitForSeconds(homingFireRate);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator SpecialAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            specialActive = true;
            float angle = 0f;
            for (int i = 0; i < specialProjectilesSpawned; i++) {
                GameObject projectile = Instantiate(bossProjectilePrefab, transform.position, Quaternion.Euler(0, 0, angle));
                angle += 360f / specialProjectilesSpawned;
                yield return new WaitForSeconds(specialFireRate);
            }
            specialActive = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        uiManager.bossTakeDamage(health);

        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            Player player = collision.gameObject.GetComponent<Player>();
            player.subtractHealth(damageDone);
        }

    }


}

    