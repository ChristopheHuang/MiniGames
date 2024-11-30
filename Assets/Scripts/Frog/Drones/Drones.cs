using System;
using UnityEngine;

public class Drones : MonoBehaviour
{
    private Frog _player;
    
    public GameObject bulletPrefab;
    private GameObject _target;
    
    // Distance to player
    public float distanceToPlayer = 3.0f;
    public float rotateSpeed = 5.0f;
    public float shootRate = 1.0f;
    
    private float _currentAngle = 0.0f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Frog>();

        if (_player)
        {
            Vector3 initialPosition = _player.transform.position + Vector3.right * distanceToPlayer;
            transform.position = initialPosition;
        }
    }

    private void RoundPlayer()
    {
        if (!_player) return;

        _currentAngle += rotateSpeed * Time.deltaTime;

        if (_currentAngle >= 360f) _currentAngle -= 360f;

        Vector3 offset = new Vector3(
            Mathf.Cos(_currentAngle * Mathf.Deg2Rad),
            0,
            Mathf.Sin(_currentAngle * Mathf.Deg2Rad)
        ) * distanceToPlayer;

        transform.position = _player.transform.position + offset;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _target = other.gameObject;
            transform.LookAt(_target.transform);
        }
    }

    public void Shoot()
    {
        // Search character in the scene and if has the tag "Enemy" then shoot it
        if (_target)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<FrogBullet>().Initialize(_target.transform.position - transform.position);
        }
    }

    private void Update()
    {
        RoundPlayer();
        
        if (shootRate > 0)
        {
            shootRate -= Time.deltaTime;
        }
        else
        {
            Shoot();
            shootRate = 1.0f;
        }
    }
}