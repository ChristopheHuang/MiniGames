using System;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    
    public void SpawnMonster()
    {
        Instantiate(monsterPrefab, transform.position, Quaternion.identity);
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMonster), 1.0f, 2.0f);
    }
}
