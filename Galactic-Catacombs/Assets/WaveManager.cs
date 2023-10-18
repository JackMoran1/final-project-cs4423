using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;       
    public GameObject bossPrefab;  
    public GameObject advancedEnemyPrefab;       
    public int[] waves = {3, 5, 7, 10, 10}; 
    public int[] advancedWaves = { 0, 2, 5, 7, 10};
    private int currentWave = 0;

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        if (currentWave < waves.Length) 
        {
            for (int i = 0; i < waves[currentWave]; i++) {
                SpawnEnemy(enemyPrefab);
            }
            for (int i = 0; i < advancedWaves[currentWave]; i++) {
                SpawnEnemy(advancedEnemyPrefab);
            }
            currentWave++;
        }
        else
        {
            SpawnBoss(); 
        }
    }

    void SpawnEnemy(GameObject enemy)
    {

        Vector3 spawnPosition = Vector3.zero;
    
        int side = Random.Range(0, 4);

        switch (side) {
            case 0:
                spawnPosition = new Vector3(Random.Range(-8f, 8f), 6, 0);
                break;

            case 1:
                spawnPosition = new Vector3(Random.Range(-8f, 8f), -6, 0);
                break;

            case 2:
                spawnPosition = new Vector3(-9, Random.Range(-8f, 6f), 0);
                break;

            case 3:
                spawnPosition = new Vector3(9, Random.Range(-8f, 6f), 0);
                break;
        }
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    void SpawnBoss()
    {
        Vector3 spawnPosition = new Vector3(0, 6, 0); 
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }

    public void EnemyDied()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 1 && GameObject.FindGameObjectsWithTag("AdvancedEnemy").Length <= 1)
        {
            StartNextWave();
        }
    }
}