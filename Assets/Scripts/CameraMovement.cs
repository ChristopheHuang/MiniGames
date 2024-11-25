using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new Vector3(0, 3, -5); 
    public float rotationSpeed = 600;  
    public float minPitch = -30f;  
    public float maxPitch = 60f;  
    public float distanceDamping = 0.1f;  

    private float currentYaw = 0f;  
    private float currentPitch = 0f;  
    private Vector3 currentOffset;

    private void Start()
    {
        currentOffset = offset;
    }

    private void LateUpdate()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float horizontalInput = mouseDelta.x * rotationSpeed * Time.deltaTime;
        float verticalInput = -mouseDelta.y * rotationSpeed * Time.deltaTime;

        currentYaw += horizontalInput;
        currentPitch = Mathf.Clamp(currentPitch + verticalInput, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        currentOffset = Vector3.Lerp(currentOffset, desiredPosition - target.position, distanceDamping);
        transform.position = target.position + currentOffset;

        transform.LookAt(target.position);
    }
}