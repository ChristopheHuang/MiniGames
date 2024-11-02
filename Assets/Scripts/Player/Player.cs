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

    public Transform shootPoint;
    public float shootInterval = 0.2f;
    private float currentInterval;
    public int shootCounts = 1;
    
    public int maxAmmo = 30;          
    private int currentAmmo;          
    public float reloadTime = 3.0f;   
    private bool isReloading = false; 
    private Vector3 targetPos;

    private void Start()
    {
        _playerController = new PlayerController();
        _playerController.PlayerMapping.Enable();
        currentInterval = shootInterval;

        targetPos = transform.forward;
        StartCoroutine(ShootRoutine()); 
    }

    float lastInterval;
    private void Update()
    {
        if (shootInterval != lastInterval)
        {
            currentInterval = shootInterval;
        }
        lastInterval = shootInterval;

        SearchEnemyInRange();
        
        transform.LookAt(targetPos);
    }

    public float searchRange = 10.0f;
    
    private void SearchEnemyInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRange);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                targetPos = collider.transform.position;
                break;
            }
        }        
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (isReloading)
            {
                yield return null;
                continue;
            }

            if (currentAmmo <= 0) 
            {
                StartCoroutine(Reload()); 
                yield return new WaitForSeconds(reloadTime);
            }
            else
            {
                yield return new WaitForSeconds(currentInterval); 
                for (int i = 0; i < shootCounts && currentAmmo > 0; i++)
                {
                    GameObject bullet = BulletPool.Instance.GetBullet(shootPoint.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().Initialize(transform.forward);
                    currentAmmo--; 
                }
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true; 
        yield return new WaitForSeconds(reloadTime); 
        currentAmmo = maxAmmo; 
        isReloading = false; 
    }
}