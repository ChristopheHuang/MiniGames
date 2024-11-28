using UnityEngine;
using UnityEngine.InputSystem;

public class Frog : MonoBehaviour
{
    // Default components
    private PlayerController _playerController;
    private Rigidbody _rigidbody;
    [Header("=========================================================================")]
    [Header("Player Settings")]
    public float speed = 10.0f;
    public Transform shootPoint;
    [Header("=========================================================================")]
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public GameObject shootEffect;
    public AudioClip soundEffect;
    public float shootRate = 0.5f;
    public int bulletCount = 5;
    public float spreadAngle = 10f;
    public float randomForceStrength = 5f;
    
    private Quaternion _targetRotation;
    private float _turnDuration = 0.2f;
    private float _turnTimer;
    private float _originShootRate;
    [Header("=========================================================================")]
    [Header("UI Settings")]
    public GameObject deathPanel;
    public GameObject pausePanel;
    
    private void Start()
    {
        _playerController = new PlayerController();
        _playerController.PlayerMapping.Enable();
        _rigidbody = GetComponent<Rigidbody>();
        _targetRotation = transform.rotation;
        _originShootRate = shootRate;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary> Player movement action
    ///  Player will move on the camera direction
    /// </summary> 
    private void MoveAction()
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
    
    /// <summary>
    ///  If player shoot will add a force to the player and make him rotate
    ///  Player will shoot some bullets will a angle spread and depends on the bullet count
    /// </summary>
    private void ShootAction()
    {
        if (shootRate > 0) return;
        
        if (_playerController.PlayerMapping.Shoot.WasPressedThisFrame())
        {
            ResetShootRate();
            SpawnFireEffect();
            PlaySoundIgnoringTimeScale();
            AddRandomForce();
            FireShotGun();
        }
    }
    
    private void ResetShootRate()
    {
        shootRate = _originShootRate;
    }

    private void SpawnFireEffect()
    {
        GameObject effect = Instantiate(shootEffect, shootPoint.position, Quaternion.identity);

        float scaleFactor = 0.3f; 
        effect.transform.localScale = Vector3.one * scaleFactor;
        Destroy(effect, 2.0f);
    }
    
    private void PlaySoundIgnoringTimeScale()
    {
        AudioSource audioSource = new GameObject("TempAudio").AddComponent<AudioSource>();
        audioSource.clip = soundEffect;
        audioSource.transform.position = shootPoint.position;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; 
        audioSource.volume = 0.5f;
        audioSource.Play();

        Destroy(audioSource.gameObject, soundEffect.length);
    }
    
    private void FireShotGun()
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
    
    void AddRandomForce()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        // Add force on the shoot point
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        _rigidbody.AddForceAtPosition(randomDirection * randomForceStrength, shootPoint.position, ForceMode.Impulse);
    }
    
    /// <summary>
    ///  Player can't jump by himself, but can be called by other scripts like leaf.cs
    /// </summary>
    public void JumpAction()
    {
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0 ,_rigidbody.linearVelocity.z);
        _rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
    
    /// <summary>
    ///  Player will focus forward and slow down the time with mouse right click
    ///  Player will look at the camera direction with quaternion slerp
    /// </summary>
    void FocusForward()
    {
        if (_playerController.PlayerMapping.Focus.IsPressed())
        {
            Vector3 targetDirection = Camera.main.transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                0.1f 
            );
            
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        
        if (_playerController.PlayerMapping.Focus.WasReleasedThisFrame())
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }

    /// <summary>
    ///  Player can lock and unlock the mouse with escape key
    /// </summary>
    void PauseAction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.gameState = GameManager.Instance.gameState = GameState.Paused;
            pausePanel.SetActive(true);
        }
    }
    
    /// <summary>
    ///  Player will apply a horizontal friction to the player
    ///  but only if the player is moving
    /// </summary>
    void ApplyHorizontalFriction()
    {
        Vector3 horizontalVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
        float frictionStrength = 5f; 
        Vector3 frictionForce = -horizontalVelocity.normalized * horizontalVelocity.magnitude * frictionStrength;

        if (horizontalVelocity.magnitude > 0.01f)
        {
            _rigidbody.AddForce(frictionForce, ForceMode.Acceleration);
        }
    }
    
    /// <summary>
    ///  Player will die and show the death panel
    /// </summary>
    public void Die()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _playerController.PlayerMapping.Disable();
        deathPanel.SetActive(true);
        Destroy(gameObject);
    }
    
    /// <summary>
    ///  Player input actions
    /// </summary>
    private void InputActions()
    {  
        MoveAction();
        ShootAction();
        FocusForward();
        PauseAction();
    }
    
    private void FixedUpdate()
    {
        ApplyHorizontalFriction();
        
        shootRate -= Time.deltaTime;
    }
    
    private void Update()
    {
        InputActions();
    }
}
