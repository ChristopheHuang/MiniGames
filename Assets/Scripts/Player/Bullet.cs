using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float damage = 5.0f;
    public float speed = 30.0f;

    private Vector3 direction;

    public void Initialize(Vector3 shootDirection)
    {
        float randomOffsetX = Random.Range(-0.05f, 0.05f);
        float randomOffsetY = Random.Range(-0.05f, 0.05f);
        Vector3 randomOffset = new Vector3(randomOffsetX, randomOffsetY, 0);

        direction = (shootDirection + randomOffset).normalized;
        Invoke(nameof(Deactivate), 3.0f); 
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); 
    }
}