using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.gameState = GameState.Playing;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Frog");
        Time.timeScale = 1;
        GameManager.Instance.gameState = GameState.Playing;
    }
}
