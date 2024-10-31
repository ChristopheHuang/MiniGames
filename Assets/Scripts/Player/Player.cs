using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>() ?? new GameObject("Player").AddComponent<Player>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
    
    private PlayerController _playerController;

    public float moveSpeed = 5.0f;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float shootInterval = 0.2f;
    private float currentInterval;

    private void Start()
    {
        _playerController = new PlayerController();
        _playerController.PlayerMapping.Enable();
        currentInterval = shootInterval;

        StartCoroutine(ShootRoutine()); 
    }

    private Vector3 targetPos;
    float lastInterval;
    private void Update()
    {
        if (shootInterval != lastInterval)
        {
            currentInterval = shootInterval;
        }
        lastInterval = shootInterval;
        
        Vector2 moveInput = _playerController.PlayerMapping.Move.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * moveSpeed, Space.World);
        }
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 screenPos = new Vector3(touch.position.x, touch.position.y, Camera.main.WorldToScreenPoint(transform.position).z);
                targetPos = Camera.main.ScreenToWorldPoint(screenPos);
                targetPos.y = transform.position.y;
            }
        }
        if(targetPos != Vector3.zero)
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);                
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentInterval); 
            GameObject bullet = BulletPool.Instance.GetBullet(shootPoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Initialize(transform.forward);
        }
    }
}