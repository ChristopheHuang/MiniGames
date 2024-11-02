using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Spawner : MonoBehaviour
{
    public List<Wave> waveList;
    public Transform spawnPoint;
    
    public WaveOS waveOS;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        foreach (Wave wave in waveList)
        {
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