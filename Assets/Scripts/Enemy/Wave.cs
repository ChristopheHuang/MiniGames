using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;
    public int spawnWeight;
}

[Serializable]
public class Wave
{
    public List<EnemySpawnData> enemySpawnDataList;
    public float rate;
}
