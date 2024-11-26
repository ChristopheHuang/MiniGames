using System;
using UnityEngine;

public class LeavesGenerater : MonoBehaviour
{
    public GameObject leafPrefab;
    public int leafCount = 30;

    private void Start()
    {
        originLeafCount = leafCount;
        // Random postion in range (-50, 50) in axis x and z, and random rotation in y axis, and random radius in range (1, 5)
        for (int i = 0; i < leafCount; i++)
        {
            float randomX = UnityEngine.Random.Range(-50, 50);
            float randomZ = UnityEngine.Random.Range(-50, 50);
            float randomRotation = UnityEngine.Random.Range(0, 360);
            float randomRadius = UnityEngine.Random.Range(1, 5);
            Vector3 randomPosition = new Vector3(randomX, 0, randomZ);
            GameObject leaf = Instantiate(leafPrefab, randomPosition, Quaternion.Euler(0, randomRotation, 0));
            leaf.transform.localScale = new Vector3(randomRadius, randomRadius, randomRadius);
        }
    }

    private int originLeafCount;
    public void Update()
    {
        if (originLeafCount != leafCount)
        {
            // Generate a new leaf
            float randomX = UnityEngine.Random.Range(-50, 50);
            float randomZ = UnityEngine.Random.Range(-50, 50);
            float randomRotation = UnityEngine.Random.Range(0, 360);
            float randomRadius = UnityEngine.Random.Range(1, 5);
            Vector3 randomPosition = new Vector3(randomX, 0, randomZ);
            GameObject leaf = Instantiate(leafPrefab, randomPosition, Quaternion.Euler(0, randomRotation, 0));
            leaf.transform.localScale = new Vector3(randomRadius, randomRadius, randomRadius);
        }
        originLeafCount = leafCount;
    }
}
