using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private GameObject target;
    private bool isInit = false;
    
    public float maxHealth = 100.0f;
    public float currentHealth;
    
    private Material enemyMaterial;
    private Color originalColor;
    public float flashDuration = 0.1f;
    
    
    public static event Action OnBroadcastMessage;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;

        enemyMaterial = GetComponent<Renderer>().material; 
        originalColor = enemyMaterial.color;
        
        target = GameObject.FindGameObjectWithTag("Wall");
    }

    private void Update()
    {
        if (target != null)
        {
            _navMeshAgent.SetDestination(target.transform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        StartCoroutine(FlashWhite());
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SendMessageToManager();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            BulletPool.Instance.ReturnBullet(other.gameObject); 
            TakeDamage(other.gameObject.GetComponent<Bullet>().damage);
        }
    }

    private void SendMessageToManager()
    {
        OnBroadcastMessage?.Invoke();
    }

    private void OnDestroy()
    {
        SendMessageToManager();
    }

    private System.Collections.IEnumerator FlashWhite()
    {
        enemyMaterial.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        enemyMaterial.color = originalColor;
    }
}
