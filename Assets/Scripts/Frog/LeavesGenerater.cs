using System;
using UnityEngine;

public class LeavesGenerater : MonoBehaviour
{
    [Header("Leaves Generater Settings")]
    public GameObject leafPrefab;
    public int leafCount = 30;
    public int mapSize = 50;
    public int maxSize = 5;
    public int minSize = 1;
    
    private void Start()
    {
        // Random postion in range (-50, 50) in axis x and z, and random rotation in y axis, and random radius in range (1, 5)
        for (int i = 0; i < leafCount; i++)
        {
            float randomX = UnityEngine.Random.Range(-mapSize, mapSize);
            float randomZ = UnityEngine.Random.Range(-mapSize, mapSize);
            float randomRotation = UnityEngine.Random.Range(0, 360);
            float randomRadius = UnityEngine.Random.Range(minSize, maxSize);
            Vector3 randomPosition = new Vector3(randomX, 0.3f, randomZ);
            GameObject leaf = Instantiate(leafPrefab, randomPosition, Quaternion.Euler(0, randomRotation, 0));
            leaf.transform.localScale = new Vector3(randomRadius, 2, randomRadius);
        }
    }

    private void GeneratNewLeaf()
    {
        float randomX = UnityEngine.Random.Range(-mapSize, mapSize);
        float randomZ = UnityEngine.Random.Range(-mapSize, mapSize);
        float randomRotation = UnityEngine.Random.Range(0, 360);
        float randomRadius = UnityEngine.Random.Range(minSize, maxSize);
        Vector3 randomPosition = new Vector3(randomX, 0.3f, randomZ);
        GameObject leaf = Instantiate(leafPrefab, randomPosition, Quaternion.Euler(0, randomRotation, 0));
        leaf.transform.localScale = new Vector3(randomRadius, 2, randomRadius);
    }
}
