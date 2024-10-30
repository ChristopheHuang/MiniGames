using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Spawner : MonoBehaviour
{
    public List<Wave> waveList;
    public Transform spawnPoint;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        foreach (Wave wave in waveList)
        {
            isSpawning = true;
            yield return StartCoroutine(SpawnEnemiesInWave(wave));
            yield return new WaitForSeconds(10f);
        }
    }

    private IEnumerator SpawnEnemiesInWave(Wave wave)
    {
        List<GameObject> weightedEnemyList = CreateWeightedEnemyList(wave.enemySpawnDataList);

        for (int i = 0; i < weightedEnemyList.Count; i++)
        {
            GameObject enemyToSpawn = weightedEnemyList[UnityEngine.Random.Range(0, weightedEnemyList.Count)];
            Instantiate(enemyToSpawn, spawnPoint.position, quaternion.identity);
            yield return new WaitForSeconds(wave.rate);
        }

        isSpawning = false;
    }

    private List<GameObject> CreateWeightedEnemyList(List<EnemySpawnData> enemySpawnDataList)
    {
        List<GameObject> weightedEnemyList = new List<GameObject>();

        foreach (var spawnData in enemySpawnDataList)
        {
            for (int i = 0; i < spawnData.spawnWeight; i++)
            {
                weightedEnemyList.Add(spawnData.enemyPrefab);
            }
        }

        return weightedEnemyList;
    }
}