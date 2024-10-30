using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int counts = 0;
    private void OnEnable()
    {
        Enemy.OnBroadcastMessage += OnReceiveMessage;
    }

    private void OnDisable()
    {
        Enemy.OnBroadcastMessage -= OnReceiveMessage;
    }

    private void OnReceiveMessage()
    {
        counts += 1;
    }
}
