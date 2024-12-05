using System;
using UnityEngine;

public class Mostique : Character
{
    [Header("Mostique Settings")]
    private GameObject player;
    
    private void Start()
    {  
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Frog>().Die();
        }
    }

    
    private void Update()
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
}
