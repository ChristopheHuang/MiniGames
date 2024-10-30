using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    public GameObject bulletPrefab;
    private Queue<GameObject> poolQueue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        poolQueue = new Queue<GameObject>();
        InitializePool(1000);
    }

    private void Update()
    {
    }


    private void InitializePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            poolQueue.Enqueue(bullet);
        }
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bullet;
        if (poolQueue.Count > 0)
        {
            bullet = poolQueue.Dequeue();
        }
        else
        {
            bullet = Instantiate(bulletPrefab);
        }

        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        poolQueue.Enqueue(bullet);
    }
}