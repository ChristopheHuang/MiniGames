using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Character : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] private float maxHealth = 100f; // 最大生命值
    private float currentHealth;
    [SerializeField] private float frictionFactor = 0.95f; // 速度衰减系数（0.95 表示每帧减少 5%）
    private Rigidbody rb;
    public float speed;
    
    [Header("Events")]
    public UnityEvent OnDeath;       // 死亡事件
    public UnityEvent OnTakeDamage;  // 受伤事件
    
    public GameObject deathEffectPrefab;

    protected bool isDead = false;

    protected virtual void Awake()
    {
        currentHealth = maxHealth; // 初始化生命值
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 5f;  // 增加线性阻力（摩擦力的模拟）
        rb.angularDamping = 2f; // 增加旋转阻力
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damageAmount">伤害值</param>
    public virtual void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnTakeDamage?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public virtual void ApplyForceWithBullet(Vector3 direction, float force)
    {
        if (isDead) return;
        if (rb)
        {
            rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// 获取当前生命值
    /// </summary>
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetCurrentHealthPercent()
    {
        return maxHealth > 0 ? currentHealth / maxHealth : 0;
    }
    
    /// <summary>
    ///  升级敌人
    ///  1. 增加生命值
    ///  2. 增加速度
    ///  </summary>
    
    public virtual void Upgrade(int level)
    {
        if (level == 0) return;
        speed *= Mathf.Pow(1.005f, level);
        maxHealth *= Mathf.Pow(1.1f, level);
        currentHealth = maxHealth;
        Debug.Log("Upgrade! Current monster Level: " + level);
    }

    /// <summary>
    /// 处理死亡逻辑
    /// </summary>
    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;
        GameManager.Instance.ScorePlus();
        OnDeath?.Invoke();
        if (deathEffectPrefab)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}