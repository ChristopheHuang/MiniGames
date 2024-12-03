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

    [Header("Screen Offset")]
    public Vector3 screenOffset = new Vector3(-5f, 0, 0); // 偏移量，x为水平偏移（负值为左）

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

        // 将目标位置偏移
        Vector3 adjustedTargetPosition = target.position + transform.right * screenOffset.x + transform.up * screenOffset.y + transform.forward * screenOffset.z;

        // 摄像机对准偏移后的目标位置
        transform.LookAt(adjustedTargetPosition);
    }
}