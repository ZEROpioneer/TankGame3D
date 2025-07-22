using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("生命值设置")]
    [Tooltip("是否强制使用自定义最大生命值（用于特殊单位）")]
    public bool useCustomMaxHealth;
    public float customMaxHealth;  // 自定义最大生命值（仅当useCustomMaxHealth为true时生效）
    
    // 实际使用的最大生命值（根据对象类型在初始化时获取）
    public float maxHealth { get; private set; }
    public float PlayermaxHealth  => PlayerSetManager.CurrentMaxHealth;
    public float EnemymaxHealth => CustomizeEnemySetManager.CurrentMaxHealth;
    public float currentHealth;
    
    [Header("效果设置")]
    public GameObject explosionEffect; // 爆炸特效预制体
    
    void Start()
    {
        // 初始化时根据对象类型获取对应最大生命值（仅执行一次）
        InitializeMaxHealth();
        currentHealth = maxHealth;  // 初始化当前血量
    }

    /// <summary>
    /// 仅在游戏开始时执行一次，根据对象类型获取最大生命值
    /// </summary>
    private void InitializeMaxHealth()
    {
        // 根据标签区分玩家和敌人，从对应设置脚本获取值
        if (CompareTag("Player"))
        {
            // 玩家生命值仅在游戏开始时从PlayerSetManager获取一次
            maxHealth = PlayermaxHealth;
        }
        else if (CompareTag("Enemy"))
        {
            // 敌人生命值仅在游戏开始时从EnemySetManager获取一次
            maxHealth = EnemymaxHealth;
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }
        
        if (CompareTag("Player"))
        {
            Debug.Log("玩家死亡！");
            // 调用玩家死亡逻辑
        }
        else if (CompareTag("Enemy"))
        {
            Debug.Log("敌人死亡！");
            // 调用敌人死亡逻辑
        }
        else
        {
            maxHealth = 3f; // 默认值
            Debug.LogWarning($"对象 {gameObject.name} 未设置Player或Enemy标签，使用默认生命值");
        }
        
        Destroy(gameObject);
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }
}
