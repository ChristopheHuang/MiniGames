using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;

    [Header("Camera Settings")]
    public float distance = 5.0f; 
    public float pitchSpeed = 150.0f; 
    public float yawSpeed = 150.0f; 
    public float minPitch = -89.0f; 
    public float maxPitch = 89.0f; 

    private float currentYaw = 0.0f; 
    private float currentPitch = 0.0f; 

    void Start()
    {
        
        if (target != null)
        {
            Vector3 direction = transform.position - target.position;
            distance = direction.magnitude;
            currentYaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            currentPitch = Mathf.Asin(direction.y / distance) * Mathf.Rad2Deg;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        
        float mouseX = Input.GetAxis("Mouse X"); 
        float mouseY = Input.GetAxis("Mouse Y"); 

        
        currentYaw += mouseX * yawSpeed * Time.deltaTime;
        currentPitch -= mouseY * pitchSpeed * Time.deltaTime;

        
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 offset = rotation * Vector3.back * distance;

        
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}