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

    private void Update()
    {
        Vector2 moveInput = _playerController.PlayerMapping.Move.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * moveSpeed, Space.World);
        }
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