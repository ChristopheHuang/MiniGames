using UnityEngine;
using UnityEngine.UI;

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
    
    private void Update()
    {
        countText.text = "Kill Count: " + counts;
    }
}
