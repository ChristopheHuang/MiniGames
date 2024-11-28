using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public void ResumeGame()
    {
        GameManager.Instance.gameState = GameState.Playing;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Frog");
        GameManager.Instance.gameState = GameState.Playing;
    }
}
