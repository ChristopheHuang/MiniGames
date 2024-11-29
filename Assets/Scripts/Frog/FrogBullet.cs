using System;
using UnityEngine;

public class FrogBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float damage = 50.0f;
    public float speed = 30.0f;
    public float force = 10.0f;
    private Vector3 direction;

    public void Initialize(Vector3 shootDirection)
    {
        float randomOffsetX = UnityEngine.Random.Range(-0.05f, 0.05f);
        Vector3 randomOffset = new Vector3(randomOffsetX, 0, 0);

        direction = (shootDirection + randomOffset).normalized;
        Destroy(gameObject, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Character>().TakeDamage(damage);
            other.gameObject.GetComponent<Character>().ApplyForceWithBullet(-other.transform.forward, force);
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
