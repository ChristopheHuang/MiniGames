using UnityEngine;
using UnityEngine.InputSystem;

public class Frog : MonoBehaviour
{
    private PlayerController _playerController;
    private Rigidbody _rigidbody;
    public float speed = 10.0f;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    
    private void Start()
    {
        _playerController = new PlayerController();
        _playerController.PlayerMapping.Enable();
        
        _rigidbody = GetComponent<Rigidbody>();
        
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void MoveAction()
    {
        Vector2 moveInput = _playerController.PlayerMapping.Move.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            // Move on the camera direction
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();
            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();
            
            Vector3 moveDirection = cameraForward * moveInput.y + cameraRight * moveInput.x;
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }
    void ShootAction()
    {
        if (_playerController.PlayerMapping.Shoot.WasPressedThisFrame())
        {
            AddRandomForce();
            FireShotGun();
        }
    }
    
    public int bulletCount = 5;
    public float spreadAngle = 10f; 
    void FireShotGun()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Quaternion randomRotation = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            );
            Vector3 shootDirection = randomRotation * transform.forward;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            bullet.GetComponent<FrogBullet>().Initialize(shootDirection);
        }
    }

    public void JumpAction()
    {
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0 ,_rigidbody.linearVelocity.z);
        _rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
    public float randomForceStrength = 5f;
    
    void AddRandomForce()
    {
        // Add force on the shoot point
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        _rigidbody.AddForceAtPosition(randomDirection * randomForceStrength, shootPoint.position, ForceMode.Impulse);
    }

    void FocusForward()
    {
        if (_playerController.PlayerMapping.Focus.IsPressed())
        {
            // Vector3 targetDirection = Camera.main.transform.forward;
            // targetDirection.y = 0;
            // transform.rotation = Quaternion.LookRotation(targetDirection);
            
            transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 10000);
            
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }


    void LockMouse()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    void InputActions()
    {  
        MoveAction();
        ShootAction();
        LockMouse();
        FocusForward();
    }
    
    private void Update()
    {
        InputActions();
    }
}
