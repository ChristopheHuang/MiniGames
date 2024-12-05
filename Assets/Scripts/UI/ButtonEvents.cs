using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
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
}
