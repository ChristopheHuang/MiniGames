using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Frog");
    }
}
