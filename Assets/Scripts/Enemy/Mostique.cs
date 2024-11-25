using UnityEngine;

public class Mostique : MonoBehaviour
{
    private GameObject player;
    void Start()
    {  
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform);
        // Chase the player
        transform.position += transform.forward * 0.5f * Time.deltaTime;
    }
}
