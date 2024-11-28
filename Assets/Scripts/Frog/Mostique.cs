using System;
using UnityEngine;

public class Mostique : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speed = 2.0f;
    Animator animator;
    void Start()
    {  
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player)
        {
            // Add a random offset to the movement
            float randomOffsetX = UnityEngine.Random.Range(-0.05f, 0.05f);
            Vector3 randomOffset = new Vector3(randomOffsetX, 0, 0);
            transform.position += transform.forward * speed * Time.deltaTime;
            // Chase the player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);            
            transform.LookAt(player.transform);
        }
    }
    
    bool isDied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is killed by Mostique");
            other.gameObject.GetComponent<Frog>().Die();
        }
    }

    public void Die()
    {
        if (isDied) return;
        animator.SetBool("Died", true);
        isDied = true;
        GameManager.Instance.ScorePlus();
        Destroy(gameObject);
    }
}
