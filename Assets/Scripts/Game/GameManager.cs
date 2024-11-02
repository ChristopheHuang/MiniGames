using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
    
    private void Update()
    {
        countText.text = "Kill Count: " + counts;
    }
}
