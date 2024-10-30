using System;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().shootInterval *= 0.5f;
        }
    }
}
