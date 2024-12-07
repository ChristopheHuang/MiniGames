using System;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public GameObject upgradePanel;
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.GameStatePlaying();
    }
    
    public void QuitGame()
    {
        GameManager.Instance.GameStateGameOver();
        Application.Quit();
    }
    
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Frog");
        Time.timeScale = 1;
        GameManager.Instance.GameStatePlaying();
    }

    private void Update()
    {
        if (upgradePanel.activeSelf)
        {
            Time.timeScale = 0;
            GameManager.Instance.GameStatePaused();
        }
    }
}
