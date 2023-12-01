using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject advancedEnemyPrefab;
    public GameObject bossPrefab;
    public int[] waves = { 3, 5, 7, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    public int[] advancedWaves = { 3, 5, 7, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    public int initialSpawnCount = 5;
    private int totalEnemiesInCurrentWave;
    private int enemiesOnScreen;
    private int currentWave = 0;

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {

        if (currentWave <= waves.Length - 1)
        {
            initialSpawnCount++;
            totalEnemiesInCurrentWave = waves[currentWave] + advancedWaves[currentWave];
            enemiesOnScreen = 0;
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            SpawnBoss();
        }
    }

IEnumerator SpawnEnemies()
{
    int regularEnemiesSpawned = 0;
    int advancedEnemiesSpawned = 0;

    while ((regularEnemiesSpawned < waves[currentWave] || advancedEnemiesSpawned < advancedWaves[currentWave]) 
           && enemiesOnScreen < initialSpawnCount)
    {
        SpawnRandomEnemy(ref regularEnemiesSpawned, ref advancedEnemiesSpawned);
        yield return null; 
    }

    while (regularEnemiesSpawned < waves[currentWave] || advancedEnemiesSpawned < advancedWaves[currentWave])
    {
        if (enemiesOnScreen < initialSpawnCount)
        {
            SpawnRandomEnemy(ref regularEnemiesSpawned, ref advancedEnemiesSpawned);
        }
        yield return null;
    }
}

void SpawnRandomEnemy(ref int regularEnemiesSpawned, ref int advancedEnemiesSpawned)
{

    GameObject enemyToSpawn = null;
    bool canSpawnRegular = regularEnemiesSpawned < waves[currentWave];
    bool canSpawnAdvanced = advancedEnemiesSpawned < advancedWaves[currentWave];

    if (canSpawnRegular && canSpawnAdvanced)
    {
         enemyToSpawn = (Random.Range(0, 2) == 0) ? enemyPrefab : advancedEnemyPrefab;
        if (enemyToSpawn == enemyPrefab) regularEnemiesSpawned++;
        else advancedEnemiesSpawned++;
    }
    else if (canSpawnRegular)
    {
        enemyToSpawn = enemyPrefab;
        regularEnemiesSpawned++;
    }


    SpawnEnemy(enemyToSpawn);
    enemiesOnScreen++;
}


void SpawnEnemy(GameObject enemy)
{
    Camera mainCam = Camera.main;
    Vector2 screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.transform.position.z));
    float buffer = 3.0f; 
    Vector3 spawnPosition = Vector3.zero;

    int side = Random.Range(0, 4);
    float xPosition, yPosition;

    switch (side)
    {
        case 0:
            xPosition = Random.Range(-screenBounds.x - buffer, -screenBounds.x);
            yPosition = Random.Range(-screenBounds.y, screenBounds.y);
            spawnPosition = new Vector3(xPosition, yPosition, 0);
            break;
        case 1:
            xPosition = Random.Range(screenBounds.x, screenBounds.x + buffer);
            yPosition = Random.Range(-screenBounds.y, screenBounds.y);
            spawnPosition = new Vector3(xPosition, yPosition, 0);
            break;
        case 2:
            xPosition = Random.Range(-screenBounds.x, screenBounds.x);
            yPosition = Random.Range(screenBounds.y, screenBounds.y + buffer);
            spawnPosition = new Vector3(xPosition, yPosition, 0);
            break;
        case 3: 
            xPosition = Random.Range(-screenBounds.x, screenBounds.x);
            yPosition = Random.Range(-screenBounds.y - buffer, -screenBounds.y);
            spawnPosition = new Vector3(xPosition, yPosition, 0);
            break;
    }

    Instantiate(enemy, spawnPosition, Quaternion.identity);
}

    void SpawnBoss()
    {
        Debug.Log("Spawning");
        Camera mainCam = Camera.main;
        float buffer = 3.0f; 
        Vector2 screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -mainCam.transform.position.z));
        Vector3 spawnPosition = new Vector3(screenBounds.x + buffer, Random.Range(-screenBounds.y, screenBounds.y), 0) + mainCam.transform.position;
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }

public void EnemyDied()
{
    enemiesOnScreen--;
    totalEnemiesInCurrentWave--;

    if (enemiesOnScreen <= 0 && totalEnemiesInCurrentWave <= 0)
    {
        if (currentWave <= waves.Length - 1)
        {
            currentWave++;
            StartNextWave();
        }
    }
}

}
