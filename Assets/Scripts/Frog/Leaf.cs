using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Leaf : MonoBehaviour
{
    // If player touch this leaf, it will be pushed to sky
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touch the leaf!");
            other.gameObject.GetComponent<Frog>().JumpAction();
        }
    }
}
