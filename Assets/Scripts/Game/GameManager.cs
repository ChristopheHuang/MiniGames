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
    private Frog _player;
    public GameState gameState = GameState.Playing;
    public int counts = 0;
    public Text countText;
    public int expNeed = 10;
    public AudioListener _audioListener;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Frog>();
        _audioListener = Camera.main.gameObject.GetComponent<AudioListener>();
    }

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

        // Call player upgrade when the player reaches the required experience (expNeed)
        if (counts >= expNeed)
        {
            _player.UpdateLevel(1);
            expNeed += 10;
        }
    }
    
    public void GameStatePlaying()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameState = GameState.Playing;
    }
    
    public void GameStatePaused()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameState = GameState.Paused;
    }
    
    public void GameStateGameOver()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameState = GameState.GameOver;
    }
    
    private void Update()
    {
        ShowKillCount();
        
        switch (gameState)
        {
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
        }
    }
}
