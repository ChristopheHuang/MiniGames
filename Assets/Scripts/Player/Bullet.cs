using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float damage = 5.0f;
    public float speed = 30.0f;

    private Vector3 direction;
    
    public GameObject hitEffect; 

    public void Initialize(Vector3 shootDirection)
    {
        float randomOffsetX = Random.Range(-0.05f, 0.05f);
        Vector3 randomOffset = new Vector3(randomOffsetX, 0, 0);

        direction = (shootDirection + randomOffset).normalized;
        Invoke(nameof(Deactivate), 3.0f); 
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(GameObject.Instantiate(hitEffect, transform.position, Quaternion.identity), 1);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); 
    }
}