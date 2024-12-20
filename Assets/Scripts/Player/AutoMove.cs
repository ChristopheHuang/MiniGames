using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [Header("Auto move Settings")]
    public float speed = 5.0f;
    public float moveDistance = 3.0f;

    private void Update()
    {
        // This gameobject will move right and left
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (transform.position.x >= moveDistance)
        {
            speed = -Mathf.Abs(speed);
        }
        else if (transform.position.x <= -moveDistance)
        {
            speed = Mathf.Abs(speed);
        }
    }
}
