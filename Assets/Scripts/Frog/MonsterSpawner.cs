using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Monster Spawner Settings")]
    public List<GameObject> monsterPrefabs;
    public GameObject monsterPrefab;
    private GameObject player;
    
    public void SpawnMonster()
    {
        GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        monster.GetComponent<Character>().Upgrade(player.GetComponent<Frog>().level);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(SpawnMonster), 1.0f, 2.0f);
    }

    /// <summary>
    /// 随机位置生成
    /// </summary>
    private void RandomwMove()
    {
        if (player == null) return;
        // Random offset in range (-50,50) with axis x and z, random height in range (10, 30) but keep distance to player in range (10, 20)
        float randomOffsetX = UnityEngine.Random.Range(-50.0f, 50.0f);
        float randomOffsetZ = UnityEngine.Random.Range(-50.0f, 50.0f);
        float randomHeight = UnityEngine.Random.Range(20.0f, 50.0f);
        Vector3 randomOffset = new Vector3(randomOffsetX, randomHeight, randomOffsetZ);
        Vector3 randomPosition = player.transform.position + randomOffset;
        randomPosition.y = 0;
        transform.position = randomPosition;
    }

    private void Update()
    {
        RandomwMove();
    } 
}
