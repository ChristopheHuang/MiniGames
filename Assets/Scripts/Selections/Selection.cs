using System;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Player shoot bullet plus one
        Player.Instance.shootCounts++;
    }
}
