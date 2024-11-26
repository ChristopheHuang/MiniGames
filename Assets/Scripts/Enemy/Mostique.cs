using System;
using UnityEngine;

public class Mostique : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speed = 2.0f;
    void Start()
    {  
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform);
        // Chase the player
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void Die()
    {
        GameManager.Instance.ScorePlus();
        Destroy(gameObject);
    }
}
