using System;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;

    private GameObject player;
    
    public void SpawnMonster()
    {
        Instantiate(monsterPrefab, transform.position, Quaternion.identity);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(SpawnMonster), 1.0f, 2.0f);
    }

    private void Update()
    {
        if (player)
        {
            // Random offset in range (-50,50) with axis x and z, random height in range (10, 30) but keep distance to player in range (10, 20)
            float randomOffsetX = UnityEngine.Random.Range(-50.0f, 50.0f);
            float randomOffsetZ = UnityEngine.Random.Range(-50.0f, 50.0f);
            float randomHeight = UnityEngine.Random.Range(20.0f, 50.0f);
            Vector3 randomOffset = new Vector3(randomOffsetX, randomHeight, randomOffsetZ);
            Vector3 randomPosition = player.transform.position + randomOffset;
            randomPosition.y = 0;
            transform.position = randomPosition;
        }
    } 
}