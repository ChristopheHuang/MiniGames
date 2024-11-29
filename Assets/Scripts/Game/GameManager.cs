using System;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }
    
    public GameState gameState = GameState.Playing;
    
    public int counts = 0;
    public Text countText;
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
    
    public void ScorePlus()
    {
        counts += 1;
    }
    
    private void ShowKillCount()
    {
        countText.text = "Kill Count: " + counts;
    }
    
    private void Update()
    {
        ShowKillCount();
        
        switch (gameState)
        {
            case GameState.Playing:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}
