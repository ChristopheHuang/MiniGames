using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuffGate : MonoBehaviour
{
    public int spawnAmount = 1;
    private Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        text.text = " X " + (spawnAmount + 1).ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(SpawnBullets(other.gameObject));
        }
    }

    private IEnumerator SpawnBullets(GameObject originalBullet)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject instance = BulletPool.Instance.GetBullet(
                originalBullet.transform.position + new Vector3(0, 0, 0), 
                originalBullet.transform.rotation
            );
            instance.GetComponent<Bullet>().Initialize(originalBullet.transform.forward);

            yield return null;
        }
    }
}