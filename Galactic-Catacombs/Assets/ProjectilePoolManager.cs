using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    public static ProjectilePoolManager Instance;

    public GameObject projectilePrefab;
    private Queue<GameObject> projectiles = new Queue<GameObject>();
    public int poolSize = 20;

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectiles.Enqueue(projectile);
        }
    }

    public GameObject GetProjectile()
    {
        if (projectiles.Count > 0)
        {
            GameObject projectile = projectiles.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }
        else
        {
            Debug.LogWarning("Projectile pool exhausted. Consider increasing pool size.");
            return Instantiate(projectilePrefab);
        }
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectiles.Enqueue(projectile);
    }
}